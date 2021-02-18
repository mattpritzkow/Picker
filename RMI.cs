using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

namespace Picker
{
    public class RMI 
    {
        //FILEDESCRIPTION Variables
        public RMIFileDescription fileDescription { get; set; }
        //AMPDATA Variables
        public IList<RMIAmpData> ampData { get; set; }
        //PICKDATA
        public IList<RMIPickData> pickData { get; set; }
        //public SeriesCollection Seri { get; set; }    //SeriesCollection Series
        public IList<string> fileNames { get; set; }
        public RMIGeoData geoData { get; set; }

        //default constructor
        public RMI()
        {
            fileDescription = new RMIFileDescription();
            ampData = new List<RMIAmpData>();
            pickData = new List<RMIPickData>();
            geoData = new RMIGeoData();
            fileNames = new List<string>();
        }

        //param constructor for when declared in main
        public RMI(IList<SeisData> src, GeometryData geoSrc)
        {
            fileNames = new List<string>();
            ampData = new List<RMIAmpData>();
            pickData = new List<RMIPickData>();

            fileDescription = new RMIFileDescription(src);
            geoData = new RMIGeoData(geoSrc);
            foreach(var file in src)
            {
                ampData.Add(new RMIAmpData(file));
                pickData.Add(new RMIPickData(file));
            }
        }

        //reading function for rmi files
        public void readData(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string line = reader.ReadLine();
            while (line != null)
            {
                if(line.Equals("FILEDESCRIPTION"))          //categorizes file by file headers
                {
                    line = fileDescription.readFileDescription(reader);
                }
                else if(line.Equals("GEODATA"))
                {
                    line = geoData.readGeoData(reader, fileDescription);
                }
                else if(line.Equals("FILENAMES"))
                {
                    line = readFileNames(reader);            //should read and store all filenames in theory
                }
                else if(line.Equals("PICKERDATA"))
                {
                    RMIAmpData tempAmp = new RMIAmpData();
                    line = tempAmp.readAmpData(reader, fileDescription);
                    
                    if(tempAmp.graphData[0].Count != (fileDescription.numTraces))
                    {
                        //throw error
                        return;
                    }

                    ampData.Add(tempAmp);

                    RMIPickData tempPick = new RMIPickData();
                    line = tempPick.readPickData(reader);
                    if (tempPick.picks.Count != (fileDescription.numChans))
                    {
                        //throw error
                        return;
                    }
                    pickData.Add(tempPick);
                }
                else
                {
                    line = reader.ReadLine();
                }
            }
            reader.Close();
        }

        //writing function for rmi files for exporting data
        public void writeData(string basePath)
        {
            try
            {
                string rmi = null;
                if (!basePath.EndsWith(".rmi"))
                {
                    rmi = basePath + ".rmi";
                }
                else
                {
                    rmi = basePath;
                }

                StreamWriter writer = new StreamWriter(rmi);

                writer.WriteLine("FILEDESCRIPTION");

                writer.WriteLine("NumOfFiles: " + fileDescription.numFiles);
                writer.WriteLine("NumOfChannels: " + fileDescription.numChans);
                writer.WriteLine("NumOfDataPerTrace: " + fileDescription.numTraces);

                writer.WriteLine("GEODATA");

                writer.WriteLine("NumOfChannelsGEODATA: " + geoData.numChannelsGeoData);
                writer.WriteLine("GeoSpacing: " + geoData.geoSpacing);
                writer.WriteLine("GeoOffset: " + geoData.geoOffset);
                writer.WriteLine("GeoDistance: " + geoData.geoDistance);

                writer.Write("ShotElev: ");
                writer.Write(geoData.shotElevation[0]);
                for (int i = 1; i < fileDescription.numFiles; i++)
                {
                    writer.Write(", " + geoData.shotElevation[i]);
                }
                writer.Write(writer.NewLine);

                writer.Write("ShotPos: ");
                writer.Write(geoData.shotPosition[0]);
                for (int i = 1; i < fileDescription.numFiles; i++)
                {
                    writer.Write(", " + geoData.shotPosition[i]);
                }
                writer.Write(writer.NewLine);

                writer.Write("GeoElevation: ");
                writer.Write(geoData.geoElevation[0]);
                for (int i = 1; i < fileDescription.numChans; i++)
                {
                    writer.Write(", " + geoData.geoElevation[i]);
                }
                writer.Write(writer.NewLine);

                writer.WriteLine("FILENAMES");

                writeFileNames(writer);

                for (int i = 0; i < fileDescription.numFiles; i++)
                {
                    writer.WriteLine("PICKERDATA");
                    writer.Write("amps: ");
                    foreach (var channel in ampData[i].graphData)
                    {
                        foreach (var dataPoint in channel)
                        {
                            writer.Write(dataPoint + " ");
                        }
                    }
                    writer.Write(writer.NewLine);
                    for (int j = 0; j < pickData[i].picks.Count; j++)
                    {
                        writer.WriteLine("pick: " + pickData[i].picks[j].toString());
                    }
                }
                writer.Close();
            }
            catch (ArgumentOutOfRangeException ne) { Debug.WriteLine(ne.StackTrace); }
        }

        // function that will read and return filenames
        public string readFileNames(StreamReader reader)
        {
            string line = reader.ReadLine();
            string[] strArr = line.Split(':');
            while (strArr[0].Equals("fileName"))
            {
                fileNames.Add(strArr[1].Trim());
                line = reader.ReadLine();
                strArr = line.Split(':');
            }
            return line;
        }

        //function that will write filenames
        public void writeFileNames(StreamWriter writer)
        {
            foreach(string name in fileNames)
            {
                writer.WriteLine("fileName: " + name.ToString());
            }
        }
    }
}

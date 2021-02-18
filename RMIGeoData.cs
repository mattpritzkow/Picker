using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Picker
{
    public class RMIGeoData
    {
        public IList<float> shotElevation { get; set; }     //stores shotElevations
        public IList<float> shotPosition { get; set; }      //stores shotPositions
        public IList<float> geoElevation { get; set; }      //stores geoElevations
        public int numChannelsGeoData { get; set; }     //numOfChannels equivalents
        public float geoSpacing { get; set; }       //stores geoSpacing variable
        public float geoOffset { get; set; }        //stores geoOffset variable
        public float geoDistance { get; set; }      //stores geoDistance variable

        public RMIGeoData()
        {
            shotElevation = new List<float>();
            shotPosition = new List<float>();
            geoElevation = new List<float>();
            numChannelsGeoData = 0;
            geoSpacing = 0;
            geoOffset = 0;
            geoDistance = 0;
        }

        //this parameterized constructor takes all data from geoData and transfers it between the two or vice versa
        public RMIGeoData(GeometryData src)
        {
            shotElevation = src.shotElevation;
            shotPosition = src.shotPosition;
            IList<float> temp = new List<float>();
            for(int i = 0; i < src.numChansGeo; i++)
            {
                temp.Add((float)src.GeophoneDatas[i].Elevation);
            }
            geoElevation = temp;
            numChannelsGeoData = src.numChansGeo;
            geoSpacing = src.geophoneSpacing;
            geoOffset = src.geophoneOffset;
            geoDistance = src.dtVal;
        }

        public string readGeoData(StreamReader reader, RMIFileDescription fileDesc)     //needs fileDesc to gain access to the numOfChannels
        {
            string line = reader.ReadLine();
            while(line != null)
            {
                line = line.Replace(" ", "");           //takes out unneccesary spaces
                string[] strArr = line.Split(':');
                if(strArr[0].Equals("NumOfChannelsGEODATA"))
                {
                    this.numChannelsGeoData = Int32.Parse(strArr[1]);
                }
                else if(strArr[0].Equals("GeoSpacing"))
                {
                    this.geoSpacing = float.Parse(strArr[1]);
                }
                else if(strArr[0].Equals("GeoOffset"))
                {
                    this.geoOffset = float.Parse(strArr[1]);
                }
                else if(strArr[0].Equals("GeoDistance"))
                {
                    this.geoDistance = float.Parse(strArr[1]);
                }
                else if(strArr[0].Equals("ShotElev"))
                {
                    readShotElevation(strArr[1]);
                }
                else if(strArr[0].Equals("ShotPos"))
                {
                    readShotPosition(strArr[1]);
                }
                else if(strArr[0].Equals("GeoElevation"))
                {
                    readGeoElevation(strArr[1]);
                }
                else
                {
                    return line;
                }

                line = reader.ReadLine();
            }
            return line;
        }

        public void readShotElevation(String line)
        {
            line = line.Trim();         //makes sure that there's no spaces, etc
            string[] shotElevStr = line.Split(',');     //shotElevations are separated by commas
            foreach (String elevation in shotElevStr)
            { 
                shotElevation.Add(float.Parse(elevation));
            }
        }

        public void readShotPosition(String line)
        {
            line = line.Trim();
            string[] shotElevStr = line.Split(',');
            foreach (String xPosition in shotElevStr)
            {
                shotPosition.Add(float.Parse(xPosition));
            }
        }

        public void readGeoElevation(String line)
        {
            line = line.Trim();
            string[] shotElevStr = line.Split(',');
            foreach (String elevation in shotElevStr)
            {
                geoElevation.Add(float.Parse(elevation));
            }
        }

    }
}

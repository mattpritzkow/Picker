using System;
using System.Collections.Generic;
using LiveCharts.Geared;
using System.IO;
using System.Diagnostics;

namespace Picker
{
    public class RMIAmpData
    {
        public IList<GearedValues<float>> graphData { get; set; }   //graphStorage
        public RMIAmpData()
        {
            graphData = new List<GearedValues<float>>();
        }

        public RMIAmpData(SeisData src)
        {
            graphData = src.graphStorage;
        }

        public String readAmpData(StreamReader reader, RMIFileDescription fileDesc)
        {
            string line = reader.ReadLine();
            string[] strArr = line.Split(':');      //splits at colon
            if (strArr[0].Equals("amps"))       //amps: is in front of all amp data (all amp data is displayed in one line)
            {
                strArr[1] = strArr[1].Trim();
                string[] ampArrString = strArr[1].Split(' ');
                for (int i = 0; i < fileDesc.numChans; i++)     //usually < 12
                {
                    IList<float> temp = new List<float>();      //instead of having to index between files, we create a new temp IList per channel
                    int index = i * fileDesc.numTraces;         //index will be at however many data points there are (ie 0*4000 -> 1*4000 -> 2*4000)
                    int nextIndex = (i + 1) * fileDesc.numTraces;   //this will be the next index (ie 1*4000 -> 2*4000)
                    for (int j = index; j < nextIndex; j++)     //for loop that will read the amp data between channels (ie read data points 0 to 4000, then data points 4000 to 8000)
                    {
                        //Debug.WriteLine(ampArrString[j]);
                        temp.Add(float.Parse(ampArrString[j]));     //this will add the array from the specified range
                    }
                    graphData.Add(new GearedValues<float>(temp));       //adds temp
                }
            }
            return line;
        }
    }
}

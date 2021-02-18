using System;
using System.IO;
using System.Collections.Generic;

namespace Picker
{
    public class RMIFileDescription
    {
        public int numFiles { get; set; }


        public int numChans { get; set; }
        public int numTraces { get; set; }
        public RMIFileDescription()
        {
            numFiles = 0;
            numChans = 0;
            numTraces = 0;
        }

        public RMIFileDescription(IList<SeisData> src)
        {
            numChans = src[0].numChannels;
            numFiles = src.Count;
            numTraces = src[0].graphStorage[0].Count;
        }

        public String readFileDescription(StreamReader reader)
        {
            string line = reader.ReadLine();
            while (line != null)
            {
                line = line.Replace(" ", "");
                string[] strArr = line.Split(':');
                if (strArr[0].Equals("NumOfFiles"))
                {
                    this.numFiles = Int32.Parse(strArr[1]);
                }
                else if (strArr[0].Equals("NumOfChannels"))
                {
                    this.numChans = Int32.Parse(strArr[1]);
                }
                else if (strArr[0].Equals("NumOfDataPerTrace"))
                {
                    this.numTraces = Int32.Parse(strArr[1]);
                }
                else
                {
                    return line;
                }

                line = reader.ReadLine();
            }
            return line;
        }
    }
}

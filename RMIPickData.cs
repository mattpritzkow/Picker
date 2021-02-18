using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Picker
{
    public class RMIPickData
    {
        public IList<Pick> picks { get; set; }   //pickData

        public RMIPickData()
        {
            picks = new List<Pick>();
        }

        public RMIPickData(SeisData src)
        {
            picks = new List<Pick>();
            picks = src.pickData;
        }

        public String readPickData(StreamReader reader)
        { 
            string line = reader.ReadLine();
            while(line != null)
            {
                string temp = line.Replace(" ", "");
                string[] strArr = temp.Split(':');
                if (strArr[0].Equals("pick"))
                {
                    string[] pick = strArr[1].Split(',');       
                    picks.Add(new Pick(double.Parse(pick[2]), double.Parse(pick[1]), pick[0]));
                    Debug.WriteLine(picks[picks.Count- 1].toString());
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

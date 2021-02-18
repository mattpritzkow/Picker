using System;
using System.Collections.Generic;
using System.Diagnostics;
using LiveCharts.Geared;
//using Trace = SEGYRead.Trace;
//using SeismicFileReader;
using SeismicFileRead;

namespace Picker
{
    public class SEGYNew : SeisData
    {
        //will take the filename as an argument and then stream data into ISegyFile line
        //will tell the number of channels and bytes in a segy file
        //will return true if this completes and will return false if no filename is stored after the first function GetFile()

        public SEGYNew() : base() { }

        protected override Boolean readData(string filePath)
        {
            Debug.WriteLine("Starting readData function!");
            if (filePath != null)
            {
                SEGYPickerTest newSEGY = new SEGYPickerTest();
                //SEG2PickerTest seisRead;
                //seisRead = newSEG2;
                newSEGY.Read(filePath);

                //IList<Trace> trace = new List<Trace>();
                IList<SEGYTrace> trace = new List<SEGYTrace>();
                //function that will read in all SEG2 data and store it in multiple ILists
                
                //should return the number of traces (was a private variable so had to create a getNumOfTraces() function
                this.numChannels = newSEGY.getNumOfTraces();
                Debug.WriteLine("Number of Traces: " + numChannels);
                //int counter = 1;

                //Debug.WriteLine("Num of Samps per Trace: " + newSEGY.getNumSampsPerTrace());

                //cast every trace into geared amplitudes
                //foreach (var traces in trace)
                for (int m = 0; m < this.numChannels; m++)
                {
                    //Iterates the data backwards, this displays the data top -> bottom
                    IList<float> temp = new List<float>();
                    for (int j = ((int)newSEGY.getNumSampsPerTrace() - 1); j >= 0; j--)
                    {
                        temp.Add(newSEGY.Traces[m].Amplitudes[j]);
                        //temp.Add(trace[m].Amplitudes[j]);
                    }
                    GearedValues<float> gearedTemp = new GearedValues<float>(temp);
                    this.graphStorage.Add(gearedTemp);
                    Debug.WriteLine("!!!!!! " + temp.Count);
                }
                return true;
            }
            else
                return false;
        }//end of readData()

    }//end of SEG2()
}

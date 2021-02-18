using System;
using System.Collections.Generic;
using System.Diagnostics;
using LiveCharts.Geared;
using SeismicFileRead;

namespace Picker
{
    public class SEG2New : SeisData
    {
        //will take the filename as an argument and then stream data into ISegyFile line
        //will tell the number of channels and bytes in a segy file
        //will return true if this completes and will return false if no filename is stored after the first function GetFile()

        protected override Boolean readData(string filePath)
        {
            Debug.WriteLine("Starting readData function!");
            if (filePath != null)
            {
                SEG2PickerTest newSEG2 = new SEG2PickerTest();
                
                //ISeismicReader seisRead;
                //seisRead = newSEGY;

                newSEG2.Read(filePath);

                //IList<SEG2Trace> trace = new List<SEG2Trace>();
                //function that will read in all SEG2 data and store it in multiple ILists

                //should return the number of traces (was a private variable so had to create a getNumOfTraces() function
                this.numChannels = newSEG2.getNumOfTraces();
                Debug.WriteLine("Number of Channels: " + numChannels);
                //int counter = 1;
                Debug.WriteLine("Number of Samples per Trace: " + newSEG2.getNumSampsPerTrace());
                Debug.WriteLine("Number of Samples per Trace: " + newSEG2.Traces[0].numSampsPerTrace);
                //cast every trace into geared amplitudes
                //foreach (var traces in trace)
                for (int m = 0; m < this.numChannels; m++)
                {
                    //Iterates the data backwards, this displays the data top -> bottom
                    IList<float> temp = new List<float>();
                    for (int j = ((int)newSEG2.getNumSampsPerTrace() - 1); j >= 0; j--)
                    //for (int j = ((int)newSEG2.Traces[m].numSampsPerTrace - 1); j >= 0; j--)
                    {
                        temp.Add(newSEG2.Traces[m].Amplitudes[j]);
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

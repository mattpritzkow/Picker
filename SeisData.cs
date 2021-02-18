using System;
using System.Collections.Generic;
using LiveCharts;
using LiveCharts.Geared;
using SeismicFileRead;

namespace Picker
{
    //SeisData will be to read in any data from SU files to SEGY files
    public abstract class SeisData
    {
        public IList<Pick> pickData { get; set; }   //stores the picking data of the user
        public int numChannels { get; set; }        //number of traces
        public IList<GearedValues<float>> graphStorage { get; set; }   //stores the data of each graph individually
        public IList<Boolean> Zeroed { get; set; }
        //public SeriesCollection Series { get; set; }    //stores the data of GVerticalLineSeries

        //constructor for SeisData (might make it parameterized constructor to cover amplitude and numchannels)
        public SeisData()
        {
            //Series = new SeriesCollection();
            graphStorage = new List<GearedValues<float>>();
            pickData = new List<Pick>();
            Zeroed = new List<Boolean>();
        }//end of SeisData()


        public SeisData(RMIAmpData amp, RMIPickData picks, RMIFileDescription desc)
        {
            graphStorage = new List<GearedValues<float>>();
            pickData = new List<Pick>();
            Zeroed = new List<Boolean>();

            this.numChannels = desc.numChans;
            this.pickData = picks.picks;
            this.graphStorage = amp.graphData;
            for (int i = 0; i < numChannels; i++)
            {
                this.Zeroed.Add(false);
            }


        }

        //Takes graphData through a filePath and proceeds to break down the SEGY file while also creating a new graph
        public Boolean readSeisData(string filePath)
        {
            this.readData(filePath);
            //Allocates pick data array properly
            for (int i = 0; i < numChannels; i++)
            {
                this.pickData.Add(new Pick());
                this.Zeroed.Add(false);
            }
            return true;
        }//end of readSeisData()


        protected abstract Boolean readData(string filePath);


    }//end of SeisData()
}//end of namespace Picker
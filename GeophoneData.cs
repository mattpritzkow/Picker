using System.Diagnostics;

namespace Picker
{
    public class GeophoneElevation
    {
        public string Header { get; set; }      //keeps track of geophone index (ie title that says "1/12" above textbox) --> assigned to textblock
        private double elevationHolder;     //backing field for Elevation
        //had to overwrite get and set methods in order to see the change in geoElevation each time
        public double Elevation
        {
            get
            {
                return elevationHolder;
            }
            set
            {
                elevationHolder = value;
                Debug.WriteLine("New Elevation: " + Elevation);
            }
        }

        //constructor that is called in GeometryData, it takes in the elevation, index, and totalChannels as arguments
        //will use index and totalChannels for the header (title), and store the elevation value
        public GeophoneElevation(double elevation, int index, int totalChannels)
        {
            this.Elevation = elevation;
            Header = (index + 1) + " / " + totalChannels;
        }
    }
}
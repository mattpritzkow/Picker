using LiveCharts;

namespace Picker
{
    public class Pick
    {

        public double Y { get; set; }
        public double X { get; set; }
        public string Title { get; set; }

        public Pick(ChartPoint point)
        {
            this.Y = point.Y + 1;   //added 1 because picks were always made y - 1, which made them innacurate
            this.X = point.X;
            this.Title = point.SeriesView.Title;
        }

        public Pick(double Y, double X, string Title)
        {
            this.Y = Y;
            this.X = X;
            this.Title = Title;
        }

        public Pick()
        {
            this.Y = -1;
            this.X = 0;
            this.Title = "NoPickMade";
        }

        public string toString()
        {
            return Title + ", " + X + ", " + Y;
        }

    }


}

using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;

namespace Picker
{
    /// <summary>
    /// Interaction logic for Colors.xaml
    /// </summary>                
    public partial class Colors : Window
    {
        public IList<ColorCombo> colors { get; set; }   //main IList that stores all the different colors in the comboBox
        public object newColorPositive { get; set; }   //single object that will set the color of positive values on graph
        public object newColorNegative { get; set; }    //single object that will set the color of negative values on graph
        public object newColorPick { get; set; }        //single object that will set the color of pick values on graph
        public int counter { get; set; }
        
        public Colors()
        {
            colors = new List<ColorCombo>();
            newColorPositive = System.Windows.Media.Brushes.Blue;       //default the three colors as such
            newColorNegative = System.Windows.Media.Brushes.Red;
            newColorPick = System.Windows.Media.Brushes.White;
            counter = 0;
            InitializeComponent();
            bindColors();
        }

        //called at the beginning of the click sequence, sets everything up
        public void initialize()        //called in main
        {
            storeColors();
            defaultColors();
            bindColors();
        }

        //declares all the different colors in the comboBoxes, and then adds them to the IList colors.  Uses ColorCombo <type> to store the hex value and name correctly
        public void storeColors()
        {
            object Red = System.Windows.Media.Brushes.Red;
            object Blue = System.Windows.Media.Brushes.Blue;
            object Green = System.Windows.Media.Brushes.ForestGreen;
            object Black = System.Windows.Media.Brushes.Black;
            object White = System.Windows.Media.Brushes.White;
            object Gold = System.Windows.Media.Brushes.Gold;
            object Pink = System.Windows.Media.Brushes.HotPink;
            object Purple = System.Windows.Media.Brushes.Purple;
            object Orange = System.Windows.Media.Brushes.Orange;
            object Navy = System.Windows.Media.Brushes.Navy;
            object Transparent = System.Windows.Media.Brushes.Transparent;
            object Aqua = System.Windows.Media.Brushes.Aqua;
            object DarkViolet = System.Windows.Media.Brushes.DarkViolet;
            object SkyBlue = System.Windows.Media.Brushes.SkyBlue;
            object DarkBlue = System.Windows.Media.Brushes.DarkBlue;

            colors.Add(new ColorCombo { brush = Red, colorName = "Red"});
            colors.Add(new ColorCombo { brush = Blue, colorName = "Blue" });
            colors.Add(new ColorCombo { brush = Green, colorName = "Green" });
            colors.Add(new ColorCombo { brush = Black, colorName = "Black" });
            colors.Add(new ColorCombo { brush = White, colorName = "White" });
            colors.Add(new ColorCombo { brush = Gold, colorName = "Gold" });
            colors.Add(new ColorCombo { brush = Pink, colorName = "Pink" });
            colors.Add(new ColorCombo { brush = Purple, colorName = "Purple" });
            colors.Add(new ColorCombo { brush = Orange, colorName = "Orange" });
            colors.Add(new ColorCombo { brush = Navy, colorName = "Navy" });
            colors.Add(new ColorCombo { brush = Transparent, colorName = "Transparent" });
            colors.Add(new ColorCombo { brush = Aqua, colorName = "Aqua" });
            colors.Add(new ColorCombo { brush = DarkViolet, colorName = "Dark Violet" });
            colors.Add(new ColorCombo { brush = SkyBlue, colorName = "Sky Blue" });
            colors.Add(new ColorCombo { brush = DarkBlue, colorName = "Dark Blue" });
        }

        public void defaultColors()
        {
                if(counter == 0)
                {
                    positiveComboBox.SelectedIndex = 1;
                    negativeComboBox.SelectedIndex = 0;
                    pickComboBox.SelectedIndex = 4;
                    counter++;
                }
                else
                {
                    bindColors();
                }
                //updateColors();
        }

        //makes sure that each color is stored correctly within colors
        public void bindColors()
        {
            System.Windows.Forms.BindingSource bindingSource = new System.Windows.Forms.BindingSource();

            positiveComboBox.ItemsSource = colors;
            positiveComboBox.DisplayMemberPath = "colorName";
            positiveComboBox.SelectedValuePath = "brush";

            negativeComboBox.ItemsSource = colors;
            negativeComboBox.DisplayMemberPath = "colorName";
            negativeComboBox.SelectedValuePath = "brush";

            pickComboBox.ItemsSource = colors;
            pickComboBox.DisplayMemberPath = "colorName";
            pickComboBox.SelectedValuePath = "brush";
        }

        //takes whatever is selected in the comboBox and changes the color
        public void updateColors()
        {
            object selectValPositive = positiveComboBox.SelectedValue;      //setting up temp objs to store the new color selection
            object selectValNegative = negativeComboBox.SelectedValue;
            object selectedValPick = pickComboBox.SelectedValue;
            this.newColorPositive = null;   //made sure that these variables are null before reassigning them new values
            this.newColorNegative = null;
            this.newColorPick = null;

            this.newColorPositive = selectValPositive;      //finally set whatever is in the comboBox equal to the actual public variable
            this.newColorNegative = selectValNegative;
            this.newColorPick = selectedValPick;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //saves all the color changes
        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            updateColors();

            Debug.WriteLine("New Color Pos: " + newColorPositive);
            Debug.WriteLine("New Color Neg: " + newColorNegative);
            Debug.WriteLine("New Color Pick: " + newColorPick);
        }

        private void SaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            updateColors();
            this.Close();
        }

        //not sure if I need this, might delete
        private void SelectChange_Click(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            updateColors();
        }

        public void close(System.ComponentModel.CancelEventArgs e)
        {
            updateColors();
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        //same as GeometryData, this keeps the window from being permanently closed after one use
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            close(e);
        }//end of Window_Closing
    }
}

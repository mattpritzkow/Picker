using System.Collections.Generic;
using System.Linq;

namespace Picker
{
    //class that is made to store colors: it fills in the <type> of IList
    public class ColorCombo
    {
        public object brush { get; set; }   //stores hex value of color
        public string colorName { get; set; }   //stores string of the colorname (manually entered)
        public ColorCombo()
        {
            brush = null;
            colorName = null;
        }
        public int Count(IList<ColorCombo> list, int index)     //my attempt to implement a counting function (only way I could test indexing) -> might delete
        {
            int temp = 0;
            for(index = 0; index < list.Count(); index++)
            {
                temp = index;
            }
            return temp;
        }
    }
}

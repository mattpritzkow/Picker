using System;
using Microsoft.Win32;

/*
Helper class: This class will contain various functions that will contribute to the main
*/
namespace Picker
{
    public static class Helper
    {
        //GetFile() is responsible for opening an interface for the user to select one to unlimited files
        public static string[] GetFile(int testNum)     //take in testNum, which will tell whether to display sg2 and sgy or to display rmi
        {
            //string of arrays f is what will be returned
            string[] f = null;  
            //this is what opens up an interface for the user to selected a file
            OpenFileDialog file = new OpenFileDialog();
            //allows a user to pick multiple files at the same time
            file.Multiselect = true;
            //limits the file selections to just SEGY, SEG2, SU
            if(testNum == 1)
            {
                file.Filter = "Seismic Records (*.sgy;*.sg2;)|*.sgy;*.sg2";
            }
            else
            {
                file.Filter = "Saved Seismic Records (*.rmi;)|*.rmi";
            }
            file.Title = "Choose File(s)";
            //f is what stores the filename that will eventually be read
            if (file.ShowDialog() == true)
            {
                f = file.FileNames;
            }
            return f;
        }//end of GetFile()

        //SaveFile() is responsible for opening a file interface and having the user select where they want to save 
        public static string SaveFile(string filter = "")
        {
            Microsoft.Win32.SaveFileDialog saveStuff = new Microsoft.Win32.SaveFileDialog();

            saveStuff.Filter = filter;

            //default FileName that appears when opening
            saveStuff.FileName = "Document";
            //bool statement to officially save the users data
            Nullable<bool> result = saveStuff.ShowDialog();
            //tells the CPU where to save the file
            string filepath = null;
            if (result == true)
            {
                filepath = saveStuff.FileName;
            }
            return filepath;

        }//end of SaveFile()

    }//end of Helper()

}//end of Picker
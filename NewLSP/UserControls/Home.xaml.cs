using Microsoft.WindowsAPICodePack.Dialogs;
using NewLSP.StaticHelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NewLSP.UserControls;
using System.IO;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnOpenSubjectFolder_Click(object sender, RoutedEventArgs e)
        {
            // Get or create the Name of the Subject folder
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            string FolderPath = "";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPath = dialog.FileName + '\\';
            }

            // Get the number of '\\'s in FolderPath
            var NumberOfSlashes = StringHelper.ReturnNumberOfDeliniters(FolderPath, '\\');


            // Get the Subjects Name from the item a position NumberOfSlashes -1
            var FolderName = StringHelper.ReturnItemAtPos(FolderPath, '\\', NumberOfSlashes - 1);
            lblTitle.Content = "This is the Subjects Tree for " + FolderName;

            // Save the Path to  the selected subject
            SubjectStaticMembers.SaveSubjectFolderPath = FolderPath;

            //Create a folder to hold the QAFiles
            if (!Directory.Exists(FolderPath + "QAFiles"))
            {
                Directory.CreateDirectory(FolderPath + "QAFiles");
            }

            //Create a folder to hold the QAResults
            if (!Directory.Exists(FolderPath + "QAResults"))
            {
                Directory.CreateDirectory(FolderPath + "QAResults");
            }

            //Create a folder to hold the hyperlinks files
            if (!Directory.Exists(FolderPath + "Hyperlinks"))
            {
                Directory.CreateDirectory(FolderPath + "Hyperlinks");
            }

            // Create a folder to hold the subject notes
            if (!Directory.Exists(FolderPath + "Notes"))
            {
                Directory.CreateDirectory(FolderPath + "Notes");
            }


            //Create a text file to hold the Itemcount
            if (!File.Exists(FolderPath+ "ItemCount.txt"))
            {
                //File.Create(FolderPath + "ItemCount.txt");
                File.WriteAllText(FolderPath + "ItemCount.txt", "0");
            }

            btnOpenSubjectFolder.IsEnabled = false;

            //Communicate the FolderPath to the ViewModel.SubjectNodeViewModel's OpenFile method
            SubjectStaticMembers.OpenFiles(FolderPath);

        }// End btnOpenSubjectFolder_Click


        /// <summary>
        /// The purpose of this method is for the user to 
        /// either open a previously designated instructions
        /// folder or create it and then copy all of
        /// the instructions text files into it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetInstructionsFolder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Either select a previously chosen Instructions folder, " +
                "or create it and then manually add all of the text files in the accompanying InstructionsTextFiles to it. ");
            // Get the Name of the Instructions folder
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            string InstructionsFolderPath = "";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                InstructionsFolderPath = dialog.FileName + '\\';
            }

            InstructionsStaticMembers.InstructionsFolderPath = InstructionsFolderPath;
        }

        
    }// End partial class Home 


}// End Namesapce

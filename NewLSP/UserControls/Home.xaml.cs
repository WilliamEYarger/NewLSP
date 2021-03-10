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
            // Get the Name of the Subject folder
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

            //Communicate the FolderPath to the ViewModel.SubjectNodeViewModel's OpenFile method
            SubjectStaticMembers.OpenFiles(FolderPath);



           
            

        }// End btnOpenSubjectFolder_Click



    }// End partial class Home 


}// End Namesapce

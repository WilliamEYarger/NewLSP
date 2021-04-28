using Microsoft.WindowsAPICodePack.Dialogs;
using NewLSP.StaticHelperClasses;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32.SafeHandles;

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


        /// <summary>
        /// This is the first method called when the user begins the application
        /// It forces the user to either create a Subjects folder and a References folder
        /// of to select such folders that have been previously created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenSubjectFolder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Create or Open an existing Subject folder");
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
            CommonStaticMembers.SubjectFolderPath = FolderPath;

            //Create a folder to hold the QAFiles and path
            if (!Directory.Exists(FolderPath + "QAFiles"))
            {
                Directory.CreateDirectory(FolderPath + "QAFiles");
                CommonStaticMembers.DataNodesQAFilePath = FolderPath + "QAFiles";
            }

            //Create a folder to hold the QAResults and path
            if (!Directory.Exists(FolderPath + "QAResults"))
            {
                Directory.CreateDirectory(FolderPath + "QAResults");
                CommonStaticMembers.DataNodesQAResultsFilePath = FolderPath + "QAResults";
            }

            //Create a folder to hold the hyperlinks files and path
            if (!Directory.Exists(FolderPath + "Hyperlinks"))
            {
                Directory.CreateDirectory(FolderPath + "Hyperlinks");
                CommonStaticMembers.DataNodesHyperlinksPath = FolderPath + "Hyperlinks";
            }

            // Create a folder to hold the subject notes and path
            if (!Directory.Exists(FolderPath + "Notes"))
            {
                Directory.CreateDirectory(FolderPath + "Notes");
                CommonStaticMembers.DataNodesNotesPath = FolderPath + "Notes";


            }


            //Create a text file to hold the Itemcount and path
            if (!File.Exists(FolderPath+ "ItemCount.txt"))
            {
                //File.Create(FolderPath + "ItemCount.txt");
                File.WriteAllText(FolderPath + "ItemCount.txt", "0");

                CommonStaticMembers.ItemCountPath = FolderPath + "ItemCount.txt";
            }

            btnOpenSubjectFolder.IsEnabled = false;

            //Communicate the FolderPath to the ViewModel.SubjectNodeViewModel's OpenFile method
            SubjectStaticMembers.OpenFiles(FolderPath);


            MessageBox.Show("Create of Open a Folder to hold References. Normally, this will be in a more generic"+
                " folder and its name will reflect the Generic Interest. for example 'Religion References'");

            // Get or create the Name of the Subject folder
            CommonOpenFileDialog referenceDialog = new CommonOpenFileDialog();
            referenceDialog.IsFolderPicker = true;
            string ReferenceFolderPath = "";
            if (referenceDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ReferenceFolderPath = referenceDialog.FileName + '\\';
            }

            // Create "NoteReferenceFiles" and "CompositData" folders

            // Create NoteReferenceFiles
            //      Create path to + "\\NoteReferenceFiles"
            string NoteReferenceFilesPath = ReferenceFolderPath + "NoteReferenceFiles";
            if (!Directory.Exists(NoteReferenceFilesPath))
            {
                Directory.CreateDirectory(NoteReferenceFilesPath);
            }

           

            // Set the NoteReferenceFilesPath
            CommonStaticMembers.NoteReferencesPath = NoteReferenceFilesPath;

            // Create CompositData folder
            string CompositDataPath = ReferenceFolderPath + "\\CompositData";
            if (!Directory.Exists(CompositDataPath))
            {
                Directory.CreateDirectory(CompositDataPath);
            }

            // set the CompositDataPath
            SubjectStaticMembers.CompositDataPath = CompositDataPath;

            // If it doesn't exist create the NotesDictionary file
            string NotesDictionaryPath = CompositDataPath + "\\NotesDictionary.txt";
            if (!File.Exists(NotesDictionaryPath))
            {
                var fileStream = File.Create(NotesDictionaryPath);
                fileStream.Close();
               
            }

            // Set the NotesDictionaryPath 
            SubjectStaticMembers.NotesDictionaryPath = NotesDictionaryPath;

            if (MessageBox.Show("Create or Select Timeline?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                MessageBox.Show("Pause here and create the Excel work sheet");
                CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog();

                string TimelineFilePath = "";
                if (openFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    TimelineFilePath = openFileDialog.FileName ;
                }

                // Set the TimelineFilePath
                SubjectStaticMembers.TimelineFilePath = TimelineFilePath;
            }





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

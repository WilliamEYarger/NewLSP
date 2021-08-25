





































using Microsoft.WindowsAPICodePack.Dialogs;
using NewLSP.StaticHelperClasses;
using System.Windows;
using System.Windows.Controls;
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


        /// <summary>
        /// This is the first method called when the user begins the application
        /// It forces the user to either create a Subjects folder and a References folder
        /// of to select such folders that have been previously created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenSubjectFolder_Click(object sender, RoutedEventArgs e)
        {
            //Create a lable so that the user can return here if needed
            SubjectFolder:  MessageBox.Show("Create or Open an existing -- S U B J E C T  -- folder");
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

            // Save the Path to  the selected subject ends with \\
            CommonStaticMembers.SubjectFolderPath = FolderPath;

            //Create a folder to hold the QAFiles and path
            if (!Directory.Exists(FolderPath + "QAFiles"))
            {
                // Tell the user he is creating a new subject folder and allow him to escape if necessary
                if (MessageBox.Show("Do you want to create a new Subject folder?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    FolderPath = "";
                    CommonStaticMembers.SubjectFolderPath = FolderPath;
                    goto SubjectFolder;
                }
                



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

            // Create a folder to hold the subject DataNodesNoteReferencesFiles and path
            if (!Directory.Exists(FolderPath + "DataNodesNoteReferencesFiles"))
            {
                Directory.CreateDirectory(FolderPath + "DataNodesNoteReferencesFiles");
                CommonStaticMembers.DataNodesNoteReferencesFilesPath = FolderPath + "DataNodesNoteReferencesFiles";


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

            // Show a message telling the user to create or select a common references folder and
            //      create a lable allowing the user to return here if desirec
            References:  MessageBox.Show("Create of Open a Folder to hold -- R E F E R E N C S --. Normally, this will be in a more generic"+
                " folder and its name will reflect the Generic Interest. for example 'Religion References'");

            /*
             * Either select an existing Common References folder or create one
             * using CommonOpenFileDialog and its FileName property to
             * Create a ReferenceFolderPath
             */


            CommonOpenFileDialog referenceDialog = new CommonOpenFileDialog();
            referenceDialog.IsFolderPicker = true;
            string ReferenceFolderPath = "";
            if (referenceDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                //The path to the Common Rererence Folder
                ReferenceFolderPath = referenceDialog.FileName + '\\';
            }

            // Create "NoteReferenceFiles" and "CompositData" folders

            // Create a path to the NoteReferenceFiles folder in the Common References folder
            string NoteReferenceFilesPath = ReferenceFolderPath + "NoteReferenceFiles";

            //Check to see if this folder(Directory) exists and if not create it
            if (!Directory.Exists(NoteReferenceFilesPath))
            {
                // Tell the user he is creating a new references folder and allow him to escape if necessary
                if (MessageBox.Show("Do you want to create a new References folder?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    ReferenceFolderPath = "";
                    goto References;
                }

                Directory.CreateDirectory(NoteReferenceFilesPath);
            }
                       

            // Set the NoteReferenceFilesPath
            CommonStaticMembers.NoteReferencesPath = NoteReferenceFilesPath;

            // Create a pathe to the CompositData folder that will hold data about Key Words
            string CompositDataPath = ReferenceFolderPath + "CompositData";

            //If this folder doesn't exist create it
            if (!Directory.Exists(CompositDataPath))
            {
                Directory.CreateDirectory(CompositDataPath);
            }

            // set the CompositDataPath
            SubjectStaticMembers.CompositDataPath = CompositDataPath;

            // If it doesn't exist create the NotesDictionary file
            string KeyWordsDictionaryPath = CompositDataPath + "\\KeyWordsDictionary.txt";
            if (!File.Exists(KeyWordsDictionaryPath))
            {
                var fileStream = File.Create(KeyWordsDictionaryPath);
                fileStream.Close();               
            }

            // Set CommonStaticMembers.KeyWordsDictionaryPath
            CommonStaticMembers.KeyWordsDictionaryPath = KeyWordsDictionaryPath;


            
            string ListOfKeyWordsPath = CompositDataPath + "\\ListOfKeyWords.txt";

            if (!File.Exists(ListOfKeyWordsPath))
            {
                var fileStream = File.Create(ListOfKeyWordsPath);
                fileStream.Close();

            }

            // Set the ListOfKeyWordsPath in KeyWordsStaticMembers
            KeyWordsStaticMembers.ListOfKeyWordsPath = ListOfKeyWordsPath;



            if (MessageBox.Show("Create or Select -- T I M E L I N E -- or Click SubjectTreePage?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
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


            // Set the initial value of CurrentNoteIDInt

            CommonStaticMembers.CurrentNoteIDInt = -1;


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

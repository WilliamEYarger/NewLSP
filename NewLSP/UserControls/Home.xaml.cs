using Microsoft.WindowsAPICodePack.Dialogs;
using NewLSP.StaticHelperClasses;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using NewLSP.DataModels;
using System.Collections.Generic;
using System.Linq;
using System;


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

        //20221211 Create booleans so that routed events for creating an new References folder can be called
        private bool CallReferenceFolder = false;
        private string ReferenceFolderPath = "";


        /// <summary>
        /// This is the first method called when the user begins the application
        /// It forces the user to either create a Subjects folder and a References folder
        /// of to select such folders that have been previously created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenSubjectFolder_Click(object sender, RoutedEventArgs e)
        {
            if (CallReferenceFolder ) goto References;
            MessageBox.Show("Create or Open an existing -- S U B J E C T  -- folder");
            // Get or create the Name of the Subject folder
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            string FolderPath = "";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPath = dialog.FileName + '\\';
                CommonStaticMembers.HomeFolderPath = FolderPath;                               
            }
            //20221211 At this point the path th the Subject/Home folder is known
            SetupHomefolder();

        /*
                    // Get the number of '\\'s in FolderPath
                    var NumberOfSlashes = StringHelper.ReturnNumberOfDeliniters(FolderPath, '\\');


                    // Get the Subjects Name from the item a position NumberOfSlashes -1
                    var FolderName = StringHelper.ReturnItemAtPos(FolderPath, '\\', NumberOfSlashes - 1);
                    lblTitle.Content = "This is the Subjects Tree for " + FolderName;

                    // ADDED 20211203
                    //Use the FolderName to name the Subject, and its main data files
                    string SubjectName = FolderName;

                    // Create path to this subjects data file
                    string SubjectsNodeDataStringsPath = CommonStaticMembers.HomeFolderPath + "NodeDataStrings.txt";
                    CommonStaticMembers.SubjectsNodeDataStringsPath = SubjectsNodeDataStringsPath;

                    // Test to see if this file exist and if not create it
                    if (!File.Exists(SubjectsNodeDataStringsPath))
                    {

                        // Tell the user he is creating a new subject folder and allow him to escape if necessary
                        if (MessageBox.Show("Do you want to create a new Subject folder?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            FolderPath = "";
                            CommonStaticMembers.HomeFolderPath = FolderPath;
                            goto SubjectFolder;
                        }

                        //Call the create new NodeDataStrings.txt file and add a Root node to it
                        CreateNewSubjectFolderFilesAndfolders();
                    }// End Test to see if this file exist and if not create it

                    else //open and read existing ItemCount.txt and NodeDataStrings.txt
                    {
                        SubjectNodes RootNode = new SubjectNodes();

                        // Read in the current item count
                        string ItemCount = File.ReadAllText(CommonStaticMembers.HomeFolderPath + "ItemCount.txt");
                        SubjectStaticMembers.ItemCount = ItemCount;

                        // Instantiate the Dictionary
                        SubjectStaticMembers.SubjectNodeDictionary = new Dictionary<string, SubjectNodes>();

                        // Instantiate a new node
                        SubjectNodes ThisNode = new SubjectNodes();
                        // Create the delimiter
                        char D = '\u0240';

                        //Read in SubjectsNodeDataStringsPath 
                        string[] SubjectNodeDataStringArray = File.ReadAllLines(SubjectsNodeDataStringsPath);

                        foreach (string line in SubjectNodeDataStringArray)
                        {
                            // get the properties of a SubjectNode
                            string[] ItemsInLine = line.Split(D);
                            ThisNode.LeadingChars = ItemsInLine[0];
                            ThisNode.CI = ItemsInLine[1];
                            ThisNode.TitleText = ItemsInLine[2];
                            ThisNode.NodeLevelName = ItemsInLine[3];
                            string IDString = ItemsInLine[4];
                            ThisNode.ID = Int32.Parse(IDString);
                            string NOCString = ItemsInLine[5];
                            ThisNode.NOC = Int32.Parse(NOCString);
                            string HasDataString = ItemsInLine[6];
                            if (HasDataString == "false")
                            {
                                ThisNode.HasData = false;
                            }
                            else
                            {
                                ThisNode.HasData = true;
                            }

                            ThisNode = new SubjectNodes();

                        }// End open and read existing ItemCount.txt and NodeDataStrings.txt

                        SubjectStaticMembers.DisplayParentsAndChildren("*");

                    }// End if else file subject file exists


                    btnOpenSubjectFolder.IsEnabled = false;

                    //Communicate the FolderPath to the ViewModel.SubjectNodeViewModel's OpenFile method
                    SubjectStaticMembers.OpenFiles(FolderPath);
        */
        //20221211 End of code establishing the HOme-Subject folder

        // Show a message telling the user to create or select a common references folder and
        //      create a lable allowing the user to return here if desirec
        References:
            //20221211
            CallReferenceFolder = false;
            MessageBox.Show("Create of Open a Folder to hold -- R E F E R E N C S --. Normally, this will be in a more generic"+
                " folder and its name will reflect the Generic Interest. for example 'Religion References'");

            /*
             * Either select an existing Common References folder or create one
             * using CommonOpenFileDialog and its FileName property to
             * Create a ReferenceFolderPath
             */


            CommonOpenFileDialog referenceDialog = new CommonOpenFileDialog();
            referenceDialog.IsFolderPicker = true;
            ReferenceFolderPath = "";
            if (referenceDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                //The path to the Common Rererence Folder
                ReferenceFolderPath = referenceDialog.FileName + '\\';
            }

            SetupReferencesFolder();
/*
            //202212110659 At this point the path to the references folder is known
            *
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


           

            // Create a Sorted KeyWord List
            string ListOfSortedKeyWordsPath = CompositDataPath + "\\SortedListOfKeyWords.txt";

            // Insure that the file doesn't already exist
            if (!File.Exists(ListOfSortedKeyWordsPath))
            {
                var fileStream = File.Create(ListOfSortedKeyWordsPath);
                fileStream.Close();

            }

            // Set the SortedListOfKeyWordsPath in KeyWordsStaticMembers
            KeyWordsStaticMembers.ListOfSortedKeyWordsPath = ListOfSortedKeyWordsPath;

           //Get the List of Key Words, Sort it and update the List of Sorted KeyWords
           string [] ArrayOfKeyWords = File.ReadAllLines(KeyWordsStaticMembers.ListOfKeyWordsPath);
            // Convert this array to a list and sort it
            List<string> SortedListOfKeyWords = ArrayOfKeyWords.ToList();
            SortedListOfKeyWords.Sort();
            //Concert this sorted list back into an array
            ArrayOfKeyWords = SortedListOfKeyWords.ToArray();
            //Write this array to the 
            File.WriteAllLines(KeyWordsStaticMembers.ListOfSortedKeyWordsPath, ArrayOfKeyWords);
            // Set the initial value of CurrentNoteIDInt

            MessageBox.Show("Click SubjectTreePage and then Click SHOW DISPLAY LIST to see all of the Base subjects for this project");

            CommonStaticMembers.CurrentNoteIDInt = -1;

*/
        }// End btnOpenSubjectFolder_Click


       /// <summary>
        /// This Private method added in the 20221210 version takes the path to the Home folder(i.e. Subject folder)
        /// and uses it to either create or open all of the required files
        /// summary>
        private void SetupHomefolder()
        {
            string FolderPath = CommonStaticMembers.HomeFolderPath;
            // Get the number of '\\'s in FolderPath
            var NumberOfSlashes = StringHelper.ReturnNumberOfDeliniters(FolderPath, '\\');


            // Get the Subjects Name from the item a position NumberOfSlashes -1
            var FolderName = StringHelper.ReturnItemAtPos(FolderPath, '\\', NumberOfSlashes - 1);
            lblTitle.Content = "This is the Subjects Tree for " + FolderName;

            // ADDED 20211203
            //Use the FolderName to name the Subject, and its main data files
            string SubjectName = FolderName;

            // Create path to this subjects data file
            string SubjectsNodeDataStringsPath = CommonStaticMembers.HomeFolderPath + "NodeDataStrings.txt";
            CommonStaticMembers.SubjectsNodeDataStringsPath = SubjectsNodeDataStringsPath;

            // Test to see if this file exist and if not create it
            if (!File.Exists(SubjectsNodeDataStringsPath))
            {

                // Tell the user he is creating a new subject folder and allow him to escape if necessary
                if (MessageBox.Show("Do you want to create a new Subject folder?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    FolderPath = "";
                    CommonStaticMembers.HomeFolderPath = FolderPath;
                    // Call the btnNewChild_Click nethod
                    RoutedEventArgs ex = new RoutedEventArgs();
                    btnOpenSubjectFolder_Click(this, ex);                   
                }

                //Call the create new NodeDataStrings.txt file and add a Root node to it
                CreateNewSubjectFolderFilesAndfolders();
            }// End Test to see if this file exist and if not create it

            else //open and read existing ItemCount.txt and NodeDataStrings.txt
            {
                SubjectNodes RootNode = new SubjectNodes();

                // Read in the current item count
                string ItemCount = File.ReadAllText(CommonStaticMembers.HomeFolderPath + "ItemCount.txt");
                SubjectStaticMembers.ItemCount = ItemCount;

                // Instantiate the Dictionary
                SubjectStaticMembers.SubjectNodeDictionary = new Dictionary<string, SubjectNodes>();

                // Instantiate a new node
                SubjectNodes ThisNode = new SubjectNodes();
                // Create the delimiter
                char D = '\u0240';

                //Read in SubjectsNodeDataStringsPath 
                string[] SubjectNodeDataStringArray = File.ReadAllLines(SubjectsNodeDataStringsPath);

                foreach (string line in SubjectNodeDataStringArray)
                {
                    // get the properties of a SubjectNode
                    string[] ItemsInLine = line.Split(D);
                    ThisNode.LeadingChars = ItemsInLine[0];
                    ThisNode.CI = ItemsInLine[1];
                    ThisNode.TitleText = ItemsInLine[2];
                    ThisNode.NodeLevelName = ItemsInLine[3];
                    string IDString = ItemsInLine[4];
                    ThisNode.ID = Int32.Parse(IDString);
                    string NOCString = ItemsInLine[5];
                    ThisNode.NOC = Int32.Parse(NOCString);
                    string HasDataString = ItemsInLine[6];
                    if (HasDataString == "false")
                    {
                        ThisNode.HasData = false;
                    }
                    else
                    {
                        ThisNode.HasData = true;
                    }

                    ThisNode = new SubjectNodes();

                }// End open and read existing ItemCount.txt and NodeDataStrings.txt

                SubjectStaticMembers.DisplayParentsAndChildren("*");

            }// End if else file subject file exists


            btnOpenSubjectFolder.IsEnabled = false;

            //Communicate the FolderPath to the ViewModel.SubjectNodeViewModel's OpenFile method
            SubjectStaticMembers.OpenFiles(FolderPath);
        }//End private void SetupHomefolder()


        private void SetupReferencesFolder()
        {
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
                    CallReferenceFolder = true;
                    RoutedEventArgs ex = new RoutedEventArgs();
                    btnOpenSubjectFolder_Click(this, ex);
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




            // Create a Sorted KeyWord List
            string ListOfSortedKeyWordsPath = CompositDataPath + "\\SortedListOfKeyWords.txt";

            // Insure that the file doesn't already exist
            if (!File.Exists(ListOfSortedKeyWordsPath))
            {
                var fileStream = File.Create(ListOfSortedKeyWordsPath);
                fileStream.Close();

            }

            // Set the SortedListOfKeyWordsPath in KeyWordsStaticMembers
            KeyWordsStaticMembers.ListOfSortedKeyWordsPath = ListOfSortedKeyWordsPath;

            //Get the List of Key Words, Sort it and update the List of Sorted KeyWords
            string[] ArrayOfKeyWords = File.ReadAllLines(KeyWordsStaticMembers.ListOfKeyWordsPath);
            // Convert this array to a list and sort it
            List<string> SortedListOfKeyWords = ArrayOfKeyWords.ToList();
            SortedListOfKeyWords.Sort();
            //Concert this sorted list back into an array
            ArrayOfKeyWords = SortedListOfKeyWords.ToArray();
            //Write this array to the 
            File.WriteAllLines(KeyWordsStaticMembers.ListOfSortedKeyWordsPath, ArrayOfKeyWords);
            // Set the initial value of CurrentNoteIDInt

            txtUrls.Text = "Click SubjectTreePage and then Click SHOW DISPLAY LIST to see all of the Base subjects for this project";

            CommonStaticMembers.CurrentNoteIDInt = -1;
        }//End SetupReferencesFolder();

        /// <summary>
        /// The purpose of this method is for the user to 
        /// either open a previously designated instructions
        /// folder or create it and then copy all of
        /// the instructions text files into it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        // 202108251129 inactivated this section
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


        /// <summary>
        /// This private method is called when the user wants to create a new folder
        /// </summary>
        private void CreateNewSubjectFolderFilesAndfolders()
        {

            CreateNewNodeDataStringsFile();
            CreateItemCountTextFile();
            CreateQAFilesFolder();
            CreateQAResultFolder();
            CreateHyperlinksFolder();
            CreateDataNodesNoteReferencesFilesFolder();
        }

        /// <summary>
        /// Creates a new NodeDataString.txt file and populates it with a Root node
        /// Called by CreateNewSubjectFolderFilesAndfolders()
        /// </summary>
        private void CreateNewNodeDataStringsFile()
        {
            // Create path to this subjects data file
            CommonStaticMembers.SubjectsNodeDataStringsPath = CommonStaticMembers.HomeFolderPath + "NodeDataStrings.txt";

            //Create a new RootNode
            SubjectNodes RootNode = new SubjectNodes(0);

            // Assign the CurrentItemCount to the Root node's ID
            RootNode.ID = 0;
            string ItemCount = "1";

            //Write the new ItemCount to the ItemCount.txt file
            File.WriteAllText(CommonStaticMembers.HomeFolderPath + "ItemCount.txt", ItemCount.ToString());

            //Set up the properties of the new RootNode
            RootNode.CI = "- ";
            RootNode.NodeLevelName = "*";
            int LengthNodeLevelName = RootNode.NodeLevelName.Length;
            string LeadingChars = new string(' ', LengthNodeLevelName);
            RootNode.LeadingChars = LeadingChars;
            RootNode.NOC = 0;
            RootNode.TitleText = "Root";
            RootNode.HasData = false;


            // Create Root Node Data String and write it to the NodeDataStrings.txt file

            char D = '\u0240';
            string RootNodeDataString = RootNode.LeadingChars + D + RootNode.CI + D + RootNode.TitleText + D + RootNode.NodeLevelName + D +
                RootNode.ID + D + RootNode.NOC + D + RootNode.HasData;

            // Write RootNodeDataString to the NodeDataStrings.txt file
            File.WriteAllText(CommonStaticMembers.SubjectsNodeDataStringsPath, RootNodeDataString);

            //Add this rootnode to the SubjectNodeDictionary
            SubjectStaticMembers.SubjectNodeDictionary.Add(RootNode.NodeLevelName, RootNode);

            // Create the DisplayString for the root node
            string RootDisplayString = $"{RootNode.LeadingChars}{RootNode.CI}{RootNode.TitleText}";

            //Add this root node to the DisplayList
            SubjectStaticMembers.DisplayList.Add(RootDisplayString);

            // Add the root Node's NodeLevelName to the DisplaySubjectNodesList
            SubjectStaticMembers.SubjectNodesLevelNameList.Add(RootNode.NodeLevelName);

        }

        /// <summary>
        /// Creates the ItemCpunt.txt file
        /// Called by CreateNewSubjectFolderFilesAndfolders()
        /// </summary>
        private void CreateItemCountTextFile()
        {
            string ItemCount = "1";
            File.WriteAllText(CommonStaticMembers.HomeFolderPath + "ItemCount.txt", ItemCount.ToString());
            // Write the item count to SubjectStaticMembers.ItemCount
            SubjectStaticMembers.ItemCount = ItemCount;           

            CommonStaticMembers.ItemCountPath = CommonStaticMembers.HomeFolderPath + "ItemCount.txt";
        }

        private void CreateQAFilesFolder()
        {
            //Create a folder to hold the QAResults and path
            if (!Directory.Exists(CommonStaticMembers.HomeFolderPath + "QAFiles"))
            {
                Directory.CreateDirectory(CommonStaticMembers.HomeFolderPath + "QAFiles");
                CommonStaticMembers.DataNodesQAResultsFilePath = CommonStaticMembers.HomeFolderPath + "QAFiles";               
            }
        }

        /// <summary>
        /// Creates the QAReslts folder 
        /// Is called by CreateNewSubjectFolderFilesAndfolders()
        /// </summary>
        private void CreateQAResultFolder()
        {
            //Create a folder to hold the QAResults and path
            if (!Directory.Exists(CommonStaticMembers.HomeFolderPath + "QAResults"))
            {
                Directory.CreateDirectory(CommonStaticMembers.HomeFolderPath + "QAResults");
                CommonStaticMembers.DataNodesQAResultsFilePath = CommonStaticMembers.HomeFolderPath + "QAResults";
            }
        }

        /// <summary>
        /// Creates the Hyperlinks folder
        /// Is called by CreateNewSubjectFolderFilesAndfolders()
        /// </summary>
        private void CreateHyperlinksFolder()
        {
            //Create a folder to hold the hyperlinks files and path
            if (!Directory.Exists(CommonStaticMembers.HomeFolderPath + "Hyperlinks"))
            {
                Directory.CreateDirectory(CommonStaticMembers.HomeFolderPath + "Hyperlinks");
                CommonStaticMembers.DataNodesHyperlinksPath = CommonStaticMembers.HomeFolderPath + "Hyperlinks";
            }
        }

        /// <summary>
        /// Creates the DataNodesReferencesFiles folder
        /// Is called by CreateNewSubjectFolderFilesAndfolders()
        /// </summary>
        private void CreateDataNodesNoteReferencesFilesFolder()
        {
            // Create a folder to hold the subject DataNodesNoteReferencesFiles and path
            if (!Directory.Exists(CommonStaticMembers.HomeFolderPath + "DataNodesNoteReferencesFiles"))
            {
                Directory.CreateDirectory(CommonStaticMembers.HomeFolderPath + "DataNodesNoteReferencesFiles");
                CommonStaticMembers.DataNodesNoteReferencesFilesPath = CommonStaticMembers.HomeFolderPath + "DataNodesNoteReferencesFiles";
            }
        }

        private void txtUrls_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //enter code to get Subject and References URLS, check their validity andif good open  Subject Tree
            // Split txtUrls. Text into HomeFolder
            if (txtUrls.Text == "Click SubjectTreePage and then Click SHOW DISPLAY LIST to see all of the Base subjects for this project") return;
            String[] Urls = txtUrls.Text.Split('^');
            string HomeFolderPath = Urls[0];
            ReferenceFolderPath = Urls[1];

            if ((!Directory.Exists(HomeFolderPath)) || (!Directory.Exists(ReferenceFolderPath)))
                {
                MessageBox.Show("One or both of these files do not exits.");
                txtUrls.Text = "";
                return;
                }

            HomeFolderPath = HomeFolderPath + '\\';
            ReferenceFolderPath = ReferenceFolderPath + '\\';
            //Verity the Subject Folder
            string ItemCountPath = HomeFolderPath + "ItemCount.txt";
            long ItemCountSize    = new FileInfo(ItemCountPath).Length;
            if (!File.Exists(ItemCountPath))
            {

                MessageBox.Show("The Subject folder has not been creates so this is invalid");
                txtUrls.Text = "";
                return;
            }

            // Verify the ReferenceFolder
            if(!Directory.Exists(ReferenceFolderPath+ "\\CompositData"))
            {
                MessageBox.Show("The Reference folder has not been creates so this is invalid");
                txtUrls.Text = "";
                return;
            }

            //Create the path to the Home folder and then call SetupHomeFolder to open or create the needed files
            
            CommonStaticMembers.HomeFolderPath = HomeFolderPath;
            SetupHomefolder();


            //Create the path the the References folder and call SetupReferencesFolder to open or create the needed files
            ReferenceFolderPath = ReferenceFolderPath  + '\\';
            SetupReferencesFolder();
            CommonStaticMembers.CurrentNoteIDInt = -1;
            txtUrls.Text = "Click SubjectTreePage and then Click SHOW DISPLAY LIST to see all of the Base subjects for this project";
        }
    }// End partial class Home 


}// End Namesapce

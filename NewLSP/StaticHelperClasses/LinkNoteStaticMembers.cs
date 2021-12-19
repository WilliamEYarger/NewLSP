using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NewLSP.DataModels;

namespace NewLSP.StaticHelperClasses
{
    public static class LinkNoteStaticMembers
    {

        #region Properties and fields


        #region Hyperlink

        private static string _Hyperlink;
        /// <summary>
        /// This is the path to a hyperlinked file chosen by the user as 
        /// a hyperlink file using the OpenFildDialog
        /// </summary>
        public static string Hyperlink
        {
            get { return _Hyperlink; }
            set { _Hyperlink = value; }
        }

        #endregion Hyperlink


        #region FileType
        private static string _FileType;
        /// <summary>
        /// This gets and sets the file type of multiple different file formats
        /// chosed by the user, ie web, docx, jpg etc
        /// </summary>
        public static string FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }

        #endregion FileType


        #region BookMarks

        private static List<string> _BookMarks = new List<string>();
        /// <summary>
        /// This stores any book mark to a docx file specified by the user,
        /// if the user opens that docx file this bookmark is copied to the
        /// ScratchPad and can be recalled to use in th GoTo bookmark menu
        /// </summary>
        public static List<string> BookMarks
        {
            get { return _BookMarks; }
            set { _BookMarks = value; }
        }


        #endregion  BookMarks

        #region HyperlinkName

        private static string _HyperlinkName;

        public static string HyperlinkName
        {
            get { return _HyperlinkName; }
            set { _HyperlinkName = value; }
        }

        #endregion HyperlinkName

        #region HyperlinkDictionary

        /// <summary>
        /// This Dictionary uses the item's position in the lbxLinks ListBox
        /// as the Key and a HyperlinkObject as the value
        /// The HyperlinkObject contains: 1) the BookMark, 2) the Name,
        /// 3) the Url and 4) the FileType
        /// </summary>);

        private static Dictionary<int, LinkNoteModel.HyperlinkObject> _HyperlinkDictionary = new Dictionary<int, LinkNoteModel.HyperlinkObject>();

        public static Dictionary<int, LinkNoteModel.HyperlinkObject> HyperlinkDictionary
        {
            get 
            { 
                return _HyperlinkDictionary; 
            }
            set { _HyperlinkDictionary = value; }
        }


        #endregion HyperlinkDictionary

        #region HyperlinkUrls
        /// <summary>
        /// This is a list of hyperlinks to files of web sites chosen by the user
        /// to be associated with a givin DataNode 
        /// It is only called locally by the  SetHyperlinkStringsList( method
        /// </summary>

        private static List<string> HyperlinkUrls = new List<string>();

        #endregion HyperlinkUrls


        #region HyperlinkStringsList
        /// <summary>
        /// a delimites string containing the elements of
        /// a hyperlink objects added to the list<string> HyperlinkStringsList
        /// Called by LinkNote.xaml.cs miSaveHyperlink_Clic
        /// </summary>
       
        private static List<string> _HyperlinkStringsList = new List<string>();

        public static List<string> HyperlinkStringsList
        {
            get { return  _HyperlinkStringsList; }
            set { List<string> _HyperlinkStringsList = value; }
        }

        #endregion HyperlinkStringsList

        #region ListOfNoteNames

        private static List<string> _ListOfNoteNames;
        /// <summary>
        /// This is created by the  local ReadInNotesFile( method
        /// which is called by the LinkNOtes.xaml.cs ReadNotesIntoSelectNoteListBox((
        ///     When the user clicks the miShowNote_Click( Show Notes menu item or by
        ///     miDisplayNoteNames_Click( 
        /// </summary>
        public static List<string> ListOfNoteNames
        {
            get { return _ListOfNoteNames; }
            set { _ListOfNoteNames = value; }
        }

        #endregion ListOfNoteNames

        #region ListOfNoteReferceFileNames

        private static List<string> _ListOfNoteReferceFileNames;

        /// <summary>
        /// This is the list on NoteReference Char26Names belonging to a DataNode
        /// </summary>
        public static List<string> ListOfNoteReferceFileNames
        {
            get { return _ListOfNoteReferceFileNames; }
            set { _ListOfNoteReferceFileNames = value; }
        }

        #endregion ListOfNoteReferceFileNames


        #region CurrentNote26Name
        /// <summary>
        /// This is the name of the currently active CommonReferences NoteReferenceFile
        /// is is created by taking the current number of files in the
        /// NoteReferenceFile folder  and converting it to a base 26 integer which 
        /// uses all Alpha Characters from A to Z where A = 0 and Z= 25 and AA = 26 etc
        /// It is call by LinkNoteStaticMember methods:
        /// SaveAndUpdateNoteReferenceAndKeywords( two times
        /// </summary>
        public static string CurrentNote26Name;

        


        #endregion CurrentNote26Name

        #region ListBoxOfSelectedNotesList
        private static List<string> _ListBoxOfSelectedNotesList;
        /// <summary>
        /// This list contains the strings that will be displayed in the 
        /// lbxOpenSelectedNote ListBox of Notes  
        /// </summary>
        public static List<string> ListBoxOfSelectedNotesList
        {
            get { return _ListBoxOfSelectedNotesList; }
            set { _ListBoxOfSelectedNotesList = value; }
        }


        #endregion ListBoxOfSelectedNotesList

        #region DataNodesNoteReferenceString

        private static string _DataNodesNoteReferenceString;
        /// <summary>
        /// This is a string Where the Name of the Note appears in the lbxOpenSelectedNote 
        /// ListBox while the NoteCurrentNote26Name (the DataNode's ID)  is spaced out of view
        /// </summary>
        public static string DataNodesNoteReferenceString
        {
            get { return _DataNodesNoteReferenceString; }
            set { _DataNodesNoteReferenceString = value; }
        }


        #endregion  DataNodesNoteReferenceString

        #region EditingBoolean
        private static bool _EditingBoolean = false;
        /// <summary>
        /// This is set true if the user checks the rbtEdit radio button 
        /// calling rbtEdit_Click(;
        /// It means that the data in the current Note or HyperLink can be edited
        /// </summary>
        public static bool EditingBoolean
        {
            get { return _EditingBoolean ; }
            set { _EditingBoolean  = value; }
        }

        #endregion EditingBoolean

        #region DelStrOfKeyWordsAndComments

        /// <summary>
        /// This string contains all of the KeyWords and '#' comments of a right Mouse button 
        /// selected NoteRefeerenceFile 
        /// It is called by Link_Note.xaml.cs.lbxOpenSelectedNote_PreviewMouseRightButtonUp( Method
        /// </summary>
        public static string DelStrOfKeyWordsAndComments { get; internal set; }

        #endregion DelStrOfKeyWordsAndComments

        #region ShowAllKeyWords boolean

        private static bool _ShowAllKeywords;

        /// <summary>
        /// If this bool is true all keywords and comments will be displaed when the user right clicks
        /// the Link_Note.xaml.cs.lbxOpenSelectedNote_PreviewMouseRightButtonUp( Method
        /// If it is false, then only the selected KeyWord and its following '#' comments will be shown
        /// </summary>
        public static bool ShowAllKeywords
        {
            get { return _ShowAllKeywords; }
            set { _ShowAllKeywords = value; }
        }




        #endregion ShowAllKeyWords boolean

        #region SelectedKeyWord
        /// <summary>
        /// This string represents the KeyWord the user has selected to search for in all notes
        /// </summary>
        public static string SelectedKeyWord { get; internal set; }


        /// <summary>
        /// If the user clicks the Search radiobutton then KeyWordSearch is true
        /// </summary>
        public static bool KeyWordSearch { get; internal set; }

        /// <summary>
        /// This is the Key word chosen when the user is in the Search mode
        /// </summary>
        public static string SearchKeyWord { get; internal set; }
        #endregion SelectedKeyWord

        #endregion Properties

        #region Public Methods

        #region AddHyperlinkToList


        /// <summary>
        /// Adds a delimites string containing the elements of
        /// a hyperlink object to the list<string> HyperlinkStringsList
        /// Called by LinkNote.xaml.cs miSaveHyperlink_Clic
        /// </summary>
        /// <param name="delimitedHyperlink"></param>
        public static void AddHyperlinkToList(string delimitedHyperlink)
        {
            HyperlinkStringsList.Add(delimitedHyperlink);
        }


        #endregion AddHyperlinkToList

        #region AddItemToHyperlinkDictionary
        /// <summary>
        /// When the user clicks the Open Hyperlink Menu item 
        /// The 'cntr' parameter specifies the number of individual hyperlink references
        /// each hyperlink is on a single line delinited by \r\n return characters
        /// the meth0d then adds it to the HyperlinkDictionary
        /// </summary>
        /// <param name="cntr"></param>
        /// <param name="thisHyperlinkObject"></param>
        public static void AddItemToHyperlinkDictionary(int cntr, LinkNoteModel.HyperlinkObject thisHyperlinkObject)
        {   
            HyperlinkDictionary.Add(cntr, thisHyperlinkObject);
        }

        #endregion AddItemToHyperlinkDictionary


        #region Get Hyperlink

        public static LinkNoteModel.HyperlinkObject GetHyperlinkObject(int itemsIndex)
        {
            return (LinkNoteModel.HyperlinkObject) HyperlinkDictionary[itemsIndex];
        }


        /// <summary>
        /// Called when the LinkNote control is expanded
        /// It  reads all on the lines in the hyperlink file into the DataNodeHyperlinkArray and then
        /// for each line it:
        ///     1) Adds the delimitd line to the HyperlinkStringsList
        ///     2) Sets the  int HyperlinkCntr to 0
        ///     3) It cycles through each line the DataNodeHyperlinkArray
        ///     creates the HyperlinkDictionary using its position 
        ///         in the array as the key and the component itmes as
        ///         properties of the LinkNoteModel.HyperlinkObject that is the value
        /// </summary>
        internal static void SetHyperlinkStringsList()
        {
            //read in the hyperlinks file
            string[] DataNodeHyperlinkArray = 
                File.ReadAllLines(CommonStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt");
            

            // Create a counter to use as the Key to the Hyperlinks dictionary
            int HyperlinkCntr = 0;

            //process each delimited line
            foreach (string line in DataNodeHyperlinkArray)
            {
                // add each line to the HyperlinkStringsList
                HyperlinkStringsList.Add(line);

                // Get the items in each line
                string[] HyperlinkLineArray = line.Split('^');
                HyperlinkUrls.Add(HyperlinkLineArray[0]);

                // create a new Hyperlink object
                LinkNoteModel.HyperlinkObject thisHyperlinkObject = new LinkNoteModel.HyperlinkObject();
                thisHyperlinkObject.Url = HyperlinkLineArray[0];
                thisHyperlinkObject.FileType = HyperlinkLineArray[1];
                thisHyperlinkObject.BookMark = HyperlinkLineArray[2];
                HyperlinkDictionary.Add(HyperlinkCntr, thisHyperlinkObject);
                HyperlinkCntr++;

            }// End foreach (string line in DataNodeHyperlinkArray

        }// End SetHyperlinkStringsList

        #endregion Get Hyperlink


        #region Create 4 Lists from DataNode's Note file

        /// <summary>
        /// This method receives a string array
        /// where every line is a '^' delimited string of note references
        /// It creates 4 lists (Names, Hyperlinks, BookMarkd, and KeyWords)
        /// and it returns the List of Note Names
        /// </summary>
        /// <param name="lines"></param>
        internal static List<string> CreateNoteLists(string[] lines)
        {

            //Docetism-Wikipeida^https://en.wikipedia.org/wiki/Docetism^^Docetism;#Definition;Ignatius of Antioch;1 John;

            // create Lists
            List<string> NamesList = new List<string>();
            List<string> HyperlinksList = new List<string>();
            List<string> BookmarkList = new List<string>();
            List<string> KeyWordsList = new List<string>();

            foreach (string line in lines)
            {
                string[] thisLineArray = line.Split('^');
                NamesList.Add(thisLineArray[0]);
                HyperlinksList.Add(thisLineArray[1]);
                BookmarkList.Add(thisLineArray[2]);
                KeyWordsList.Add(thisLineArray[3]);
            }

            return NamesList;
        }// End CreateNoteLists

        #endregion Create 4 Lists from DataNode's Note file


        #region ReadInNotesFile

        /// <summary>
        /// Uses the DataNode.ID to create a path to the References DataNodesNoteReferencesFiles 
        /// folder's DataNodeReference file text file
        /// It Creates a list of each 
        /// 
        /// </summary>
        public static void ReadInNotesFile()
        {
            //Create path to the DataNodeIDs notes file
            string IDFileName = SubjectStaticMembers.DataNode.ID.ToString();
            CommonStaticMembers.CurrentNoteIDInt = SubjectStaticMembers.DataNode.ID;
            //string DataNodesNotesPath = CommonStaticMembers.DataNodesNoteReferencesFilesPath  + IDFileName + ".txt";
            string DataNodesNotesPath = CommonStaticMembers.DataNodesNoteReferencesFilesPath;

            // create a string array of all lines in the DataNodeReferences file
            string[] lines = System.IO.File.ReadAllLines(DataNodesNotesPath);

            // Create a new instance of ListBoxOfSelectedNotesList
            ListBoxOfSelectedNotesList = new List<string>();
            // Convert lines to a display string and add it to the lbxSelectedNodes
            foreach (string line in lines)
            {
                string NoteName = StringHelper.ReturnItemAtPos(line, '^', 0);
                string NoteReferenceFileName = StringHelper.ReturnItemAtPos(line, '^', 1);
                int NumberOfSpaces = 250 - NoteName.Length;
                string spacedStr = new string(' ', NumberOfSpaces);
                string outputStr = NoteName + spacedStr + '^' + NoteReferenceFileName;
                ListBoxOfSelectedNotesList.Add(outputStr);
            }

            // Crear and instantiate all the required lists
            ListOfNoteNames = new List<string>();
            ListOfNoteReferceFileNames = new List<string>();
            //ListOfNoteHyperlinks = new List<string>();
            //ListOfNoteBookMarks = new List<string>();
            //ListOfNoteKeyWords = new List<string>();

            foreach(string line in lines)
            {
                // create an array of the note components
                string[] NoteComponents = line.Split('^');
             
                ListOfNoteNames.Add(NoteComponents[0]);
                ListOfNoteReferceFileNames.Add(NoteComponents[1]);
                //ListOfNoteBookMarks.Add(NoteComponents[2]);
                //ListOfNoteKeyWords.Add(NoteComponents[3]);

            }

        }

        #endregion ReadInNotesFile


        #region SaveAndUpdateNoteReferenceAndKeywords()
        /// <summary>
        /// This Method saves a note reference called Clicking the miSaveNote menu item
        /// The TASKS if must perform are to:
        ///     1. *Save the NoteReferenceStr to the NoteReference file
        ///     2.  *Cycle through the ';' delimited keywords
        ///         a.  Eliminating those that  begin with '#'
        ///         b.  converting ' ' to '_' in the remaining
        ///         c.  Cycle through the list of Remaining key words
        ///             1)  Checking to the the that keyword's dictionary value contains the 
        ///                 CurrentNote26Name, and if not appending it and then saving the
        ///                 revused value to the KeyWordDictionary object 
        ///            2)  When all Keywords have been processed writing th KeyWordDictionary to its file
        ///     3.  *Create a DataNodesNoteReferenceString

        /// </summary>
        public static void SaveAndUpdateNoteReferenceAndKeywords(string NoteReferenceStr)
        {
            //TASK1 Save the NoteReferenceStr to the NoteReference file and Add a link to the DataNode's DataNodeNoteReferenceFile
            //      Determine if it is an edited file
            if(EditingBoolean && (CommonStaticMembers.CurrentNote26Name != ""))
            {
                // This is an edited note reference 
                //  Get path to the Common NoteReferenceFiles
                string NoteReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + CommonStaticMembers.CurrentNote26Name + ".txt";
                File.WriteAllText(NoteReferenceFilePath, NoteReferenceStr);

                //Add a link to the DataNode's DataNodeNoteReferenceFile
                //      Get the DataNodes DataNodeNoteReferenceFile
                string DataNodesID = CommonStaticMembers.CurrentNoteIDInt.ToString();
                string DataNodesReferenceNotesPath = CommonStaticMembers.DataNodesNoteReferencesFilesPath+ DataNodesID+".txt";

                //      Test to see if the file exists and if so read all lines
                string[] DataNodesCurrentReferencesArray = null;
                if (File.Exists(DataNodesReferenceNotesPath))
                {
                    DataNodesCurrentReferencesArray = File.ReadAllLines(DataNodesReferenceNotesPath);

                    //      Construct the DataNode's reference string
                    string thisNoteName = StringHelper.ReturnItemAtPos(NoteReferenceStr, '^', 0);
                    string DataNodesReferenceStr = thisNoteName + '^' + CommonStaticMembers.CurrentNote26Name;

                    //      create a bool to indicate wether this refernces is already in the DataNodes referencew
                    bool ReverencePresent = false;

                    //      Cycle through DataNodesCurrentReferencesArray seein if DataNodesReferenceStr is present and if it isn't add it
                    foreach (string line in DataNodesCurrentReferencesArray)
                    {
                        if (line.IndexOf(DataNodesReferenceStr) == 0)
                        {
                            ReverencePresent = true;
                            break;
                        }
                    }

                    //      If refeerence is not present add it
                    if (!ReverencePresent)
                    {
                        List<string> ListOfDataNodesReferences = DataNodesCurrentReferencesArray.ToList();
                        ListOfDataNodesReferences.Add(DataNodesReferenceStr);
                        File.WriteAllLines(DataNodesReferenceNotesPath, ListOfDataNodesReferences);
                    }
                }
                else
                {
                    //      Construct the DataNode's reference string
                    string thisNoteName = StringHelper.ReturnItemAtPos(NoteReferenceStr, '^', 0);
                    string DataNodesReferenceStr = thisNoteName + '^' + CommonStaticMembers.CurrentNote26Name;
                    File.WriteAllText(DataNodesReferenceNotesPath, DataNodesReferenceStr);

                }
               


                ProcessKeyWords(NoteReferenceStr);
                return;
            }
            else
            {
                SaveNoteReference(NoteReferenceStr);
            }
            

            //TASK Create a DataNodesNoteReferenceString and save it to the DataNode's Reference File
            ProcessKeyWords(NoteReferenceStr);

            //Create a DataNodesNoteReferenceString
            
            string NoteName = StringHelper.ReturnItemAtPos(NoteReferenceStr, '^', 0);
            DataNodesNoteReferenceString = NoteName + '^' + CommonStaticMembers.CurrentNote26Name + "\r\n";
          

            //Get the DataNode's name
            //string DataNodeNoteReferenceFilePath = SubjectStaticMembers.GetDataNodeNoteReferenceFilePath();
            //Append this to the DataNodes DataNodesReferenceFile
           
            if (File.Exists(CommonStaticMembers.DataNodesNoteReferencesFilesPath))
            {
                File.AppendAllText(CommonStaticMembers.DataNodesNoteReferencesFilesPath, DataNodesNoteReferenceString);
            }
            else
            {
                // Create a new DataNodesReferenceFile
                FileStream fs = File.Create(CommonStaticMembers.DataNodesNoteReferencesFilesPath);
                fs.Close();
                File.AppendAllText(CommonStaticMembers.DataNodesNoteReferencesFilesPath, DataNodesNoteReferenceString);
            }


        }// End SaveAndUpdateNoteReferenceAndKeywords(


        #endregion SaveAndUpdateNoteReferenceAndKeywords()


        #region ProcessKeyWords
        /// <summary>
        /// This privagte method receives the NoteReferenceStr
        /// Determines new key words and updated all the new
        /// and any old keyWord values that do not already
        /// contain the Reference Note's CurrentNote26Name
        /// </summary>
        /// <param name="NoteReferenceStr"></param>
        private static void ProcessKeyWords(string NoteReferenceStr)
        {
            // Make a copy of the KeyWordDictionary
            Dictionary<string, string> thisKeyWordDictionary = KeyWordsStaticMembers.KeyWordsDictionary;


            //  Get the delimited string of key words
            string delimitedStringOfKeyWords = StringHelper.ReturnItemAtPos(NoteReferenceStr, '^', 3);
            //  Remove the terminal ';'
            delimitedStringOfKeyWords = delimitedStringOfKeyWords.Substring(0, delimitedStringOfKeyWords.Length - 1);

            // Create an array of KeyWords in this Note
            string[] KeyWordsArray = delimitedStringOfKeyWords.Split(';');

            // Eliminate all generics from the KeyWordsArray
            List<string> RealKeyWords = new List<string>();
            foreach (string keyWord in KeyWordsArray)
            {
                if (keyWord.IndexOf('#') == -1)
                {
                    RealKeyWords.Add(keyWord);
                }
            }

                // Cycle Through this array
                foreach (string keyWord in RealKeyWords)
            {
               

                // Replace all spaces with '_'
                string thisKeyWord = keyWord.Replace(' ', '_');

                // Determine if this keyWord is already in the dictionary
                if (thisKeyWordDictionary.ContainsKey(thisKeyWord))
                {
                    // determine if the value contains the current CommonStaticMembers.CurrentNote26Name
                    string CurrentNote26Name = CommonStaticMembers.CurrentNote26Name;
                    string KeyWordValue = thisKeyWordDictionary[thisKeyWord];
                    string searchTerm = ';' + CurrentNote26Name + ';';
                    // Determine if this NoteReference26Name has already been linked the this Key word
                    int posOfCurrentNote26Name = KeyWordValue.IndexOf(searchTerm);
                    if (posOfCurrentNote26Name == -1)
                    {
                        // add this CurrentNote26Name to the value
                        KeyWordValue = KeyWordValue + CurrentNote26Name + ';';

                        // return this updated value to the dictionary
                        thisKeyWordDictionary[thisKeyWord] = KeyWordValue;

                    }
                }// End this key word was already in the dictionary
                else
                {
                    // this is a new KeyWord
                    thisKeyWordDictionary.Add(thisKeyWord, ';' + CurrentNote26Name + ';');
                }

            }// End Cycle Through this array of KeyWords

            // Updated the  KeyWordDictionary
            KeyWordsStaticMembers.KeyWordsDictionary = thisKeyWordDictionary;

            // Save the KeyWordDictionary

            KeyWordsStaticMembers.SaveDictionary();

        }
        #endregion ProcessKeyWords


        #endregion  Public Methods

        #region Private Methods

        #region SaveNoteReference

        private static void SaveNoteReference(string NoteReferenceStr)
        {
            if (CommonStaticMembers.CurrentNote26Name == "")
            {
                //  Get the next available CommonStaticMembers.CurrentNote26Name
                int fCount = Directory.GetFiles(CommonStaticMembers.NoteReferencesPath, "*", SearchOption.TopDirectoryOnly).Length;

                // Convert fCount to a base 26 character string(ie containing only ABC..Z)s
                CommonStaticMembers.CurrentNote26Name = ConvertToBase26(fCount);
            }

            // Use it to create the name for the next NoteReference.txt file
            string ReferenceName = CommonStaticMembers.CurrentNote26Name + ".txt";

            //Construct path to this new reference file
            string ReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + ReferenceName;

            //Write the NoteReferenceStr to this file
            File.WriteAllText(ReferenceFilePath, NoteReferenceStr);


        }// End SaveNoteReference

        #endregion SaveNoteReference



        #region ConvertToBase26
        public static string ConvertToBase26(int fCount)
        {
            // create a string of all of the Capitals
            string AlphaCapsString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

           // create the string to return fCount converted into a ABC...Z number where A=0 ad Z=25
            string N26 = "";
            var C = fCount;
            string L = "";
            int D = 0;
            int M = 0;
            int R = 0;
            // Determine how many characters this N26 number will have
            if (C / 26 == 0)
            {
                N26 = AlphaCapsString.Substring(C, 1);
            }
            else if (C / (26*26) == 0)
            {
                D = C / 26;
                N26 = AlphaCapsString.Substring(D, 1);
                M = C % 26;
                N26 = N26 + AlphaCapsString.Substring(M, 1);
            }
            else if (C/ (26*26*26) == 0)
            {
                D = C / (26 * 26);
                N26 = AlphaCapsString.Substring(D, 1);
                C = C % (26 * 26); 
                D = C / 26;
                N26 = N26 + AlphaCapsString.Substring(D, 1);
                M = C % 26;
                N26 = N26 + AlphaCapsString.Substring(M, 1);

            }
           


            return N26;


        }
        #endregion ConvertToBase26

        #region UpdateKeyWordDictionary

        /// <summary>
        /// This Method receives a list of KeyWords, ending in a ';' 
        /// and it appends the CurrentNote26Name to the end of each value in the KeyWord dictionary
        /// </summary>
        /// <param name="delimitedKeyWordString"></param>
        private static void UpdateKeyWordDictionary(string delimitedKeyWordString)
        {

            //  remove the terminal ';' from the NoteReferenceStr
            string shortenedDelimitedKeyWordString = delimitedKeyWordString.Substring(0, delimitedKeyWordString.Length - 1);

            //  Create a string [] from the shortenedDelimitedKeyWordString 
            string[] KeyWords = shortenedDelimitedKeyWordString.Split(';');

           

            //  Cycle through the Key words updating the Dictionary

            foreach (string keyWord in KeyWords)
            {
                // Eliminate all generics
                if (keyWord.IndexOf('#') == -1)
                {
                    // replace spaces in keyWord
                    string newKeyWord = keyWord.Replace(' ', '_');

                    // get the value for the KeyWordDictionary item with newKeyWord as the Key
                    string delimitedNoteNamesString = KeyWordsStaticMembers.delimitedStringOfNoteNames(newKeyWord);

                    // append CurrentNote26Name to the end of delimitedNoteNamesString
                    delimitedNoteNamesString = delimitedNoteNamesString + CurrentNote26Name + ';';

                    //Return the updated value to the dictionary
                    KeyWordsStaticMembers.ChangeDictionaryValue(newKeyWord, delimitedNoteNamesString);
                }

                //// Save the Dictionary
                //KeyWordsStaticMembers.SaveDictionary();


            }// End for each keyWord in KewWords


        }// End UpdateKeyWordDictionary()

        #endregion UpdateKeyWordDictionary

        #endregion Private Methods
    }// End LinkNoteStaticMembers

}// End namespace NewLSP.StaticHelperClasses

using System.Collections.Generic;
using System.IO;
using NewLSP.DataModels;

namespace NewLSP.StaticHelperClasses
{
    public static class LinkNoteStaticMembers
    {

        #region Properties and fields


        #region Hyperlink

        private static string _Hyperlink;

        public static string Hyperlink
        {
            get { return _Hyperlink; }
            set { _Hyperlink = value; }
        }

        #endregion Hyperlink


        #region FileType
        private static string _FileType;

        public static string FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }



        #endregion FileType

        #region Pulbic Bool OnStartUp

        public static bool OnStartUp = true;

        //private static bool  _OnStartUp = true;

        //public static bool OnStartUp
        //{
        //    get { return _OnStartUp ; }
        //    set { _OnStartUp = value; }
        //}

        #endregion Pulbic Bool OnStartUp


        #region BookMarks

        private static List<string> _BookMarks;

        public static List<string> BookMarks
        {
            get { return _BookMarks; }
            set { _BookMarks = value; }
        }




        #endregion  BookMarks

        #region HyperlinkDictionary


        /// <summary>
        /// This Dictionary uses the item's position in the lbxLinks ListBox
        /// as the Key and a HyperlinkObject as the value
        /// The HyperlinkObject contains: 1) the BookMark, 2) the Name,
        /// 3) the Url and 4) the FileType
        /// </summary>
        public static Dictionary<int, LinkNoteModel.HyperlinkObject> HyperlinkDictionary = new Dictionary<int, LinkNoteModel.HyperlinkObject>();

        #endregion HyperlinkDictionary

        #region HyperlinkUrls

        public static List<string> HyperlinkUrls = new List<string>();

        #endregion HyperlinkUrls


        #region HyperlinkStringsList

        public static List<string> HyperlinkStringsList = new List<string>();
        #endregion HyperlinkStringsList

        #region ListOfNoteNames

        private static List<string> _ListOfNoteNames;

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

        #region ListOfNoteHyperlinks

        private static List<string> _ListOfNoteHyperlinks;

        public static List<string> ListOfNoteHyperlinks
        {
            get { return _ListOfNoteHyperlinks; }
            set { _ListOfNoteHyperlinks = value; }
        }


        #endregion ListOfNoteHyperlinks


        #region ListOfNoteBookMarks

        private static List<string> _ListOfNoteBookMarks;

        public static List<string> ListOfNoteBookMarks
        {
            get { return _ListOfNoteBookMarks; }
            set { _ListOfNoteBookMarks = value; }
        }


        #endregion ListOfNoteBookMarks

        #region ListOfNoteKeyWords

        private static List<string> _ListOfNoteKeyWords;

        public static List<string> ListOfNoteKeyWords
        {
            get { return _ListOfNoteKeyWords; }
            set { _ListOfNoteKeyWords = value; }
        }


        #endregion ListOfNoteKeyWords

        #region CurrentNote26Name
        /// <summary>
        /// This is the name of the currently active CommonReferences NoteReferenceFile
        /// is is created by taking the current number of files in the
        /// NoteReferenceFile folder  and converting it to a base 26 integer which 
        /// uses all Alpha Characters from A to Z where A = 0 and Z= 25 and AA = 26 etc
        /// </summary>
        public static string CurrentNote26Name;

        #endregion CurrentNote26Name

        #region ListBoxOfSelectedNotesList
        private static List<string> _ListBoxOfSelectedNotesList;

        public static List<string> ListBoxOfSelectedNotesList
        {
            get { return _ListBoxOfSelectedNotesList; }
            set { _ListBoxOfSelectedNotesList = value; }
        }


        #endregion ListBoxOfSelectedNotesList

        #region NewDataNodesNoteReferenceFileName

        private static string _NewDataNodesNoteReferenceFileName;

        public static string NewDataNodesNoteReferenceFileName
        {
            get { return _NewDataNodesNoteReferenceFileName; }
            set { _NewDataNodesNoteReferenceFileName = value; }
        }


        #endregion NewDataNodesNoteReferenceFileName

        #endregion Properties



        #region Public Methods

        #region AddHyperlinkToList


        /// <summary>
        /// Adds a delimites string containing the elements of
        /// a hyperlink object to the list<string></string> HyperlinkStringsList
        /// Called by LinkNote.xaml.cs miSaveHyperlink_Clic
        /// </summary>
        /// <param name="delimitedHyperlink"></param>
        public static void AddHyperlinkToList(string delimitedHyperlink)
        {
            HyperlinkStringsList.Add(delimitedHyperlink);
        }


        #endregion AddHyperlinkToList


        #region AddItemToHyperlinkDictionary

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


        public static void ReadInNotesFile()
        {
            //Create path to the DataNodeIDs notes file
            string IDFileName = SubjectStaticMembers.DataNode.ID.ToString();
            CommonStaticMembers.CurrentNoteIDInt = SubjectStaticMembers.DataNode.ID;
            string DataNodesNotesPath = CommonStaticMembers.DataNodesNoteReferencesFilesPath  + IDFileName + ".txt";

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


        #region SaveNoteReference()
        /// <summary>
        /// This Method saves a note reference called Clicking the miSaveNote menu item
        /// There a two tasks this method must perfor
        ///     1.  It must save or update the NoteReference to the CommonReferences folder
        ///         To determine whether to create a new NoteReference it first examines 
        ///         the global CommonStaticMembers.CurrentNote26Name formerly (CommonStaticMembers.CurrentNoteIDInt)
        ///         a.  if the CommonStaticMembers.CurrentNote26Name = "" then a new NoteReference file needs to 
        ///             be creates
        ///         b.  Else, this is an existing note and only the KeyWords field needs to be updated
        ///     2.  It must create a note references link link for the DataNode's DataNodesNoteReferencesFiles
        ///         This line is composed of the NoteName plus enough spaced to make its length = 250
        ///         +^+The NoteReferences' C26 name
        ///  If CommonStaticMembers.CurrentNoteIDInt != -=1 then use CommonStaticMembers.CurrentNoteIDInt to save the note
        ///  The NoteReferenceStr contains the NoteName^Hyperlink^BookMark^';' delimited Keywords
        /// </summary>
        public static void SaveNoteReference(string NoteReferenceStr)
        {
            //  TASK1 Save or Update the CommonReferences folder'a NoteReference

            //      Determine if this is a new or an existing NoteReference file
            if(CommonStaticMembers.CurrentNote26Name == "")
            {
                // This is a NEW NoteReference file so save the entire NoteReferenceStr
                //  Get then next available CommonStaticMembers.CurrentNote26Name
                int fCount = Directory.GetFiles(CommonStaticMembers.NoteReferencesPath, "*", SearchOption.TopDirectoryOnly).Length;

                // Convert fCount to a base 26 character string(ie containing only ABC..Z)s
                CurrentNote26Name = ConvertToBase26(fCount);

               // Use it to create the name for the next NoteReference.txt file
                string ReferenceName = CurrentNote26Name + ".txt";

                // Save this new CurrentNote26Name
                CommonStaticMembers.CurrentNote26Name = CurrentNote26Name;

                //Construct path to this new reference file
                string ReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + ReferenceName;

                //Write the NoteReferenceStr to this file
                File.WriteAllText(ReferenceFilePath, NoteReferenceStr);

                // TOTO - 20210609 Make sure to include a call to a method to update the keywords dictionary here

                // Update the KeyWordsDictionary
                //  1.  Get the delimited Keywors string
                string delimitedKeyWordString = StringHelper.ReturnItemAtPos(NoteReferenceStr, '^', 3);
                UpdateKeyWordDictionary(delimitedKeyWordString);

            }
            else
            {
                // This is an existing NoteReference file so only update its KeyWords

                // Get the current text is this NoteReference file
                // Create the path to the current Common References's NoteReferenceFile file
                string NoteReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + CommonStaticMembers.CurrentNote26Name + ".txt";
                if (File.Exists(NoteReferenceFilePath))
                {
                    //Read in the text
                    string CurrentDelimitedNoteReferenceString = File.ReadAllText(NoteReferenceFilePath);

                    // split the string on '^' into a string array
                    string[] ReferenceComponentsArray = CurrentDelimitedNoteReferenceString.Split('^');

                    // Get the OldDelimitedKeyWordsString
                    string OldDelimitedKeyWordsString = ReferenceComponentsArray[3];
                  

                    //Get the updated string of KeyWords from NoteReferenceStr
                    string NewDelimitedKeyWordsString = StringHelper.ReturnItemAtPos(NoteReferenceStr,'^', 3);

                    // Create an array from  NewDelimitedKeyWordsString
                    //  1.  Delete the terminal ';' from the list
                    NewDelimitedKeyWordsString = NewDelimitedKeyWordsString.Substring(0, NewDelimitedKeyWordsString.Length - 1);
                    //  2.  Create an array from this shortened string
                    string[] NewDelimitedKeyWordsArray = NewDelimitedKeyWordsString.Split(';');


                    //Extract new KeyWords from the currend list of KeyWords
                    //  1.  Create a delimited string of NewKeyWordSw to hold them
                    string NewKeyWordsString = "";

                    //  2.  Cycle thru NewDelimitedKeyWordsString extracting KeyWords not found in OldDelimitedKeyWordsString
                    foreach(string keyWord in NewDelimitedKeyWordsArray)
                    {
                        if(OldDelimitedKeyWordsString.IndexOf(keyWord+';') == -1)
                        {
                            NewKeyWordsString = NewKeyWordsString + keyWord + ';';
                        }
                    }

                    // Replace the current key words with the updated list of key words
                    ReferenceComponentsArray[3] = NewDelimitedKeyWordsString +';';

                    //Reassemble the NoteReferenceFile stirng
                    string NoteReferenceFileString = ReferenceComponentsArray[0] + '^' + ReferenceComponentsArray[1] +
                        '^' + ReferenceComponentsArray[2] + '^' + ReferenceComponentsArray[3];

                    //Write this update string to the NoteReferences file
                    File.WriteAllText(NoteReferenceFilePath, NoteReferenceFileString);

                    // Send NewKeyWordsString to UpdateKeyWordDictionary();
                    UpdateKeyWordDictionary(NewKeyWordsString);

                    return;
                }
            }// End TASK 1

            // TASK 2 create a new line for the DataNode's DataNodesNoteReferencesFiles showing the NoteName^CurrentNote26Name

            // Get the NoteFileName
            string NoteFileName = StringHelper.ReturnItemAtPos(NoteReferenceStr, '^', 0);
             
            // Get the CommonReferences CommonStaticMembers.CurrentNote26Name I MAY NOT NEED THIS
            CurrentNote26Name = CommonStaticMembers.CurrentNote26Name;

            // Create the string to add to the DataNode's DataNodesReferenceFile
            NewDataNodesNoteReferenceFileName = NoteFileName + '^' + CurrentNote26Name+"\r\n";

            //Get the DataNode's name
            string DataNodeNoteReferenceFilePath = SubjectStaticMembers.GetDataNodeNoteReferenceFilePath();
            //Append this to the DataNodes DataNodesReferenceFile
           
            if (File.Exists(DataNodeNoteReferenceFilePath))
            {
                File.AppendAllText(DataNodeNoteReferenceFilePath, NewDataNodesNoteReferenceFileName);
            }
            else
            {
                // Create a new DataNodesReferenceFile
                FileStream fs = File.Create(DataNodeNoteReferenceFilePath);
                fs.Close();
                File.AppendAllText(DataNodeNoteReferenceFilePath, NewDataNodesNoteReferenceFileName);
            }



            // //SAVE THE Note NumberChars Name to the Dictionary file of all of its keywords

            // //  remove the terminal ';' from the NoteReferenceStr
            // string shortenedNoteReferenceStr = NoteReferenceStr.Substring(0, NoteReferenceStr.Length - 1);

            // //  Create a string [] from the hyperlinks 
            // string[] KeyWords = StringHelper.ReturnItemAtPos(shortenedNoteReferenceStr, '^', 3).Split(';');

            // // Get the Note name so it can be displayes in the  lbxOpenSelectedNote list box
            // string NewNoteName = StringHelper.ReturnItemAtPos(shortenedNoteReferenceStr, '^', 0);

            // //  Cycle through the Key words updating the Dictionary

            // foreach (string keyWord in KeyWords)
            // {
            //     // Eliminate all generics
            //     if (keyWord.IndexOf('#') != -1) break;

            //     // replace spaces in keyWord
            //     string newKeyWord = keyWord.Replace(' ', '_');

            //     // get the value for the Dictionary item with newKeyWord as the Key
            //     string delimitedNoteNamesString = KeyWordsStaticMembers.delimitedStringOfNoteNames(newKeyWord);

            //     // append CurrentNote26Name to the end of delimitedNoteNamesString
            //     delimitedNoteNamesString = delimitedNoteNamesString + CurrentNote26Name + ';';

            //     //Return the updated value to the dictionary
            //     KeyWordsStaticMembers.ChangeDictionaryValue(newKeyWord, delimitedNoteNamesString);


            // }// End for each keyWord in KewWords

            // // Append the new Note Name and its associated NoteAlphaChars26Name to the lbxOpenSelectedNote list box
            // // Create a blank filler string to insert between the Note name and the ^CurrentNote26Name
            // string fillerStr = new String(' ', 200);
            // string newNoteReferenceStr = NewNoteName + fillerStr + '^' + CurrentNote26Name;


        }// End SaveNoteReference(


        #endregion SaveNoteReference()

        #endregion  Public Methods

        #region Private Methods


        #region ConvertToBase26
        private static string ConvertToBase26(int fCount)
        {
            // create a string of all of the Capitals
            string AlphaCapsString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

           // create the string to return fCount converted into a ABC...Z number
            string NumBase26 = "";

            // Calculate the first interation 
            int modulus = fCount % 26;
            int dividend = fCount / 26;
            string currentModulusChar = AlphaCapsString.Substring(modulus, 1);
            NumBase26 = currentModulusChar;

            //repeat until the divident is = 0
            while(dividend != 0)
            {
                modulus = fCount % 26;
                dividend = fCount / 26;
                currentModulusChar = AlphaCapsString.Substring(modulus, 1);
                NumBase26 = NumBase26 + currentModulusChar;
            }


            return NumBase26;


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

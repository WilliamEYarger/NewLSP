using System;
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
            string DataNodesNotesPath = CommonStaticMembers.DataNodesNotesPath + "\\" + IDFileName + ".txt";

            // create a string array of all lines in the DataNodeReferences file
            string[] lines = System.IO.File.ReadAllLines(DataNodesNotesPath);

            // Crear and instantiate all the required lists
            ListOfNoteNames = new List<string>();
            ListOfNoteHyperlinks = new List<string>();
            ListOfNoteBookMarks = new List<string>();
            ListOfNoteKeyWords = new List<string>();

            foreach(string line in lines)
            {
                // create an array of the note components
                string[] NoteComponents = line.Split('^');
             
                ListOfNoteNames.Add(NoteComponents[0]);
                ListOfNoteHyperlinks.Add(NoteComponents[1]);
                ListOfNoteBookMarks.Add(NoteComponents[2]);
                ListOfNoteKeyWords.Add(NoteComponents[3]);

            }

        }

        #endregion ReadInNotesFile


        #region SaveNoteReference()
        /// <summary>
        /// This Method saves a note reference and can be called by either
        ///     1)  Clicking the miSaveNote menu item or by 
        ///     2)  clicking the KeyWordControl's btnSubmit 
        /// </summary>
        public static void SaveNoteReference(string NoteReferenceStr)
        {
           // Find out how many files are in the C:\Users\Owner\OneDrive\Documents\Learning\Religion\ReligionReferences\NoteReferenceFiles folder
            int fCount = Directory.GetFiles(CommonStaticMembers.NoteReferencesPath, "*", SearchOption.TopDirectoryOnly).Length;


            //Use it to create the string NoteNumberCharsName
            string NoteNumberCharsName = fCount.ToString();
            // Use it to create the name for the next NoteReference.txt file
            string ReferenceName = NoteNumberCharsName + ".txt";

            string ReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + ReferenceName;

            

            //Write the NoteReferenceStr to this file
            File.WriteAllText(ReferenceFilePath, NoteReferenceStr);

            // Crete the path to store this DataNodes's Note           
            string NotesFilePath = CommonStaticMembers.HomeFolderPath + "Notes\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

            // Check to see if the SubjectName\Notes folder contains a file whose name is the DataNodeID.txt
            if (File.Exists(NotesFilePath))
            {
                // The file already exists so append the new note reference to it
                File.AppendAllText(NotesFilePath, NoteReferenceStr+"\r\n");
            }
            else
            {
                // The file doesn't exist so write the note reference to a new file
                File.WriteAllText(NotesFilePath, NoteReferenceStr + "\r\n");
            }

            //SAVE THE Note NumberChars Name to the Dictionary file of all of its keywords

            //  remove the terminal ';' from the NoteReferenceStr
            string shortenedNoteReferenceStr = NoteReferenceStr.Substring(0, NoteReferenceStr.Length - 1);

            //  Create a string [] from the hyperlinks 
            string[] KeyWords = StringHelper.ReturnItemAtPos(shortenedNoteReferenceStr, '^', 3).Split(';');

            //  Cycle through the Key words updating the Dictionary

            foreach(string keyWord in KeyWords)
            {
                // Eliminate all generics
                if (keyWord.IndexOf('#') != -1) break;

                // replate spaces in keyWord
                string newKeyWord = keyWord.Replace(' ', '_');

                // get the value for the Dictionary item with newKeyWord as the Key
                string delimitedNoteNamesString = KeyWordsStaticMembers.delimitedStringOfNoteNames(newKeyWord);

                // append NoteNumberCharsName to the end of delimitedNoteNamesString
                delimitedNoteNamesString = delimitedNoteNamesString + NoteNumberCharsName + ';';
                   
                //Return the updated value to the dictionary
                KeyWordsStaticMembers.ChangeDictionaryValue(newKeyWord, delimitedNoteNamesString);
                
                
            }



        }
        #endregion SaveNoteReference()

        #endregion  Public Methods

    }// End LinkNoteStaticMembers

}// End namespace NewLSP.StaticHelperClasses

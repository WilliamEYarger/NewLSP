using System;
using System.Collections.Generic;
using System.IO;


namespace NewLSP.StaticHelperClasses
{
    public static class KeyWordsStaticMembers
    {
        #region Properties


        #region KeyWordList
        private static List<string> _KeyWordList = new List<string>();
        /// <summary>
        /// This is the current list of key words in thier unconverted fors (may contain spaces)
        /// </summary>
        public static List<string> KeyWordList
        {
            get { return _KeyWordList; }
            set { _KeyWordList = value; }
        }
        #endregion KeyWordList

       
        #region SortedKeyWordList
        private static List<string> _SortedKeyWordList = new List<string>();
        /// <summary>
        /// This is the current list of key words in thier unconverted fors (may contain spaces)
        /// </summary>
        public static List<string> SortedKeyWordList
        {
            get { return _SortedKeyWordList; }
            set { _SortedKeyWordList = value; }
        }
        #endregion SortedKeyWordList
        

        #region KeyWordsDictionary

        private static Dictionary<string, string> _KeyWordsDictionary = new Dictionary<string, string>();
        /// <summary>
        /// The KeyWordDictionary uses a modified KeyWord (where all ' ' have been
        /// replaces with '_' as a Key.
        /// The Value is a ';' delimited string of NoteReferenceFile CurrentNote26Names
        /// NOTE: all CurrentNote26Names must be surrounded by ';' to allow 
        /// unique identification
        /// </summary>
        public static Dictionary<string, string> KeyWordsDictionary
        {
            get { return _KeyWordsDictionary; }
            set { _KeyWordsDictionary = value; }
        }


        //static Dictionary<string, string> KeyWordsDictionary = new Dictionary<string, string>();
        #endregion KeyWordsDictionary


        #region ListAccess boolean

        /// <summary>
        /// Indicates whether the user is in the Create note reference mode = true
        /// or Search for note references containing listed Key words = false
        /// If true, the user can have access to add new key words to the list of key words
        /// </summary>
        private static bool _ListAccess = true;

        public static bool ListAccess
        {
            get { return _ListAccess; }
            set { _ListAccess = value; }
        }
        #endregion ListAccess boolean



        #region KeyWordsDictionaryPath

        /// <summary>
        /// This is the path to the folder that holds the Keywords dictionary
        /// The Key to the KeyWords dictionary is a string of words, where all 
        /// spaces have been replaced with undersocres('_')
        /// The value of the KeyWords dictionary is a ';' delimited string
        /// of CurrentNote26Names
        private static string _KeyWordsDictionaryPath;

        public static string KeyWordsDictionaryPath
        {
            get { return _KeyWordsDictionaryPath; }
            set
            {
                _KeyWordsDictionaryPath = value;

                // Open the KeyWord Dictionary text file, split it into Key and Value and add it to the Dictioanry
                string[] KeyWordsEntriesArray = File.ReadAllLines(_KeyWordsDictionaryPath);
                foreach (string line in KeyWordsEntriesArray)
                {
                    string[] lineItems = line.Split('^');
                    KeyWordsDictionary.Add(lineItems[0], lineItems[1]);
                }

            }
        }
        #endregion KeyWordsDictionaryPath


        #region ListOfKeyWordsPath
        private static string _ListOfKeyWordsPath;

        /// <summary>
        /// This is the path to the list of all of the orriginal current KeyWords
        /// which can contain spaces
        /// </summary>
        public static string ListOfKeyWordsPath
        {
            get { return _ListOfKeyWordsPath; }
            set 
            { 
                _ListOfKeyWordsPath = value;
                //If file doesn't exist, create it
                if (!File.Exists(_ListOfKeyWordsPath))
                {
                    var fileStream = File.Create(_ListOfKeyWordsPath);
                    fileStream.Close();
                }

                // read Keywords into KeyWordsList
                string[] KeyWordsArray = File.ReadAllLines(_ListOfKeyWordsPath);
                foreach(string line in KeyWordsArray)
                {
                    KeyWordList.Add(line);
                }

                //Sort the  KeyWordsList
                SortKeyWordsList();


            }
        }


        #endregion ListOfKeyWordsPath

        #region List of Sorted KeyWords

        private static string _ListOfSortedKeyWordsPath;


        /// <summary>
        /// This is the path to the list of all of the orriginal current KeyWords
        /// which can contain spaces
        /// </summary>
        /// 

        public static string ListOfSortedKeyWordsPath
        {
            get { return _ListOfSortedKeyWordsPath; }

            set
            {
                _ListOfSortedKeyWordsPath = value;
                //If file doesn't exist, create it
                if (!File.Exists(_ListOfSortedKeyWordsPath))
                {
                    var fileStream = File.Create(_ListOfSortedKeyWordsPath);
                    fileStream.Close();
                }

                // read Keywords into KeyWordsList
                string[] SortedKeyWordsArray = File.ReadAllLines(_ListOfSortedKeyWordsPath);

                foreach (string line in SortedKeyWordsArray)
                {
                    SortedKeyWordList.Add(line);
                }
            }
           

        }
        #endregion List of Sorted KeyWords


        #endregion Properties

        #region Private Methods


        #region SortKeyWordsList
        /// <summary>
        /// This Method sorts the list of KeyWords into alphabetic order
        /// </summary>
        private static void SortKeyWordsList()
        {
            KeyWordList.Sort();
        }
        #endregion SortKeyWordsList


        #endregion  Private Methods

        #region Public Methods


        #region AppendNewKeyWord
        /// <summary>
        /// Append a new KeyWord to the list of KeyWords in File(ListOfKeyWordsPath)
        /// </summary>
        /// <param name="newKeyWord"></param>
        public static void AppendNewKeyWord(string newKeyWord)
        {
            File.AppendAllText(ListOfKeyWordsPath, newKeyWord+"\r\n");
        }
        #endregion AppendNewKeyWord

        #region Append new Sorted Key Word

        /// <summary>
        /// This method received a new KeyWord 
        /// It first reads in the keywords in the SortedListOfKeyWords.txt file
        /// It adds the new keyWord and then resorts the list
        /// it then writes the updated list to the file
        /// </summary>
        /// <param name="keyWord"></param>
        internal static void AppendNewSortedKeyWord(string keyWord)
        {
            
            string ListOfSortedKeyWordsPath = KeyWordsStaticMembers.ListOfSortedKeyWordsPath;

            // check check to see if the file exists and if so read in all lines
            if (File.Exists(ListOfSortedKeyWordsPath))
            {
                string[] ArrayOfSortedKeyWords = File.ReadAllLines(ListOfSortedKeyWordsPath);

                List<string> ListOfSortedKeyWords = new List<string>(ArrayOfSortedKeyWords);
                ListOfSortedKeyWords.Add(keyWord);

                ListOfSortedKeyWords.Sort();

                //conveert the list back to an array
                ArrayOfSortedKeyWords = ListOfSortedKeyWords.ToArray();

                File.WriteAllLines(ListOfSortedKeyWordsPath, ArrayOfSortedKeyWords);
            }

        }

        #endregion Append new Sorted Key Word


        #region AppendNewKeyWordDictionaryItemString()
        /// <summary>
        /// Receives a converted KewWord(where all ' ' have been converted to '_'
        /// and created a Dictionary string entry with
        /// Key = convertedKeyWord
        /// delimiter of '^'
        /// and value = ";"
        /// All subsequent CurrentNote26Names will be added bracked by ';' so that 
        /// every unique number char can be located in the search operation
        /// each CurrentNote26Name char referres to the name of a NoteRefeerenceFile
        /// </summary>
        /// <param name="convertedKeyWord"></param>
        public static void AppendNewKeyWordDictionaryItemString(string convertedKeyWord)
        {
            File.AppendAllText(KeyWordsDictionaryPath, convertedKeyWord + "^;\r\n");
        }
        #endregion AppendNewKeyWordDictionaryItemString()



        #region ChangeDictionaryValue()
        /// <summary>
        /// This method receives a convertedKeyWord(' ' -> '_') and
        /// a ';' delimited string of CurrentNote26Names
        /// it then replaces the old value with the new value (a new NoteReference now contains this key word)
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="delimitedNoteNamesString"></param>
        internal static void ChangeDictionaryValue(string keyWord, string delimitedNoteNamesString)
        {
            KeyWordsDictionary[keyWord] = delimitedNoteNamesString;
        }
        #endregion ChangeDictionaryValue()

        #region SaveDictionary()
        /// <summary>
        /// This method is callbe by both the 
        ///     MainWindow's miCloseApplication_Click menu item  method
        ///     LinkNoteStaticMembers ProcessKeywrods method
        /// It converts each dictionary item to string of a '^' delimited string of
        /// KeyWord key and ';' delimited CurrentNote26Name value a
        /// adds each string to a list of strings and then
        /// writes them to the KeyWordsDictionaryPath, overriding any
        /// preexisting data
        /// </summary>
        internal static void SaveDictionary()
        {
            // Create a List<string> to hold all of the dictionary lines
            List<string> KeyWordsDictionaryList = new List<string>();
            foreach (KeyValuePair<string, string> KVP in KeyWordsDictionary)
            {
                string key = KVP.Key;
                string value = KVP.Value;
                string line = key + '^' + value;
                KeyWordsDictionaryList.Add(line);

            }

            // convert the list to an array
            string[] KeyWordsDictionarArray = KeyWordsDictionaryList.ToArray();
            // Write all these lines to the dictionary file
            File.WriteAllLines(KeyWordsDictionaryPath, KeyWordsDictionarArray);

        }// End SaveDictionary
        #endregion SaveDictionary()


        #region Return delimitedStringOfNoteNames()
        /// <summary>
        /// This method receives a converted KeyWord (all spaces have been replaced c '_')
        /// key to the KeyWordDictionary and returns the associated value
        /// which is a ';' delimited list of all CurrentNote26Names
        /// </summary>
        /// <param name="NoteName"></param>
        /// <returns></returns>
        public static string delimitedStringOfNoteNames(string ConvertedKeyWord)
        {

            return KeyWordsDictionary[ConvertedKeyWord];
        }




        #endregion Return delimitedStringOfNoteNames()



        #region ReturnNoteNameList
        /// <summary>
        /// This is called when in Search mode and the text in the list of all Key words text box is changed
        /// 0.  It creats a List<string> NoteNamesList  of NoteReferenceNames-CurrentNote26Name 
        /// 1.  It converte the KeyWord to a dictionary compatable form
        /// 2.  It gets  the value associated with that key word
        /// 3.  It adjust the value removing the leading and terminel ';' and then 
        ///     splits the value on '^' to get a string [] of NoteReferenceFile CurrentNote26Name
        /// 4.  It cycles thru the array getting the text of each NoteReferenceFile with these names
        ///     getting the name and creates a display string using it and the CurrentNote26Name key and 
        ///     add it to the NoteNamesList
        /// </summary>
        /// <param name="searchKeyWord" is a single KeyWord to use as the Key to the KeyWordDictionar></param>
        /// <returns></returns>
        internal static List<string> ReturnNoteNameList(string searchKeyWord)
        {
            //In searchKeyword convert ' ' to '_'
            searchKeyWord = searchKeyWord.Replace(' ', '_');

            // Create the return List
            List<string> NoteNamesList = new List<string>();

            // Get the Value from the KeyWordsDictionary for the searchKeyWord KeyWord
            string CurrentNote26NameString = KeyWordsDictionary[searchKeyWord];

            //Remove the initial and terminal ';' from this string
            CurrentNote26NameString = CurrentNote26NameString.Substring(1);
            CurrentNote26NameString = CurrentNote26NameString.Substring(0, CurrentNote26NameString.Length - 1);

            // Create a string [] from CurrentNote26NameString 
            string[] currentNote26NamesArray = CurrentNote26NameString.Split(';');
            string CommonNoteReferenceFilePath = CommonStaticMembers.NoteReferencesPath;
            string NoteReferenceFileText = "";
            foreach (string currentNote26Name in currentNote26NamesArray)
            {
                // get the value associated with this currentNote26Name
               if(File.Exists(CommonNoteReferenceFilePath+"\\"+ currentNote26Name + ".txt"))
                {
                    NoteReferenceFileText = File.ReadAllText(CommonNoteReferenceFilePath + "\\"+currentNote26Name + ".txt");
                }

                // Get the NoteName
                string NoteName = StringHelper.ReturnItemAtPos(NoteReferenceFileText, '^', 0);
                int lengthOfSpaces = 250 - NoteName.Length;
                string spacesStr = new string(' ', lengthOfSpaces);
                string DisplayString = NoteName + spacesStr + '^' + currentNote26Name;
                NoteNamesList.Add(DisplayString);
            }

            // return NoteNamesList
            return NoteNamesList;
        }// End ReturnNoteNameList(


        #endregion ReturnNoteNameList


        #endregion  Public Methods

    }// End KeyWordsStaticMembers class
}

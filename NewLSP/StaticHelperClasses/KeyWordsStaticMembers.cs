using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewLSP.StaticHelperClasses
{
    public static class KeyWordsStaticMembers
    {
        #region Properties


        #region KeyWordList
        private static List<string> _KeyWordList = new List<string>();
        /// <summary>
        /// This is the current list of key words
        /// </summary>
        public static List<string> KeyWordList
        {
            get { return _KeyWordList; }
            set { _KeyWordList = value; }
        }
        #endregion KeyWordList

        #region KeyWordsDictionary

        private static Dictionary<string, string> _KeyWordsDictionary = new Dictionary<string, string>();
        /// <summary>
        /// The KeyWordDictionary uses a modified KeyWord (where all ' ' have been
        /// replaces with '_' as a Key.
        /// The Value is 
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
        /// The value of the KeyWords dictionary is a '^' delimited string
        /// of number characters. each Number char (i.e. ^'3'^'25'^ 
        /// is the name of a \NoteReferenceFiles which contains
        /// A Numeric character string which references a NoteReferenceFiles .txt file
        ///     i.e. 25.txt = Docetism_Wikipeida^25
        /// ^
        /// ^Docetism;#Definition;Ignatius of Antioch;1 John;
        /// 1) A Numeric character string which references (25 meant that the NoteReferenceFiles has a file names 25.txt)
        /// 2) a NoteName (Docetism-Wikipeida)
        /// 3) A hyperlink to the data (https://en.wikipedia.org/wiki/Docetism)
        /// 4) A book mark (blank here)
        /// 5) a ';' delimited list of Key words that apply to this reference (Docetism;#Definition;Ignatius of Antioch;1 John;
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
        /// This is the path to the list of all of the current KeyWords
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


        #region SelectedNoteReferenceValuesDictionary
        // Create the Dictionary
        public static Dictionary<int, string> SelectedNoteReferenceValuesDictionary = new Dictionary<int, string>();
        #endregion SelectedNoteReferenceValuesDictionary


        #region Property DictionaryOfNoteRefSPosAndNoteIDs

        private static Dictionary<int,int> _DictionaryOfNoteRefSPosAndNoteIDs = new Dictionary<int, int>();

        public static Dictionary<int,int> DictionaryOfNoteRefSPosAndNoteID
        {
            get { return _DictionaryOfNoteRefSPosAndNoteIDs ; }
            set { _DictionaryOfNoteRefSPosAndNoteIDs  = value; }
        }


        #endregion  DictionaryOfNoteRefSPosAndNoteIDs

        #endregion Properties

        #region Private Methods


        #region SortKeyWordsList
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


        #region AppendNewKeyWordDictionaryItemString()
        /// <summary>
        /// Receives a converted KewWord(where all ' ' have been converted to '_'
        /// and created a Dictionary string entry with
        /// Key = convertedKeyWord
        /// delimiter of '^'
        /// and value = ";"
        /// All subsequent number characters will be added bracked by ';' so that 
        /// every unique number char can be located in the search operation
        /// each number char referres to the name of a NoteRefeerenceFile
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
        /// a ';' delimited string of NoteAlphaChars26Name s
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
        internal static void SaveDictionary()
        {
            // Create a List<string> to hold all of the dictionary lines
            List<string> KeyWordsDictionaryList = new List<string>();
            foreach (KeyValuePair<string, string> KVP in KeyWordsDictionary)
            {
                string key = KVP.Key;
                if (key.IndexOf('#') != -1) break;
                string value = KVP.Value;
                string line = key + '^' + value;
                KeyWordsDictionaryList.Add(line);

            }

            // convert the list to an array
            string[] KeyWordsDictionarArray = KeyWordsDictionaryList.ToArray();
            // Write all these lines to the dictionary file
            File.WriteAllLines(KeyWordsDictionaryPath, KeyWordsDictionarArray);

        }
        #endregion SaveDictionary()


        #region Return delimitedStringOfNoteNames()
        /// <summary>
        /// This method receives a converted KeyWord (all spaces have been replaced c '_'
        /// and it returns all of the ';' delimited
        /// </summary>
        /// <param name="NoteName"></param>
        /// <returns></returns>
        public static string delimitedStringOfNoteNames(string ConvertedKeyWord)
        {

            return KeyWordsDictionary[ConvertedKeyWord];
        }




        #endregion Return delimitedStringOfNoteNames()



        #region ProcessKeyWordsList pubic method
        /// <summary>
        /// This method processes all keysords chosen in the search mode
        /// the text parameter 
        /// </summary>
        /// <param name="delimitedKeyWords" is the  ';' delimited list of key words></param>
        internal static void ProcessKeyWordsList(string delimitedKeyWords)
        {
            // ie Paul the Apostle;Simon Peter;Jerusalem Council;James the Just;
            // Create a string array from the delimited list of KWs
            string[] KeywordsArray = delimitedKeyWords.Split(';');

            // Create a Dictionary<int,string> from this array
            Dictionary<int, string> KeyWordsDictionary = new Dictionary<int, string>();
            //The array contains a blank termnal entry because of the concludinng ';' so account for that
            for (int i = 0; i < KeywordsArray.Length-1; i++)
            {
                KeyWordsDictionary.Add(i, KeywordsArray[i]);
            }

            // Create a delimited list of ReferenceNoteIDNames to hold references which
            string StringListOfCommonReferenceNoteIDs = "";

            //cycle through all dictionary keys makeing binary comparisons of all
            int last = KeyWordsDictionary.Count;
            int first = 0;
            while (first < last)
            {
                // compare the current first to all remaining items
                for(int i = first+1; i<last; i++)
                {
                    //Get the keywords for the current first and i
                    string firstKeyWord = KeyWordsDictionary[first];
                    string testKeyWord = KeyWordsDictionary[i];

                    // string the leading and trainling ';' before creating a string[] from each
                    firstKeyWord = firstKeyWord.Substring(0, firstKeyWord.Length - 1);
                    testKeyWord = testKeyWord.Substring(0, testKeyWord.Length - 1);
                    firstKeyWord = firstKeyWord.Substring(1);
                    testKeyWord = testKeyWord.Substring(1);
                    string[] firstKeyWordArray = firstKeyWord.Split(';');
                    string [] testKeyWordArray = testKeyWord.Split(';');

                    //Get the intersection of these two arrays
                    var commonElements = firstKeyWordArray.Intersect(testKeyWordArray);

                    // Add any of these commonElements into StringListOfCommonReferenceNoteIDs


                    // get the delimited string of ReferenceFileNumbers from KeyWordsDictionary

                    // TODO - 2021 05 21 clarify the logic to get only references which contain ALL key words
                }

                first++;
            }

        }//End ProcessKeyWordsList


        #endregion ProcessKeyWordsList pubic method


        #region ReturnNoteNameList
        /// <summary>
        /// 
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
        }


        /// <summary>
        /// This method is called when the user right clicks a NoteName in the lbxOpenSelectedNote
        /// 
        /// </summary>
        /// <param name="noteSelectedIndex" = the Key to the SelectedNoteReferenceValuesDictionary></param>
        /// <returns></returns>
        internal static string ReturnAssociatedKeyWords(int DictionaryKey)
        {           

            // Get the value in the SelectedNoteReferenceValuesDictionary assoicated with DictionaryKey
            var Value = SelectedNoteReferenceValuesDictionary[DictionaryKey];

            var AssociatedKeyWordsString = StringHelper.ReturnItemAtPos(Value, '^',3);

            // return the list
            return AssociatedKeyWordsString;
        }

        #endregion ReturnNoteNameList


        #region GetValueAssociatedWithSelectedNote


        /// <summary>
        /// This method is called when the user left clicks a
        /// Note name in the lbxOpenSelectedNote
        /// It supplies the value of all of the parameters for this note
        /// </summary>
        /// <param name="noteSelectedIndex"></param>
        /// <returns></returns>
        internal static string GetValueAssociatedWithSelectedNote(int noteSelectedIndex)
        {
            
            return SelectedNoteReferenceValuesDictionary[noteSelectedIndex];
        }



        #endregion GetValueAssociatedWithSelectedNote


        #region GetSelectedNoteIDInt Method
        internal static int GetSelectedNoteIDInt(int noteSelectedIndex)
        {
            int SelectedNoteIDInt = DictionaryOfNoteRefSPosAndNoteID[noteSelectedIndex];
            return SelectedNoteIDInt;
        }
        #endregion GetSelectedNoteIDInt Method



        #endregion  Public Methods

    }// End KeyWordsStaticMembers class
}

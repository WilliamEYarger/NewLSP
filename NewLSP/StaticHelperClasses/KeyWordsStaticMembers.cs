using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLSP.StaticHelperClasses
{
    public static class KeyWordsStaticMembers
    {
        #region Properties


        #region KeyWordList
        private static List<string> _KeyWordList = new List<string>();

        public static List<string> KeyWordList
        {
            get { return _KeyWordList; }
            set { _KeyWordList = value; }
        }
        #endregion KeyWordList

        #region KeyWordsDictionary

        private static Dictionary<string, string> _KeyWordsDictionary = new Dictionary<string, string>();

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
        /// 25
        /// Docetism-Wikipeida^
        /// https://en.wikipedia.org/wiki/Docetism
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

        public static string delimitedStringOfNoteNames(string NoteName)
        {

            return KeyWordsDictionary[NoteName];
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

        #endregion  Public Methods

    }// End KeyWordsStaticMembers class
}

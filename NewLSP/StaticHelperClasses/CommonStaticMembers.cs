
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewLSP.StaticHelperClasses
{
    public static class CommonStaticMembers
    {
        #region Properties

        #region Paths


        #region NoteReferencesPath


        /// <summary>
        /// Holds the path the the DataNode's Note file
        /// </summary>
        private static string _NoteReferencesPath;
        /// <summary>
        /// This is the pathe to the NoteReferencdesFiles in the 
        /// Common References folder
        /// </summary>
        public static string NoteReferencesPath
        {
            get { return _NoteReferencesPath; }
            set { _NoteReferencesPath = value; }
        }


        #endregion NoteReferencesPath


        #region  HomeFolderPath;

        private static string _HomeFolderPath;

        public static string HomeFolderPath
        {
            get { return _HomeFolderPath; }
            set { _HomeFolderPath = value; }
        }

        #endregion HomeFolderPath;

        #region SubjectFolderPath
        private static string _SubjectFolderPath;
        /// <summary>
        /// This is the path to the Subject folder
        /// selected by the User in response to the opening
        /// message to select or create a main Subject folder
        /// </summary>
        public static string SubjectFolderPath
        {
            get { return _SubjectFolderPath; }
            set { _SubjectFolderPath = value; }
        }


        #endregion SubjectFolderPath

        #region DataNodesHyperlinksPath

        private static string _DataNodesHyperlinksPath;
        /// <summary>
        /// This is the path to the folder that holds hyperlinks for
        /// all DataNodes that have hyperlinks attached to them
        /// </summary>
        public static string DataNodesHyperlinksPath
        {
            get { return _DataNodesHyperlinksPath; }
            set { _DataNodesHyperlinksPath = value; }
        }


        #endregion DataNodesHyperlinksPath

        #region DataNodesNotesPath

        private static string _DataNodesNotesPath;
        /// <summary>
        /// This is the path to the folder that holds the 
        /// NoteRefereces that have been assigned to any DataNode
        /// Each file is names from the designated DataNode's ID 
        /// and each line in each file contains a NoteName a '^' delimiter
        /// and a String of capital alpha characters representing the
        /// number of files in the Common References' NoteReferenceFiles
        /// converted to base 26
        /// </summary>
        public static string DataNodesNoteReferencesFilesPath
        {
            get { return _DataNodesNotesPath; }
            set { _DataNodesNotesPath = value; }
        }

        #endregion DataNodesNotesPath

        #region DataNodesQAResultsFilePath
        /// <summary>
        /// This is the path to the folder that holds the QAResults, the 
        /// results of taking a text on a QA File
        /// </summary>
        public static string DataNodesQAResultsFilePath { get; internal set; }
        #endregion DataNodesQAResultsFilePath


        #region DataNodesQAFilePath
        /// <summary>
        /// This is path to the Subject's QA files
        /// </summary>
        public static string DataNodesQAFilePath { get; internal set; }

        #endregion DataNodesQAFilePath

        #region ItemCountPath
        /// <summary>
        /// This is the path to the file that contains an integer
        /// representing the number of subject items currently
        /// assigned to a subject
        /// </summary>
        public static string ItemCountPath { get; internal set; }

        #endregion ItemCountPath

        #region KeyWordsDictionaryPath
        private static string _KeyWordsDictionaryPath;
        /// <summary>
        /// The KeyWordsDictionary contains a
        /// list of KeyWord strings (where all spaces have been replaced by '_'
        /// followed by a ^ delimiter and then a 
        /// list of ';' delimited AlphaChar25Number names of all of the files containing that key word
        /// MAKE SURE THAT EACH ALPHACHAR NAME IS SURROUNDED ON BOTH SIDES BY ';'
        /// 
        /// </summary>
        public static string KeyWordsDictionaryPath
        {
            get { return _KeyWordsDictionaryPath; }
            set 
            { 
                _KeyWordsDictionaryPath = value;
                KeyWordsStaticMembers.KeyWordsDictionaryPath = _KeyWordsDictionaryPath;
            }
        }
        #endregion KeyWordsDictionaryPath

        #endregion Paths

        #region Booleans

        #region HasNote boolean

        internal static bool NodeHasNoteFile(int nodeID)
        {

            string DataFilePath = HomeFolderPath + "DataNodesNoteReferencesFiles\\" + nodeID.ToString() + ".txt";
            if (File.Exists(DataFilePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion HasNote boolean


        #endregion Booleans

        #region CurrentNoteIDInt Property

        private static int _CurrentNoteIDInt = -1;

        /// <summary>
        /// The CurrentNoteIDInt is the name of a selectedDataNode (i.e.  "4.txt" for Jerusalem Council)
        /// converted to an int (ie 4)
        /// </summary>
        public static int CurrentNoteIDInt
        {
            get { return _CurrentNoteIDInt; }
            set { _CurrentNoteIDInt  = value; }
        }


        #endregion CurrentNoteIDInt Property



        #region DictionaryOfNoteNamesIDs Property


        private static Dictionary<int, string> _DictionaryOfNoteNamesIDs = new Dictionary<int, string>();

        public static Dictionary<int, string> DictionaryOfNoteNamesIDs
        {
            get { return _DictionaryOfNoteNamesIDs; }
            set { _DictionaryOfNoteNamesIDs = value; }
        }


        /// <summary>
        /// This string represents the CurrentNote26Name name
        /// </summary>

        private static string _CurrentNote26Name = "";

        public static string CurrentNote26Name
        {
            get { return _CurrentNote26Name; }
            set { _CurrentNote26Name  = value; }
        }


        //public static string CurrentNote26Name { get; internal set; }


        #endregion  DictionaryOfNoteNamesIDs Property


        #endregion Properties

        #region Public Methods

        #region DictionaryOfNoteNamesIDsClear



        internal static void FillDictionaryOfNoteNamesIDs()
        {
            // Clear any previous values
            DictionaryOfNoteNamesIDs.Clear();

            //

        }
        #endregion DictionaryOfNoteNamesIDsClear

        #endregion Public Methods


    }//End  class CommonStaticMembers
}// End StaticHelperClasses namespace

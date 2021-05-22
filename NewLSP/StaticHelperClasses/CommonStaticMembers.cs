using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string SubjectFolderPath
        {
            get { return _SubjectFolderPath; }
            set { _SubjectFolderPath = value; }
        }


        #endregion SubjectFolderPath

        #region DataNodesHyperlinksPath

        private static string _DataNodesHyperlinksPath;

        public static string DataNodesHyperlinksPath
        {
            get { return _DataNodesHyperlinksPath; }
            set { _DataNodesHyperlinksPath = value; }
        }


        #endregion DataNodesHyperlinksPath

        #region DataNodesNotesPath

        private static string _DataNodesNotesPath;

        public static string DataNodesNotesPath
        {
            get { return _DataNodesNotesPath; }
            set { _DataNodesNotesPath = value; }
        }

        #endregion DataNodesNotesPath

        #region DataNodesQAResultsFilePath

        public static string DataNodesQAResultsFilePath { get; internal set; }
        #endregion DataNodesQAResultsFilePath


        #region DataNodesQAFilePath

        public static string DataNodesQAFilePath { get; internal set; }

        #endregion DataNodesQAFilePath

        #region ItemCountPath

        public static string ItemCountPath { get; internal set; }

        #endregion ItemCountPath

        #region 
        private static string _KeyWordsDictionaryPath;

        public static string KeyWordsDictionaryPath
        {
            get { return _KeyWordsDictionaryPath; }
            set 
            { 
                _KeyWordsDictionaryPath = value;
                KeyWordsStaticMembers.KeyWordsDictionaryPath = _KeyWordsDictionaryPath;
            }
        }
        #endregion 

        #endregion Paths

        #region Booleans

        #region HasNote boolean

        internal static bool NodeHasNoteFile(int nodeID)
        {

            string DataFilePath = HomeFolderPath + "Notes\\" + nodeID.ToString() + ".txt";
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

        #endregion Properties


    }//End  class CommonStaticMembers
}// End StaticHelperClasses namespace

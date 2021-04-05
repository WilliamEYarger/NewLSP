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



        public static Dictionary<int, LinkNoteModel.HyperlinkObject> HyperlinkDictionary = new Dictionary<int, LinkNoteModel.HyperlinkObject>();

        #endregion HyperlinkDictionary


        public static List<string> HyperlinkUrls = new List<string>();

        public static List<string> HyperlinkStringsList = new List<string>();

        #endregion Properties



        #region Public Methods


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

        public static void AddItemToHyperlinkDictionary(int cntr, LinkNoteModel.HyperlinkObject thisHyperlinkObject)
        {
            HyperlinkDictionary.Add(cntr, thisHyperlinkObject);
        }

        #region Get Hyperlink

        public static LinkNoteModel.HyperlinkObject GetHyperlinkObject(int itemsIndex)
        {
            return (LinkNoteModel.HyperlinkObject) HyperlinkDictionary[itemsIndex];
        }


        /// <summary>
        /// Called when the LinkNote control is expanded
        /// It  reads all on the lines in the hyperlink file into an array and then
        /// for each line it:
        ///     1) Adds the delimitd line to the HyperlinkStringsList
        ///     2)  It creates the HyperlinkDictionary using its position 
        ///         in the array as the key and the component itmes as
        ///         properties of the LinkNoteModel.HyperlinkObject that is the value
        /// </summary>
        internal static void SetHyperlinkStringsList()
        {
            //read in the hyperlinks file
            string[] DataNodeHyperlinkArray = 
                File.ReadAllLines(SubjectStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt");
            

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

            


        }

        #endregion Get Hyperlink

        #endregion  Public Methods

    }// End LinkNoteStaticMembers

}// End namespace NewLSP.StaticHelperClasses

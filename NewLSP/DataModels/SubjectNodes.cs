
using System;

namespace NewLSP.DataModels
{
    public class SubjectNodes
    {


        #region Constructors

        public SubjectNodes()
        {

        }

        public SubjectNodes(int ItemCount)
        {
            ID = ItemCount;
        }

        #endregion Constructor



        #region LeadingChars

        /// <summary>
        /// This string of ' ' will be used to indicate the node's position in the
        /// hiderarchy of nodes displayes in the ListView
        /// Level 0 = ""
        /// Level 1 = "   "
        /// Level 2 = "      " 
        /// etc
        /// </summary>
        private string _LeadingChars;

        public string LeadingChars
        {
            get { return _LeadingChars; }
            set { _LeadingChars = value; }
        }
        #endregion LeadingChar

        #region ChildIndicatorString
        /// <summary>
        /// This string has only 3 possibilities
        /// 1. "- "; indicating currently there are no child nodes
        /// 2. "+ "; indicating currently there are are child nodes
        /// 3. "T "; indicating a terminal node (with no possiblity of having children)
        /// </summary>
        private string _CI = "- ";

        public string CI
        {
            get { return _CI; }
            set
            {
                if (value.Length == 2)
                {
                    _CI = value;
                }
                else
                {
                    throw new FormatException("The Child Indicator length must be 2 characters");
                }
            }
        }


        #endregion ChildIndicatorString

        #region TitleText
        /// <summary>
        /// This is the text string that will appear in the List View
        /// </summary>
        private string _TitleText;

        public string TitleText
        {
            get { return _TitleText; }
            set { _TitleText = value; }
        }


        #endregion TitleText

        #region NodeLevelName
        /// <summary>
        /// this mutable porperty  of AlphaNumeric characters will be a unique identifier that is created using 
        /// single alpahnumeric characters [0..9][a..z[A..Z] to create a 
        /// string, in which the length of the string indicates its position in the hierachy
        /// Its terminal char will refelect its child number of its parent
        /// and the leading characters will be its parent's NodeLevelName
        /// </summary>
        private string _NodeLevelName;

        public string NodeLevelName
        {
            get { return _NodeLevelName; }
            set { _NodeLevelName = value; }
        }

        #endregion NodeLevelName

        #region ID
        /// <summary>
        /// this immutalbe integer will be created by adding 1 the the number of object created. 
        /// It cannot be changed. If for some reason the user decides to move an object to some 
        /// other place in the hierachy its name and the name of all of its children would
        /// change to reflect its new position in the hierarchy, but its ID, which could possible 
        /// be used to link this object to some external data resource, such as a QA file, 
        /// a information URL, a data file etc would never change.
        /// </summary>
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        #endregion ID

        #region NumberOfChildren (NOC)
        /// <summary>
        /// This integer indicated the number of primary children that the objects has at any time, 
        /// subject to the addition or removal at some later time.It does not indicate the number 
        /// of any grand children etc. It is used to calculate the object NodeLevelName terminal 
        /// character as described above
        /// References:
        ///     SubjectStaticMembers: By the OpenFile and the GetNodeLevelPosition where it
        ///         is used to calculate the NodeLevelName
        ///     SubjectTree.xaml.cs: by the Delete RD in 4 places, by the CreateNewChild method 3 times
        /// </summary>
        private int _NOC;

        public int NOC
        {
            get { return _NOC; }
            set { _NOC = value; }
        }


        #endregion NumberOfChildren (NOC)

        #region HasData
        /// <summary>
        /// this boolean indicates whether there are any accessory data files assigned 
        /// to this node's ID. If it is true, this precludes deleting this node.
        /// </summary>
        private bool _HasData;

        public bool HasData
        {
            get { return _HasData; }
            set { _HasData = value; }
        }


        #endregion HasData








    }// End Class SubjectNOdes
}




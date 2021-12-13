using NewLSP.DataModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace NewLSP.StaticHelperClasses
{
    public static class SubjectStaticMembers
    {
        #region Properties

        #region Property SelectedNode

        /// <summary>
        /// The selected node is the subject node in the ListView which the user
        /// clicked
        /// </summary>
        private static SubjectNodes _SelectedNode;

        public static SubjectNodes SelectedNode
        {
            get { return _SelectedNode; }
            set { _SelectedNode = value; }
        }// End SubjectNodes

        #endregion SelectedNode


        #region Property ItemCount
        /// <summary>
        /// ItemCount is the number of items created,
        ///     Not the number of items present because
        ///     some may have been deleted
        /// It is used to tie a subject to 
        ///    various data files
        /// </summary>
        private static string _ItemCount;

        public static string ItemCount
        {
            get { return _ItemCount; }
            set { _ItemCount = value; }
        }
        #endregion ItemCount


        #region Propety Data Node Selected

        private static SubjectNodes _DataNode;

        public static SubjectNodes DataNode
        {
            get { return _DataNode; }
            set
            {
                _DataNode = value;
            }
        }

       
        #endregion  Data Node Selected

        #region Property NoteReferenceFilesPath

        /// <summary>
        /// Holds the path to the folder that will contain
        /// the note  text files whose name is generated from
        /// the current number of files in the NoteReferenceFiles folder (0.txt, 1.txt...n.txt etc.)
        /// </summary>
        //private static string _NoteReferenceFilesPath;

        //public static string NoteReferenceFilesPath
        //{
        //    get { return _NoteReferenceFilesPath; }
        //    set { _NoteReferenceFilesPath = value; }
        //}

        #endregion Property NoteReferenceFilesPath

        #region Property CompositDataPath

        private static string _CompositDataPath;

        /// <summary>
        ///  Holds the path to the folder that will contain the
        ///  KeyWords Dictionary.txt,  and a Timelin file,
        /// </summary>
        public static string CompositDataPath
        {
            get { return _CompositDataPath; }
            set { _CompositDataPath = value; }
        }

        #endregion Property CompositDataPath

        #region Property NotesDictionaryPath

        private static string _KeyWordsDictionaryPath;

        public static string KeyWordsDictionaryPath
        {
            get { return _KeyWordsDictionaryPath; }
            set { _KeyWordsDictionaryPath = value; }
        }

        #endregion Property NotesDictionaryPath

        #endregion Properties

        #region Public Fields

        #region Field Dictionary of Subject Nodes (SubjectNodeDictionary)
        // Create a dictionary of all subject nodes whose key is the node.NodeLevelName
        public static Dictionary<string, SubjectNodes> SubjectNodeDictionary = new Dictionary<string, SubjectNodes>();



        #endregion  SubjectNodeDictionary


        #region Field List of Display strings (DisplayList)

        //Create an List of strings for the ListView display and array of SubjectNodes to match it
        /// <summary>
        /// The DisplayList is what shows up in the Subjects Tree, Each item has only 3 components
        ///     1. The leading blanks to indicate the Item level
        ///     2. The Has Chidred indication (+/-)
        ///     3. The Name yoy assigned to the node
        /// </summary>
        public static List<string> DisplayList = new List<string>();
        #endregion DisplayList


        #region Field List of Subject NodeLevelName strings ListView display (SubjectNodesLevelName)

        // Create a List of SubjectNode NodeLevelName strings to match DisplayList

        public static List<string> SubjectNodesLevelNameList = new List<string>();

        #endregion SubjectNodesLevelNam


        #region Field OldNLN
        public static string OldNLN;
        #endregion Field OldNLN

        #endregion Public Fields


        #region Private Fields


        private static string ItemsCountFilePath;

       // public static string HomeFolderPath;

        private static string SubjectName;

        // This is the path to the folder that holds the 'ɀ' delimited tree nodes in the selected Subject tree
        private static string SubjectsNodeDataStringsPath;


        // This is the path to the Folder that holds the questions and answers for a given subject tree node
        public static string DataNodesQAFilePath;




        #endregion Private Fields

        #region Public Methods



        #region Open Data Files method (OpenFiles)

        /// <summary>
        /// This method receives the path to the main holding folder
        /// for a particular subject and opens add of the
        /// reqired data files to populate the ListView display
        /// of Subjects. The Name of the main holding folder
        /// is the display name of the Subects as well
        /// as of the .txt file that holds the display data
        /// References:
        ///     1. Home.xaml.cs btnOpenSubjectFolder_Click
        /// </summary>
        /// <param name="HomeFolderPat"></param>
        public static void OpenFiles(string ThisHomeFolderPath)
        {
            

            // Set DataNode Hyperlinks Path
            
            CommonStaticMembers.DataNodesHyperlinksPath = CommonStaticMembers.HomeFolderPath + "Hyperlinks";

            // Set the Path to the DataNodesNoteReferencesFilesPath
            CommonStaticMembers.DataNodesNoteReferencesFilesPath = CommonStaticMembers.HomeFolderPath + "DataNodesNoteReferencesFiles\\";

            // The following can't be done here because no data node has been defined
            // Set DataNodeNoteReferenceFilePath
            //SubjectStaticMembers.SetDataNodeNoteReferenceFilePath();


            //// Create path to this subjects data file
            SubjectsNodeDataStringsPath = CommonStaticMembers.HomeFolderPath + "NodeDataStrings.txt";
            //string SubjectNodesDataFilePath = SubjectsNodeDataStringsPath;

            
            
                SubjectNodes RootNode = new SubjectNodes();

                // Read in the current item count
                string ItemCount = File.ReadAllText(CommonStaticMembers.HomeFolderPath + "ItemCount.txt");
                SubjectStaticMembers.ItemCount = ItemCount;


              

                // Instantiate the Dictionary
                SubjectNodeDictionary = new Dictionary<string, SubjectNodes>();

                // Instantiate a new node
                SubjectNodes ThisNode = new SubjectNodes();
                // Create the delimiter
                char D = '\u0240';


                //Read in SubjectsNodeDataStringsPath  into a string array
                string[] SubjectNodeDataStringArray = File.ReadAllLines(SubjectsNodeDataStringsPath);

                //Create the SubjectNodeDictionary
                // Parse each line in the SubjectNodeDataStringArray to add a dictionary item
                foreach (string line in SubjectNodeDataStringArray)
                {
                    // get the properties of a SubjectNode
                    string[] ItemsInLine = line.Split(D);
                    ThisNode.LeadingChars = ItemsInLine[0];
                    ThisNode.CI = ItemsInLine[1];
                    ThisNode.TitleText = ItemsInLine[2];
                    ThisNode.NodeLevelName = ItemsInLine[3];
                    string IDString = ItemsInLine[4];
                    ThisNode.ID = Int32.Parse(IDString);
                    string NOCString = ItemsInLine[5];
                    ThisNode.NOC = Int32.Parse(NOCString);
                    string HasDataString = ItemsInLine[6];
                    if (HasDataString == "false")
                    {
                        ThisNode.HasData = false;
                    }
                    else
                    {
                        ThisNode.HasData = true;
                    }


                    SubjectNodeDictionary.Add(ThisNode.NodeLevelName, ThisNode);
                    ThisNode = new SubjectNodes();

                }// End foreach
                DisplayParentsAndChildren("*");

            
        }//End OpenFiles method



        #endregion OpenFiles

        #region GetLeadingChars

        public static string GetLeadingChars(string nodeLevelName)
        {
            int LengthOfNodeLevelName = nodeLevelName.Length - 1;
            return new string(' ', LengthOfNodeLevelName * 3);
        }// End GetLeadingChars
        #endregion GetLeadingChars

        #region GetNodeLevelPosition

        public static string GetNodeLevelPosition(int ParentsNOC)
        {
            string NodeLevelPositionString = "0123456789abcedfghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] NodeLevelPositionChars = NodeLevelPositionString.ToCharArray();
            char NodeLevelChar = NodeLevelPositionChars[ParentsNOC];
            return NodeLevelChar.ToString();

        }// End GetNodeLevelPosition

        
        #endregion GetNodeLevelPosition

        #region Remove Node From Dictionary

        public static void RemoveNodeFromDictionary(string thisNodesLevelName)
        {
            SubjectNodeDictionary.Remove(thisNodesLevelName);

        }// EndRemove Node From Dictionary
        #endregion Remove Node From Dictionary

        public static void ReplaceNode(string thisNodesLevelName, SubjectNodes newNode)
        {
            SubjectNodeDictionary[thisNodesLevelName] = newNode;
        }


        #region SaveFiles

        public static void SaveFiles()
        {
            // Create a List<string> OutputNodeDataStringList adding delimited node properties to the List
            List<string> OutputNodeDataStringList = new List<string>();

            // Cycle thorugh SubjectNodeDictionary adding delimited node properties to the List
            foreach (KeyValuePair<string, SubjectNodes> KVP in SubjectNodeDictionary)
            {
                string Key = KVP.Key;
                SubjectNodes ThisNode = KVP.Value;
                string SubjectNodeDelimitedString = RetrunNodeDelimitedString(ThisNode);
                OutputNodeDataStringList.Add(SubjectNodeDelimitedString);
            }

            // Create a string array of OutputNodeDataStringList
            string[] OutputNodeDataStringArray = OutputNodeDataStringList.ToArray();

            // Save OutputNodeDataStringList to SubjectsNodeDataStringsPath.txt
            File.WriteAllLines(CommonStaticMembers.HomeFolderPath + "NodeDataStrings.txt", OutputNodeDataStringArray);

            // Save the CurrentItemCount
            string ItemCount = SubjectStaticMembers.ItemCount;
            File.WriteAllText(CommonStaticMembers.HomeFolderPath + "ItemCount.txt", ItemCount);

           


        }// End SaveFiles


        #endregion SaveFiles


        #region Display a node's parents, the node and the nodes children (DisplayParentsAndChildren)

        /// <summary>
        /// This method creates the List of display strings for the ListView 
        /// as well as a dictionary of Subject nodes whose 
        /// </summary>
        /// <param name="ThisNode"></param>
        /// <returns></returns>
        public static List<string> DisplayParentsAndChildren(string ThisNodesLevelName)
        {

            // Clear the existing List
            DisplayList.Clear();
            SubjectNodesLevelNameList.Clear();

            SubjectNodes CurrentNode = new SubjectNodes();
            // Display Parents and chosen node
            for (int i = 0; i < ThisNodesLevelName.Length; i++)
            {
                // Increase the length of the NodeLevelName by 1 on each iteration
                string CurrentNodeLevelName = ThisNodesLevelName.Substring(0, i + 1);
                if (SubjectNodeDictionary.ContainsKey(CurrentNodeLevelName))
                {
                    CurrentNode = SubjectNodeDictionary[CurrentNodeLevelName];
                }

                string ThisNodesDisplayString = ReturnDisplayString(CurrentNode);
                DisplayList.Add(ThisNodesDisplayString);
                SubjectNodesLevelNameList.Add(CurrentNodeLevelName);
            }

            // Get the children of the chosen node

            // Get the Length of the choden nodes node level name because its children NLN will be 1 longer 
            int L = ThisNodesLevelName.Length;
            // begins witn ThisNodesLevelName and whose length is 1 greater that that of ThisNodesLevelName

            foreach (KeyValuePair<string, SubjectNodes> KVP in SubjectNodeDictionary)
            {
                string Key = KVP.Key;
                SubjectNodes ThisNode = SubjectNodeDictionary[Key];
                if ((Key.IndexOf(ThisNodesLevelName) == 0))
                {
                    if (ThisNode.NodeLevelName.Length == L + 1)
                    {
                        string ThisDisplaystring = ReturnDisplayString(ThisNode);
                        DisplayList.Add(ThisDisplaystring);
                        string ThisNodeLevelName = ThisNode.NodeLevelName;
                        SubjectNodesLevelNameList.Add(ThisNodeLevelName);
                    }
                }
            }

            return DisplayList;

        }// End DisplayParentsAndChildren method

        #endregion DisplayParentsAndChildren

        #region Read in the count of items existing at startup  (GetCurrentItemCount)

        public static int GetCurrentItemCount()
        {
            //ItemCount = -1;
            if (File.Exists(ItemsCountFilePath))
            {
                //BinaryReader binReader = new BinaryReader(File.Open(ItemsCountFilePath, FileMode.Open));
                //ItemCount = binReader.ReadInt32();
                string ItemCount = File.ReadAllText(CommonStaticMembers.HomeFolderPath + "ItemCount.txt");
            }

            return Int32.Parse(ItemCount);
        }// End GetCurrentItemCount

        #endregion (GetCurrentItemCount)

        #region AddNodeToDictionary

        public static void AddNodeToDictionary(SubjectNodes ThisNode)
        {
            SubjectNodeDictionary.Add(ThisNode.NodeLevelName, ThisNode);
        }
        #endregion AddNodeToDictionary

        #region SetDataNodesQAFilePath


        /// <summary>
        /// Called by STxc .rbDataNode_Click
        /// which then sets the DataNodesQAFilePath to the 
        /// QAFiles folder's DataNode's ID.txt file
        /// </summary>
        public static void SetDataNodesQAFilePath()
        {
             DataNodesQAFilePath = CommonStaticMembers.HomeFolderPath + "QAFiles\\" + DataNode.ID.ToString() + ".txt";
           
        }

        public static string GetDataNodesQAFilePath()
        {
            return DataNodesQAFilePath;
        }


        #endregion SetDataNodesQAFilePath

        #region GetSetDataNodeNoteReferenceFilePath
        static string DataNodeNoteReferenceFilePath;
        public static void SetDataNodeNoteReferenceFilePath()
        {
            SubjectNodes DateNode = new SubjectNodes();
            DateNode = SubjectStaticMembers.DataNode;
            int DataNodeId = DataNode.ID;
            DataNodeNoteReferenceFilePath = CommonStaticMembers.HomeFolderPath + "DataNodesNoteReferencesFiles\\" + DataNodeId.ToString() + ".txt";
        }

        public static string GetDataNodeNoteReferenceFilePath()
        {
            return DataNodeNoteReferenceFilePath;
        }

        #endregion GetSetDataNodeNoteReferenceFilePath

        public static SubjectNodes ReturnNodeAtPos(string NodeLevelName)
        {
            return SubjectNodeDictionary[NodeLevelName];
        }


        #region Public Method to change all nodes with an old NLN to a new NLN: ChangeMovedNodesNLN()

        internal static void ChangeMovedNodesNLN(string oldNLN, string newNLN)
        {
            int currentCountOfDictionary = SubjectNodeDictionary.Count;
            

            // Create an array of SubjectNodes to change the values
           
            SubjectNodes[] ArrayOfSubjectNodes = new SubjectNodes[currentCountOfDictionary];

            //Cycle through the dictionary adding each node to the array
            int cntr = 0;
           foreach(KeyValuePair<string, SubjectNodes> KVP in SubjectNodeDictionary)
            {
                string key = KVP.Key;
                SubjectNodes thisNode = KVP.Value;
                ArrayOfSubjectNodes[cntr] = thisNode;
                cntr++;
            }


            //Cycle through the ArrayOfSubjectNodes add  nodes to be moved 
            foreach (SubjectNodes thisNode in ArrayOfSubjectNodes)
            {
                string NodeLevelName = thisNode.NodeLevelName;


                //Find all nodes that beign with OldNLN
                if (NodeLevelName.IndexOf(oldNLN) == 0)
                {
                    // Get the currnt NLN
                    string thisNodeLevelName = thisNode.NodeLevelName;

                    // Repalcde it with newNLN
                    thisNodeLevelName = thisNodeLevelName.Replace(oldNLN, newNLN);

                    // Change the NLN in the node
                    thisNode.NodeLevelName = thisNodeLevelName;

                    // Adjust the leading chars
                    int LengthNodeLevelName = thisNode.NodeLevelName.Length*3;
                    string LeadingChars = new string(' ', LengthNodeLevelName);
                    thisNode.LeadingChars = LeadingChars;
                }
            }// End //Cycle through the ArrayOfSubjectNodes add  nodes to be moved 

            // Instantiate a new dictionary

            SubjectNodeDictionary = new Dictionary<string, SubjectNodes>();

            // Cycle through ArrayOfSubjectNodes adding them to SubjectNodeDictionary
            foreach (SubjectNodes thisNode in ArrayOfSubjectNodes)
            {
                string Key = thisNode.NodeLevelName;

                // Add thisNode to the dictionary
                SubjectNodeDictionary.Add(Key, thisNode);
            }

        }// End ChangeMovedNodesNLN

        #endregion ChangeMovedNodesNLN()

        #region public method NodeHasQAFile

        internal static bool NodeHasQAFile(int nodeID)
        {
            string QAFilePath = CommonStaticMembers.HomeFolderPath + "QAFiles\\" + nodeID.ToString() + ".txt";
            if (File.Exists(QAFilePath))
            {
                return true;
            }
            else
            {
                return false;
            }

        }//End NodeHasQAFile


        #endregion public method NodeHasQAFile

        #region public method NodeHadDataFile

        internal static bool NodeHasHyperlinksFile(int nodeID)
        {

            string DataFilePath = CommonStaticMembers.DataNodesHyperlinksPath + "\\" + nodeID.ToString() + ".txt";
            if (File.Exists(DataFilePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion public method NodeHadDataFi

        #endregion Public Methods


        #region Priate Methods

        #region Retrun the display string for a node   (ReturnDisplayString)

        private static string ReturnDisplayString(SubjectNodes ThisNode)
        {
            string DisplayString = "";
            string LeadingString = ThisNode.LeadingChars;
            string ChildIndicator = ThisNode.CI;
            string NodeText = ThisNode.TitleText;
            DisplayString = LeadingString + ChildIndicator + NodeText;

            return DisplayString;
        }// End ReturnDisplayString

        #endregion (ReturnDisplayString)


        #region Return a delimited string of the items in a SubjectNode    (RetrunNodeDelimitedString)

        private static string RetrunNodeDelimitedString(SubjectNodes thisNode)
        {

            string LeadingCharsString = thisNode.LeadingChars;
            string TitleText = thisNode.TitleText;
            int ItemIDInt = thisNode.ID;
            string ItemIDString = ItemIDInt.ToString();
            int ItemsNumberOfChildren = thisNode.NOC;
            string ItemsNumberOfChildrenString = ItemsNumberOfChildren.ToString();
            string ItemsChildrenIncidator = thisNode.CI;
            string ItemsNodeLevelName = thisNode.NodeLevelName;
            bool ItemHasData = thisNode.HasData;
            string ItemHasDataString;
            if (ItemHasData == false)
            {
                ItemHasDataString = "false";
            }
            else
            {
                ItemHasDataString = "true";
            }

            char D = '\u0240';
            string OutputString = LeadingCharsString + D + ItemsChildrenIncidator + D + TitleText +
                D + ItemsNodeLevelName + D + ItemIDString + D + ItemsNumberOfChildrenString + D + ItemHasDataString;

            return OutputString;
        }// End RetrunNodeDelimitedString


        #endregion RetrunNodeDelimitedString

        #endregion Priate Methods


    }// End SubjectStaticMembers


}// End NameSpace

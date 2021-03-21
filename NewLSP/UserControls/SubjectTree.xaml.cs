﻿
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NewLSP.StaticHelperClasses;
using NewLSP.DataModels;
using System.Collections.Generic;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for SubjectTree.xaml
    /// </summary>
    public partial class SubjectTree : UserControl
    {
        public SubjectTree()
        {
            InitializeComponent();
        }


        #region Display the Selected Subject's Root and Children

        /// <summary>
        /// Displays every line in SubjectStaticMembers.DisplayList
        /// in the lbSubjects ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowDisplayLisgt_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectStaticMembers.DisplayList.Count != 0)
            {
                foreach (string line in SubjectStaticMembers.DisplayList)
                {
                    lvSubjects.Items.Add(line);
                }
            }
        }// EndbtnShowDisplayLisgt_Click


        #endregion Display the Selected Subject's Root and Children


        #region Mouse Rigth Button Up to Select new parent


        private void lvSubjects_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lvSubjects.SelectedIndex >= 0)
            {
                // Get the NodeLevelName for this node from the SubjectNodesLevelNameList
                string NodeLevelName = SubjectStaticMembers.SubjectNodesLevelNameList[lvSubjects.SelectedIndex];

                // Use the NodeLevelName to get the correct node fromthe dictionary of SubjectNodeDictionary
                NewParentNode = SubjectStaticMembers.SubjectNodeDictionary[NodeLevelName];

            }

            
            MoveNode();
        }

        #endregion Mouse Rigth Button Up

        #region MoveNode()

        private void MoveNode()
        {
            //Increment the new parent's number of children
            NewParentNode.NOC ++;

            //determine if the leading character is correct
            if(NewParentNode.CI != "+ ")
            {
                NewParentNode.CI = "+ ";
            }

            //Save the New Parent to the dictionary
            SubjectStaticMembers.SubjectNodeDictionary[NewParentNode.NodeLevelName] = NewParentNode;

            //Get the Child's NodeLevelPosition
            string ChildsNodeLevelPosition = SubjectStaticMembers.GetNodeLevelPosition(NewParentNode.NOC);

            // create new NLN for the moved node
            string NewNLN = NewParentNode.NodeLevelName + ChildsNodeLevelPosition;

            //get Old NodeLevelName
            string OldNLN = SubjectStaticMembers.OldNLN;

            // Call static to cycly through dictionary replacing OldNLN with NewNLN
            SubjectStaticMembers.ChangeMovedNodesNLN(OldNLN, NewNLN);

        }
        #endregion MoveNode()

        //=======================================================//

        #region Properties
        private static SubjectNodes SelectedNode;
        private static SubjectNodes ParentNode;
        private static SubjectNodes OldParentNode;
        private static SubjectNodes NewParentNode;
        private static SubjectNodes NewChildNode;
        private static SubjectNodes NodeToMove;

        // a boolean that is true when a node has been selected to move to a new parent
        // private static bool NodeIsMoving = false;

        #endregion Properties

        //=======================================================//

        #region Chose Selected Node in ListView

        /// <summary>
        /// Uses the MouseLeftButtonUp event to designate the SelectedNode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSubjects_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lvSubjects.SelectedIndex >= 0)
            {
                // Get the NodeLevelName for this node from the SubjectNodesLevelNameList
                string NodeLevelName = SubjectStaticMembers.SubjectNodesLevelNameList[lvSubjects.SelectedIndex];

                // Use the NodeLevelName to get the correct node fromthe dictionary of SubjectNodeDictionary
                SelectedNode = SubjectStaticMembers.SubjectNodeDictionary[NodeLevelName];

            }

        }// End  lvSubjects_PreviewMouseLeftButtonU

        #endregion Chose Selected Node in ListView

        //=======================================================//

        #region Radio Button Methods

        #region Radio button New Chile
        private void rbNewChild_Checked(object sender, RoutedEventArgs e)
        {
            var rbNewChild = sender as RadioButton;

            if (tbxNodeName.Text == "")
            {
                MessageBox.Show("You must Enter text into the Enter Node Text TextBox and select a Parent Node");
                rbNewChild.IsChecked = false;
                return;
            }
            if (SelectedNode == null)
            {
                MessageBox.Show("You Must select a Parent Node before Clicking Create a New Child Node");
                rbNewChild.IsChecked = false;
                return;
            }
            if (SelectedNode.CI == "T ")
            {
                MessageBox.Show("You Cannot add a child to a Terminal node");
                rbNewChild.IsChecked = false;
                return;
            }

            // Get ItemIndex
            int CurrentItemCount = SubjectStaticMembers.ItemCount;

            // Create a new node
            CreateNewChildSubjectNode(CurrentItemCount);

        }



        #endregion Radio button New Chile

        #region  #region Radio button Change Title Text of selected node (rbText_Checked)

        /// <summary>
        /// The purpose of this method is to 
        /// change the TitleText of the selected node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbText_Checked(object sender, RoutedEventArgs e)
        {

            if (tbxNodeName.Text == "")
            {
                MessageBox.Show("You must Enter text into the Enter Node Text TextBox and select the node to Change");
                rbText.IsChecked = false;
                return;
            }
            if (SelectedNode == null)
            {
                MessageBox.Show("You Must select a Node before Clicking Change Title Text");
                rbText.IsChecked = false;
                return;
            }

            string NewTitleText = tbxNodeName.Text;
            SelectedNode.TitleText = NewTitleText;
            string ThisNodeLevelName = SelectedNode.NodeLevelName;
            SubjectStaticMembers.SubjectNodeDictionary[ThisNodeLevelName] = SelectedNode;

            List<string> NewDisplayList = SubjectStaticMembers.DisplayParentsAndChildren(ThisNodeLevelName);

            lvSubjects.Items.Clear();

            foreach (string DisplayLine in NewDisplayList)
            {
                lvSubjects.Items.Add(DisplayLine);
            }
        }


        #endregion  (rbText_Checked)


        #region Radio button make node terminal
        // TODO Code thr make terminal radio button
        private void rbTerminal_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New Terminal RB Clicked");
        }

        #endregion Radio button make node terminal


        #region Radio button Delete

        // TODO Code the delete node radio button
        private void rbDelete_Checked(object sender, RoutedEventArgs e)
        {
            // Test to see if the node has data or children before deleting it
            if (SelectedNode.HasData || SelectedNode.NOC > 0)
            {
                MessageBox.Show("You cannot delete a node that has children or data, you can only move it");
            }

            // Remove the node from the dictionary
            SubjectStaticMembers.RemoveNodeFromDictionary(SelectedNode.NodeLevelName);

            // Designate the parent node
            string ParentNodesNLN = SelectedNode.NodeLevelName.Substring(0, SelectedNode.NodeLevelName.Length - 1);

            ParentNode = SubjectStaticMembers.ReturnNodeAtPos(ParentNodesNLN);

            // Adjust the parent's number of children
            ParentNode.NOC = ParentNode.NOC - 1;

            // adjust the Parent's nodes child indicaator if necessarr
            if (ParentNode.NOC == 0)
            {
                ParentNode.CI = "- ";
            }
            // store updated parent node in the dictionary
            SubjectStaticMembers.ReplaceNode(ParentNode.NodeLevelName, ParentNode);


            // Change the display list to the Parents Children
            List<string> NewDisplayList = SubjectStaticMembers.DisplayParentsAndChildren(ParentNode.NodeLevelName);
            lvSubjects.Items.Clear();

            foreach (string DisplayLine in NewDisplayList)
            {
                lvSubjects.Items.Add(DisplayLine);
            }
        }


        #endregion Radio button Delete


        #region Radio Button Show node, ancestory and children

        /// When a node's children are not showing clicking
        /// will show this node, its parents and it children
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbExpandCollapse_Checked(object sender, RoutedEventArgs e)
        {

            if (SelectedNode == null)
            {
                MessageBox.Show("You must select a subject node before proceeding!");
                rbExpandCollapse.IsChecked = false;
                return;
            }
            string SelectedNodeNameLevelName = SelectedNode.NodeLevelName;
            SubjectStaticMembers.DisplayParentsAndChildren(SelectedNodeNameLevelName);
            lvSubjects.Items.Clear();
            foreach (string item in SubjectStaticMembers.DisplayList)
            {
                lvSubjects.Items.Add(item);
            }
            rbExpandCollapse.IsChecked = false;
            SelectedNode = null;

        }

        #endregion Radio Button Show node, ancestory and children



        #region Radio Button create DataNode


        private void rbDataNode_Click(object sender, RoutedEventArgs e)
        {
            // Set the SubjectStaticMembers DataNode
            SubjectStaticMembers.DataNode = SelectedNode;

            // Set the Path to the DataNode's QAfile
            SubjectStaticMembers.SetDataNodesQAFilePath();

        }
        #endregion Radio Button create DataNode


        #region RadioButton Move Node

        private void rbMoveNode_Checked(object sender, RoutedEventArgs e)
        {

           
            // Designate the Node to move
            NodeToMove = SelectedNode;

            SelectedNode = NodeToMove;

            //Set the node level name of the node to be moved
            SubjectStaticMembers.OldNLN = SelectedNode.NodeLevelName;
            // Get the NLN of this node's parent
            string ParentNLN = SelectedNode.NodeLevelName.Substring(0, SelectedNode.NodeLevelName.Length - 1);
            OldParentNode = SubjectStaticMembers.SubjectNodeDictionary[ParentNLN];


            MessageBox.Show("Select the New Parent Node by Clicking the Right Mouse Button");
        }// Ed rbMoveNode_Checked

        #endregion RadioButton Move Node

        #endregion Radio Button Methods

        //=======================================================//

        #region Private local methods


        #region Create a new child node  (CreateNewChildSubjectNode)
        /// <summary>
        /// This method receives the curent number of items creates
        ///     (currentItemCount) and creates a new node wiht this as the ID
        ///  It gets the parent from the list view index of the selected node
        ///  It Adds the node to the dictionary, adjusts the Parent node
        ///  and resets the display to reflect the Parents children
        /// </summary>
        /// <param name="currentItemCount"></param>
        private void CreateNewChildSubjectNode(int currentItemCount)
        {

            // Instantiate a SubjectNode with the currentItemCount
            NewChildNode = new SubjectNodes(currentItemCount);

            // Get the Parent Node
            ParentNode = GetParentNode();

            //Get the Parent's Number of Children to calcuate the child's NodeLevelName
            int ParentsNumchildren = ParentNode.NOC;

            //Get the Child's NodeLevelPosition
            string ChildsNodeLevelPosition = SubjectStaticMembers.GetNodeLevelPosition(ParentsNumchildren);

            // Set the NewChildNodes NodeLevel
            NewChildNode.NodeLevelName = ParentNode.NodeLevelName + ChildsNodeLevelPosition;

            // Set the Child node's leading char sgtring
            NewChildNode.LeadingChars = SubjectStaticMembers.GetLeadingChars(NewChildNode.NodeLevelName);

            // Set the Child indicator to no children
            NewChildNode.CI = "- ";

            //Set the TitleText to the text in the node name textbox
            NewChildNode.TitleText = tbxNodeName.Text;

            // set the has associated data files to false
            NewChildNode.HasData = false;

            // set the child node's number of children to 0
            NewChildNode.NOC = 0;

            // Add this child to the dictionary
            SubjectStaticMembers.AddNodeToDictionary(NewChildNode);

            // Increment the parents NOC and CI
            ParentNode.CI = "+ ";
            ParentNode.NOC++;

            // NEW 20210222 START

            SubjectStaticMembers.DisplayParentsAndChildren(ParentNode.NodeLevelName);
            lvSubjects.Items.Clear();
            foreach (string item in SubjectStaticMembers.DisplayList)
            {
                lvSubjects.Items.Add(item);
            }

            // Clear the NodeName textbox
            tbxNodeName.Text = "";

            //UnCheck the new child radio button
            rbNewChild.IsChecked = false;

            //store the updated parents node in the dictionary
            SubjectStaticMembers.SubjectNodeDictionary[ParentNode.NodeLevelName] = ParentNode;
            //Increment and save the ItemsIndex
            SubjectStaticMembers.ItemCount++;
            SelectedNode = null;
        }// End CreateNewChildSubjectNode


        #endregion (CreateNewChildSubjectNode)

        #region Get the Parent (ie the selected node) of a new child node  (GetParentNode)

        /// <summary>
        /// Use the Selected Index to get the parent of a new child node
        /// </summary>
        /// <returns></returns>
        private SubjectNodes GetParentNode()
        {
            int SelectedIndex = lvSubjects.SelectedIndex;
            string SelectedNodesLevelName = SubjectStaticMembers.SubjectNodesLevelNameList[SelectedIndex];
            // Create ParentNode
            ParentNode = SubjectStaticMembers.SubjectNodeDictionary[SelectedNodesLevelName];
            return ParentNode;
        }// End GetParentNode
        #endregion GetParentNode

        #endregion Private local methods


        private void btnSaveFiles_Click(object sender, RoutedEventArgs e)
        {
            SubjectStaticMembers.SaveFiles();
        }


    }// End Class

}//End Namespace

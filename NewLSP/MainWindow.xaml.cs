
using System.IO;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;

namespace NewLSP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miHome_Click(object sender, RoutedEventArgs e)
        {
             SetActiveUserControl(ucHome);
        }

        private void miSubjectTree_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectStaticMembers.DisplayList.Count ==0) 
            { 
                MessageBox.Show("You cannot open this tab until you select a Subject Folder");
                return;
            }
            if (QAStaticMembers.DictionaryChanged == true)
            {
                if (MessageBox.Show("There are unsaved changes to the QADictionary. Do you want to ignore them?",  "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Close the window  
                }
                else
                {
                    return;
                }
            }

            SetActiveUserControl(ucSubjectTree);
        }

        private void miTest_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectStaticMembers.DataNode == null)
            {
                MessageBox.Show("You must select a DataNode before opening this window");
                return;
            }

            if (QAStaticMembers.DictionaryChanged == true)
            {
                if (MessageBox.Show("There are unsaved changes to the QADictionary. Do you want to ignore them?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Close the window  
                }
                else
                {
                    return;
                }
            }

            //Check to make sure the QAFile exists and populate the dictionary and numbers list
            if (QAStaticMembers.DoesQAFileExist())
            {
                TestReviewStaticMembers.InitializeData();
                    //StartNewQATestReview();
            }
            else
            {
                MessageBox.Show("There is no QAFile for this subject");
            }

           

            SetActiveUserControl(ucTestReview);


            MessageBox.Show("You must select a Test/Review action first. If you want to randomize the questions select QuestionOrder -> Randomize first" );
        }


        public void SetActiveUserControl(UserControl control)
        {
            // Make the visibility of all user controls collapsed
            ucHome.Visibility = Visibility.Collapsed;
            ucSubjectTree.Visibility = Visibility.Collapsed;
            ucCreatEditQA.Visibility = Visibility.Collapsed;
            ucTestReview.Visibility = Visibility.Collapsed;
            //ucInstructions.Visibility = Visibility.Collapsed;
            ucLinkNote.Visibility = Visibility.Collapsed;

           // Make the selected control visible
            control.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Called when the user clicks the QAPages tab.
        /// If there is not data node a message is shown and control returns
        /// Else SetActiveUserContro is called to open the ucCreateEditQA control
        /// and a message to choose the edit Mode before proceeding is shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateOrEdit_Click(object sender, RoutedEventArgs e)
        {

            if (SubjectStaticMembers.DataNode == null)
            {
                MessageBox.Show("You must select a DataNode before opening this window");
                return;
            }
            SetActiveUserControl(ucCreatEditQA);
            MessageBox.Show("You must Choose the Edit Mode before Proceeding");
        }

        private void miCreateQA_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectStaticMembers.DataNode == null)
            {
                MessageBox.Show("You must select a DataNode before opening this window");
                return;
            }
        }


        private void miLinkNotes_Click(object sender, RoutedEventArgs e)
        {
            bool HasHyperlink = false;
            bool HadDataNodeReferenceFile = false;
            SetActiveUserControl(ucLinkNote);

            if (SubjectStaticMembers.DataNode != null)
            {
                //1.    Test to see if this node has hyperlinks
                //      a.  Create the filepath to the DataNodes HyperlinkFile
                string DataNodesHyperlinkPath = CommonStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

                //      b.  Test to see if a hyperlink file exists
                if (File.Exists(DataNodesHyperlinkPath))
                {
                    HasHyperlink = true;
                    // Added- 20211020 The following was activated to try and populate the list of hyperlinks this works
                    LinkNoteStaticMembers.SetHyperlinkStringsList();
                   // End todo of activation 20211020
                }

                //      c. Test to see if this data node has a DataNodeReference file and if so set  HadDataNodeReferenceFile to true
                string DataNodesReferenceFilePath = CommonStaticMembers.DataNodesNoteReferencesFilesPath; 
                
                
                if (File.Exists(CommonStaticMembers.DataNodesNoteReferencesFilesPath))
                {
                    HadDataNodeReferenceFile = true;
                }

                if (HasHyperlink && !HadDataNodeReferenceFile)
                {
                    MessageBox.Show("This node has a hyperlink file call Files -> Open Hyperlink");
                    
                }
                else if(!HasHyperlink && HadDataNodeReferenceFile)
                {
                    MessageBox.Show("This node has a DataNodeReference file call Files -> Open Notes");
                   
                }
                else if (HasHyperlink && HadDataNodeReferenceFile)
                {
                    MessageBox.Show("This node has a Notes file call Files -> Open Notes \r\n" +
                        "and a hyperlink file call Files -> Open Hyperlink");
                    
                }

            }
           
        }

        private void miCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            // Save the KeyWords Dictionary file
            KeyWordsStaticMembers.SaveDictionary();
            Application curApp = Application.Current;
            curApp.Shutdown();
        }
    }
}

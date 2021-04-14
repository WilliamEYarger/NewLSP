
using System.IO;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using NewLSP.StaticHelperClasses;
using NewLSP.UserControls;
using NewLSP.DataModels;

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


            MessageBox.Show("You must select a Test/Review action first");
        }


        private void miInstructions_Click(object sender, RoutedEventArgs e)
        {

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

            if (InstructionsStaticMembers.InstructionsFolderPath == "")
            {
                MessageBox.Show("You cannot open this page until you have selected the instructions folder!" +
                    "Instructions for creating or opening the Instructions Page \r\n"+
                    "1.To select or create the folder which will hold the Instructions for using this program. \r\n" +

    "First click the “Create or Open Instructions Folder” button \r\n" +
"2.When the Open File Dialog opens single click on an existing instructions folder IF YOU HAVE \r\n" +
 " PREVIOUSLY DESIGNATED IT and then click the Select Folder button at the bottom \r\n" +
"3.If You wish to create a new instructions folder, after clicking the “Create or Open the Subject \r\n" +
  "Folder” button \r\n" +

    "a.Locate the folder where this new instructions folder will be located \r\n" +

    "b.Right click the file dialog screen and click “New” -> “Folder” \r\n"+

    "c.When the New folder appear, click it once(The background will turn blue) and replace \r\n" +

        "New folder with _Instructions \r\n" +

    "d.Click the Select Folder button at the bottom. \r\n" +
"4.Only after you have created or selected a Subject folder will you be able to open the \r\n" +

    "InstructionsPage" );
                string[] readText = File.ReadAllLines(InstructionsStaticMembers.InstructionsFolderPath +
                    "Instructions for creating or opening the Instructions Page.txt");
                string InstructionsText = "";
                foreach (string line in readText)
                {
                    InstructionsText = InstructionsText + line + "\r\n";
                }
                MessageBox.Show(InstructionsText);
                return;
            }
            SetActiveUserControl(ucInstructions);
        }

        public void SetActiveUserControl(UserControl control)
        {
            // Make the visibility of all user controls collapsed
            ucHome.Visibility = Visibility.Collapsed;
            ucSubjectTree.Visibility = Visibility.Collapsed;
            ucCreatEditQA.Visibility = Visibility.Collapsed;
            ucTestReview.Visibility = Visibility.Collapsed;
            ucInstructions.Visibility = Visibility.Collapsed;
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
            bool HasNotes = false;
            SetActiveUserControl(ucLinkNote);

            if (SubjectStaticMembers.DataNode != null)
            {
                //1.    Test to see if this node has hyperlinks
                //      a.  Create the filepath to the DataNodes HyperlinkFile
                string DataNodesHyperlinkPath = SubjectStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

                //      b.  Test to see if a hyperlink file exists
                if (File.Exists(DataNodesHyperlinkPath))
                {
                    HasHyperlink = true;
                   // LinkNoteStaticMembers.SetHyperlinkStringsList();
                }

                

                if(HasHyperlink && !HasNotes)
                {
                    MessageBox.Show("This node has a hyperlink file call Files -> Open Hyperlink");
                    
                }
                else if(!HasHyperlink && HasNotes)
                {
                    MessageBox.Show("This node has a Notes file call Files -> Open Notes");
                   
                }
                else if (HasHyperlink && HasNotes)
                {
                    MessageBox.Show("This node has a Notes file call Files -> Open Notes \r\n" +
                        "and a hyperlink file call Files -> Open Hyperlink");
                    
                }

            }
           
        }
    }
}

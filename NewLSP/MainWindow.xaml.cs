using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using NewLSP.StaticHelperClasses;
using NewLSP.UserControls;

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
           
            SetActiveUserControl(ucSubjectTree);
            //MessageBox.Show("You must click the Show Display List to view the subject tree");
        }

        //private void miCreateQA_Click(object sender, RoutedEventArgs e)
        //{
        //    if(SubjectStaticMembers.DataNode == null)
        //    {
        //        MessageBox.Show("You must select a DataNode before opening this window");
        //        return;
        //    }
        //    SetActiveUserControl(ucCreatEditQA);
        //    MessageBox.Show("You must Choose the Edit Mode before Proceeding");
        //    //int CurrentQANumber = QAStaticMembers.CurrentQANumberInt;
        //    //CreatEditQA ThisQAObject = new CreatEditQA();
        //    //ThisQAObject.tbkCurrentQuestionNumber.Text = CurrentQANumber.ToString();
        //}

        private void miTest_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectStaticMembers.DataNode == null)
            {
                MessageBox.Show("You must select a DataNode before opening this window");
                return;
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
            if(InstructionsStaticMembers.InstructionsFolderPath == "")
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


           // Make the selected control visible
            control.Visibility = Visibility.Visible;
        }

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

        
    }
}

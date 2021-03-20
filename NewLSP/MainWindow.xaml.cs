using System;
using System.Collections.Generic;
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
            MessageBox.Show("You must click the Show Display List to view the subject tree");
        }

        private void miCreateQA_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectStaticMembers.DataNode == null)
            {
                MessageBox.Show("You must select a DataNode before opening this window");
                return;
            }
            SetActiveUserControl(ucCreatEditQA);
            MessageBox.Show("You must Choose the Edit Mode before Proceeding");
            //int CurrentQANumber = QAStaticMembers.CurrentQANumberInt;
            //CreatEditQA ThisQAObject = new CreatEditQA();
            //ThisQAObject.tbkCurrentQuestionNumber.Text = CurrentQANumber.ToString();
        }

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

        public void SetActiveUserControl(UserControl control)
        {
            // Make the visibility of all user controls collapsed
            ucHome.Visibility = Visibility.Collapsed;
            ucSubjectTree.Visibility = Visibility.Collapsed;
            ucCreatEditQA.Visibility = Visibility.Collapsed;
            ucTestReview.Visibility = Visibility.Collapsed;


           // Make the selected control visible
           control.Visibility = Visibility.Visible;
        }

      

       
    }
}

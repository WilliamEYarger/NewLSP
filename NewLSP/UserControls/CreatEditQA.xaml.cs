
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using NewLSP.StaticHelperClasses;
using NewLSP.DataModels;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for CreatEditQA.xaml
    /// </summary>
    public partial class CreatEditQA : UserControl
    {
       
        public CreatEditQA()
        {
            InitializeComponent();
        }


        #region Fields

        private Dictionary<string, string> qaDictionary = new Dictionary<string, string>();

        private QADataModel QADataModelObject = new QADataModel();
        //--------------------------Booleans---------------------------------------//
        // Edit mode is append to a new or existing file
        private bool appendToFile = false;
        // Edit mode is edit selected qa Pairs
        private bool editSelectedQAPairs = false;
        // Edit mode is edit All files Seriatem
        private bool editAllSeriatem = false;
        //--------------------------String---------------------------------------//


        private string QuestionJpgUrl;
        private string AnswernJpgUrl;
        private string QuestionMp3Url;
        private string AnswerMp3Url;
        //private string currentQAPairStr = "";

        #endregion Fields




        #region Menu Items Click Methods

        #region Files Menu

        #region SaveFile

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            QAStaticMembers.SaveQADictionary();
        }


        #endregion SaveFile

        #region Append to File

        private void Append_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Append Menu Item clicked");
        }
        #endregion Append to File

        #endregion Files Menu

        #region Edit Menu

        #region Begin New QAFile

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            //Check to insure that the file doesn't exist and then create it
            if (QAStaticMembers.DoesQAFileExist())
            {
                
                MessageBoxResult result = MessageBox.Show("This QA Node already has a QAFile, do you want to override it", "Yes", MessageBoxButton.YesNo);
                switch(result)
                {
	                case MessageBoxResult.Yes:
                         QAStaticMembers.CreateNewQAFile(); 
                        break;
	                case MessageBoxResult.No:
		                MessageBox.Show("Then Select append");
		                break;
	
                }

            }
            else
            {
                QAStaticMembers.CreateNewQAFile();
            }

          

            int CurrentQANumberInt = QAStaticMembers.CurrentQANumberInt;
            tbkCurrentQuestionNumber.Text = CurrentQANumberInt.ToString();
        }


        #endregion Begin New QAFile



        #endregion Edit Menu
        private void miQuestionJpgUrl_Click(object sender, RoutedEventArgs e)
        {

            QuestionJpgUrl = ReturnJpgUrl();


        }

        private void miAnswerJpgUrl_Click(object sender, RoutedEventArgs e)
        {
            string JpgUrlPath = ReturnJpgUrl();

            QADataModelObject.AnswerJpgUrl = JpgUrlPath;
        }

        private void miQuestionMp3Url_Click(object sender, RoutedEventArgs e)
        {
            string Mp3UrlPath = ReturnMp3Url();
            QADataModelObject.QuestionMp3Url = Mp3UrlPath;

            //the following will be needed in the take a qa test or review
            //System.Diagnostics.Process.Start(Mp3UrlPath);
        }

        private void miAnswerMp3Url_Click(object sender, RoutedEventArgs e)
        {

            string Mp3UrlPath = ReturnMp3Url();
            QADataModelObject.AnswerMp3Url = Mp3UrlPath;
        }





        #endregion Menu Items Click Methods


        private void btnGetNextQA_Click(object sender, RoutedEventArgs e)
        {

            if(QADataModelObject == null)
            {
                QADataModelObject = new QADataModel();
            }

            // replace all of the \r\n in the question
            string question = tbxQuestion.Text;
            question = question.Replace("\r\n", "~");
            QADataModelObject.Question = question;

            //replace all the the \r\n in the answer

            string answer = tbxAnswer.Text;
            answer = answer.Replace("\r\n", "~");

            QADataModelObject.Answer = answer;
            QADataModelObject.QuestionJpgUrl = QuestionJpgUrl;
            QADataModelObject.QuestionMp3Url = QuestionMp3Url;
            QADataModelObject.AnswerJpgUrl = AnswernJpgUrl;
            QADataModelObject.AnswerMp3Url = AnswerMp3Url;
            // Save the current  QADataModelObject
            QAStaticMembers.AddQAObjectToDictionary(QAStaticMembers.CurrentQANumberInt.ToString(), QADataModelObject);

            // Clear this object and create a new one
            QADataModelObject = null;
            QADataModelObject = new QADataModel();

            // Increment the current question number
            QAStaticMembers.CurrentQANumberInt++;


            //send the current quesntion number string to tbkCurrentQuestionNumber
            tbkCurrentQuestionNumber.Text = QAStaticMembers.CurrentQANumberInt.ToString();


            // Clear the controls data and data string

           
            tbxQuestion.Text = "";
            tbxAnswer.Text = "";
            QuestionJpgUrl = "";
            QuestionMp3Url = "";
            AnswernJpgUrl = "";
            AnswerMp3Url = "";
        }

        #region Private Methods

        #region Open JPG FIles

        #endregion Open JPG FIles

        private static string ReturnJpgUrl()
        {
            string JpgUrlPath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Jpg files (*.jpg)|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                JpgUrlPath = openFileDialog.FileName;
            }
            return JpgUrlPath;

        }


        private static string ReturnMp3Url()
        {
            string Mp3UrlPath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Mp3 files (*.mp3)|*.mp3";
            if (openFileDialog.ShowDialog() == true)
            {
                Mp3UrlPath = openFileDialog.FileName;
            }
            return Mp3UrlPath;

        }




        #endregion  Private Methods
    }// End Class






}// End namespace

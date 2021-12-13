
using System.IO;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using System;
using NewLSP.DataModels;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for TestReview.xaml
    /// </summary>
    public partial class TestReview : UserControl
    {
        public TestReview()
        {
            InitializeComponent();
        }

       
        

        #region Private Fields

        #region Boolean IsTest
        private static bool IsTest;

       
        #endregion Boolean IsTest

        #endregion Private Fields

        #region Menu Items


        #region Files Menu

        #region SaveFile

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //Check to see if the QAResults directory exists and if not create it
            string ResultsDirectoryPath = CommonStaticMembers.HomeFolderPath + "QAResults";
            if (!Directory.Exists(ResultsDirectoryPath))
            {
                Directory.CreateDirectory(ResultsDirectoryPath);
            }
            //check to see if the NodeID result .txt file exists and if not create

            //Get  DataNode.ID.ToString()
            string DataNodeIDStr = SubjectStaticMembers.DataNode.ID.ToString();

            //Create File path
            string ResultsFilePath = ResultsDirectoryPath + "\\" + DataNodeIDStr + ".txt";


            //Get the current date time
            DateTime currentDate = DateTime.Now;

            string CurrentDateTimeString = currentDate.ToString("yyy MM dd HHmm");
            /*
              DateTime myDate = (DateTime)value;
        if (myDate != DateTime.MinValue)
        {
           return myDate.ToString("dd/MM/yyyy HH:mm:ss");
        }
             */

            // compose the output line
            //string FormattedDateStr = String.Format("YYYYMMDDHHmm", currentDate);
            double total =QAStaticMembers.QADictionary.Count;
            double wrong = TestReviewStaticMembers.NumberOfWrongAnswers;

            double PercentCorrect = ((total - wrong) / total)*100;

            string PercentCorrectStr = string.Format("{0:N2}% correct = ", PercentCorrect);
            

            // create the output line
            string OutputStr = CurrentDateTimeString + "|  " + PercentCorrectStr + " " + TestReviewStaticMembers.DelimitedWrongAnswersStr;

            // Append this line to the existing file
            File.AppendAllText(ResultsFilePath, OutputStr + Environment.NewLine);


            // Check to see if the dictionary has been changed and if so save it
            if (QAStaticMembers.DictionaryChanged == true)
            {


                QAStaticMembers.SaveQADictionary();
            }



                // Send a message telling the use that it has been saved
                MessageBox.Show("The Results are saved. You can exit now.");

        }


        #endregion SaveFile


        #endregion  Files Menu

        #region Test/Review Menu


        #region Test

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            IsTest = true;
            TestOrReview();
            return;
        }

        #endregion Test

        #region Review

        private void Review_Click(object sender, RoutedEventArgs e)
        {
           IsTest = false;
            TestOrReview();
            return;
        }

        #endregion Review

        #endregion Test/Review Menu

        #region Subject Order Menu


        #region Menu Item Seriatim

        private void miSeriatim_Click(object sender, RoutedEventArgs e)
        {
            TestReviewStaticMembers.QuestionsSeriatim = true;
        }


        #endregion Menu Item Seriatim


        #region MenuItem Random

        private void miRandom_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion MenuItem Random
        #endregion Subject Order Menu


        #region Instructions Menu

        #region UseForm

        private void UseForm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Use Form Menu Clicked");
        }
        #endregion UseForm

        #endregion Instructions


        #endregion Menu Items

        #region Button Click Methods

        #region OpenImage

        private void btnOpenImage_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TestReviewStaticMembers.JpgUrl);
        }

        #endregion OpenImage


        #region OpenMp3

        private void btnOpenMp3_Click(object sender, RoutedEventArgs e)
        {

            System.Diagnostics.Process.Start(TestReviewStaticMembers.Mp3Url);
        }

        #endregion OpenMp3

        #region ScoreCorrect

        private void btnScoreCorrect_Click(object sender, RoutedEventArgs e)
        {
            TestReviewStaticMembers.CurrentQuestionNumberString = "";
            btnScoreCorrect.IsEnabled = false;
            btnScoreWrong.IsEnabled = false;
            TestOrReview();
        }

        #endregion ScoreCorrect

        #region ScoreWrong

        private void btnScoreWrong_Click(object sender, RoutedEventArgs e)
        {
            // if review add CurrentQuestionNumberString to the end QANUmbersString
            if (!IsTest)
            {
                // this is a review
                TestReviewStaticMembers.QANUmbersString = 
                    TestReviewStaticMembers.QANUmbersString  + TestReviewStaticMembers.CurrentQuestionNumberString + '^';
            }
            else
            {
                // this is test
                TestReviewStaticMembers.DelimitedWrongAnswersStr =
                     TestReviewStaticMembers.DelimitedWrongAnswersStr + 
                        TestReviewStaticMembers.CurrentQuestionNumberString+'^';
                TestReviewStaticMembers.NumberOfWrongAnswers++;
            }

            btnScoreCorrect.IsEnabled = false;
            btnScoreWrong.IsEnabled = false;
            TestOrReview();
        }

        #endregion ScoreWrong


        #region ShowCorrect

        private void btnShowCorrect_Click(object sender, RoutedEventArgs e)
        {
            tbxCorrectAnswer.Text = TestReviewStaticMembers.ThisAnswer;
            AnswerQuestions('A');
            btnScoreCorrect.IsEnabled = true;
            btnScoreWrong.IsEnabled = true;
            btnShowCorrect.IsEnabled = false;
        }

        #endregion ShowCorrect

        #region Button SaveEditsClicked

        /// <summary>
        /// When this button is clicked and changes made to the
        /// Question and Answer file will be saved and the
        /// QAFileChanged boolean is set to true so that
        /// Results/Edits -> Save File and Return menuitem
        /// is clicked the changed QA file will be updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEdits_Click(object sender, RoutedEventArgs e)
        {
            // Get the current question
            string currentQuestion = tbxQuestion.Text;
            // replace all "\r\n" with ~ 
            currentQuestion = currentQuestion.Replace("\r\n", "~");

            //Get the current correct answer
            string currentCorrectAnswer = tbxCorrectAnswer.Text;
            // replace all "\r\n" with ~ 
            currentCorrectAnswer = currentCorrectAnswer.Replace("\r\n", "~");

            // get the QuestionNumber Key
            string currentQANumStr  = TestReviewStaticMembers.CurrentQuestionNumberString;

            //// Get the current QADataModel
            //QADataModel NewQADataModel = QAStaticMembers.ReturnQAObject(currentQANumStr);
            QADataModel NewQADataModel = QAStaticMembers.QADictionary[currentQANumStr];

            // replace the currentQuestion and the currentCorrectAnswer
            NewQADataModel.Question = currentQuestion;
            NewQADataModel.Answer = currentCorrectAnswer;

            // replace the QADataModel in the QADictionary
            QAStaticMembers.ReplaceThisQADataModel(currentQANumStr, NewQADataModel);
             

            // Set the QADataModel's dictionary changed to true
            QAStaticMembers.DictionaryChanged = true;


        }//End btnSaveEdits_Click

        #endregion Button SaveEditsClicke

        #endregion Button Click Methods

        #region Private Methods

        #region AnswerQuestions()

        /// <summary>
        /// This method is called when ever
        /// a new quesion is going to post
        /// or when the use presses the  ShowCorrectAnsser button
        /// The T is either a 'Q' or an 'A'
        /// References:
        ///     This Review Menu item clicked
        ///     this Test Menu item clicked
        ///  
        /// </summary>
        /// <param name="T"></param>
        private void AnswerQuestions(char T)
        {
            // Return all media buttons to disabled
            ResetMediaButtons();

            //reset all media field to initial values
            TestReviewStaticMembers.ResetMediaFields();


            //determine whether this is a question or an answer call
            if (T == 'Q')
            {
                //This is a call for a question//
               

                // Deternine if there are questions left and if not message
                string thisQuestion = TestReviewStaticMembers.ThisQuestion;
                if(thisQuestion == "")
                {
                    MessageBox.Show("There are no questions left. Call the Files Menu to either save" +
                        " the reuslts or clear them.");
                    return;
                }

                // get the CurrentQuestionNumberString
                string CurrentQANumber = TestReviewStaticMembers.CurrentQuestionNumberString;
                // set the QA number
                tblkCurrentQANum.Text = CurrentQANumber;

                tbxQuestion.Text = thisQuestion;

                if(TestReviewStaticMembers.ThisQuestionJPG != "")
                {
                    TestReviewStaticMembers.ThereIsAnImageFile = true;
                    TestReviewStaticMembers.JpgUrl = TestReviewStaticMembers.ThisQuestionJPG;
                }
                if(TestReviewStaticMembers.ThisQuestionMp3Url != "")
                {
                    TestReviewStaticMembers.ThereIsASoundFile = true;
                    TestReviewStaticMembers.Mp3Url = TestReviewStaticMembers.ThisQuestionMp3Url;
                }

            }//End this is a question
            else
            {
                string thisAnswer = TestReviewStaticMembers.ThisAnswer;
                tbxCorrectAnswer.Text = thisAnswer;

                if (TestReviewStaticMembers.ThisAnswerJpgUrl != "")
                {
                    TestReviewStaticMembers.ThereIsAnImageFile = true;
                    TestReviewStaticMembers.JpgUrl = TestReviewStaticMembers.ThisAnswerJpgUrl;
                }
                if (TestReviewStaticMembers.ThisAnswerMp3Url != "")
                {
                    TestReviewStaticMembers.ThereIsASoundFile = true;
                    TestReviewStaticMembers.Mp3Url = TestReviewStaticMembers.ThisAnswerMp3Url;
                }
            }
            ResetMediaButtons();
            EnableMediaButtons();
            btnShowCorrect.IsEnabled = true;


        }// End AnswerQuestions(T)

        #endregion AnswerQuestions()

        #region OpenExternalHperlink

        /// <summary>
        /// This method is called whenever the enabled 
        /// Open Image  or Open Sound file button is clicked
        /// </summary>
        /// <param name="Url"></param>
        private void OpenExternalHperlink(string Url)
        { 
            System.Diagnostics.Process.Start(Url); 
        }

        #endregion OpenExternalHperlink


        #region ResetMediaButtons
       
        /// <summary>
        /// This private method insures that both media buttons are disabled
        /// </summary>
        private void ResetMediaButtons()
        {

            btnOpenImage.IsEnabled = false;
            btnOpenMp3.IsEnabled = false;
        }

       

        private void EnableMediaButtons()
        {
            if (TestReviewStaticMembers.ThereIsAnImageFile)
            {
                btnOpenImage.IsEnabled = true;
            }
            if (TestReviewStaticMembers.ThereIsASoundFile)
            {
                btnOpenMp3.IsEnabled = true;
            }
        }


        #endregion EnableMediaButton()

        #endregion  Private Methods

        #region Public Methods


        #region Test/Review Public Method
        /// <summary>
        /// This Method cycles through the QADictionary 
        /// until:
        /// a.  Test = all questions have been answered and scordx
        /// b.   Review = all questions have been answered correctly 
        ///     but not scored
        /// Depending on whether TextReviewStaticMember.QuestionsSeriatim is true or false
        /// prepare a deliited string of question numbers
        /// </summary>
        internal void TestOrReview()
        {
            // Return all media buttons to disabled
            ResetMediaButtons();

            //reset all media field to initial values
            TestReviewStaticMembers.ResetMediaFields();
            tbxQuestion.Text = "";
            tbxCorrectAnswer.Text = "";
            tbxYourAnswer.Text = "";
            btnShowCorrect.IsEnabled = true;
            //Retrieve the first item in the QANUmbersString until its length =0
            // The QANUmbersString is a string of question numbeers, beginning with 0 that are delimited with ^
            while (TestReviewStaticMembers.QANUmbersString.Length != 0)

            {
                string CurrentQANumberString = TestReviewStaticMembers.QANUmbersString;
                // get  the 0th item from the QANumberString
                // thisKey is the current qa pair number
                string thisKey = StringHelper.ReturnItemAtPos(CurrentQANumberString, '^', 0);
                //string thisKey = StringHelper.GetAndRemoveNthItem(ref CurrentQANumberString, '^', 0);
                TestReviewStaticMembers.QANUmbersString = CurrentQANumberString;
                //Save the current key so that if this is a review and the answer is wrong
                // it can be appended to the end of the CurrentQANumberString
                TestReviewStaticMembers.CurrentQuestionNumberString = thisKey;
                TestReviewStaticMembers.SetCurrentQAValues(thisKey);
                CurrentQANumberString = StringHelper.RemoveFirstItem(CurrentQANumberString, '^');
                TestReviewStaticMembers.QANUmbersString = CurrentQANumberString;
                AnswerQuestions('Q');
                return;
            }
           if(TestReviewStaticMembers.QANUmbersString.Length == 0)
            {
                MessageBox.Show("This was the last QA Pair, Save  or Reset the data");
            }
                
            
        }




        #endregion Test/Review Public Method

        #endregion  Public Methods

    }//End TestReview User Control
}// End NameSpace NewSP.UserControl
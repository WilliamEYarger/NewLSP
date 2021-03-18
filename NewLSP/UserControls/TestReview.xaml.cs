﻿
using System.IO;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using System;

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
            string ResultsDirectoryPath = SubjectStaticMembers.SaveSubjectFolderPath+"QAResults";
            if (!Directory.Exists(ResultsDirectoryPath))
            {
                Directory.CreateDirectory(ResultsDirectoryPath);
            }
            //check to see if the NodeID result .txt file exists and if not create

            //Get  DataNode.ID.ToString()
            string DataNodeIDStr = SubjectStaticMembers.DataNode.ID.ToString();

            //Create File path
            string ResultsFilePath = ResultsDirectoryPath + "\\" + DataNodeIDStr + ".txt";


            StreamWriter sw = File.AppendText(ResultsFilePath);
            DateTime currentDate = DateTime.Now;


            string FormattedDateStr = String.Format("YYYYMMDDHHmm", currentDate);
            int total = TestReviewStaticMembers.QADictionary.Count;
            int wrong = TestReviewStaticMembers.NumberOfWrongAnswers;
            double PercentCorrect = ((total - wrong) / total)*100;

            string PercentCorrectStr = string.Format("{0:N2}% correct = ", PercentCorrect);

            string OutputStr = FormattedDateStr + " " + PercentCorrectStr + " " + TestReviewStaticMembers.DelimitedWrongAnswersStr;
            sw.WriteLine(OutputStr);

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


        #region Instructions

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
                    TestReviewStaticMembers.Mp3Url = TestReviewStaticMembers.Mp3Url;
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
        /// </summary>
        internal  void TestOrReview()
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
            while (TestReviewStaticMembers.QANUmbersString.Length != 0)
            {
                string CurrentQANumberString = TestReviewStaticMembers.QANUmbersString;
                // get and remove the 0th item from the QANumberString
                string thisKey = StringHelper.GetAndRemoveNthItem(ref CurrentQANumberString, '^', 0);
                TestReviewStaticMembers.QANUmbersString = CurrentQANumberString;
                //Save the current key so that if this is a review and the answer is wrong
                // it can be appended to the end of the CurrentQANumberString
                TestReviewStaticMembers.CurrentQuestionNumberString = thisKey;
                TestReviewStaticMembers.SetCurrentQAValues(thisKey);                
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
    }
}

using System;
using System.Collections.Generic;
using NewLSP.DataModels;
using System.IO;
using NewLSP.UserControls;

namespace NewLSP.StaticHelperClasses
{
    public static class TestReviewStaticMembers
    {
        #region private properties and fields


        #region Dictionary

        // Create an instance of the QADictionary to hold all of the question-answer data
        private static Dictionary<string, QADataModel> QADictionary = new Dictionary<string, QADataModel>();

        #endregion Dictionary

        #region This QADataMobelObject

        private static QADataModel ThisQADataModelObject;
        #endregion This QADataMobelObject

        #region QANUmbersString property

        private static string _QANUmbersString;

        public static string QANUmbersString
        {
            get { return _QANUmbersString; }
            set { _QANUmbersString = value; }
        }

        #endregion QANUmbersString property


        private static TestReview ThisTextReviewObject = new TestReview();




        #endregion  private properties and fields

        #region Public Propeties

        #region ThisQuestion

        private static string _ThisQuestion;

        public static string ThisQuestion
        {
            get { return _ThisQuestion; }
            set { _ThisQuestion = value; }
        }

        #endregion ThisQuestion


        #region ThisAnswer
        private static string _ThisAnswer;

        public static string ThisAnswer
        {
            get { return _ThisAnswer; }
            set { _ThisAnswer = value; }
        }


        #endregion ThisAnswer

        #region ThisQuestionJPG

        private static string _ThisQuestionJPG;

        public static string ThisQuestionJPG
        {
            get { return _ThisQuestionJPG; }
            set { _ThisQuestionJPG = value; }
        }


        #endregion ThisQuestionJPG

        #region ThisQuestionMp3Url

        private static string _ThisQuestionMp3Url;

        public static string ThisQuestionMp3Url
        {
            get { return _ThisQuestionMp3Url; }
            set { _ThisQuestionMp3Url = value; }
        }


        #endregion ThisQuestionMp3Url

        #region ThisAnswerJpgUrl

        private static string _ThisAnswerJpgUrl;

        public static string ThisAnswerJpgUrl
        {
            get { return _ThisAnswerJpgUrl; }
            set { _ThisAnswerJpgUrl = value; }
        }


        #endregion ThisAnswerJpgUrl

        #region ThisAnswerMp3Url
        private static string _ThisAnswerMp3Url;

        public static string ThisAnswerMp3Url
        {
            get { return _ThisAnswerMp3Url; }
            set { _ThisAnswerMp3Url = value; }
        }


        #endregion ThisAnswerMp3Url

       


        #region ThisIsATest boolean property


        private static bool _ThisIsATest = false;

        public static bool ThisIsATest
        {
            get { return _ThisIsATest; }
            set { _ThisIsATest = value; }
        }

        #endregion ThisIsATest boolea

        #endregion Public Propeties


        #region Public Methods


        #region Public StartNewQATestReview

        public static void  StartNewQATestReview()
        {
            SetupDictionaryAndQAString();
        }

        #endregion  Public StartNewQATestReview


        #region public SetupDictionaryAndQAString()
        /// <summary>
        /// This method reads in a QA file text file and creates
        /// the QADictionary and a QANUmbersString
        /// It should be called by a StartANewQATestReview in a 
        /// TestReviewStaticMembers class
        /// </summary>
        public static void SetupDictionaryAndQAString()
        {
           

            // Clear any previous values in the QANumberString
            QANUmbersString = "";

            // Read all of the lines in the qa file into an array
            string[] QALinesArray = File.ReadAllLines(SubjectStaticMembers.GetDataNodesQAFilePath());

            //process each delimited line converting it into a key(question number)
            // and value QADataModel object

            foreach (string line in QALinesArray)
            {
                // split this string on ^
                string[] thisQALineArray = line.Split('^');
                // Create a new QADataModel object
                QADataModel qADataModel = new QADataModel();

                //Get the key and store it
                string Key = thisQALineArray[0];


                qADataModel.QANumber = Int32.Parse(Key);

                qADataModel.Question = thisQALineArray[1];

                qADataModel.Answer = thisQALineArray[2];

                qADataModel.QuestionJpgUrl = thisQALineArray[3];

                qADataModel.QuestionMp3Url = thisQALineArray[4];

                qADataModel.AnswerJpgUrl = thisQALineArray[5];

                qADataModel.AnswerMp3Url = thisQALineArray[6];

                //Add qADataModel to the QADictionary
                QADictionary.Add(Key, qADataModel);

                // add the question number to the QANUmbersString
                QANUmbersString = QANUmbersString + Key + '^';

            }// End for each line

            ////remove the terminal ^ from QANUmbersString
            //QANUmbersString = QANUmbersString.Substring(0, QANUmbersString.Length - 1);

        }// end SetupDictionaryAndQAString

        #endregion pubic SetupDictionaryAndQAString()

        public static void AnswerQuestions()
        {
            int cntr = 0;
            while(QANUmbersString.Length > 0)
            {
                string CurrentQANumberString = QANUmbersString;
                // get and remove the 0th item from the QANumberString
                string thisKey = StringHelper.GetAndRemoveNthItem(ref CurrentQANumberString, '^', 0);

                // Use thisKey to get the delimited components of a QADataModel object
                //[] thisQADataModelArray = QADictionary

                ThisQADataModelObject = QADictionary[thisKey];
              
                ThisQuestion = ThisQADataModelObject.Question;

                ThisAnswer = ThisQADataModelObject.Answer;

               
                ThisQuestionJPG = ThisQADataModelObject.QuestionJpgUrl;
               

                ThisQuestionMp3Url = ThisQADataModelObject.QuestionMp3Url;

                ThisAnswerJpgUrl = ThisQADataModelObject.AnswerJpgUrl;

                ThisAnswerMp3Url = ThisQADataModelObject.AnswerMp3Url;

                return;


                //Convert the delimited string into a global QADataModel object for use in 
                //processing this QA line
            }
        }

        #endregion Public Methods


    }// End TestReviewStaticMembers class
}// End namespace NewLSP.StaticHelperClasses

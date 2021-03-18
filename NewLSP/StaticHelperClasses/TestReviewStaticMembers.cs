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

        #region CurrentQuestionNumberString

        private static string _CurrentQuestionNumberString;

        public static string CurrentQuestionNumberString
        {
            get { return _CurrentQuestionNumberString; }
            set { _CurrentQuestionNumberString = value; }
        }


       

        #endregion CurrentQuestionNumberString


        #region Dictionary

        // Create an instance of the QADictionary to hold all of the question-answer data
        public static Dictionary<string, QADataModel> QADictionary = new Dictionary<string, QADataModel>();

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


        #region CurrentKeyValue
        private static string CurrentKeyValue;

        #endregion CurrentKeyValue

        private static TestReview ThisTextReviewObject = new TestReview();


        private static int _NumberOfWrongAnswers =0;

        public static int NumberOfWrongAnswers
        {
            get { return _NumberOfWrongAnswers; }
            set { _NumberOfWrongAnswers = value; }
        }

        private static string _DelimitedWrongAnswersStr;

        public static string DelimitedWrongAnswersStr
        {
            get { return _DelimitedWrongAnswersStr; }
            set { _DelimitedWrongAnswersStr = value; }
        }


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


        //private static bool _ThisIsATest = false;

        //public static bool ThisIsATest
        //{
        //    get { return _ThisIsATest; }
        //    set { _ThisIsATest = value; }
        //}

        #endregion ThisIsATest boolean

        #region ThereIsAnImageFile boolean property
        private static  bool _ThereIsAnImageFile;

        public static bool ThereIsAnImageFile
        {
            get { return _ThereIsAnImageFile; }
            set { _ThereIsAnImageFile = value; }
        }

        #region public string JpgUrl

        private static string _JpgUrl;

        public static string JpgUrl
        {
            get { return _JpgUrl; }
            set { _JpgUrl = value; }
        }

        #endregion

        #region public string Mp3Url
        private static string _Mp3Url;

        public static string Mp3Url
        {
            get { return _Mp3Url; }
            set { _Mp3Url = value; }
        }

        #endregion public string Mp3Url

        #endregion ThereIsAnImageFile boolean property

        //ThereIsAnImageFile and ThereIsASoundFile

        #region ThereIsASoundFile boolean

        private static bool _ThereIsASoundFile;

        public static bool ThereIsASoundFile
        {
            get { return _ThereIsASoundFile; }
            set { _ThereIsASoundFile = value; }
        }

        #endregion ThereIsASoundFile boolean

        #endregion Public Propeties


        #region Public Methods


        #region  public InitializeData Method

        /// <summary>
        /// This Method downloads and initializes the 
        /// dictionary, as well a creates and initialized
        /// NumberOfWrongAnswers, DelimitedWrongAnswersStr
        /// and QANUmbersString
        /// References:
        ///     1.  Mainwindow.xaml.cs miTest_Click(
        /// </summary>
        internal static void InitializeData()
        {

            QANUmbersString = "";
            NumberOfWrongAnswers = 0;
            DelimitedWrongAnswersStr = "";
            SetupDictionaryAndQAString();

        }

        #endregion public InitializeData Method



        #region Public StartNewQATestReview

        //public static void  StartNewQATestReview()
        //{
        //    SetupDictionaryAndQAString();
        //}

        #endregion  Public StartNewQATestReview


        #region Reset Media Fields

        /// <summary>
        /// This method returns all media related fields 
        /// to their initial state
        /// </summary>
        internal static void ResetMediaFields()
        {
            ThereIsAnImageFile = false;
            ThereIsASoundFile = false;
            JpgUrl = "";
            Mp3Url = "";
        }

        #endregion Reset Media Fields




        public static void SetCurrentQAValues(string thisKey)
        {
            
            while(QANUmbersString.Length > 0)
            {
                // Save thisKey as the CurrentKeyValue so it can be added to the QANUmbersString if necessary

                CurrentKeyValue = thisKey;

                //SET TestReviewStaticMembers.CurrentQuestionNumberString
                CurrentQuestionNumberString = thisKey;

                //Save all of the current dictionary item's data as properties                

                ThisQADataModelObject = QADictionary[thisKey];
              
                ThisQuestion = ThisQADataModelObject.Question;

                ThisAnswer = ThisQADataModelObject.Answer;
               
                ThisQuestionJPG = ThisQADataModelObject.QuestionJpgUrl;               

                ThisQuestionMp3Url = ThisQADataModelObject.QuestionMp3Url;

                ThisAnswerJpgUrl = ThisQADataModelObject.AnswerJpgUrl;

                ThisAnswerMp3Url = ThisQADataModelObject.AnswerMp3Url;

                return;

            }
        }

        #endregion Public Methods

        #region Private Methods

        #region private SetupDictionaryAndQAString()
        /// <summary>
        /// This method reads in a QA file text file and creates
        /// the QADictionary and a QANUmbersString
        /// It should be called by a StartANewQATestReview in a 
        /// TestReviewStaticMembers class
        /// References:
        ///     1. Self InitializeData()
        /// </summary>
        private static void SetupDictionaryAndQAString()
        {

            // Clear any previous values in the QANumberString


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

            // Call AnswerQuestions to load the first item in the dictionary
            SetCurrentQAValues("0");

        }// end SetupDictionaryAndQAString

        #endregion pubic SetupDictionaryAndQAString()
        #endregion Private Methods



    }// End TestReviewStaticMembers class
}// End namespace NewLSP.StaticHelperClasses

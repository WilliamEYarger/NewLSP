
using System.Collections.Generic;
using NewLSP.DataModels;
using System.IO;
using System;

namespace NewLSP.StaticHelperClasses
{
    public static class QAStaticMembers
    {

        #region private  fields and properties

        #region Path to the QA file field private variable

        /// <summary>
        /// This is the path to the QA File that holds 
        /// the answers and questions 
        /// Used by:
        ///     internal static void CreateNewQAFile() in QAStaticMembers
        /// </summary>
        public static string dataNodesQAFilePath;

        #endregion Path to the QA file field private variable

        #endregion private fields and properties

        #region public fields and properties


        #region QADictionary public field

        /// <summary>
        /// This dictionary uses the question number as a string
        /// as the Key and an instation of a QADataModel object
        /// as the value
        /// </summary>
        public static Dictionary<string, QADataModel> QADictionary = new Dictionary<string, QADataModel>();
        #endregion QADictionary public field

        //#region QANUmbersString property

        //private static string _QANUmbersString;

        //public static string QANUmbersString
        //{
        //    get { return _QANUmbersString; }
        //    set { _QANUmbersString = value; }
        //}

        //#endregion QANUmbersString property

        #region CurrentQANumberInt property

        /// <summary>
        /// gets and sets the Current QANumber integer
        /// </summary>
        private static int _CurrentQANumberInt = 0;

        public static int CurrentQANumberInt
        {
            get { return _CurrentQANumberInt; }
            set { _CurrentQANumberInt = value; }
        }
        #endregion CurrentQANumberInt property

        #endregion public fields and properties


        #region Public methods

        #region Return DataModel obect in Array public method

        /// <summary>
        /// This method receives a key to a QAMOdelObject as a string
        /// and returns the QAModelObject associted with it in the
        /// QADictionary
        /// </summary>
        /// <param name="NumberKey"></param>
        /// <returns></returns>
        public static QADataModel ReturnQAObject(string NumberKey)
        {
            QADataModel qADataModelObject = QADictionary[NumberKey];

            return qADataModelObject;

        }


        #endregion Return DataModel obect in Array


        #region CreateNewQAFile public method

        /// <summary>
        /// Check to insure that the file doesn't exist and then creat it
        /// Called by CreateEditQA.xaml.cs
        /// </summary>
        internal static void CreateNewQAFile()
        {
            // Get DataNodesQAFielPath
            /* 
             * System.IO.DirectoryNotFoundException: 'Could not find a part of the path
             * 'C:\Users\Owner\OneDrive\Documents\_StudyFolder\Sub0323\QAFiles\1.txt'.'

             */
            dataNodesQAFilePath = SubjectStaticMembers.GetDataNodesQAFilePath();
            File.Create(dataNodesQAFilePath);


        }


        #endregion CreateNewQAFile

        #region Check to see if QAFile exists

        /// <summary>
        /// This method used the path to the currently
        /// selected DataNode and returns true if
        /// a file already exists or false if it doesn't
        /// Used by CreateEditQA.xaml.cs
        /// </summary>
        /// <returns></returns>
        public static bool DoesQAFileExist()
        {
            if (File.Exists(SubjectStaticMembers.GetDataNodesQAFilePath()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Check to see if QAFile exists


        #region Add a new QADataModel object to the Dictionary

        /// <summary>
        /// Receives a question number as a string and a QADataModel object
        /// and stores them in the QADictionary
        /// </summary>
        /// <param name="NumberKey"></param>
        /// <param name="qADataModelObject"></param>
        public static void AddQAObjectToDictionary(string NumberKey, QADataModel qADataModelObject)
        {
            QADictionary.Add(NumberKey, qADataModelObject);

        }// End AddQAObjectToDictionary

        #endregion Add a new QADataModel object to the Dictionary

        #region Save the QADictionary to an external .txt. file
        /// <summary>
        /// This method saves the current QADictionary to a text file
        /// Called By QACreateEdit.xaml.cs
        /// </summary>
        internal static void SaveQADictionary()
        {
            // Create a List<string> to hold the delimited QAFile lines
            List<string> QALines = new List<string>();

            // cycle thru the QADictionary creating and storing the QALines
            foreach (KeyValuePair<string, QADataModel> KVP in QADictionary)
            {
                string key = KVP.Key;
                QADataModel thisQAObject = KVP.Value;
                string OutputString = key + '^' + ConvertQADataObjectToDelimitedString(thisQAObject);
                QALines.Add(OutputString);
            }

            File.WriteAllLines(dataNodesQAFilePath, QALines);
        }


        #endregion Save the QADictionary 


        //#region pubic SetupDictionaryAndQAString()
        ///// <summary>
        ///// This method reads in a QA file text file and creates
        ///// the QADictionary and a QANUmbersString
        ///// It should be called by a StartANewQATestReview in a 
        ///// TestReviewStaticMembers class
        ///// </summary>
        //public static void SetupDictionaryAndQAString()
        //{
        //    // Create an instance of the QADictionary to hold all of the question-answer data
        //    Dictionary<string, QADataModel> QADictionary = new Dictionary<string, QADataModel>();

        //    // Clear any previous values in the QANumberString
        //    QANUmbersString = "";

        //    // Read all of the lines in the qa file into an array
        //    string[] QALinesArray = File.ReadAllLines(dataNodesQAFilePath);

        //    //process each delimited line converting it into a key(question number)
        //    // and value QADataModel object

        //    foreach(string line in QALinesArray)
        //    {
        //        // split this string on ^
        //        string[] thisQALineArray = line.Split('^');
        //        // Create a new QADataModel object
        //        QADataModel qADataModel = new QADataModel();

        //        //Get the key and store it
        //        string Key = thisQALineArray[0];


        //        qADataModel.QANumber = Int32.Parse(Key);

        //        qADataModel.Question = thisQALineArray[1];

        //        qADataModel.Answer = thisQALineArray[2];

        //        qADataModel.QuestionJpgUrl = thisQALineArray[3];

        //        qADataModel.QuestionMp3Url = thisQALineArray[4];

        //        qADataModel.AnswerJpgUrl = thisQALineArray[5];

        //        qADataModel.AnswerMp3Url = thisQALineArray[6];

        //        //Add qADataModel to the QADictionary
        //        QADictionary.Add(Key, qADataModel);

        //        // add the question number to the QANUmbersString
        //        QANUmbersString = QANUmbersString + Key + '^';

        //    }// End for each line

        //}// end SetupDictionaryAndQAString

        //#endregion pubic SetupDictionaryAndQAString()

        #endregion Public methods


        #region Private Methods

        #region Convert QADataModel object to delimited string

        /// <summary>
        /// This method converts a QADataModel object into a ^
        /// delimited string for external storage in a .txt file
        /// </summary>
        /// <param name="qADataModel"></param>
        /// <returns></returns>
        private static string ConvertQADataObjectToDelimitedString(QADataModel qADataModel)
        {
            string ReturnString = qADataModel.Question + '^' + qADataModel.Answer + '^' + qADataModel.QuestionJpgUrl +
                '^' + qADataModel.QuestionMp3Url + '^' + qADataModel.AnswerJpgUrl + '^' + qADataModel.AnswerMp3Url;

            return ReturnString;
        }

        #endregion Convert QADataModel object to delimited string

        #endregion Private Methods
    }
}

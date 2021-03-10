
using System;
using System.Collections.Generic;
using NewLSP.DataModels;
using System.IO;

namespace NewLSP.StaticHelperClasses
{
    public static class QAStaticMembers
    {

        #region Static fields and methods

        #region QADictionary

        public static Dictionary<string, QADataModel> QADictionary = new Dictionary<string, QADataModel>();
        #endregion QADictionary

        private static string dataNodesQAFielPath;

        #region Add a new QADataModel object to the Dictionary

        public static void AddQAObjectToDictionary(string NumberKey, QADataModel qADataModelObject)
        {
            QADictionary.Add(NumberKey, qADataModelObject);

            ////Add the question
            //thisQAObjectString = thisQAObjectString + qADataModelObject.Question + '^';
            ////Add the answer
            //thisQAObjectString = thisQAObjectString + qADataModelObject.Answer + '^';
            ////Add the QuestionJpgUrl
            //thisQAObjectString = thisQAObjectString + qADataModelObject.QuestionJpgUrl + '^';
            ////Add the QuestionMp3Url
            //thisQAObjectString = thisQAObjectString + qADataModelObject.QuestionMp3Url + '^';
            ////Add the AnswerJpgUrl
            //thisQAObjectString = thisQAObjectString + qADataModelObject.AnswerJpgUrl + '^';
            ////Add the AnswerMp3Url
            //thisQAObjectString = thisQAObjectString + qADataModelObject.AnswerMp3Url + '^';

            //// Add the Key and the value to the Dictionary
            //QADictionary.Add(NumberKey, thisQAObjectString);

        }// End AddQAObjectToDictionary


        /// <summary>
        /// This method saves the current QADictionary to a text file
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

            File.WriteAllLines(dataNodesQAFielPath, QALines);
        }



        #endregion Add a new QADataModel object to the Dictionary

        #region Return DataModel obect in Array

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


        #region CurrentQANumberInt

        private static int _CurrentQANumberInt = 0;

        public static int CurrentQANumberInt
        {
            get { return _CurrentQANumberInt; }
            set { _CurrentQANumberInt = value; }
        }
        #endregion CurrentQANumberInt


        #region CreateNewQAFile

        /// <summary>
        /// Check to insure that the file doesn't exist and then creat it
        /// </summary>
        internal static void CreateNewQAFile()
        {
            // Get DataNodesQAFielPath

            dataNodesQAFielPath = SubjectStaticMembers.GetDataNodesQAFilePath();
            File.Create(dataNodesQAFielPath);


        }


        #endregion CreateNewQAFile

        #region Check to see if QAFile exists


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


        #endregion Static fields and methods


        #region Private Methods

        private static string ConvertQADataObjectToDelimitedString(QADataModel qADataModel)
        {
            string ReturnString = qADataModel.Question + '^' + qADataModel.Answer + '^' + qADataModel.QuestionJpgUrl +
                '^' + qADataModel.QuestionMp3Url + '^' + qADataModel.AnswerJpgUrl + '^' + qADataModel.AnswerMp3Url;

            return ReturnString;
        }

        #endregion Private Methods
    }
}

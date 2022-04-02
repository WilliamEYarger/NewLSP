
using System.Collections.Generic;
using NewLSP.DataModels;
using System.IO;
using System;
using System.Windows;

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
        //public static string dataNodesQAFilePath;

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



        #region Public property dictionary changed

        private static bool _DictionaryChanged = false;

        public static bool DictionaryChanged
        {
            get { return _DictionaryChanged; }
            set { _DictionaryChanged = value; }
        }


        #endregion Public property dictionary change



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
        /// Called by CreateEditQA.xaml.cs NewFile_Click(
        /// it 1: Sets the dataNodesQAFilePath and then passes
        /// that to the File.Create procedure in system.IO
        /// </summary>
        internal static void CreateNewQAFile()
        {
            // Get DataNodesQAFielPath

          
            string dataNodesQAFilePath = SubjectStaticMembers.GetDataNodesQAFilePath();
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

            string dataNodesQAFilePath = SubjectStaticMembers.GetDataNodesQAFilePath();
            File.WriteAllLines(dataNodesQAFilePath, QALines);
        }


        #endregion Save the QADictionary 

        #region Replace an Entry in the QA Dictionary

        public static void ReplaceThisQADataModel(string placeKey, QADataModel thisQADataModel)
        {
            QADictionary[placeKey] = thisQADataModel;
        }// End
        #endregion  Replace an Entry in the QA Dictionary


        #region Read the QAFile into the Dictionary ReadQAFileIntoDictionary()
        /// <summary>
        /// This methods read the data in the QAFile text file and enters it into the QAStaticMembers.QADictionar
        /// </summary>
        public static void ReadQAFileIntoDictionary()
        {
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

                string question = thisQALineArray[1];
                question = question.Replace("~", "\r\n");
                qADataModel.Question = question;

                string answer = thisQALineArray[2];
                answer = answer.Replace("~", "\r\n");
                qADataModel.Answer = answer;
                try
                {
                    qADataModel.QuestionJpgUrl = thisQALineArray[3];

                    qADataModel.QuestionMp3Url = thisQALineArray[4];

                    qADataModel.AnswerJpgUrl = thisQALineArray[5];

                    qADataModel.AnswerMp3Url = thisQALineArray[6];
                }
                catch (Exception e)
                {
                    string DataNodeId = SubjectStaticMembers.DataNode.ID.ToString();
                    MessageBox.Show("Cannot execute this step because of an error in QAFile "+ DataNodeId+".txt line " + line);
                    return;
                }

                //Add qADataModel to the QADictionary
                QAStaticMembers.QADictionary.Add(Key, qADataModel);

                // add the question number to the QANUmbersString
                //QANUmbersString = QANUmbersString + Key + '^';

            }// End for each line
        }
        #endregion Read the QAFile into the DictionaryReadQAFileIntoDictionary()

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

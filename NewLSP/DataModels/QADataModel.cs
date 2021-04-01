
namespace NewLSP.DataModels
{
    public class QADataModel
    {

        

        #region Properties

        #region QANumber

        /// <summary>
        /// QANumber is the sequential number on 
        /// qa pairs generated and will be
        /// used as the key the a dictionary
        /// holding QA member properties
        /// </summary>
        private int _QANumber;

        public int QANumber
        {
            get { return _QANumber; }
            set { _QANumber = value; }
        }
        #endregion QANumber

        #region Question
        private string _Question;

        public string Question
        {
            get { return _Question; }
            set { _Question = value; }
        }


        #endregion Question

        #region Answer
        private string _Answer;

        public string Answer
        {
            get { return _Answer; }
            set { _Answer = value; }
        }


        #endregion Answer

        #region QuestionJpgUrl
        /// <summary>
        /// Holds a reference to a jpg file that will
        /// be opened whenever a question loads or
        /// possible it activates a question has image button
        /// </summary>
        private string _QuestionJpgUrl;

        public string QuestionJpgUrl
        {
            get { return _QuestionJpgUrl; }
            set { _QuestionJpgUrl = value; }
        }

        #endregion QuestionJpgUrl

        #region QuestionMp3Url
        /// <summary>
        /// Contains a reference to  a mp3 file that
        /// will become availabe whenever a new 
        /// question is loaded
        /// </summary>
        private string _QuestionMp3Url;

        public string QuestionMp3Url
        {
            get { return _QuestionMp3Url; }
            set { _QuestionMp3Url = value; }
        }


        #endregion QuestionMp3Url

        #region AnswerJpgUrl
        /// <summary>
        /// contains a reference to an image that will become 
        /// available whenever the answer is loaded
        /// </summary>
        private string _AnswerJpgUrl;

        public string AnswerJpgUrl
        {
            get { return _AnswerJpgUrl; }
            set { _AnswerJpgUrl = value; }
        }


        #endregion AnswerJpgUrl

        #region AnswerMp3Url
        /// <summary>
        /// Contanins a referece to a mp3 file
        /// that will become available whenever an
        /// answer is loaded
        /// </summary>
        private string _AnswerMp3Url;

        public string AnswerMp3Url
        {
            get { return _AnswerMp3Url; }
            set { _AnswerMp3Url = value; }
        }
        


        #endregion AnswerMp3Url

        #endregion Properties




    }// End class QADataModel
}// End namespace NewLSP.DataModels

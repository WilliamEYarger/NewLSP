using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLSP.DataModels
{
    public class LinkNoteModel
    {
        #region HyperlinkObject Class


        /// <summary>
        /// LNSelectedFilePath objects hold the data for each hyperlink:
        /// 1) The File type (*.docx, *.jpg., http:\\.... etc
        /// </summary>
        public class HyperlinkObject
        {
            private string _BookMark;

            public string BookMark
            {
                get { return _BookMark; }
                set { _BookMark = value; }
            }


            private string _Url;

            public string Url
            {
                get { return _Url; }
                set { _Url = value; }
            }

            private string _FileType;

            public string FileType
            {
                get { return _FileType; }
                set { _FileType = value; }
            }


            private string _Name;

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

        }// End HyperlinkObject Class

        #endregion HyperlinkObject Class
    }
}

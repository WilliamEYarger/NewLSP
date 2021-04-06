using Microsoft.Win32;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using System.IO;
using NewLSP.DataModels;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for Link_Note.xaml
    /// </summary>
    public partial class Link_Note : UserControl
    {
        public Link_Note()
        {
            InitializeComponent();
        }

        #region Private Methods

        private string ReturnFilePath()
        {
            OpenFileDialog od = new OpenFileDialog();
            if (od.ShowDialog() == true) ;
            {
                return od.FileName;
            }

        }

        #endregion  Private Methods


        #region Menu Click Methods

        #endregion Menu Click Methods

        #region Applications Menu

        #region OpenFileDialog MenuItem


        /// <summary>
        /// This method gets a file path string by calling the
        /// ReturnFilePath() private method
        /// It then posts the hyperlink to tbxHyperlink.Txt
        /// It then gets the file type and posts it to LinkNoteStaticMembers.FileType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            string Hyperlink = ReturnFilePath();
            LinkNoteStaticMembers.Hyperlink = Hyperlink;


            // determine if this link is a web address
            if (Hyperlink.IndexOf("http") == 0)
            {
                //This is a web link
                LinkNoteStaticMembers.FileType = "Web";
                cmbxFileType.SelectedIndex = 2;
            }
            else
            {
                // This is some other type of file so get the file extenxtion

                //      get the position of the last '.'
                int posLastDot = Hyperlink.LastIndexOf('.');
                string FileExtension = Hyperlink.Substring(posLastDot + 1);
                switch (FileExtension)
                {
                    case "docx":
                        LinkNoteStaticMembers.FileType = "Word";
                        cmbxFileType.SelectedIndex = 0;
                        break;
                    case "txt":
                        LinkNoteStaticMembers.FileType = "Text";
                        cmbxFileType.SelectedIndex = 1;
                        break;
                    case "xlsx":
                        LinkNoteStaticMembers.FileType = "Excel";
                        cmbxFileType.SelectedIndex = 3;
                        break;
                    case "jpg":
                        LinkNoteStaticMembers.FileType = "Image";
                        cmbxFileType.SelectedIndex = 4;
                        break;
                    case "mp3":
                        LinkNoteStaticMembers.FileType = "Sound";
                        cmbxFileType.SelectedIndex = 5;
                        break;
                    case "mp4":
                        LinkNoteStaticMembers.FileType = "Video";
                        cmbxFileType.SelectedIndex = 6;
                        break;
                }


                tbxHyperlink.Text = Hyperlink;

            }
           

        }// End 


        #endregion OpenFileDialog MenuItem


        #region Word MenuItem

        private void miWord_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE");
        }// miWord_Click

        #endregion Word MenuItem


        #region Excel MenuItem
        private void miExcel_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE");
       
        }// End miExcel_Click

        #endregion  Excel MenuItem

        #region Windows Media Player MenuItem

       
        private void miWindowsMediaPlayer_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe");
        }

        #endregion Windows Media Player MenuItem


        #region FireFox MenuItem

        private void miFireFox_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Mozilla Firefox\firefox.exe");
        }
        #endregion FireFox MenuItem

        #region Notepad++ MenuItem

        private void Notepad_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe");
        }
        #endregion  Notepad++ MenuItem

        #endregion Applications Menu

        private void miHyperlink_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miHyperlink_Click");

        }

        private void miQAItem_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("miQAItem_Click");
        }

        private void miSaveHyperlink_Click(object sender, RoutedEventArgs e)
        {
            SaveHyperlink();
            return;

        }// End miSaveHyperlink_Click

        private void miSaveNote_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("miSaveNote_Click");
        }

        private void miSaveQAItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miSaveQAItem_Click");

        }

        private void miOpenHyperLink_Click(object sender, RoutedEventArgs e)
        {
            // Create a List<string> of Hyperlink display string
            //List<string> HyperlinkUrls = new List<string>();

            string DataNodesHyperlinkPath = SubjectStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

            //      b.  Test to see if a hyperlink file exists
            if (File.Exists(DataNodesHyperlinkPath))
            {
                lbxLinks.Items.Clear();
                foreach(string line in LinkNoteStaticMembers.HyperlinkUrls)
                {
                    lbxLinks.Items.Add(line);
                }
            }
        }

        private void miOpenNote_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("miOpenNote_Click");
        }

        private void miQAddImage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miQAddImage_Click");

        }

        private void miQAddMp3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miQAddMp3_Click");

        }





        private void miNote_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miNote_Click");
        }

        private void miQAddJpg_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miQAddJpg_Click");
        }

        private void miAAddJpg_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miAAddJpg_Click");
        }

        private void miAAddMp3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miAAddMp3_Click");
        }

        #region Select File Type from cmbxFileType ComboBox

        private void cmbxFileType_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string FileType = cmbxFileType.SelectedItem.ToString();
            LinkNoteStaticMembers.FileType = FileType;
        }


        #endregion  Select File Type from cmbxFileType ComboBox

        private void cmbxFileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem thisItem = (ComboBoxItem)cmbxFileType.SelectedItem;
            if (thisItem == null) return;
            string thisItemsTag = thisItem.Tag.ToString();
            LinkNoteStaticMembers.FileType = thisItemsTag;

        }

        private void miOpenHyperlink_Click_1(object sender, RoutedEventArgs e)
        {

        }


        #region Mouse up on List box of Links

        private void lbxLinks_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int ItemClicked = lbxLinks.SelectedIndex;
            LinkNoteModel.HyperlinkObject thisHyperlinkObject = LinkNoteStaticMembers.GetHyperlinkObject(ItemClicked);
            string LinkName = thisHyperlinkObject.Name;
            string Url = thisHyperlinkObject.Url;
            string BookMark = thisHyperlinkObject.BookMark;
            string Filetype = thisHyperlinkObject.FileType;

            // set the FiltType combo box
            switch (Filetype)
            {
                case "Word":
                    cmbxFileType.SelectedIndex = 0;
                    break;
                case "Web":
                    cmbxFileType.SelectedIndex = 1;
                    break;
                case "Excel":
                    cmbxFileType.SelectedIndex = 2;
                    break;
                case "Image":
                    cmbxFileType.SelectedIndex = 3;
                    break;
                case "Sound":
                    cmbxFileType.SelectedIndex = 4;
                    break;
                case "Video":
                    cmbxFileType.SelectedIndex = 5;
                    break;
                default: break;
            }// End Switch

            // set the hyperlink
            tbxHyperlink.Text = Url;

            // Set the BookMark
            tbxBookMark.Text = BookMark;

            if(tbxBookMark.Text != "")
            {
                Clipboard.SetText(tbxBookMark.Text);
            }

            //Open the hyperlink
            OpenAnApp(Url);

        }// End Mouse Up

        #endregion Mouse up on List box of Links

        #region Private Methods

        #region private method open an executable or specific file type

        private void OpenAnApp(string hyperlink)
        {
            System.Diagnostics.Process.Start(hyperlink);
        }



        #endregion private method open an executable or specific file type

        #region private method SaveHyperlink

        private void SaveHyperlink()
        {
            if (SubjectStaticMembers.DataNode == null)
            {
                MessageBox.Show("You cannot save this hyperlink because there is no designated DataNode");
                return;
            }
            if (tbxLinkName.Text == "")
            {
                MessageBox.Show("You cannot save this hyperlink because there is no Name");
                return;
            }

            // create a hyperlink delimited string
            // Get Hyperlink string fields

            //      Get any bookmakr if present
            string BookMark = tbxBookMark.Text;

            //      Get Name
            string HyperlinkName = tbxLinkName.Text;

            //      Get Url
            string Url = LinkNoteStaticMembers.Hyperlink;

            //      Get FileType
            string FileType = LinkNoteStaticMembers.FileType;

            string thisHyperlink = HyperlinkName + '^' + Url + '^' + FileType + '^' + BookMark;

            // Add thisHyperLink to  HyperlinkStrings           

            LinkNoteStaticMembers.AddHyperlinkToList(thisHyperlink);

            // Get the updated HyperlinkStringsList
            List<string> currentHyperlinkStringsList = LinkNoteStaticMembers.HyperlinkStringsList;

            // Create a string [] from HyperlinkStringsList
           // string[] HyperlinksArray = new string[4];


            // Create the filepath to the DataNodes HyperlinkFile
            string DataNodesHyperlinkPath = SubjectStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

            //Append this to the DataNode's Hyperlink file 
            File.WriteAllLines(DataNodesHyperlinkPath, currentHyperlinkStringsList);

            // Add this line to the Dictionary
            LinkNoteStaticMembers.HyperlinkDictionary.Clear();


            //For each line in currentHyperlinkStringsList get the component parts and convert them into a Dictionary value
            int HyperlinkCntr = 0;
            foreach (string line in currentHyperlinkStringsList)
            {
                string[] HyperlinkLineArray = line.Split('^');
                // create a new Hyperlink object
                LinkNoteModel.HyperlinkObject thisHyperlinkObject = new LinkNoteModel.HyperlinkObject();
                thisHyperlinkObject.Name = HyperlinkLineArray[0];
                thisHyperlinkObject.Url = HyperlinkLineArray[1];
                thisHyperlinkObject.FileType = HyperlinkLineArray[2];
                thisHyperlinkObject.BookMark = HyperlinkLineArray[3];

                //Add thisHyperlinkObject to the HyperlinkDictionary
                LinkNoteStaticMembers.HyperlinkDictionary.Add(HyperlinkCntr, thisHyperlinkObject);
                HyperlinkCntr++;
            }

            // Clear the lbxLinks listbox
            lbxLinks.Items.Clear();
            LinkNoteStaticMembers.BookMarks = new List<string>();


            // add the revised HyperlinkToList to the ListBox
            foreach (string line in LinkNoteStaticMembers.HyperlinkStringsList)
            {
                string[] linkSegments = line.Split('^');
                lbxLinks.Items.Add(linkSegments[0]);
                LinkNoteStaticMembers.BookMarks.Add(linkSegments[3]);
            }

            // Clear all the fields
            cmbxFileType.SelectedIndex = -1;
            tbxHyperlink.Text = "";
            tbxLinkName.Text = "";
            tbxBookMark.Text = "";

            return;
        }
        #endregion private method SaveHyperlink

        #region Open Hyperlink

        private void OpenHyperlink()
        {

        }
        #endregion Open Hyperlink

        #endregion Private Methods

    }// End class
}// End Name space

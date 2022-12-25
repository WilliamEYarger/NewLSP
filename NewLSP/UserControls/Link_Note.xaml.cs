using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using System.IO;
using NewLSP.DataModels;
using System;
using System.Windows.Input;

using System.Windows.Shapes;
using System.Linq;

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



        #region Menu Click Methods


        #region Applications Menu

        #region OpenFileDialog MenuItem


        /// <summary>
        /// This method is called when the user clicks the Open 'A File or Web Site' menu item 
        /// in the Applications Menu. It sets the LinkNoteStaticMembers.FileType for each particular type of file
        /// 
        /// hod which uses the OpenFileDialog to get the path 
        /// to a file. that the user wants to save as a hyperlink for a DataNode
        /// It then posts the hyperlink to tbxHyperlink.Txt
        /// It then gets the file type and posts it to LinkNoteStaticMembers.FileType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {

            LinkNoteStaticMembers.LNSelectedFilePath = ReturnFilePath();
            // determine if this link is a web address or a file
            if (LinkNoteStaticMembers.LNSelectedFilePath.IndexOf("http") == 0)
            {
                //This is a web link
                LinkNoteStaticMembers.FileType = "Web";
                cmbxFileType.SelectedIndex = 2;
            }
            else
            {
                // This is a file on the computer so get the file extenxtion 
                int posLastDot = LinkNoteStaticMembers.LNSelectedFilePath.LastIndexOf('.');
                string FileExtension = LinkNoteStaticMembers.LNSelectedFilePath.Substring(posLastDot + 1);
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
                tbxHyperlink.Text = LinkNoteStaticMembers.LNSelectedFilePath;
            }// End determine if this link is a web address or a file
        }// End miOpenFileDialog_Click

        #endregion OpenFileDialog MenuItem

        #region Word MenuItem

        /// <summary>
        /// This Method opens MS Word for the user to get or open an 
        /// existing .docx file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miWord_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE");
        }// miWord_Click

        #endregion Word MenuItem

        #region Excel MenuItem
        /// <summary>
        /// This method opens MS Excel which allows the user
        /// to open and existing or create a new excel file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExcel_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE");
        }// End miExcel_Click

        #endregion  Excel MenuItem

        #region Windows Media Player MenuItem
        /// <summary>
        /// This method calls the Windows Media Player so that the user can 
        /// play a mp4 file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void miWindowsMediaPlayer_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe");
        }

        #endregion Windows Media Player MenuItem

        #region FireFox MenuItem
        /// <summary>
        /// This method opens the FireFox Browser for the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miFireFox_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Mozilla Firefox\firefox.exe");
        }
        #endregion FireFox MenuItem

        #region Notepad++ MenuItem
        /// <summary>
        /// This method opens Notepad++ for the user of
        /// open or create a new text note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Notepad_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe");
        }
        #endregion  Notepad++ MenuItem

        #region Meun Item MS Paint
        /// <summary>
        /// This method opens MP paint 
        /// so the user can display a .jpg file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miPaint_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Windows\system32\mspaint.exe");
        }
        #endregion Meun Item MS Paint

        #endregion Applications Menu

        #region Files Menu

        #region Save Hyperlink MenuItem
        /// <summary>
        /// Called when the user clicks the SaveHyperlink menu item it calls 
        /// the  SaveHyperlink() local method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miSaveHyperlink_Click(object sender, RoutedEventArgs e)
        {
            SaveHyperlink();
            return;
        }// End miSaveHyperlink_Click

        #endregion Save Hyperlink MenuItem


        #region Save Note MenuItem

        /// <summary>
        /// This method Saves a Note when the program is in the Create mode.
        /// It can be called by the Link_Note.xaml's "miSaveNote" menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miSaveNote_Click(object sender, RoutedEventArgs e)
        {
            // Test to see if in Editing mode
            if (LinkNoteStaticMembers.EditingBoolean)
            {
                // create a NoteReference string
                string NoteReferenceStr = tbxLinkName.Text + "^" + tbxHyperlink.Text + "^" + tbxBookMark.Text + "^" + tbxAllKeyWords.Text;
                // Call the static method to save both old and new note files
                LinkNoteStaticMembers.SaveAndUpdateNoteReferenceAndKeywords(NoteReferenceStr);
                return;
            }
            // Make sure that all required items for the NoteReferenceStr are present
            if (KeyWordsStaticMembers.ListAccess)
            {
                // CREATE A NEW NOTE
                //  1.  Make sure that all required data fields are present
                if ((tbxLinkName.Text == "") || (tbxHyperlink.Text == "") || (tbxAllKeyWords.Text == ""))
                {
                    MessageBox.Show("You cannot save a Note unless there is a Name, a hyperlink and KeyWord(s)");
                    return;
                }
                //  2.  Make sure the Key words are delimited witb ;s
                if (!tbxAllKeyWords.Text.Contains(";"))
                {
                    MessageBox.Show("Key words must be delimited wint ';'s ");
                    return;
                }
                // create a NoteReference string
                string NoteReferenceStr = tbxLinkName.Text + "^" + tbxHyperlink.Text + "^" + tbxBookMark.Text + "^" + tbxAllKeyWords.Text;
                // Save the NoteReferenceStr as a file in the CommonStaticMember's NoteReference folder
                LinkNoteStaticMembers.SaveAndUpdateNoteReferenceAndKeywords(NoteReferenceStr);

                // Get the DataNodesNoteReferenceString and append it to the lbxOpenSelectedNote listBox
                string DataNodesNoteReferenceString = LinkNoteStaticMembers.DataNodesNoteReferenceString;

                string NoteName = StringHelper.ReturnItemAtPos(DataNodesNoteReferenceString, '^', 0);
                string NoteCurrentNote26Name = StringHelper.ReturnItemAtPos(DataNodesNoteReferenceString, '^', 1);

                // Delete the terminal  \r\n"
                NoteCurrentNote26Name = NoteCurrentNote26Name.Replace("\r\n", "");

                ////Test to see if there is a CommonStaticMembers.CurrentNote26Name and if not create one


                //  1.  Create a string of ' ' to make the NodeName length = 250
                int NumberOfSpaces = 250 - tbxLinkName.Text.Length;
                //  2.  create a string with this many spacdes
                string spacesStr = new String(' ', NumberOfSpaces);
                //  3.  Create the DataNodesReferenceFileLine 
                string displayString = NoteName + spacesStr + '^' + NoteCurrentNote26Name;

                // If not editing add this note to the listbox
                if (LinkNoteStaticMembers.EditingBoolean == false)
                {
                    // Add dispalySgtring to the lbxOpenSelectedNote
                    lbxOpenSelectedNote.Items.Add(displayString);
                }

                //Clear Note entry fields
                tbxLinkName.Text = "";
                tbxHyperlink.Text = "";
                tbxAllKeyWords.Text = "";
                lbxKeyWords.Items.Clear();
                tbxAllKeyWords.Text = "";
                tbxInput.Text = "";
                tbxBookMark.Text = "";
                // Set Editing to false and uncheck the rbtEdit
                LinkNoteStaticMembers.EditingBoolean = false;
                rbtEdit.IsChecked = false;
            }



        }// End miSaveNote_Click



        #endregion Save Note MenuItem


        #region Open Hyperlink MenuInte

        private void miOpenHyperLink_Click(object sender, RoutedEventArgs e)
        {

            // Create a List<string> of LNSelectedFilePath display string
            //List<string> HyperlinkUrls = new List<string>();

            string DataNodesHyperlinkPath = CommonStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

            //      b.  Test to see if a hyperlink file exists
            if (File.Exists(DataNodesHyperlinkPath))
            {
                // Read the hyperlinks file into the LinkNoteStaticMembers.HyperlinkDictionary

                // read all lines in the hyperlink file into a string []
                string[] HyperlinksArray = File.ReadAllLines(DataNodesHyperlinkPath);

                LinkNoteStaticMembers.HyperlinkDictionary.Clear();



                lbxLinks.Items.Clear();
                int HyperlinkCntr = 0;
                foreach (string line in HyperlinksArray)
                {
                    string Name = "";
                    string Url = "";
                    string FileType = "";
                    string BookMark = "";
                    try
                    {
                        // Get the component parts
                        string[] componentItems = line.Split('^');
                        Name = componentItems[0];
                        Url = componentItems[1];
                        FileType = componentItems[2];
                        BookMark = componentItems[3];
                    }
                    catch (Exception ex1)
                    {
                        MessageBox.Show("Cannot open this hyperlink because of " + ex1.Message);
                        return;
                    }

                    LinkNoteModel.HyperlinkObject hyperlinkObject = new LinkNoteModel.HyperlinkObject();
                    hyperlinkObject.Name = Name;
                    hyperlinkObject.Url = Url;
                    hyperlinkObject.FileType = FileType;
                    hyperlinkObject.BookMark = BookMark;
                    LinkNoteStaticMembers.AddItemToHyperlinkDictionary(HyperlinkCntr, hyperlinkObject);

                    lbxLinks.Items.Add(Name);
                    HyperlinkCntr++;
                }
            }
        }

        #endregion Open Hyperlink MenuInte

        #region Show MenuItem

        /// <summary>
        /// Called when the user clicks the "Show Notes" menu item in Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miShowNote_Click(object sender, RoutedEventArgs e)
        {
            // Get the Node ID for the selected data node
            int NodeID = SubjectStaticMembers.DataNode.ID;

            //check to see that this data node has a notes file and if so read its values into
            /*
            ListOfNoteNames = new List<string>();
            ListOfNoteHyperlinks = new List<string>();
            ListOfNoteBookMarks = new List<string>();
            ListOfNoteKeyWords 
             */
            if (CommonStaticMembers.NodeHasNoteFile(NodeID))
            {
                ReadNotesIntoSelectNoteListBox();
            }

        }

        #endregion Show Note MenuItem


        #region Display Note Names MenuItem

        /// <summary>
        /// This method is called when the user clicks the
        /// "Display Note Name MenuItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDisplayNoteNames_Click(object sender, RoutedEventArgs e)
        {
            ReadNotesIntoSelectNoteListBox();
            PopulateNoteListBox();
        }


        #endregion Display Note Names MenuItem

        #endregion Files Menu


        #region ResetPage Menu 
        /// <summary>
        /// The user clicks this menu item when they
        /// want to clear all the fields, properties and controls so 
        /// that a new DataNode can be seleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miResetPage_Click(object sender, RoutedEventArgs e)
        {
            //Clear the link to any file selected
            LinkNoteStaticMembers.LNSelectedFilePath = "";
            //Clear the text in the  tbxHyperlink Text Box
            tbxHyperlink.Text = "";
            lbxLinks.Items.Clear();
            tbxHyperlink.Text = "";
            cmbxFileType.SelectedIndex = -1;
            tbxLinkName.Text = "";
            tbxAllKeyWords.Text = "";
            lbxKeyWords.Items.Clear();
            lbxOpenSelectedNote.Items.Clear();
            tbxDisplayKeyWords.Text = "";
            tbxBookMark.Text = "";
            rbtAdd.IsChecked = false;
            rbtSearch.IsChecked = false;
            rbtEdit.IsChecked = false;
            LinkNoteStaticMembers.EditingBoolean = false;
            LinkNoteStaticMembers.SearchKeyWord = null;

            LinkNoteStaticMembers.HyperlinkDictionary.Clear();
            LinkNoteStaticMembers.HyperlinkStringsList.Clear();
        }


        #endregion ResetPage MenuItem

        #endregion Menu Click Methods

        #region Private Methods

        #region FileType combobox changed method

        /// <summary>
        /// When the File type is changed this method
        /// converts the combobox item tag to a string
        /// and sets the LinkNotesStaticMembers FileType to that string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbxFileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem thisItem = (ComboBoxItem)cmbxFileType.SelectedItem;
            if (thisItem == null) return;
            string thisItemsTag = thisItem.Tag.ToString();
            LinkNoteStaticMembers.FileType = thisItemsTag;

        }

        #endregion FileType combobox changed method

        #region Mouse up on List box of Links

        /// <summary>
        /// This method is called when the user clicks a hyperlink Name in 
        /// the lbxLinks List Box. It gets the HyperlinkObject
        /// associated with this name and get the url and book mark,
        /// which it copies to the clipboard and opens the ]hyperlink
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            if (tbxBookMark.Text != "")
            {
                Clipboard.SetText(tbxBookMark.Text);
            }

            //Open the hyperlink
            OpenAnApp(Url);

        }// End Mouse Up

        #endregion Mouse up on List box of Links


        #region private method open an executable or specific file type

        private void OpenAnApp(string hyperlink)
        {
            try
            {
                System.Diagnostics.Process.Start(hyperlink);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " = " + hyperlink);
            }
        }



        #endregion private method open an executable or specific file type

        #region private method SaveHyperlink
        /// <summary>
        /// Called by miSaveHyperlink_Click method
        /// It checks the validity of the hyperlink
        /// It then creates a sting to be stored in the DataNode's hyperlink file composed of
        /// HyperlinkName^Url^FileType^BookMark
        /// </summary>

        private void SaveHyperlink()
        {
            //Test to see that a DataNode has been chosed and that the hyperlink has a name
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
            //      Get any bookmakr if present
            string BookMark = tbxBookMark.Text;
            //      Get Name
            string HyperlinkName = tbxLinkName.Text;
            //      Get Url
            string Url = LinkNoteStaticMembers.LNSelectedFilePath;
            //      Get FileType
            string FileType = LinkNoteStaticMembers.FileType;
            string thisHyperlink = HyperlinkName + '^' + Url + '^' + FileType + '^' + BookMark;
            // Add thisHyperLink to  HyperlinkStrings 
            LinkNoteStaticMembers.AddHyperlinkToList(thisHyperlink);
            // Create the filepath to the DataNodes HyperlinkFile
            string DataNodesHyperlinkPath = CommonStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

            //Append this to the DataNode's LNSelectedFilePath file 
            File.WriteAllLines(DataNodesHyperlinkPath, LinkNoteStaticMembers.HyperlinkStringsList);

            // Add this line to the Dictionary
            LinkNoteStaticMembers.HyperlinkDictionary.Clear();


            //For each line in LinkNoteStaticMembers.HyperlinkStringsList get the component parts and convert them into a Dictionary value
            int HyperlinkCntr = 0;
            foreach (string line in LinkNoteStaticMembers.HyperlinkStringsList)
            {
                string[] HyperlinkLineArray = line.Split('^');
                // create a new LNSelectedFilePath object
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



        #region Return File Path of File Dialog OpenFile


        /// <summary>
        /// Uses the open file dialog to return a complete file path
        /// </summary>
        /// <returns></returns>
        private string ReturnFilePath()
        {
            OpenFileDialog od = new OpenFileDialog();
            if (od.ShowDialog() == true) ;
            {
                return od.FileName;
            }
        }

        #endregion Return File Path of File Dialog OpenFile



        #region Open Selected Note Left Mouse click

        /// <summary>
        /// This method opens the selected Note when
        /// the user left clicks on a selected note name
        /// It uses the text string of the selected item to get the NoteReferenceFile's CurrentNote26Name
        /// Which it then uses to open the NoteReferene file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxOpenSelectedNote_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string currnetItem = lbxOpenSelectedNote.SelectedItem.ToString();
            //Get the CurrentNote26Name of a NoteReferenceFiles text file
            string thisNoteReferenceName = StringHelper.ReturnItemAtPos(currnetItem, '^', 1);

            // Update the CommonStaticMembers.CurrentNote26Name
            CommonStaticMembers.CurrentNote26Name = thisNoteReferenceName;

            //Use this name to create the path to the NoteReferenceFile
            string NoteReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + thisNoteReferenceName + ".txt";

            // Create a string[] to hold the data
            string[] CurrentNoteReferenceFileDataArray = new string[4];
            //Open the file and read in its text
            if (File.Exists(NoteReferenceFilePath))
            {
                string thisNoteReferencesData = File.ReadAllText(NoteReferenceFilePath);
                CurrentNoteReferenceFileDataArray = thisNoteReferencesData.Split('^');
            }

            //Set the Editing Boolean to true
            LinkNoteStaticMembers.EditingBoolean = true;



            // get the values in  ListOfNoteNames, ListOfNoteHyperlinks, ListOfNoteBookMarks and ListOfNoteKeyWords
            // associated with NoteSelectedIndex
            string NoteName = CurrentNoteReferenceFileDataArray[0];
            string NoteHyperlink = CurrentNoteReferenceFileDataArray[1];
            string NoteBookmark = CurrentNoteReferenceFileDataArray[2];
            string NoteKeyWords = CurrentNoteReferenceFileDataArray[3];

            // Clear all Fields relative to a Note
            tbxLinkName.Text = "";
            tbxBookMark.Text = "";
            tbxHyperlink.Text = "";
            tbxAllKeyWords.Text = "";
            lbxLinks.Items.Clear();
            rbtEdit.IsChecked = true;

            //Fill in the fields 
            tbxLinkName.Text = NoteName;
            tbxBookMark.Text = NoteBookmark;
            tbxHyperlink.Text = NoteHyperlink;
            tbxAllKeyWords.Text = NoteKeyWords;

            //If there is no book mark change it from 

            if (NoteBookmark != null)
            {
                Clipboard.SetText(NoteBookmark);
            }
            else
            {
                NoteBookmark = "";
            }




            //open the hyperlink
            System.Diagnostics.Process.Start(NoteHyperlink);


        }

        #endregion Open Selected Note Left Mouse click


        #region Show Key Words of Right Selected Note Name
        /// <summary>
        /// This method uses the selected Note name right clicked in the OpenSelectedNote list box 
        /// to create a global string DelStrOfKeyWordsAndComments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxOpenSelectedNote_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //get the selected items text string
            string SelectedItemsText = lbxOpenSelectedNote.SelectedItem.ToString();

            // Get the Common references NoteReference file name from this string by calling the selected Note Reference file name which is off the visible page
            string NotereferenceFileName = StringHelper.ReturnItemAtPos(SelectedItemsText, '^', 1);

            // Use this value to set the CurrentNote26Name
            CommonStaticMembers.CurrentNote26Name = NotereferenceFileName;

            // Create a path to the NoteReferenceFile
            string NoteReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + NotereferenceFileName + ".txt";

            // Read in the text in the NoteReferenceFile into a single ';' delimited string storred as DelStrOfKeyWordsAndComments
            try
            {
                LinkNoteStaticMembers.DelStrOfKeyWordsAndComments = File.ReadAllText(NoteReferenceFilePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not find" + NoteReferenceFilePath);
                return;
            }

            // Create a string variable to hold the keywords and comments that will be displayed in the 
            //  tbxDisplayKeyWords TextBox
            string KeyWordsString = "";
            string tbxDisplayKeyWordsTextStr = "";

            if (LinkNoteStaticMembers.ShowAllKeywords)
            // The ShowAllKeywords radio button is clicked so show all keywords and comments
            {
                //Get the KeyWords section  
                KeyWordsString = StringHelper.ReturnItemAtPos(LinkNoteStaticMembers.DelStrOfKeyWordsAndComments, '^', 3);

                //Delete the last ';'
                KeyWordsString = KeyWordsString.Substring(0, KeyWordsString.Length - 1);

                // replace all ';' with \r\n
                KeyWordsString = KeyWordsString.Replace(";", "\r\n");

                // Display tbxDisplayKeyWords
                tbxDisplayKeyWords.Text = KeyWordsString;
            }
            else
            // the Show only selected Keywords radiobutton is checked so show only the selected KeyWord and its following '#' comments
            {
                KeyWordsString = StringHelper.ReturnItemAtPos(LinkNoteStaticMembers.DelStrOfKeyWordsAndComments, '^', 3);

                // Get the SelectedKeyWord
                string SearchKeyWord = LinkNoteStaticMembers.SearchKeyWord;

                if (SearchKeyWord == null)
                {
                    MessageBox.Show("You must have designated a single search key word.");
                    return;
                }

                // Find its position in the KeyWordsString
                int posKeyWord = StringHelper.GetItemNumberOfThisSubstring(KeyWordsString, SearchKeyWord, ';');
                if (posKeyWord != -1)
                {
                    // this is the position of the selected key word
                    tbxDisplayKeyWordsTextStr = SearchKeyWord + "\r\n";
                    // Set the position of the next item in the KeyWordsString
                    int nextItemPos = posKeyWord + 1;
                    string nextItem = StringHelper.ReturnItemAtPos(KeyWordsString, ';', nextItemPos);
                    while (nextItem.Substring(0, 1) == "#")
                    {
                        tbxDisplayKeyWordsTextStr = tbxDisplayKeyWordsTextStr + nextItem + "\r\n";
                        nextItemPos++;
                        nextItem = StringHelper.ReturnItemAtPos(KeyWordsString, ';', nextItemPos);
                        if (nextItem == "") break;
                    }
                    // Display tbxDisplayKeyWords
                    tbxDisplayKeyWords.Text = tbxDisplayKeyWordsTextStr;

                }
            }
        }// End lbxOpenSelectedNote_PreviewMouseRightButtonUp(






        #endregion Show Key Words of Right Selected Note Name

        #region Private Method to Read noteReference file into lbxOpenSelectedNote

        private void ReadNotesIntoSelectNoteListBox()
        {
            LinkNoteStaticMembers.ReadInNotesFile();

            // Clear lbxOpenSelectedNote and tbxDisplayKeyWords
            lbxOpenSelectedNote.Items.Clear();

            // Fill the list box with ListOfNoteNameAndFileNames
            foreach (string line in LinkNoteStaticMembers.ListBoxOfSelectedNotesList)
            {
                lbxOpenSelectedNote.Items.Add(line);
            }

        }

        #endregion Private Method to Read noteReference file into lbxOpenSelectedNote


        #region PopulateNoteListBox

        /// <summary>
        /// Called by  miDisplayNoteNames_Click(
        /// </summary>
        private void PopulateNoteListBox()
        {
            List<string> NoteNamesList = LinkNoteStaticMembers.ListOfNoteNames;
            lbxOpenSelectedNote.Items.Clear();
            foreach (string noteName in NoteNamesList)
            {
                lbxOpenSelectedNote.Items.Add(noteName);
            }

        }



        #endregion PopulateNoteListBox

        #endregion  Private Methods

        #region Link_Note UserControl Methods



        #region RadioButton Add
        /// <summary>
        /// Called when the  user checks the Add Radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtAdd_Click(object sender, RoutedEventArgs e)
        {
            btnRevert.Visibility = Visibility.Hidden;
            KeyWordsStaticMembers.ListAccess = true;
            lblKeyWordsAction.Content = "Add Key Words to a New Note Reference";

            btnRevert.Content = "Revert";
        }

        #endregion RadioButton Add


        #region Radio button Search

        /// <summary>
        /// Sets the ListAccess boolean to false because the program is 
        /// in the Search mode and new KeyWords are not allowed 
        /// It also sets the properts KeyWordSearch boolean to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtSearch_Click(object sender, RoutedEventArgs e)
        {
            tbxAllKeyWords.Text = "";
            KeyWordsStaticMembers.ListAccess = false;
            LinkNoteStaticMembers.KeyWordSearch = true;

        }

        #endregion Radio button Search


        #region Input Textbox Key Up Procedure

        //private static bool AddCommentToReusedKeyWord = false; //If true the comment should be added to the ReusedKeyWord
        private static bool LongKeyWordsOK = false;// if true Key words can be longer that 50 characters
        private static bool AddCommentToPreviouslyUsedKeyWord = false;
        private string ReusedKeyWord = "";//This keyword has beed entered more that once 
        private string CurrentKeyWord = "";


        private void tbxInput_KeyUp(object sender, KeyEventArgs e)
        {
            // local variables
            bool Comment = false;
            bool IsKeyWord = false;

            /* if (e.Key == Key.Back)
             * If the Receives Key e is Key.Back and if pressing it results in there
             * being no text in the tbxInput text box then remove any items that
             * may have been listed in the list of key words in the lbxKeyWords 
             * that previously started with the former text in tbxInput
            if (e.Key == Key.Back) */
            {
                if (tbxInput.Text == "") lbxKeyWords.Items.Clear();
            }

           
            
            tbxInput.Text.Trim();
            //Clear all the items in the list of key words

            //Determin whether a key word or a comment is being entered
            if (tbxInput.Text.IndexOf('#') == 0) { Comment = true; }
            else if ((tbxInput.Text.IndexOf('#') != 0) && (tbxInput.Text.Length > 0)) { IsKeyWord = true; }
            //else MessageBox.Show("There is an error");

            //A key word is being entered
            if (IsKeyWord)
            /*
             Goals to accomplish when a key word is being entered but the Enter key has not been pressed
                1.*  Make sure it doesn't contain a ';'
                2.*  List all keywords which start with the text in tbxInput
                3.*  If search is pressed and there are no matching key words WARN the user  
                4.*  If the length of the text is >50 and LongKeyWordsOK is false, then
                    ask the user it this is OK
                    a.  If it is then set LongKeyWordsOK to true and dont ask again WHILE THIS KEY WORD IN BEING ENTERED
                        !!! MAKE SURE TO SET LongKeyWordsOK TO FALSE AFTER THE ENTER KEY IS PRESSED AND THE KEY WORD IS PROCDESSED
            Goals to accomplish when a key word is being entered and the Enter key is pressed
                1.  Determin if this KeyWord is already present in tbxAllKeyWords. 
                    If it is:
                        a.  If it has set the ReusedKeyWord to the rbxInput text
                        b.  Set AddCommentToPreviouslyUsedKeyWord to true
                        c.  Clear the text in tbxInput and lbxKeyWords
                2.  It this keyword has not been used then:
                    a.  Add it to tbxAllKeyWords with a ';'
                    b.  Update List of KeyWords 
                    c.  Add a new item to the KeyWord Dictionary with this as the key
                    d.  Add this to the List of KeyWords
                    e.  Update the List and Sorted list text files
                    f.  Clear tbxInput and lbxKeyWords
                    
             */
            {// Beginning if (IsKeyWord)
                //If key != Enter
                if (e.Key != Key.Enter)
                {
                   
                    
                    if (!ContentValid(tbxInput.Text))
                    {
                        return;
                    }
                    //If the text is greater that 50 ask the user if this is OK
                    if( (tbxInput.Text.Length>50) && (!LongKeyWordsOK) ) SetLongKeyWordBool();
                  
                    //List all Key Words that begin with the text in the rbxInput TextBox
                    ShowKeyWordsBeginningWithThisString();
                    //If the user is searching and there are no KeyWords listed notify him, clear the input text and return
                    if ((lbxKeyWords.Items.Count == 0) && (rbtSearch.IsChecked == true)) 
                    {
                        ShowSearchKeyWordInvalid();
                        tbxInput.Text = "";
                        return;

                    }// End of if (e.Key != Key.Enter)
                    return;
                }// End if (e.Key != Key.Enter)
                else
                /*
                Goals to accomplish when a key word is being entered and the Enter key is pressed
                0*  Clear previously used globals
                1*  Depending on whether the lbxKeyWords is empty or not set CurrentKeyWord
                    a.  If this is a new Key Word add it to the appropriate data
                2.  Determin if this KeyWord is already present in tbxAllKeyWords. 
                    If it is:
                        a*  If it has set the ReusedKeyWord to the rbxInput text
                        b* Set AddCommentToPreviouslyUsedKeyWord to true
                        c*  Clear the text in tbxInput and lbxKeyWords
                        d*  return to the UI
                2.  Else  this keyword has not been used then:
                    a*  Add it to tbxAllKeyWords with a ';'
                    b*  Update List of KeyWords 
                    c*  Add a new item to the KeyWord Dictionary with this as the key
                    d*  Add this to the List of KeyWords
                    e  Update the List and Sorted list text files
                    f.  Clear tbxInput and lbxKeyWords                
                */

                {// The user is entering a KeyWord and the Enter Key was hit
                 //Added 20221111
                    if (tbxInput.Text.EndsWith(" "))
                    {
                        tbxInput.Text = tbxInput.Text.Trim();
                    }

                    //End added20221111

                    // Clear globals that may have been set previously
                    LongKeyWordsOK = false;
                    AddCommentToPreviouslyUsedKeyWord = false;
                    ReusedKeyWord = "";

                    //Depending on whether the lbxKeyWords is empty or not set CurrentKeyWord
                    if (lbxKeyWords.Items.Count != 0)
                    {
                        //If there are KeyWords in lbxKeyWord set Current key word to the 1st
                        CurrentKeyWord = GetFirstItemInLbxKeyWords();
                    }//End if(lbxKeyWords.Items.Count != 0)
                    else
                    {
                        //otherwise create a new Key word update the storage variables, clear input fields and return
                        CurrentKeyWord = tbxInput.Text;
                        AddInputTextToTBXAllKeyWords(CurrentKeyWord);
                        AddNewKeyWordToKeyWordList(CurrentKeyWord);
                        tbxInput.Text = "";
                        lbxKeyWords.Items.Clear();
                        return;
                    }

                    //Determine if this key word has already been used
                    if (ThisKeyWordHasAlreadyBeenUsed(CurrentKeyWord))
                    {
                        //This Key ward is already present in tbxAllKeyWords so set the appropriate variables
                        ReusedKeyWord = CurrentKeyWord;
                        AddCommentToPreviouslyUsedKeyWord = true;
                        //Clear tbxInput and lbxKeyWords
                        tbxInput.Text = "";
                        lbxKeyWords.Items.Clear();
                        return;
                    }// End if (ThisKeyWordHasAlreadyBeenUsed(CurrentKeyWord))
                    AddInputTextToTBXAllKeyWords(CurrentKeyWord);
                }// End   if (e.Key != Key.Enter) ELSE
            }// End if (IsKeyWord)

            //Process Comments
            if (Comment)
            {
                if (e.Key == Key.Enter)
                {
                    //Determine whether the comment contains a ‘;’ or a ‘#’ and if so will return the used to the main screen
                    if (!CommentIsValid(tbxInput.Text)) return;
                    // Entering a new Comment
                    /*  Determine if this comment should be added to a previously used key word
                        if so, then call AddThisCommentToTheReusedKeyWord and clear the varialbes*/
                    if (AddCommentToPreviouslyUsedKeyWord)
                    {
                        AddThisCommentToTheReusedKeyWord(tbxInput.Text);
                        tbxInput.Text = "";
                        lbxKeyWords.Items.Clear();
                        return;
                    }
                    else
                    { //Add this comment to tbxAllKeyWords.Text 
                        tbxAllKeyWords.Text = tbxAllKeyWords.Text + tbxInput.Text + ';';
                        tbxInput.Text = "";
                        lbxKeyWords.Items.Clear();
                        return;
                    }// End  if if (AddCommentToPreviouslyUsedKeyWord) ELSE

                } //End  if (e.Key == Key.Enter)
                else
                {
                    // Check to see if this is a multi comment
                    if (tbxInput.Text.IndexOf('~') != -1)
                    {
                        if (MessageBox.Show("Containe tildas '~'. Is this a Multi-Comment?", "Multi-Comment?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //change '~' to ';'
                            string oldInputText = tbxInput.Text;

                            string newInputText = oldInputText.Replace('~', ';');

                            tbxAllKeyWords.Text = tbxAllKeyWords.Text + newInputText;
                            tbxInput.Text = "";
                            return;
                        }
                        else
                        {

                            tbxInput.Text = "";
                            return;
                        }

                    }
                   



                }

                //Comment ADDED TO PREVIOUSLY USED KEY WORD, NEXT  MAKE THIS HAPPEN FOR A WORK CLICKED IN LBXKEYWORDS AND CLEAR VARIABLES WHEN A NEW KEY WORD IS ENETERE

            }// End if (Comment)
             //Verify that the text in the tbxInput TextBox is either a valid keyword or comment
            if (!ContentValid(tbxInput.Text)) return;//THIS IS PROBABLY NOT AT THE CORRECT POSITION

            // lbxKeyWords contains all of the keywords that start with the characters typed into the text box tbxInput
            //Clear the current content of lbxKeyWords 
            lbxKeyWords.Items.Clear();

            /*  Create a List of KeyWords whose leading chars match the substring in the input textbox
                Cycle through the list of all key words in the KeyWords dictionary
                and list, selecting all that begin with the characters in tbxInput
                and place them in the Listbox of matching key words so that the user
                can either select the 0th entry by hitting return or one down in 
                in the list by left clicking it
            */
            foreach (string line in KeyWordsStaticMembers.KeyWordList)
            {
                //In the current line begins with the characters in tbxInput add line to lbxKeyWords
                if (line.IndexOf(tbxInput.Text) == 0)
                {
                    lbxKeyWords.Items.Add(line);
                }
            }// End of foreach (string line in KeyWordsStaticMembers.KeyWordList)

            /* If the user hits the Enter key */
            if (e.Key == Key.Enter)
            {
                if (rbtEdit.IsChecked == true) KeyWordsStaticMembers.ListAccess = true;

                // !! CHECH TO SEE IF ANY SEARCH WORDS ARE PRESENT IN THE CURRENT LIST OF KEY WORDS !! //
                // If ListAccess is false, that means you are in the search mode 
                // If you are in the search mode and you type characters which do not occur at the start of 
                // any words in the listof KeyWords yoy get the following message
                //ListAccess is the abilits to access the current list of keywords
                if (!KeyWordsStaticMembers.ListAccess)
                {
                    // If lbxKeyWords is empty in the Search statge send a message that you can only accepts existing keywords
                    if (lbxKeyWords.Items.Count == 0)
                    {
                        MessageBox.Show("When You are in the Search mode you can only search for existing Keywords");
                        // return to UI
                        return;
                    }
                }// End of if (!KeyWordsStaticMembers.ListAccess)

                string ThisKeyWord = "";
                /* Determine if there are Keywords showing in lbxKeyWords then select #0,  
                 * Set  LinkNoteStaticMembers.SelectedKeyWord to this value, 
                 * Add the keyword to the tbxAllKeyWords.Text, 
                 * and return */
                if (lbxKeyWords.Items.Count != 0)
                //There is data in the lbxKeyWords ListBox
                {
                    // Create a list from the KeyWords in lbxKeyWords so that the 0th item can be chosen
                    List<string> myCurrentKeyWordsList = new List<string>();
                    // Populate  myCurrentKeyWordsList with the selected columns.
                    foreach (string thisKeyWordItem in lbxKeyWords.Items)
                    {
                        myCurrentKeyWordsList.Add(thisKeyWordItem);
                    }
                    //Set the Selected KeyWord to the 0th entry
                    ThisKeyWord = myCurrentKeyWordsList[0];
                    //Set the LinkNoteStaticMembers.SelectedKeyWord string to this value
                    LinkNoteStaticMembers.SelectedKeyWord = ThisKeyWord;
                    LinkNoteStaticMembers.SearchKeyWord = ThisKeyWord;
                    // error here
                    //20221225 change start
                    //if (LinkNoteStaticMembers.KeyWordSearch)
                    //    // add this Keyword to the list of selected keywords
                    //    //tbxAllKeyWords.Text = tbxAllKeyWords.Text + ThisKeyWord + ';';
                    //    //new
                    //    tbxAllKeyWords.Text = tbxAllKeyWords.Text + ';';
                    ////20221225 change end

                    //Clear tbxInput
                    tbxInput.Text = "";
                    //Clear lbxKeyWords
                    lbxKeyWords.Items.Clear();
                    //retrun  to UI
                    return;
                }// End of if (lbxKeyWords.Items.Count != 0)

                // If the program reaches this point, there is no data in the lbxKeyWords ListBox
                // Create a new KeyWord from the current text in tbxInput
                ThisKeyWord = tbxInput.Text;



                // Remove any leading or trailing spaces
                ThisKeyWord = ThisKeyWord.Trim();

                // Add this KeyWord to tbxAllKeyWords with a ';' seperator
                tbxAllKeyWords.Text = tbxAllKeyWords.Text + ThisKeyWord + ';';


                //!! DETECE COMMENTS AND DONT ADD THEM TO THE KEYWORD LIST 11 //
                // If this is a generic comment(ie it begins with #)  return  without adding it to the KeyWordList
                if (ThisKeyWord.IndexOf("#") != -1)
                {
                    tbxInput.Text = "";
                    return;
                }// End of if (ThisKeyWord.IndexOf("#") != -1)

                // !! UPDATE THE KEYWORD FILES !! //
                // Update the active KeyWordList
                KeyWordsStaticMembers.KeyWordList.Add(tbxInput.Text);
                // Append this new Keyword to the Keyword txt Fild
                KeyWordsStaticMembers.AppendNewKeyWord(ThisKeyWord);

                //Added 20211202
                KeyWordsStaticMembers.AppendNewSortedKeyWord(ThisKeyWord);
                //End added 20211202


                // Convert Keyword to Dictionary Item by replacing all spaces with '_'
                string thisKeyWord = tbxInput.Text;
                string ConvertedThisKeyWord = thisKeyWord.Replace(' ', '_');

                //Add the new converted Key word to the dictionary with a value containing only the starting delimiter, ;
                KeyWordsStaticMembers.KeyWordsDictionary.Add(ConvertedThisKeyWord, ";");

                // Add the new converted keyword to the NotesDictionary.txt file
                KeyWordsStaticMembers.AppendNewKeyWordDictionaryItemString(ConvertedThisKeyWord);

                tbxInput.Text = "";
            }// End of if (e.Key == Key.Enter)

            // Code to clear tbxInput if backspace results in empty text
            if (e.Key == Key.Back)
            {
                if (tbxInput.Text == "") lbxKeyWords.Items.Clear();
            }// End of if (e.Key == Key.Back)
        


            // !!!!! END OF 20220407 ADDITIONS 



            //Verify that the text in the tbxInput TextBox is either a valid keyword or comment
            if (!ContentValid(tbxInput.Text)) return;

            // lbxKeyWords contains all of the keywords that start with the characters typed into the text box tbxInput
            //Clear the current content of lbxKeyWords 
            lbxKeyWords.Items.Clear();

            /*  Create a List of KeyWords whose leading chars match the substring in the input textbox
                Cycle through the list of all key words in the KeyWords dictionary
                and list, selecting all that begin with the characters in tbxInput
                and place them in the Listbox of matching key words so that the user
                can either select the 0th entry by hitting return or one down in 
                in the list by left clicking it
            */
            foreach (string line in KeyWordsStaticMembers.KeyWordList)
            {
                //In the current line begins with the characters in tbxInput add line to lbxKeyWords
                if (line.IndexOf(tbxInput.Text) == 0)
                {
                    lbxKeyWords.Items.Add(line);
                }
            }

            /* If the user hits the Enter key */
                if (e.Key == Key.Enter)
            {
                if (rbtEdit.IsChecked == true) KeyWordsStaticMembers.ListAccess = true;

                // !! CHECH TO SEE IF ANY SEARCH WORDS ARE PRESENT IN THE CURRENT LIST OF KEY WORDS !! //
                // If ListAccess is false, that means you are in the search mode 
                // If you are in the search mode and you type characters which do not occur at the start of 
                // any words in the listof KeyWords yoy get the following message
                //ListAccess is the abilits to access the current list of keywords
                if (!KeyWordsStaticMembers.ListAccess)
                {
                    // If lbxKeyWords is empty in the Search statge send a message that you can only accepts existing keywords
                    if (lbxKeyWords.Items.Count == 0)
                    {
                        MessageBox.Show("When You are in the Search mode you can only search for existing Keywords");
                        // return to UI
                        return;
                    }
                }
                string KeyWord = "";
                /* Determine if there are Keywords showing in lbxKeyWords then select #0,  
                 * Set  LinkNoteStaticMembers.SelectedKeyWord to this value, 
                 * Add the keyword to the tbxAllKeyWords.Text, 
                 * and return */
                if (lbxKeyWords.Items.Count != 0)
                //There is data in the lbxKeyWords ListBox
                {
                    // Create a list from the KeyWords in lbxKeyWords so that the 0th item can be chosen
                    List<string> myCurrentKeyWordsList = new List<string>();
                    // Populate  myCurrentKeyWordsList with the selected columns.
                    foreach (string thisKeyWordItem in lbxKeyWords.Items)
                    {
                        myCurrentKeyWordsList.Add(thisKeyWordItem);
                    }
                    //Set the Selected KeyWord to the 0th entry
                    KeyWord = myCurrentKeyWordsList[0];
                    //Set the LinkNoteStaticMembers.SelectedKeyWord string to this value
                    LinkNoteStaticMembers.SelectedKeyWord = KeyWord;
                    LinkNoteStaticMembers.SearchKeyWord = KeyWord;
                    if (LinkNoteStaticMembers.KeyWordSearch)
                        // add this Keyword to the list of selected keywords
                        tbxAllKeyWords.Text = tbxAllKeyWords.Text + KeyWord + ';';
                    //Clear tbxInput
                    tbxInput.Text = "";
                    //Clear lbxKeyWords
                    lbxKeyWords.Items.Clear();
                    //retrun  to UI
                    return;
                }// End there is data in the lbxKeyWords ListBox
                // If the program reaches this point, there is no data in the lbxKeyWords ListBox
                // Create a new KeyWord from the current text in tbxInput
                KeyWord = tbxInput.Text;



                // Remove any leading or trailing spaces
                KeyWord = KeyWord.Trim();

                // Add this KeyWord to tbxAllKeyWords with a ';' seperator
                tbxAllKeyWords.Text = tbxAllKeyWords.Text + KeyWord + ';';


                //!! DETECE COMMENTS AND DONT ADD THEM TO THE KEYWORD LIST 11 //
                // If this is a generic comment(ie it begins with #)  return  without adding it to the KeyWordList
                if (KeyWord.IndexOf("#") != -1)
                {
                    tbxInput.Text = "";
                    return;
                }


                // !! UPDATE THE KEYWORD FILES !! //
                //The KeyWordist is the items in the ListOfKekWords.txt file
                // add this new key word to the KeyWordList
                KeyWordsStaticMembers.KeyWordList.Add(tbxInput.Text);
                // Append this new Keyword to the Keyword txt Fild
                KeyWordsStaticMembers.AppendNewKeyWord(KeyWord);
                //Add this new keyword to the SortedListOfKeyWords.txt file
                KeyWordsStaticMembers.AppendNewSortedKeyWord(KeyWord);
                // Convert Keyword to Dictionary Item by replacing all spaces with '_'
                string thisKeyWord = tbxInput.Text;
                //Convert the keyword to the dictionary key format (no spaces)
                string ConvertedThisKeyWord = thisKeyWord.Replace(' ', '_');
                //Add the new converted Key word to the dictionary with a value containing only the starting delimiter, ;
                KeyWordsStaticMembers.KeyWordsDictionary.Add(ConvertedThisKeyWord, ";");
                // Add the new converted keyword to the NotesDictionary.txt file
                KeyWordsStaticMembers.AppendNewKeyWordDictionaryItemString(ConvertedThisKeyWord);

                tbxInput.Text = "";
            }// End Enter Key clicked

            // Code to clear tbxInput if backspace results in empty text
            if (e.Key == Key.Back)
            {
                if (tbxInput.Text == "") lbxKeyWords.Items.Clear();
            }
        }// End tbxInput_KeyUp 






        #endregion  Input Textbox Key Up Procedure




        #region lbxKeyWords_MouseLeftButtonUp

        /// <summary>
        /// This method is called when the user selects a KeyWord from the list of KeyWords in the lbxKeyWords ListBox  
        /// by left clicking it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxKeyWords_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /* STEPS to be done when the user selects a key word from the lbxKeyWords
             *  1*.  Set CurrentKeyWord to Selected Key word
                2*.  Clear previously used globals
                3.  Determin if this KeyWord is already present in tbxAllKeyWords. 
                  If it is:
                        a*.  If it has set the ReusedKeyWord to the rbxInput text
                        b*.  Set AddCommentToPreviouslyUsedKeyWord to true
                        c*.  Clear the text in tbxInput and lbxKeyWords
                        d*.  return to the UI
                  Else  this keyword has not been used then:
                    a*.  Call AddInputTextToTBXAllKeyWords
                    b.  Clear tbxInput and lbxKeyWords                
             */


            string KeyWord = "";
            try
            {
                KeyWord = lbxKeyWords.SelectedItem.ToString();
                LinkNoteStaticMembers.SearchKeyWord = KeyWord;
                if (!LinkNoteStaticMembers.ShowAllKeywords)
                {
                    // The user is in the Search Mode
                    LinkNoteStaticMembers.SelectedKeyWord = KeyWord;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You cannot have an empty keyword");
                return;
            }
            CurrentKeyWord = KeyWord;
            // Clear globals that may have been set previously
            LongKeyWordsOK = false;
            AddCommentToPreviouslyUsedKeyWord = false;
            ReusedKeyWord = "";

            if (ThisKeyWordHasAlreadyBeenUsed(CurrentKeyWord))
            {
                    AddCommentToPreviouslyUsedKeyWord = true;
                    ReusedKeyWord = CurrentKeyWord;

            }//End if (ThisKeyWordHasAlreadyBeenUsed(CurrentKeyWord))
            else
            {
                AddInputTextToTBXAllKeyWords(CurrentKeyWord);

            }// End if (ThisKeyWordHasAlreadyBeenUsed(CurrentKeyWord)) ELSE 
            tbxInput.Text = "";
            lbxKeyWords.Items.Clear();

        }// End lbxKeyWords_MouseLeftButtonUp


        #endregion lbxKeyWords_MouseLeftButtonUp

        #region Revert Button Clicked
        /// <summary>
        /// This button is visible only when the user is in the 
        /// search mode. when clicked it removes the last
        /// KeyWord from the KeyComparison and 
        /// displays it in the tbxAllKeyWords
        /// It also changes the display in the lbxOpenSelectedNote
        /// to only those Note Namew whose reference IDs are found in the
        /// current KeyComparison or Original Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevert_Click(object sender, RoutedEventArgs e)
        {

            KeyWordsStaticMembers.ListAccess = false;
            btnRevert.Visibility = Visibility.Visible;

            lblKeyWordsAction.Content = "Revert to the previous Key Combination or Word";
            btnRevert.Content = "Revert";



        }// End btnRevert_Click

        #endregion Revert Button Clicked


        #region tbxAllKeyWords_TextChanged
        /// <summary>
        /// This method is called whenever the text in the  tbxAllKeyWords
        /// is changed if the user in not in the Search mode then the program returns
        /// a.	When a keyword is entered create and call a method to ReturnNoteNameList, which will:
		///	    1)	Create a Dictionary<int, string> SelectedNoteReferenceValues, in which
        ///         a)	The Key will be an integer from 0 to Number of referecnes containing that key-1 and
        ///         b)	The Value will be the '^' delimited string of the selected NoteReferenceFile
		///	    2)	the ReturnNoteNameList method will then return a '~' delimited string of the
        ///	        NoteName fields(position 0 in the '^' delimited string of the selected NoteReferenceFile) 
        ///	        of the selected Note References 
        ///	    3)	The Link_note.xaml.cs  file will display this list in the lbxOpenSelectedNote list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAllKeyWords_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Add a new keyword to this new note reference
            if (rbtAdd.IsChecked == true)
            {
                // This is creating a new Note
                string CurrentKeyWords = tbxAllKeyWords.Text;
                CurrentKeyWords = CurrentKeyWords + tbxInput.Text + ';';
                return;
            }
            //Check to see if in edit mode
            if ((rbtEdit.IsChecked == true))
            {
                if (tbxAllKeyWords.Text != "")
                {
                    string CurrentKeyWords = tbxAllKeyWords.Text;
                    return;
                }
                else
                {
                    return;
                }
            }
            // Create a string variable from the text in tbxAllKeyWords
            var SearchKeyWord = tbxAllKeyWords.Text;
            if (SearchKeyWord == "") return;

            // Delete the terminal ';' to create the SearchKeyWord
            SearchKeyWord = SearchKeyWord.Substring(0, SearchKeyWord.Length - 1);

            //Send this SearchKeyWord to KeyWordsStatidcMembers.ReturnNoteNameList to get a List<string> NoteNamesDispalyList
            List<string> NoteNamesDispalyList = KeyWordsStaticMembers.ReturnNoteNameList(SearchKeyWord);

            // Display this list in lbxOpenSelectedNote
            //      Clear any previous values
            lbxOpenSelectedNote.Items.Clear();

            // Cycle through NoteNamesDispalyList adding them to the list box
            foreach (string NoteName in NoteNamesDispalyList)
            {
                lbxOpenSelectedNote.Items.Add(NoteName);
            }

        }// End tbxAllKeyWords_TextChanged

        #endregion tbxAllKeyWords_TextChanged

        #endregion  KeyWord Controls






        private void rbtEdit_Click(object sender, RoutedEventArgs e)
        {
            LinkNoteStaticMembers.EditingBoolean = true;

            KeyWordsStaticMembers.ListAccess = true;
        }

        private void tbxHyperlink_TextChanged(object sender, TextChangedEventArgs e)
        {
            LinkNoteStaticMembers.LNSelectedFilePath = tbxHyperlink.Text;
        }

        private void tbxBookMark_TextChanged(object sender, TextChangedEventArgs e)
        {
            LinkNoteStaticMembers.BookMarks.Add(tbxBookMark.Text);
        }

        private void tbxLinkName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LinkNoteStaticMembers.HyperlinkName = tbxLinkName.Text;
        }

        /// <summary>
        /// This Method is called when the user clicks the open All KeyWords radio button. 
        /// It calls a local private method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtKeyWordsAll_Checked(object sender, RoutedEventArgs e)
        {
            LinkNoteStaticMembers.ShowAllKeywords = true;
        }

        private void Selected_Checked(object sender, RoutedEventArgs e)
        {
            LinkNoteStaticMembers.ShowAllKeywords = false;

        }


        #region Methods called by tbxInput.KeyUp method

        /// <summary>
        /// This is called when the user is in search mode and there are no KeyWords that being with 
        /// the string in the tbxInput TextBoxc to notify the user that 
        /// "When You are in the Search mode you can only search for existing Keywords"
        /// </summary>
        /// <param name="listLbxKeyWords"></param>
        private void ShowSearchKeyWordInvalid()
        {
            // If lbxKeyWords is empty in the Search statge send a message that you can only accepts existing keywords
            if (lbxKeyWords.Items.Count == 0)
            {
                MessageBox.Show("When You are in the Search mode you can only search for existing Keywords");
                return;
            }// End if (lbxKeyWords.Items.Count == 0)

        } // End ShowSearchKeyWordInvalid(



        /// <summary>
        /// This is called the first time that the length of a new KeyWord is >50 and it asks
        /// the user if this is what is desired. If the reply is YES then a LongKeyWordsOK to 
        /// true, and if additional characters are added to this KeyWord it will skip this
        /// method. 
        /// </summary>
        private void SetLongKeyWordBool()
        {

            if (MessageBox.Show("Your KeyWord is >50 Charcters. Do you want to store it?", "Please Select", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LongKeyWordsOK = true;
            }
            else
            {
                LongKeyWordsOK = false;
                tbxInput.Text = "";
            } 
            return;
        }// End private void SetLongKeyWordBool()

        /// <summary>
        /// Receives the current key word and trims it. It then adds it to the existing KeyWordList,
        /// in KeyWordsStaticMembers. It then appends it to the ListOfKeyWords.txt file and to the
        /// ListOfSortedKeyWords.txt file. It the changes all spaces ' ' to underscores '_' and
        /// creats in new item in the KeyWordDictionary with this as the Key. Finally it appelds
        /// a '^' and return characters and adds it to the bottom of the KeyWordDictionary.txt file
        /// 
        /// 
        /// </summary>
        /// <param name="currentKeyWord"></param>
        private void AddNewKeyWordToKeyWordList(string currentKeyWord)
        {
            CurrentKeyWord.Trim();
            //Addded 02221111
            if (KeyWordsStaticMembers.KeyWordList.Contains(currentKeyWord)) return;
            //End Added20221111
            // add this new key word to the KeyWordList
            KeyWordsStaticMembers.KeyWordList.Add(CurrentKeyWord);
            // Append this new Keyword to the Keyword txt Fild
            KeyWordsStaticMembers.AppendNewKeyWord(CurrentKeyWord);
            //Add this new keyword to the SortedListOfKeyWords.txt file
            KeyWordsStaticMembers.AppendNewSortedKeyWord(CurrentKeyWord);
            // Convert Keyword to Dictionary Item by replacing all spaces with '_'
            string thisKeyWord = CurrentKeyWord;
            //Convert the keyword to the dictionary key format (no spaces)
            string ConvertedThisKeyWord = thisKeyWord.Replace(' ', '_');
            //Add the new converted Key word to the dictionary with a value containing only the starting delimiter, ;
            //Added 20221111
            if (!KeyWordsStaticMembers.KeyWordsDictionary.ContainsKey(CurrentKeyWord))
            {
                KeyWordsStaticMembers.KeyWordsDictionary.Add(ConvertedThisKeyWord, ";");
                // Add the new converted keyword to the NotesDictionary.txt file
                KeyWordsStaticMembers.AppendNewKeyWordDictionaryItemString(ConvertedThisKeyWord);
            }
            //End Added20221111
            //KeyWordsStaticMembers.KeyWordsDictionary.Add(ConvertedThisKeyWord, ";");
            //// Add the new converted keyword to the NotesDictionary.txt file
            //KeyWordsStaticMembers.AppendNewKeyWordDictionaryItemString(ConvertedThisKeyWord);

        }// End private void AddNewKeyWordToKeyWordList(string KeyWordInAllKeyWordsText)

        /// <summary>
        /// Receives the text of a comment 
        /// It it has illegal charcters it sends a message to the user
        /// and returns false
        /// otherwise it returns true
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool CommentIsValid(string text)
        {
            bool Valid = true;
            string thisMessaage = "";
            //Test to insure that the text does not contain a semicolon ;
            if (tbxInput.Text.Contains(";"))
            {
                thisMessaage = "You cannot include a semicolon ';' in a key word or comment!";
                Valid = false;
            }
            int NumOfHashes = StringHelper.ReturnNumberOfDeliniters(tbxInput.Text, '#');
            if (NumOfHashes > 1)
            {
                thisMessaage = "A Comment can only have one #";
                Valid = false;
            }
            return Valid;
        }// End private bool CommentIsValid(string text)

        /// <summary>
        /// Receives a Key Word and determines if it has already been used or not
        /// </summary>
        /// <param name="thisKeyWord"></param>
        /// <returns></returns>
        private bool ThisKeyWordHasAlreadyBeenUsed(string thisKeyWord)
        {
            string currentTextInTbxAllKeyWords = tbxAllKeyWords.Text;
            //MODIFICATION 20221115
            //There is a logic error here if the keyword is the first item in the list
            if((currentTextInTbxAllKeyWords.IndexOf(thisKeyWord + ';') == 0) || (currentTextInTbxAllKeyWords.IndexOf(';'+thisKeyWord + ';') != -1) )
            {
                return true;
            }
            else
            {
                return false;
            }// End elseif (currentTextInTbxAllKeyWords.IndexOf(thisKeyWord+';') != -1)
        }// End private bool ThisKeyWordHasAlreadyBeenUsed(string thisKeyWord)

        /// <summary>
        /// Returns the first item in the lbxKeyWords List Box
        /// </summary>
        /// <returns></returns>
        private string GetFirstItemInLbxKeyWords()
        {
            string firstItem = "";
            //Convert the Items in lbxKeyWords into a string []
            if (lbxKeyWords.Items.Count != 0)
            {
                string[] lbxKeyWordsArray = lbxKeyWords.Items.OfType<string>().ToArray();
                firstItem = lbxKeyWordsArray[0];
                
            }
            return firstItem;
        }// End of private string GetFirstItemInLbxKeyWords()



        private void AddANewKeyWord(string currentKeyWord)
        {

            //Add this keyword to the appropriate variables and 
            LinkNoteStaticMembers.SelectedKeyWord = currentKeyWord;
            //Set the LinkNoteStaticMembers.SelectedKeyWord string to CurrentKeyWord
            // It represents the KeyWord the user has selected to search for in all notes
            LinkNoteStaticMembers.SearchKeyWord = currentKeyWord;
            string currentValueOfTbxAllKeyWords = tbxAllKeyWords.Text;
            currentValueOfTbxAllKeyWords = currentValueOfTbxAllKeyWords + currentKeyWord + ';';
            tbxAllKeyWords.Text = currentValueOfTbxAllKeyWords;
            //Clear tbxInput
            tbxInput.Text = "";
            //Clear lbxKeyWords
            lbxKeyWords.Items.Clear();


        }//End AddANewKeyWord(string currentKeyWord)


        /// <summary>
        /// This is called when a keyword that had previously been used was entered and the
        /// value of ThisKeyWordHasAlreadyBeenUsed is true
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void AddThisCommentToTheReusedKeyWord(string text)
        {
            //Create a temp dictionary of the ';' delimited items in tbxAllKeyWords using KeyWords as Keys and comments as values
            // Remove the terminal ';' from tbxAllKeyWords.Text
            string textInTbxAllKeyWords = tbxAllKeyWords.Text.Substring(0, tbxAllKeyWords.Text.Length - 1);
            //Use Split to convert the text in tbxAllKeyWords.Text into a string array
            string[] ArrayOfAllKeyWordsItems = textInTbxAllKeyWords.Split(';');
            //Create a Dictionary where the Keys are the KeyWords and the value are the Comments attached to that key word
            Dictionary<string, string> KeyWord_CommentsDictionary = new Dictionary<string, string>();
            string KeyWordInAllKeyWordsText = "";

            /*Populate KeyWord_CommentsDictionary all KVPs consist of previously entered
             * where the Key is a Key Word and the Value are all of the comments
             * which followed it until the next Key word*/
            //Create a variable to hold the value of the most recently entered key word
            string mostRecentKeyWord = "";
            foreach (string Item in ArrayOfAllKeyWordsItems)
            {
                if (Item.IndexOf('#') == -1)
                {
                    //This is a KeyWord so create a new dictionary item
                    KeyWord_CommentsDictionary.Add(Item, "");
                    mostRecentKeyWord = Item;
                } //End of if(Iten.IndexOf('#') == -1)
                if (Item.IndexOf('#') == 0)
                {
                    //This is a comment so add it to the Dictionary item whose key = KeyWordInAllKeyWordsText

                    string CurrentValue = KeyWord_CommentsDictionary[mostRecentKeyWord];
                    CurrentValue = CurrentValue + Item + ';';
                    //Add the updated value to the dictionary
                    KeyWord_CommentsDictionary[mostRecentKeyWord] = CurrentValue;
                }//End if(Iten.IndexOf('#') == 0)
            }//End of foreach(string Item in ArrayOfAllKeyWordsItems

            /* Get the current Value of the Keyword Key which was already present in the tbxAllKeyWords.Text 
             * which consists of all comments originally entered after that Key word  prior 
             to the new Comment which was just added*/
            string CurrentValueOfReusedKeyWord = KeyWord_CommentsDictionary[ReusedKeyWord];
            string NewCommentToAdd = tbxInput.Text;
            CurrentValueOfReusedKeyWord = CurrentValueOfReusedKeyWord + NewCommentToAdd + ';';
            KeyWord_CommentsDictionary[ReusedKeyWord] = CurrentValueOfReusedKeyWord;
            //Change the Dictionary back into a ';' delimited string
            string RevisedtbxAllKeyWordsText = "";
            //cycle thru the Dictionary getting all items
            foreach (KeyValuePair<string, string> KVP in KeyWord_CommentsDictionary)
            {
                string Key = KVP.Key;
                string Value = KVP.Value;
                RevisedtbxAllKeyWordsText = RevisedtbxAllKeyWordsText + Key + ';' + Value;
            }//End foreach(KeyValuePair KVP in KeyWord_CommentsDictionary       

            tbxAllKeyWords.Text = RevisedtbxAllKeyWordsText;
            return;

        }// End private void AddThisCommentToTheReusedKeyWord(string text)

        #region ContentValid Verification


        /// <summary>
        /// This method receives the text in the tbxInput TextBox and verifies that it is valid or not.
        /// if it is either a comment or a keyword it cannot contain a ';' and if it is a comment
        /// i.e. starts with # it cannot have more that 1 #
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool ContentValid(string text)
        {
            bool Valid = true;

            //Test to insure that the text does not contain a semicolon ;
            if (text.Contains(";"))
            {
                MessageBox.Show("You cannot include a semicolon ';' in a key word or comment!");
                Valid = false;
                return Valid;
            }

            //Test to see if text contains "#"
            if (text.Contains("#"))
            {
                //This may be a comment
                int numberOfHashes = StringHelper.ReturnNumberOfDeliniters(text, '#');
                //If there is only one and its position is 0 this is a valid comment
                if (numberOfHashes == 1)
                {
                    if (text.StartsWith("#"))
                    {
                        return Valid;
                    }
                    else
                    {
                        MessageBox.Show("The Hash mark # must start a comment!");
                        Valid = false;
                        return Valid;
                    }
                }
                else if (numberOfHashes > 1)
                {
                    MessageBox.Show("You can only have 1 Hash mark in an expression!");
                    Valid = false;
                    return Valid;
                }
            }// end text contains #

            //At this point we should have only keywords, test length to detect unusually long key words
            //Test to see if the KeyWord is longer than 50 characters
            if (text.Length > 50)
            {
                //MessageBox.Show("Your KeyWord is >50 Charcters. Do you want to store it?", "No", MessageBoxButton.YesNo);
                MessageBoxResult result = MessageBox.Show("Your KeyWord is > 50 Charcters.Do you want to store it ? ", "No", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:

                        return Valid;
                    case MessageBoxResult.No:
                        Valid = false;
                        return Valid;//return to the input box and allow the user to change the key word

                }//End switch (result)
            }//End if (text.Length > 50)
            return Valid;
        }// End bool ContentValid(string text)
        #endregion ContentValid Verification

        /// <summary>
        /// Uses the text in the tbxInput TextBox to query the list of all KeyWords in the current
        /// KeyWordList and dictionary and all key words that begin with this string will be added
        /// to the Items in the lbxKeyWords List Box
        /// </summary>
        private void ShowKeyWordsBeginningWithThisString()
        {
            lbxKeyWords.Items.Clear();
            string leadingChars = tbxInput.Text;
            //Get the list of existing key words
            List<string> KeyWordsList = KeyWordsStaticMembers.KeyWordList;
            //cycle thru KeyWordsList adding all who start with leadingChars to lbxKeyWords
            foreach (string line in KeyWordsList)
            {
                int indexOfLeadingChars = line.IndexOf(leadingChars);
                if (indexOfLeadingChars == 0)
                {
                    lbxKeyWords.Items.Add(line);
                }
            }
        } // End ShowKeyWordsBeginningWithThisString()


        /// <summary>
        /// This method receives the text currently in tbxInput and adds it and a ';' to
        /// the end of tbxAllKeyWords
        /// </summary>
        /// <param name="currentInputText"></param>
        private void AddInputTextToTBXAllKeyWords(string currentInputText)
        {
            string CurrentTextInTbxAllKeyCords = tbxAllKeyWords.Text;
            CurrentTextInTbxAllKeyCords = CurrentTextInTbxAllKeyCords + currentInputText + ';';
            tbxAllKeyWords.Text = CurrentTextInTbxAllKeyCords;
        }// End AddInputTextToTBXAllKeyWords(string currentInputText)










        #endregion Methods called by tbxInput.KeyUp method











    }// End class
}// End Name space

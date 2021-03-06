using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using System.IO;
using NewLSP.DataModels;
using System;
using System.Windows.Input;


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
        /// This method gets a file path string by calling the
        /// ReturnFilePath() private method which uses the OpenFileDialog to get the path 
        /// to a file that the user wants to save as a hyperlink for a DataNode
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

                // NEW 20210620

                // create a NoteReference string
                string NoteReferenceStr = tbxLinkName.Text + "^" + tbxHyperlink.Text + "^" + tbxBookMark.Text + "^" + tbxAllKeyWords.Text;
                

                // Call the static method to save both old and new note files
                LinkNoteStaticMembers.SaveAndUpdateNoteReferenceAndKeywords(NoteReferenceStr);

                // Get the DataNodesNoteReferenceString and append it to the lbxOpenSelectedNote listBox
                string DataNodesNoteReferenceString = LinkNoteStaticMembers.DataNodesNoteReferenceString;

               


                //LinkNoteStaticMembers.SaveDataNodesNoteReferenceString(DataNodesNoteReferenceString);

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
                if(LinkNoteStaticMembers.EditingBoolean == false)
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

            // Create a List<string> of Hyperlink display string
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
                    // Get the component parts
                    string[] componentItems = line.Split('^');
                    string Name = componentItems[0];
                    string Url = componentItems[1];
                    string FileType = componentItems[2];
                    string BookMark = componentItems[3];
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

            LinkNoteStaticMembers.HyperlinkDictionary.Clear();
        }


        #endregion ResetPage MenuItem

        #region Instructions Menu

        #region Create New Hyperlink Instructions Menu Item

        private void miCreateNewHyperlinkInstructions_Click(object sender, RoutedEventArgs e)
        {

        }


        #endregion Create New Hyperlink Instructions Menu Item

        #endregion Instructions Menu


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

            if(tbxBookMark.Text != "")
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
                MessageBox.Show(e.Message+ " = "+ hyperlink);
            }
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
            string DataNodesHyperlinkPath = CommonStaticMembers.HomeFolderPath + "Hyperlinks\\" + SubjectStaticMembers.DataNode.ID.ToString() + ".txt";

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



        #region Return File Path of File Dialog OpenFile



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
            //Get the CurrentNote26Name
            string thisNoteReferenceName = StringHelper.ReturnItemAtPos(currnetItem, '^', 1);

            // Update the CommonStaticMembers.CurrentNote26Name
            CommonStaticMembers.CurrentNote26Name = thisNoteReferenceName;

            //Use this name to create the path to the NoteReferenceFile
            string NoteReferenceFilePath = CommonStaticMembers.NoteReferencesPath +"\\"+ thisNoteReferenceName + ".txt";

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

            if(NoteBookmark != null)
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
        /// This method displays the Key words associated with the
        /// Note name that the User clicks with the Right Mouse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxOpenSelectedNote_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //get the selected items text string
            string SelectedItemsText = lbxOpenSelectedNote.SelectedItem.ToString();

            // Get the Common references NoteReference file name from this string
            string NotereferenceFileName = StringHelper.ReturnItemAtPos(SelectedItemsText, '^', 1);

            // Use this value to set the CurrentNote26Name
            CommonStaticMembers.CurrentNote26Name = NotereferenceFileName;

            // Create a path to the NoteReferenceFile
            string NoteReferenceFilePath = CommonStaticMembers.NoteReferencesPath + "\\" + NotereferenceFileName + ".txt";

            // Read in the text in the NoteReferenceFile
            string NoteReferenceFileText = File.ReadAllText(NoteReferenceFilePath);

            //Get the KeyWords section
            string KeyWordsString = StringHelper.ReturnItemAtPos(NoteReferenceFileText, '^', 3);

            //Delete the last ';'
            KeyWordsString = KeyWordsString.Substring(0, KeyWordsString.Length - 1);

            // replace all ';' with \r\n
            KeyWordsString = KeyWordsString.Replace(";", "\r\n");

            // Display tbxDisplayKeyWords
            tbxDisplayKeyWords.Text = KeyWordsString;

            


        }






        #endregion Show Key Words of Right Selected Note Name

        #region Private Method to Read noteReference file into lbxOpenSelectedNote

        private void ReadNotesIntoSelectNoteListBox()
        {
            //List<string> ListOfNoteNameAndFileNames = LinkNoteStaticMembers.ListBoxOfSelectedNotesList;

            LinkNoteStaticMembers.ReadInNotesFile();

            // Clear lbxOpenSelectedNote and tbxDisplayKeyWords
            lbxOpenSelectedNote.Items.Clear();

            // Fill the list box with ListOfNoteNameAndFileNames
            foreach(string line in LinkNoteStaticMembers. ListBoxOfSelectedNotesList)
            {
                lbxOpenSelectedNote.Items.Add(line);
            }

        }

        #endregion Private Method to Read noteReference file into lbxOpenSelectedNote


        #region PopulateNoteListBox

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

        #region KeyWord Controls

        

        #region RadioButton Add
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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtSearch_Click(object sender, RoutedEventArgs e)
        {
            tbxAllKeyWords.Text = "";
            KeyWordsStaticMembers.ListAccess = false;
        }



        #endregion Radio button Search


        #region Input Textbox Key Up Procedure
        /// <summary>
        /// Take the characters typed into lbxKeyWords and show all terms in KeyWordList that start with these characters
        /// If the User Hits the Enter Key
        ///     a. If there are itemn in the list, in either mode return the top item in the list
        ///     b. If there are no Items
        ///         1)  In search, Warn and return
        ///         2)  In Create, create a new KeyWord from the characters in the textbox
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxInput_KeyUp(object sender, KeyEventArgs e)
        {
            //Clear the current content of lbxKeyWords
            lbxKeyWords.Items.Clear();

            // Cycle through KeyWordList selecting all that begin with the characters in tbxInput
            foreach (string line in KeyWordsStaticMembers.KeyWordList)
            {
                //In the current line begins with the characters in tbxInput add line to lbxKeyWords
                if (line.IndexOf(tbxInput.Text) == 0)
                {
                    lbxKeyWords.Items.Add(line);
                }
            }

            //If the user hits the Enter key
            if (e.Key == Key.Enter)
            {
                if (rbtEdit.IsChecked == true) KeyWordsStaticMembers.ListAccess = true;
                //ListAccess is the abilits to access the current list of keywords
                if (!KeyWordsStaticMembers.ListAccess)
                {
                    // If lbxKeyWords is empty in the Search statge send a message that you can only accepts existing keywords
                    if (lbxKeyWords.Items.Count == 0)
                    {
                        MessageBox.Show("When You are in the Search mod you can only search for existing Keywords");

                        // return to UI
                        return;
                    }
                }


                string KeyWord = "";

                //Determine if there are Keywords showing in lbxKeyWords and if so select #0 and return
                if (lbxKeyWords.Items.Count != 0)
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

                    // add this Keyword to the list of selected keywords
                    tbxAllKeyWords.Text = tbxAllKeyWords.Text + KeyWord + ';';

                    //Clear tbxInput
                    tbxInput.Text = "";

                    //Clear lbxKeyWords
                    lbxKeyWords.Items.Clear();

                    //retrun  to UI
                    return;
                }


                // Create a new KeyWord from the current text in tbxInput
                KeyWord = tbxInput.Text;
                KeyWord = KeyWord.Trim();

                // Add this KeyWord to tbxAllKeyWords
                tbxAllKeyWords.Text = tbxAllKeyWords.Text + KeyWord + ';';

                // If this is a generic(ie it begins with #)  return  without adding it to the KeyWordList
                if (KeyWord.IndexOf("#") != -1)
                {
                    tbxInput.Text = "";
                    return;
                }

                // Update the active KeyWordList
                KeyWordsStaticMembers.KeyWordList.Add(tbxInput.Text);
                // Append this new Keyword to the Keyword txt Fild
                KeyWordsStaticMembers.AppendNewKeyWord(KeyWord);
                                

                // Convert Keyword to Dictionary Item by replacing all spaces with '_'
                string thisKeyWord = tbxInput.Text;
                string ConvertedThisKeyWord = thisKeyWord.Replace(' ', '_');

                //Add the new converted Key word to the dictionary with a value containing only the starting delimiter, ;
                KeyWordsStaticMembers.KeyWordsDictionary.Add(ConvertedThisKeyWord, ";");

                // Add the new converted keyword to the NotesDictionary.txt file
                KeyWordsStaticMembers.AppendNewKeyWordDictionaryItemString(ConvertedThisKeyWord);

                tbxInput.Text = "";
            }

            // Code to clear tbxInput if backspace results in empty text
            if (e.Key == Key.Back)
            {
                if (tbxInput.Text == "") lbxKeyWords.Items.Clear();
            }
        }// End tbxInput_KeyUp 
        #endregion  Input Textbox Key Up Procedure


        #region lbxKeyWords_MouseLeftButtonUp


        private void lbxKeyWords_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string KeyWord = lbxKeyWords.SelectedItem.ToString();
            tbxAllKeyWords.Text = tbxAllKeyWords.Text + KeyWord + ';';
            tbxInput.Text = "";
            lbxKeyWords.Items.Clear();
        }








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
            if((rbtEdit.IsChecked == true))
            {
                if(tbxAllKeyWords.Text != "")
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
            foreach(string NoteName in NoteNamesDispalyList)
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
            LinkNoteStaticMembers.Hyperlink = tbxHyperlink.Text;
        }

        private void tbxBookMark_TextChanged(object sender, TextChangedEventArgs e)
        {
            LinkNoteStaticMembers.BookMarks.Add(tbxBookMark.Text);
        }

        private void tbxLinkName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LinkNoteStaticMembers.HyperlinkName = tbxLinkName.Text;
        }
    }// End class
}// End Name space

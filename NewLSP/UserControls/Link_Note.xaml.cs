using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;
using System.IO;
using NewLSP.DataModels;
using System;

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


        #region Meun Item MS Paint

        private void miPaint_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Windows\system32\mspaint.exe");
        }
        #endregion Meun Item MS Paint


        #endregion Applications Menu


        #region DataType Menu

        #region Menu Item Hyperlink

        private void miHyperlink_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("miHyperlink_Click");

        }

        #endregion Menu Item Hyperlink


        #region Menu Item Note

        private void miNote_Click(object sender, RoutedEventArgs e)
        {
            //The User is working on notes
            MessageBox.Show("the menu ite Note clicked");
        }

        #endregion Menu Item Note

        #endregion DataType Menu


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
            if (KeyWordsStaticMembers.ListAccess)
            {
                // The program is in the Create mode
                // Make sure that all required data fields are present
                if ((tbxLinkName.Text == "") || (tbxHyperlink.Text == "") || (ucKeyWordControl.tbxAllKeyWords.Text == ""))
                {
                    MessageBox.Show("You cannot save a Note unless there is a Name, a hyperlink and KeyWord(s)");
                    return;
                }

                // Make sure the Key words are delimited witb ;s
                if (!ucKeyWordControl.tbxAllKeyWords.Text.Contains(";"))
                {
                    MessageBox.Show("Key words must be delimigted wint ';'s ");
                    return;
                }
                // create a NoteReference string
                string NoteReferenceStr = tbxLinkName.Text + "^" + tbxHyperlink.Text + "^" + tbxBookMark.Text + "^" + ucKeyWordControl.tbxAllKeyWords.Text;

                LinkNoteStaticMembers.SaveNoteReference(NoteReferenceStr);



                // Clear lbxOpenSelectedNote and tbxDisplayKeyWords
                lbxOpenSelectedNote.Items.Clear();
                tbxDisplayKeyWords.Text = "";

                // Call ReadNotesIntoSelectNoteListBox()
                ReadNotesIntoSelectNoteListBox();

                PopulateNoteListBox();

                //Clear Note entry fields
                tbxLinkName.Text = "";
                tbxHyperlink.Text = "";
                ucKeyWordControl.tbxAllKeyWords.Text = "";
                ucKeyWordControl.lbxKeyWords.Items.Clear();
                ucKeyWordControl.tbxAllKeyWords.Text = "";
                ucKeyWordControl.tbxInput.Text = "";


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

        private void miShowNote_Click(object sender, RoutedEventArgs e)
        {
            int NodeID = SubjectStaticMembers.DataNode.ID;
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



        /// <summary>
        /// This method is called when the user clicks the 
        /// OpenFileDialog menu item 
        /// It uses the OpenFile Dialog to return the
        /// file path to the selected file
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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxOpenSelectedNote_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           //get the selected items index number
            int NoteSelectedIndex = lbxOpenSelectedNote.SelectedIndex;

            //Get the hyperlink and bookmark
            string NoteHyperlink = LinkNoteStaticMembers.ListOfNoteHyperlinks[NoteSelectedIndex];
            string NoteBookMark = LinkNoteStaticMembers.ListOfNoteBookMarks[NoteSelectedIndex];
            
            //populate the hyperlink and bookmark text boxes
            tbxHyperlink.Text = NoteHyperlink;
            tbxBookMark.Text = NoteBookMark;

            //copy the bookmark to the clipboard
            Clipboard.SetText(NoteBookMark);

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
            //get the selected items index number
            int NoteSelectedIndex = lbxOpenSelectedNote.SelectedIndex;

            // Clear tbxDisplayKeyWords
            tbxDisplayKeyWords.Text = "";

            //get the keywords string
            string DelimitedKeyWords = LinkNoteStaticMembers.ListOfNoteKeyWords[NoteSelectedIndex];

            // Replace ';' with "\r\n"
            DelimitedKeyWords = DelimitedKeyWords.Replace(";", "\r\n");

            //Display the keywords
            tbxDisplayKeyWords.Text = DelimitedKeyWords;

        }






        #endregion Show Key Words of Right Selected Note Name

        #region Private Method to Read noteReference file into lbxOpenSelectedNote

        private void ReadNotesIntoSelectNoteListBox()
        {
            
            // Call LNStatic to read in the file
            LinkNoteStaticMembers.ReadInNotesFile();
           

            // Clear lbxOpenSelectedNote and tbxDisplayKeyWords
            lbxOpenSelectedNote.Items.Clear();
            tbxDisplayKeyWords.Text = "";

            
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


    }// End class
}// End Name space

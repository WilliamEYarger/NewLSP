using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NewLSP.StaticHelperClasses;
using System.Windows.Controls;
using NewLSP.UserControls;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for KeyWordControl.xaml
    /// </summary>
    public partial class KeyWordControl : UserControl
    {
        private object ucLinkNote;

        public KeyWordControl()
        {
            InitializeComponent();
        }

        //private void rbtOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    KeyWordsStaticMembers.ListAccess = true;
        //}


        #region RadioButton Add
        private void rbtAdd_Click(object sender, RoutedEventArgs e)
        {
            btnRevert.Visibility = Visibility.Hidden;
            KeyWordsStaticMembers.ListAccess = true;
            lblKeyWordsAction.Content = "Add Key Words to a New Note Reference";

            btnRevert.Content = "Revert";
        }
        #endregion RadioButton Add

        //private void rbtClosed_Click(object sender, RoutedEventArgs e)
        //{
        //    KeyWordsStaticMembers.ListAccess = false;
        //}


        #region Radio button Search

        /// <summary>
        /// Sets the ListAccess boolean to false because the program is 
        /// in the Search mode and new KeyWords are not allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtSearch_Click(object sender, RoutedEventArgs e)
        {
            //KeyWordsStaticMembers.ListAccess = false;
            //btnSubmit.Visibility = Visibility.Visible;

            //lblKeyWordsAction.Content = "Search for NoteReferences with these Key Words";
            //btnSubmit.Content = "Search";
        }
        #endregion Radio button Search

        #region tbxInput_KeyUp
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
                // Append this new Keyword to the Keyword Fild
                KeyWordsStaticMembers.AppendNewKeyWord(KeyWord);


                // Convert Keyword to Dictionary Item
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
        }
        #endregion tbxInput_KeyUp

        #region bxKeyWords_MouseLeftButtonUp
        private void lbxKeyWords_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string KeyWord = lbxKeyWords.SelectedItem.ToString();
            tbxAllKeyWords.Text = tbxAllKeyWords.Text + KeyWord + ';';
            tbxInput.Text = "";
            lbxKeyWords.Items.Clear();
        }
        #endregion bxKeyWords_MouseLeftButtonUp


       

        #region tbxAllKeyWords_TextChanged
        /// <summary>
        /// This method is called whenever the text in the  tbxAllKeyWords
        /// is changed if the user in not in the Search mode then the program returns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAllKeyWords_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Get the entry after the ';'
            int posSemi = tbxAllKeyWords.Text.IndexOf(';');
            string frontItem = "";
            string backItem = "";
            List<string> frontList = new List<string>();

            // Determine if this is the last character, ie there is only a key word entered
            if (posSemi == tbxAllKeyWords.Text.Length-1)
            {
                // This is the originay keyword
                // Display all NoteNames whose notes contain this Key word

                frontItem = tbxAllKeyWords.Text.Substring(0, tbxAllKeyWords.Text.Length - 1);
                string[] InitialKeyWordNotesArray = KeyWordsStaticMembers.delimitedStringOfNoteNames(frontItem).Split(';');

                // Create a List<string> of these note names eliminating blank entries                
                for (int i = 0; i < InitialKeyWordNotesArray.Length; i++)
                {
                    if (InitialKeyWordNotesArray[i] != "")
                    {
                        frontList.Add(InitialKeyWordNotesArray[i]);
                    }
                }

                // Create a Dictionary<string,string> MatchingNoteRefsDictionary where the Key is the NoteIDName and the Value is the Note
                Dictionary<string, string> MatchingNoteRefsDictionary = new Dictionary<string, string>();
                foreach (string NoteIDName in frontList)
                {
                    string RefNoteName = NoteIDName + ".txt";
                    try
                    {
                        string NoteFilePath = CommonStaticMembers.NoteReferencesPath + RefNoteName;
                        if (File.Exists(NoteFilePath))
                        {
                            string NoteContent = File.ReadAllText(NoteFilePath);
                            MatchingNoteRefsDictionary.Add(NoteIDName, NoteContent);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There is not note for NoteIDName");
                        return;
                    }

                    //Cycle thorugh the Dictionary displaying all note Names in lbxOpenSelectedNote
                    foreach(KeyValuePair< string, string> KVP in MatchingNoteRefsDictionary)
                    {
                        string NoteID = KVP.Key;
                        string Value = KVP.Value;
                        string NoteName = StringHelper.ReturnItemAtPos(Value, '^', 0);

                    }

                }


            }
            else
            {
                 frontItem = tbxAllKeyWords.Text.Substring(0, posSemi - 1);
                 backItem = tbxAllKeyWords.Text.Substring(posSemi + 1);

                //Determine if frontItem is a KeyCombination or a KeyWord
                if(frontItem.IndexOf(" + ") != -1)
                {
                    //This is a keyword combination  get its list of notereferences from the KeyCombinationsDictionary
                }
                else
                {
                    //This is the original keyword get its note references from the KeyWordsDictionary
                    string[] InitialKeyWordNotesArray = KeyWordsStaticMembers.delimitedStringOfNoteNames(frontItem).Split(';');

                    // Create a List<string> of these note names eliminating blank entries
                   
                    for (int i=0; i< InitialKeyWordNotesArray.Length; i++)
                    {
                        if (InitialKeyWordNotesArray[i] != "")
                        {
                            frontList.Add(InitialKeyWordNotesArray[i]);
                        }

                    }
                }
            }
        }// End tbxAllKeyWords_TextChanged

        #endregion tbxAllKeyWords_TextChanged


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

    }
}

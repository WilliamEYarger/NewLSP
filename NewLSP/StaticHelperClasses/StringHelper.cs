using System.Collections.Generic;
using System.Linq;

namespace NewLSP.StaticHelperClasses
{
    public static class StringHelper
    {

        #region ReturnNumberOfDeliniters

        public static int ReturnNumberOfDeliniters(string line, char del)
        {
            char[] LineCharArray = line.ToCharArray();
            int count = 0;
            for (int i = 0; i < LineCharArray.Length; i++)
            {
                char thisChar = LineCharArray[i];
                if (thisChar == del) count++;
            }

            return count;
        }


        #endregion ReturnNumberOfDeliniters


        #region ReturnItemAtPos
        public static string ReturnItemAtPos(string delString, char del, int Pos)
        {
            string[] itemsArray = delString.Split(del);
            return itemsArray[Pos];
        }
        #endregion ReturnItemAtPos

        #region ReturnLastItem

        public static string ReturnLastItem(string line, char del)
        {
            var PosLastDel = line.LastIndexOf(del);
            var ReturnString = line.Substring(PosLastDel + 1);
            return ReturnString;

        }


        #endregion ReturnLastItem

        #region ReturnLastAndUpdate
        public static string[] ReturnLastAndUpdate(string delString, char del)
        {
            var returnArr = new string[2];
            int posLastDelimiter = delString.LastIndexOf(del);
            returnArr[0] = delString.Substring(posLastDelimiter);
            returnArr[1] = delString.Substring(0, posLastDelimiter - 1);

            return returnArr;

        }
        #endregion ReturnLastAndUpdate


        #region RemoveLastValue
        public static string RemoveLastValue(string delString, char del)
        {

            int posLastDelimiter = delString.LastIndexOf(del);
            if (posLastDelimiter < 0)
            {
                return "";
            }
            string updatedString = delString.Substring(0, posLastDelimiter);
            return updatedString;
        }
        #endregion RemoveLastValue


        #region CreateDisplayString

        /// <summary>
        /// Create a display string to show in a list box
        /// </summary>
        /// <param name="LeedingChar"> is a + or - </param>
        /// <param name="Text"></param>
        /// <param name="ID"> the Items AlphaNumber</param>
        /// <param name="NumberOfChildren"> The Items number of Children</param>
        /// <returns></returns>
        public static string CreateDisplayString(char LeedingChar, string Text, string ID, int NumberOfChildren)
        {
            string thisItemsListString;

            int LengthOFItemText = Text.Length;
            int addSpacesNumber = 100 - LengthOFItemText;
            string spacesString = new string(' ', addSpacesNumber);
            if (LeedingChar == '-')
            {
                thisItemsListString = "- " + Text + spacesString + '^' + ID + '^' + NumberOfChildren.ToString();
            }
            else
            {
                thisItemsListString = "+ " + Text + spacesString + '^' + ID + '^' + NumberOfChildren.ToString();
            }


            return thisItemsListString;
        }

        #endregion CreateDisplayString

        #region ReplaceItemAtPosition
        public static void ReplaceItemAtPosition(ref string line, char del, int postions, string item)
        {
            string[] LineArray = line.Split(del);
            LineArray[postions] = item;
            line = "";
            foreach (string Item in LineArray)
            {
                line = line + Item + del;
            }
            line = line.Substring(0, line.Length - 1);

        }
        #endregion ReplaceItemAtPosition


        #region GetAndRemoveNthItem

        public static string GetAndRemoveNthItem(ref string  delimitedString, char del, int pos)
        {

            string[] itemsArray = delimitedString.Split(del);
            string returnItem = itemsArray[pos];
            List<string> ItemsList = itemsArray.ToList<string>();
            ItemsList.RemoveAt(pos);
            delimitedString = "";
            foreach(string item in ItemsList)
            {
                delimitedString = delimitedString + item + del;
            }
            delimitedString = delimitedString.Substring(0, delimitedString.Length - 1);

            return returnItem;

        }//End GetAndRemoveNthItem

        #endregion GetAndRemoveNthItem


        #region RemoveFirstItem

        public static string RemoveFirstItem(string delString, char del)
        {
            int posFirstDel = delString.IndexOf(del);
            delString = delString.Substring(posFirstDel + 1);
            return delString;
        }

        #endregion RemoveFirstItem

    }// End Class
}// End Namespace

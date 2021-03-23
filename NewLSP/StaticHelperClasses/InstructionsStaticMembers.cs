using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLSP.StaticHelperClasses
{
    public static class InstructionsStaticMembers
    {
        #region  Properties 

        #region InstructionsFolderPath property 

        private static string _InstructionsFolderPath = "";

        public static string  InstructionsFolderPath
        {
            get { return _InstructionsFolderPath; }
            set { _InstructionsFolderPath = value; }
        }

        #endregion InstructionsFolderPath property

        #endregion Properties

    }//End InstructionsStaticMembers


}// End  NewLSP.StaticHelperClasses


using System.IO;
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for Instrutions.xaml
    /// </summary>
    public partial class Instrutions : UserControl
    {
        public Instrutions()
        {
            InitializeComponent();
        }

       
        private void SelectSubject_Click(object sender, RoutedEventArgs e)
        {
            if(InstructionsStaticMembers.InstructionsFolderPath == "")
            {
                MessageBox.Show("You cannot open an instruction file until you have selected the Instructions Folder");
                return;
            }
            string[] readText = File.ReadAllLines(InstructionsStaticMembers.InstructionsFolderPath + "Instructions for starting a new session.txt");
            string InstructionsText = "";
            foreach(string line in readText)
            {
                InstructionsText = InstructionsText + line + "\r\n";
            }
            tbxInstructions.Text = InstructionsText;

        }

        private void ShowRoot_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ShowRoot_Click");

        }

        private void AddChild_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AddChild_Click");

        }

        private void MoveNode_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MoveNode_Click");

        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            if (InstructionsStaticMembers.InstructionsFolderPath == "")
            {
                MessageBox.Show("You cannot open an instruction file until you have selected the Instructions Folder");
                return;
            }
            string[] readText = File.ReadAllLines(InstructionsStaticMembers.InstructionsFolderPath +
                "Instruction for the Text or Review Option of QAPages.txt");
            string InstructionsText = "";
            foreach (string line in readText)
            {
                InstructionsText = InstructionsText + line + "\r\n";
            }
            tbxInstructions.Text = InstructionsText;

        }

        private void SubjectTreeInstructions_Click(object sender, RoutedEventArgs e)
        {
            if (InstructionsStaticMembers.InstructionsFolderPath == "")
            {
                MessageBox.Show("You cannot open an instruction file until you have selected the Instructions Folder");
                return;
            }
            string[] readText = File.ReadAllLines(InstructionsStaticMembers.InstructionsFolderPath + "Instructions for using the SubjectTreePage.txt");
            string InstructionsText = "";
            foreach (string line in readText)
            {
                InstructionsText = InstructionsText + line + "\r\n";
            }
            tbxInstructions.Text = InstructionsText;
        }
    }
}

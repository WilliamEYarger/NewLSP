using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewLSP.UserControls;
using NewLSP.StaticHelperClasses;

namespace NewLSP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miHome_Click(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(ucHome);
        }

        private void miSubjectTree_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectStaticMembers.DisplayList.Count ==0) 
            { 
                MessageBox.Show("You cannot open this tab until you select a Subject Folder");
                return;
            }
           
            SetActiveUserControl(ucSubjectTree);
        }

        private void miCreateQA_Click(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(ucCreatEditQA);
        }

        private void miTest_Click(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(ucTestReview);
        }

        public void SetActiveUserControl(UserControl control)
        {
            // Make the visibility of all user controls collapsed
            ucHome.Visibility = Visibility.Collapsed;
            ucSubjectTree.Visibility = Visibility.Collapsed;
            ucCreatEditQA.Visibility = Visibility.Collapsed;
            ucTestReview.Visibility = Visibility.Collapsed;


           // Make the selected control visible
           control.Visibility = Visibility.Visible;
        }

    }
}

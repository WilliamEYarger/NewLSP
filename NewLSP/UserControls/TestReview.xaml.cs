
using System.Windows;
using System.Windows.Controls;
using NewLSP.StaticHelperClasses;

namespace NewLSP.UserControls
{
    /// <summary>
    /// Interaction logic for TestReview.xaml
    /// </summary>
    public partial class TestReview : UserControl
    {
        public TestReview()
        {
            InitializeComponent();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("SaveFile Menu Clicked");
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            TestReviewStaticMembers.ThisIsATest = true;
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            TestReviewStaticMembers.ThisIsATest = false;
            TestReviewStaticMembers.AnswerQuestions();
            tbxQuestion.Text = TestReviewStaticMembers.ThisQuestion;
            return;
        }

        private void UseForm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Use Form Menu Clicked");
        }

        private void btnOpenImage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open Image Button Clicked");
        }

        private void btnOpenMp3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open Mp3 Button Clicked");
        }

        private void btnScoreCorrect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Score Corret Button Clicked");
        }

        private void btnScoreWrong_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Score Wrong Button Clicked");
        }

        private void btnShowCorrect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Score Correct Button Clicked");
        }
    }
}

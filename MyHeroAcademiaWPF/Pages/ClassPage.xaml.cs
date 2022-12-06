using MyHeroAcademiaWPF.ADO;
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

namespace MyHeroAcademiaWPF.Pages
{
    /// <summary>
    /// Interaction logic for ClassPage.xaml
    /// </summary>
    public partial class ClassPage : Page
    {
        public List<Student> Students;
        public ClassPage()
        {
            InitializeComponent();
        }

        public ClassPage(Class heroClass)
        {
            InitializeComponent();
            Students = App.Connection.Student.Where(z => z.IdClass == heroClass.IdClass).ToList();
            ListStudents.ItemsSource = Students;
            ClassName.Text = heroClass.Name;
            NumOfStudentTextBox.Text = Students.Count.ToString();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void StudentSuperpowerListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var studentSuperpower = (sender as ListBox).SelectedItem as StudentSuperpower;

            NavigationService.Navigate(new SuperpowerPage(App.Connection.Superpower.Where(z =>
            z.IdSuperpower == studentSuperpower.IdSuperpower).FirstOrDefault()));
        }

        private void ShowStudentClick(object sender, RoutedEventArgs e)
        {
            int studentId = int.Parse((sender as Button).Tag.ToString());

            var stud = App.Connection.Student.Where(z => z.IdStudent == studentId).FirstOrDefault();
            if(stud != null)
                NavigationService.Navigate(new StudentPage(stud));   
        }

        private void NameSortClick(object sender, RoutedEventArgs e)
        {
            Students = Students.OrderBy(z => z.Name).ToList();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void RankSortClick(object sender, RoutedEventArgs e)
        {
            Students = Students.OrderBy(z => z.IdRank).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void AttackSort(object sender, RoutedEventArgs e)
        {
            Students = Students.OrderBy(z => z.AttackPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void ProtectionSortClick(object sender, RoutedEventArgs e)
        {
            Students = Students.OrderBy(z => z.ProtectionPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void SupportSortClick(object sender, RoutedEventArgs e)
        {
            Students = Students.OrderBy(z => z.SupportPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void SupplyClick(object sender, RoutedEventArgs e)
        {
            Students = Students.OrderBy(z => z.SupplyForce).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void SearchSortClick(object sender, RoutedEventArgs e)
        {
            Students = Students.OrderBy(z => z.SearchPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }
    }
}

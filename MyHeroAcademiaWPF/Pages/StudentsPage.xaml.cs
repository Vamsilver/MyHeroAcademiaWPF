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
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        List<Student> Students;
        public StudentsPage()
        {
            InitializeComponent();
            Students = App.Connection.Student.ToList();
            ListStudents.ItemsSource = Students;
        }

        private void NameSortClick(object sender, RoutedEventArgs e)
        {
            Students = App.Connection.Student.ToList();
            Students = Students.OrderBy(z => z.Name).ToList();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void RankSortClick(object sender, RoutedEventArgs e)
        {
            Students = App.Connection.Student.ToList();
            Students = Students.OrderBy(z => z.IdRank).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void AttackSort(object sender, RoutedEventArgs e)
        {
            Students = App.Connection.Student.ToList();
            Students = Students.OrderBy(z => z.AttackPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void ProtectionSortClick(object sender, RoutedEventArgs e)
        {
            Students = App.Connection.Student.ToList();
            Students = Students.OrderBy(z => z.ProtectionPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void SupportSortClick(object sender, RoutedEventArgs e)
        {
            Students = App.Connection.Student.ToList();
            Students = Students.OrderBy(z => z.SupportPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void SupplyClick(object sender, RoutedEventArgs e)
        {
            Students = App.Connection.Student.ToList();
            Students = Students.OrderBy(z => z.SupplyForce).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void SearchSortClick(object sender, RoutedEventArgs e)
        {
            Students = App.Connection.Student.ToList();
            Students = Students.OrderBy(z => z.SearchPower).ToList();
            Students.Reverse();
            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
        }

        private void StudentSuperpowerListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var studentSuperpower = (sender as ListBox).SelectedItem as StudentSuperpower;

            if(studentSuperpower != null)
            {
                NavigationService.Navigate(new SuperpowerPage(App.Connection.Superpower.Where(z =>
                                 z.IdSuperpower == studentSuperpower.IdSuperpower).FirstOrDefault()));
            }
        }

        private void ShowStudentClick(object sender, RoutedEventArgs e)
        {
            int studentId = int.Parse((sender as Button).Tag.ToString());

            var stud = App.Connection.Student.Where(z => z.IdStudent == studentId).FirstOrDefault();
            if (stud != null)
                NavigationService.Navigate(new StudentPage(stud));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

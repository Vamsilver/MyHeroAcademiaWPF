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
    /// Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        public Student CurrentStudent;
        public StudentPage()
        {
            InitializeComponent();
        }

        public StudentPage(Student student)
        {
            InitializeComponent();
            CurrentStudent = student;
            NameTextBlock.Text = student.Name;
            AttackTextBlock.Text = student.AttackPower.ToString();
            ProtectionTextBlock.Text = student.ProtectionPower.ToString();
            SearchTextBlock.Text = student.SearchPower.ToString();
            SupportTextBlock.Text = student.SupportPower.ToString();
            SupplyTextBlock.Text = student.SupplyForce.ToString();
            RankTextBlock.Text = student.Rank.Value;

            var listStudentSuperpowers = App.Connection.StudentSuperpower.Where(z => z.IdStudent == student.IdStudent).ToList();
            var listSuperpowers = new List<Superpower>();
            foreach(var power in listStudentSuperpowers)
            {
                listSuperpowers.Add(App.Connection.Superpower.Where(z => z.IdSuperpower == power.IdSuperpower).FirstOrDefault());
            }
            SuperpowersListBox.ItemsSource = listSuperpowers;


            AttackProgressBar.Maximum = 100000;
            AttackProgressBar.Minimum = 0;
            AttackProgressBar.Value = Convert.ToDouble(student.AttackPower);

            ProtectionProgressBar.Maximum = 100000;
            ProtectionProgressBar.Minimum = 0;
            ProtectionProgressBar.Value = Convert.ToDouble(student.ProtectionPower);

            SearchProgressBar.Maximum = 100000;
            SearchProgressBar.Minimum = 0;
            SearchProgressBar.Value = Convert.ToDouble(student.SearchPower);

            SupportProgressBar.Maximum = 100000;
            SupportProgressBar.Minimum = 0;
            SupportProgressBar.Value = Convert.ToDouble(student.SupportPower);

            SupplyProgressBar.Maximum = 100000;
            SupplyProgressBar.Minimum = 0;
            SupplyProgressBar.Value = Convert.ToDouble(student.SupplyForce);
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SuperpowersListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var power = SuperpowersListBox.SelectedItem as Superpower;
            if ( power != null)
            {
                NavigationService.Navigate(new SuperpowerPage(power));
            }
        }
    }
}

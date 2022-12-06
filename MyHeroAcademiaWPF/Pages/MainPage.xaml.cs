using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using MyHeroAcademiaWPF.Pages;
using MyHeroAcademiaWPF.ADO;

namespace MyHeroAcademiaWPF.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<Class> classes;
        public MainPage()
        {
            InitializeComponent();
            FillClassListBox();
        }

        private void AddAvatarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() != null)
                {
                    var ImageMatt = File.ReadAllBytes(openFile.FileName);
                    ADO.Image image = new ADO.Image()
                    {
                        ImageFile = ImageMatt
                    };
                    App.Connection.Image.Add(image);
                    App.Connection.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выберете что нибудь!");
                }
            }
            catch
            {
                MessageBox.Show("Только фото пжожалуйста!");
            }
        }

        private void AddSuperPowerClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSuperpowerPage());
        }

        private void AddClassClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddClassPage());
        }

        private void FillClassListBox()
        {
            classes = App.Connection.Class.ToList();
            ClassesListBox.ItemsSource = classes;
        }

        private void ClassesListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new ClassPage(ClassesListBox.SelectedItem as Class));
        }

        private void NameSortClick(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.Name).ToList();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void RankSortClick(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.IdRank).ToList();
            classes.Reverse();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void AttackSort(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.AttackPower).ToList();
            classes.Reverse();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void ProtectionSortClick(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.ProtectionPower).ToList();
            classes.Reverse();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void SupportSortClick(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.SupportPower).ToList();
            classes.Reverse();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void SupplyClick(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.SupplyForce).ToList();
            classes.Reverse();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void SearchSortClick(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.SearchPower).ToList();
            classes.Reverse();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void StudentsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentsPage());
        }

        private void PotentialSortClick(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            classes = classes.OrderBy(z => z.IdPotential).ToList();
            classes.Reverse();
            ClassesListBox.ItemsSource = classes;
            ClassesListBox.Items.Refresh();
        }

        private void AddFinalSuperpower(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddFinalSuperpower());
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            FillClassListBox();
            ClassesListBox.Items.Refresh();
        }
    }
}

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
    /// Interaction logic for AddFinalSuperpower.xaml
    /// </summary>
    public partial class AddFinalSuperpower : Page
    {
        public AddFinalSuperpower()
        {
            InitializeComponent();

            SuperpowersComboBox.ItemsSource = App.Connection.Superpower.ToList();

            AttackProgressBar.Maximum = 10000;
            AttackProgressBar.Minimum = 0;
            AttackProgressBar.Value = 0;

            ProtectionProgressBar.Maximum = 10000;
            ProtectionProgressBar.Minimum = 0;
            ProtectionProgressBar.Value = 0;

            SearchProgressBar.Maximum = 10000;
            SearchProgressBar.Minimum = 0;
            SearchProgressBar.Value = 0;

            SupportProgressBar.Maximum = 10000;
            SupportProgressBar.Minimum = 0;
            SupportProgressBar.Value = 0;

            SupplyProgressBar.Maximum = 10000;
            SupplyProgressBar.Minimum = 0;
            SupplyProgressBar.Value = 0;

        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(NameTextBox.Text) || String.IsNullOrEmpty(AttackTextBox.Text) ||
                    String.IsNullOrEmpty(ProtectionTextBox.Text) || String.IsNullOrEmpty(SearchTextBox.Text) ||
                    String.IsNullOrEmpty(SupportTextBox.Text) || String.IsNullOrEmpty(SupplyTextBox.Text) ||
                    SuperpowersComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                {
                    FinalPower finalPower = new FinalPower()
                    {
                        Name = NameTextBox.Text,
                        AttackPower = Convert.ToDecimal(AttackTextBox.Text),
                        ProtectionPower = Convert.ToDecimal(ProtectionTextBox.Text),
                        SearchPower = Convert.ToDecimal(SearchTextBox.Text),
                        SupportPower = Convert.ToDecimal(SupportTextBox.Text),
                        SupplyForce = Convert.ToDecimal(SupplyTextBox.Text),
                        IdSuperpower = (SuperpowersComboBox.SelectedItem as Superpower).IdSuperpower
                    };
                    App.Connection.FinalPower.Add(finalPower);
                    App.Connection.SaveChanges();
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AttackTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = Convert.ToDouble(AttackTextBox.Text);
                if (value >= 0 && value <= 10000)
                {
                    AttackProgressBar.Value = value;
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                MessageBox.Show("Некорректный ввод! (Значение силы может быть в пределах от 0 до 10000)");
                (sender as TextBox).Text = "";
            }
        }

        private void ProtectionTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = Convert.ToDouble(ProtectionTextBox.Text);
                if (value >= 0 && value <= 10000)
                {
                    ProtectionProgressBar.Value = value;
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                MessageBox.Show("Некорректный ввод! (Значение силы может быть в пределах от 0 до 10000)");
                (sender as TextBox).Text = "";
            }
        }

        private void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = Convert.ToDouble(SearchTextBox.Text);
                if (value >= 0 && value <= 10000)
                {
                    SearchProgressBar.Value = value;
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                MessageBox.Show("Некорректный ввод! (Значение силы может быть в пределах от 0 до 10000)");
                (sender as TextBox).Text = "";
            }
        }

        private void SupportTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = Convert.ToDouble(SupportTextBox.Text);
                if (value >= 0 && value <= 10000)
                {
                    SupportProgressBar.Value = value;
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                MessageBox.Show("Некорректный ввод! (Значение силы может быть в пределах от 0 до 10000)");
                (sender as TextBox).Text = "";
            }
        }

        private void SupplyTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = Convert.ToDouble(SupplyTextBox.Text);
                if (value >= 0 && value <= 10000)
                {
                    SupplyProgressBar.Value = value;
                }
                else
                    (sender as TextBox).Text = "";
            }
            catch
            {
                MessageBox.Show("Некорректный ввод! (Значение силы может быть в пределах от 0 до 10000)");
                (sender as TextBox).Text = "";
            }
        }
    }
}

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
using MyHeroAcademiaWPF.ADO;

namespace MyHeroAcademiaWPF.Pages
{
    /// <summary>
    /// Interaction logic for AddSuperpowerPage.xaml
    /// </summary>
    public partial class AddSuperpowerPage : Page
    {
        public AddSuperpowerPage()
        {
            InitializeComponent();
            var listQuaility = App.Connection.PowerQualityType.ToList();
            QualityComboBox.ItemsSource = listQuaility;
            var listAcquisition = App.Connection.PowerAcquisitionType.ToList();
            AcquisitionComboBox.ItemsSource = listAcquisition;

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
                    QualityComboBox.SelectedItem == null || AcquisitionComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                {
                    var listRank = App.Connection.Rank.ToList();

                    int idPowerType = 0;
                    var attackPower = new PowerValueAndType(1,Convert.ToDecimal(AttackTextBox.Text));
                    var protectionPower = new PowerValueAndType(2, Convert.ToDecimal(ProtectionTextBox.Text));
                    var supportPower = new PowerValueAndType(3, Convert.ToDecimal(SupportTextBox.Text));
                    var searchPower = new PowerValueAndType(4, Convert.ToDecimal(SearchTextBox.Text));
                    var supplyPower = new PowerValueAndType(5, Convert.ToDecimal(SupplyTextBox.Text));

                    if (attackPower.Value == protectionPower.Value && attackPower.Value == supplyPower.Value &&
                        attackPower.Value == searchPower.Value && attackPower.Value == supplyPower.Value)
                    {
                        idPowerType = 6;
                    }
                    else
                    {
                        PowerValueAndType[] powerValues = { attackPower, protectionPower, supportPower, searchPower, supplyPower };

                        var sortedPowerValues = powerValues.OrderBy(powerValue => powerValue.Value).ToList();
                        idPowerType = sortedPowerValues.Last().Id;
                    }

                    var AveragePower = (attackPower.Value + protectionPower.Value + supportPower.Value + searchPower.Value + supplyPower.Value) / 5;
                    int idRank = listRank.Where(z => AveragePower >= z.MinAveragePower && AveragePower <= z.MaxAveragePower).FirstOrDefault().IdRank;

                    Superpower superpower = new Superpower()
                    {
                        Name = NameTextBox.Text,
                        IdPowerType = idPowerType,
                        AttackPower = attackPower.Value,
                        ProtectionPower = protectionPower.Value,
                        SupportPower = supportPower.Value,
                        SearchPower = searchPower.Value,
                        SupplyForce = supplyPower.Value,
                        IdRank = idRank,
                        IdPowerQualityType = (QualityComboBox.SelectedItem as ADO.PowerQualityType).IdPowerQualityType,
                        IdPowerAcquisitionType = (AcquisitionComboBox.SelectedItem as ADO.PowerAcquisitionType).IdPowerAcquisitionType
                    };

                    App.Connection.Superpower.Add(superpower);
                    App.Connection.SaveChanges();
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        struct PowerValueAndType
        {
            public int Id;
            public decimal Value;

            public PowerValueAndType(int id, decimal value)
            {
                Id = id;
                Value = value;
            }
        }

        private void FillRankSuperpower()
        {
            var Attack = String.IsNullOrEmpty(AttackTextBox.Text) ? 0 : Convert.ToDecimal(AttackTextBox.Text);  
            var Protection = String.IsNullOrEmpty(ProtectionTextBox.Text) ? 0 : Convert.ToDecimal(ProtectionTextBox.Text);  
            var Search = String.IsNullOrEmpty(SearchTextBox.Text) ? 0 : Convert.ToDecimal(SearchTextBox.Text);  
            var Support = String.IsNullOrEmpty(SupportTextBox.Text) ? 0 : Convert.ToDecimal(SupportTextBox.Text);  
            var Supply = String.IsNullOrEmpty(SupportTextBox.Text) ? 0 : Convert.ToDecimal(SupportTextBox.Text);

            var listRank = App.Connection.Rank.ToList();

            var AveragePower = (Attack + Protection + Search + Support + Supply) / 5;
            int idRank = listRank.Where(z => AveragePower >= z.MinAveragePower && AveragePower <= z.MaxAveragePower)
                .FirstOrDefault().IdRank;

            RankTextBlock.Text = listRank.Where(z => z.IdRank == idRank).FirstOrDefault().Value;
        }

        private void AttackTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = Convert.ToDouble(AttackTextBox.Text);
                if(value >= 0 && value <= 10000)
                {
                    AttackProgressBar.Value = value;
                    FillRankSuperpower();
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
                    FillRankSuperpower();
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
                    FillRankSuperpower();
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
                    FillRankSuperpower();
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
                    FillRankSuperpower();
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

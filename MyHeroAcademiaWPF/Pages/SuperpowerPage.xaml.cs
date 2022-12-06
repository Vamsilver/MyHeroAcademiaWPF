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
    /// Interaction logic for SuperpowerPage.xaml
    /// </summary>
    public partial class SuperpowerPage : Page
    {
        Superpower CurrentSuperpower;
        public SuperpowerPage()
        {
            InitializeComponent();
        }

        public SuperpowerPage(Superpower superpower)
        {
            InitializeComponent();
            CurrentSuperpower = superpower;
            NameTextBlock.Text = superpower.Name;
            AttackTextBlock.Text = superpower.AttackPower.ToString();
            ProtectionTextBlock.Text = superpower.ProtectionPower.ToString();
            SearchTextBlock.Text = superpower.SearchPower.ToString();
            SupplyTextBlock.Text = superpower.SupplyForce.ToString();
            SupplyTextBlock.Text = superpower.SupportPower.ToString();
            AcquisitionTextBlock.Text = superpower.PowerAcquisitionType.ToString();
            QualitytTextBlock.Text = superpower.PowerQualityType.ToString();
            RankTextBlock.Text = superpower.Rank.Value;

            AttackProgressBar.Maximum = 10000;
            AttackProgressBar.Minimum = 0;
            AttackProgressBar.Value = Convert.ToDouble(superpower.AttackPower);

            ProtectionProgressBar.Maximum = 10000;
            ProtectionProgressBar.Minimum = 0;
            ProtectionProgressBar.Value = Convert.ToDouble(superpower.ProtectionPower);

            SearchProgressBar.Maximum = 10000;
            SearchProgressBar.Minimum = 0;
            SearchProgressBar.Value = Convert.ToDouble(superpower.SearchPower);

            SupportProgressBar.Maximum = 10000;
            SupportProgressBar.Minimum = 0;
            SupportProgressBar.Value = Convert.ToDouble(superpower.SupportPower);

            SupplyProgressBar.Maximum = 10000;
            SupplyProgressBar.Minimum = 0;
            SupplyProgressBar.Value = Convert.ToDouble(superpower.SupplyForce);
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

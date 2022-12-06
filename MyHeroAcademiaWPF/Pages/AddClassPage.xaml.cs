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
using MyHeroAcademiaWPF.ADO;
using static System.Net.Mime.MediaTypeNames;

namespace MyHeroAcademiaWPF.Pages
{
    /// <summary>
    /// Interaction logic for AddClassPage.xaml
    /// </summary>
    public partial class AddClassPage : Page
    {
        Random random = new Random();

        List<String> Names = new List<String>()
        {
            "Ai ",
            "Aika ",
            "Aiko ",
            "Aimi ",
            "Aina ",
            "Airi ",
            "Akane ",
            "Akemi ",
            "Aki ",
            "Akihiro ",
            "Akiko ",
            "Akio ",
            "Akira ",
            "Amaterasu ",
            "Ami ",
            "Aoi ",
            "Arata ",
            "Asami ",
            "Ayame ",
            "Azarni ",
            "Benjiro ",
            "Botan ",
            "Chika ",
            "Chikako ",
            "Chinatsu ",
            "Chiyo ",
            "Chizu ",
            "Cho ",
            "Dai ",
            "Daichi ",
            "Daiki ",
            "Daisuke ",
            "Etsu ",
            "Etsuko ",
            "Fudo ",
            "Fujita ",
            "Goro ",
            "Gin ",
            "Hana ",
            "Hanako ",
            "Haru ",
            "Haruka ",
        };
        List<String> Surnames = new List<String>()
        {
            "Abe",
            "Akiyama",
            "Arai",
            "Araki",
            "Asano",
            "Asayama",
            "Vada",
            "Vatanabe",
            "Esimura",
            "Ikeda",
            "Imai",
            "Inoe",
            "Isida",
            "Isikava",
            "Ito",
            "Kacura",
            "Kido",
            "Kimura",
            "Kita",
            "Kitano",
            "Kobayasi",
            "Kodzima",
            "Kondo",
            "Kubo",
            "Kubota",
            "Kuroki",
            "Maruyama",
            "Matida",
            "Macuda",
            "Macui",
            "Maeda",
            "Minami",
            "Miura",
            "Morimoto",
            "Morita",
            "Murakami",
            "Murata",
            "Nagai",
            "Nakagava",
            "Nakada",
            "Nakai",
            "Nakamura",
            "Nakano",
            "Nakahara",
            "Nakayama",
            "Naradzaki",
            "Ogava",
            "Odzava",
            "Okada"
        };

        List<Student> Students = new List<Student>();
        List<StudentSuperpower> StudentSuperpowers = new List<StudentSuperpower>();
        public AddClassPage()
        {
            InitializeComponent();
            GenerateClass(Convert.ToInt32(NumOfStudentTextBox.Text));
            ListStudents.ItemsSource = Students;
        }

        private void GenerateClass(int numOfStudent)
        {
            Students.Clear();
            StudentSuperpowers.Clear();

            for (int i = 1; i <= Convert.ToInt32(NumOfStudentTextBox.Text); i++)
            {
                int idStudent;
                int idStudentSuperpower;
                if(App.Connection.Student.ToList().Count == 0)
                {
                    idStudent = i;
                }
                else
                {
                    idStudent = App.Connection.Student.ToList().Last().IdStudent + i;
                }

                if(App.Connection.StudentSuperpower.ToList().Count == 0 && StudentSuperpowers.Count == 0)
                {
                    idStudentSuperpower = i;
                }
                else if (StudentSuperpowers.Count == 0)
                {
                    idStudentSuperpower = App.Connection.StudentSuperpower.ToList().Last().IdStudentSuperpower + i;
                }
                else
                {
                    idStudentSuperpower = StudentSuperpowers.Last().IdStudentSuperpower + i;
                }
                Students.Add(GenerateStudent(idStudent, idStudentSuperpower));
            }

            ListStudents.ItemsSource = Students;
            ListStudents.Items.Refresh();
            InitializeComponent();
        }

        private Student GenerateStudent(int idStudent, int idStudentSuperpower)
        {
            List<Superpower> superpowers = App.Connection.Superpower.ToList();
            List<StudentSuperpower> studentSuperpowers = App.Connection.StudentSuperpower.ToList();
            List<ADO.Image> images = App.Connection.Image.ToList();

            var newStudent = new Student();

            newStudent.IdStudent = idStudent;
            newStudent.Name = Surnames[random.Next(Surnames.Count)] + " " + Names[random.Next(Names.Count)];
            newStudent.Image = images[random.Next(images.Count)];
            newStudent.IdImage = newStudent.Image.IdImage;

            List<StudentSuperpower> newStudentSuperpowers = new List<StudentSuperpower>();

            newStudentSuperpowers.Add(new StudentSuperpower(idStudentSuperpower, newStudent.IdStudent,
                superpowers[random.Next(superpowers.Count)].IdSuperpower));

            var superpower = superpowers.Find(z => z.IdSuperpower == newStudentSuperpowers.First().IdSuperpower);
            
            newStudent.StudentSuperpower = newStudentSuperpowers;

            var listStudentSuperpowers = new List<Superpower>();
            foreach(var power in newStudentSuperpowers)
            {
                listStudentSuperpowers.Add(App.Connection.Superpower
                    .Where(z => z.IdSuperpower == power.IdSuperpower).FirstOrDefault());
                StudentSuperpowers.Add(power);
            }

            int count = 0;
            while (random.Next(1, 4) == 3)
            {
                count++;
                var newSuperpower = superpowers[random.Next(superpowers.Count)];
                
                if (listStudentSuperpowers.Where(z => z.IdSuperpower == newSuperpower.IdSuperpower)
                    .FirstOrDefault() != null)
                    continue;
                else
                {
                    listStudentSuperpowers.Add(newSuperpower);
                    var tempStudentSuperpowers = new StudentSuperpower(StudentSuperpowers.Last().IdStudentSuperpower + count,
                                        newStudent.IdStudent, newSuperpower.IdSuperpower);
                    newStudentSuperpowers.Add(tempStudentSuperpowers);
                    StudentSuperpowers.Add(tempStudentSuperpowers);
                }
            }

            foreach (var power in listStudentSuperpowers)
            {
                newStudent.AttackPower += power.AttackPower;
                newStudent.ProtectionPower += power.ProtectionPower;
                newStudent.SearchPower += power.SearchPower;
                newStudent.SupportPower += power.SupportPower;
                newStudent.SupplyForce += power.SupplyForce;
            }

            decimal averagePower = (newStudent.AttackPower + newStudent.ProtectionPower +
                newStudent.SearchPower + newStudent.SupportPower + newStudent.SupplyForce) / 5;

            newStudent.Rank = App.Connection.Rank
                .Where(z => z.MinAveragePower >= averagePower && averagePower <= z.MaxAveragePower)
                .FirstOrDefault();

            newStudent.IdRank = newStudent.Rank.IdRank;

            return newStudent;
        }

        private void GenerateClassClick(object sender, RoutedEventArgs e)
        {
            try 
            { 
                if(!String.IsNullOrEmpty(NumOfStudentTextBox.Text))
                {
                    GenerateClass(Convert.ToInt32(NumOfStudentTextBox.Text));
                }
                else
                {
                    MessageBox.Show("Укажите число студентов");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumOfStudentTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Convert.ToInt32((sender as TextBox).Text);
            }
            catch
            {
                MessageBox.Show("Можно вводить только цифры!");
                (sender as TextBox).Text = "";
            }
        }

        private void StudentSuperpowerListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var studentSuperpower = (sender as ListBox).SelectedItem as StudentSuperpower;

            NavigationService.Navigate(new SuperpowerPage(App.Connection.Superpower.Where(z => 
            z.IdSuperpower == studentSuperpower.IdSuperpower).FirstOrDefault()));
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddClassClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!String.IsNullOrEmpty(ClassName.Text))
                {

                    ADO.Class newClass = new Class()
                    {
                        Name = ClassName.Text, 
                    };

                    decimal potentialAverageClassPower = 0;

                    foreach (var student in Students)
                    {
                        newClass.AttackPower += student.AttackPower;
                        newClass.ProtectionPower += student.ProtectionPower;
                        newClass.SupportPower += student.SupportPower;
                        newClass.SearchPower += student.SearchPower;
                        newClass.SupplyForce += student.SupplyForce;

                        var finalpower = App.Connection.FinalPower.Where(z => z.IdSuperpower ==
                            App.Connection.StudentSuperpower.Where(x => x.IdStudent == student.IdStudent)
                            .FirstOrDefault().IdSuperpower).FirstOrDefault();
                        if ( finalpower != null)
                        {
                            potentialAverageClassPower += (finalpower.AttackPower + finalpower.ProtectionPower +
                                    finalpower.SearchPower + finalpower.SupportPower + finalpower.SupplyForce) / 5;
                        }
                    }

                    Decimal averageClassPower = (newClass.AttackPower + newClass.ProtectionPower + 
                        newClass.SearchPower + newClass.SupportPower + newClass.SupplyForce) / 5;

                    newClass.IdRank = App.Connection.Rank
                        .Where(z => z.MinAveragePower >= averageClassPower && averageClassPower <= z.MaxAveragePower)
                        .FirstOrDefault().IdRank;

                    List<Potential> potentials = App.Connection.Potential.ToList();
                    if (averageClassPower == Convert.ToDecimal(0))
                    {
                        newClass.IdPotential = 1;
                    }
                    else if(averageClassPower > Convert.ToDecimal(0) && averageClassPower <= averageClassPower * Convert.ToDecimal(1.25))
                    {
                        newClass.IdPotential = 2;
                    }
                    else if(averageClassPower > averageClassPower * Convert.ToDecimal(1.25) && averageClassPower <= averageClassPower * Convert.ToDecimal(1.50))
                    {
                        newClass.IdPotential = 3;
                    }
                    else if (averageClassPower > averageClassPower * Convert.ToDecimal(1.50) && averageClassPower <= averageClassPower * Convert.ToDecimal(2))
                    {
                        newClass.IdPotential = 4;
                    }
                    else
                    {
                        newClass.IdPotential = 5;
                    }

                    App.Connection.Class.Add(newClass);

                    foreach (var student in Students)
                    {
                        student.IdClass = newClass.IdClass;    
                        App.Connection.Student.Add(student);
                    }

                    foreach (var studentSuperPower in StudentSuperpowers)
                    {
                        App.Connection.StudentSuperpower.Add(studentSuperPower);
                    }

                    App.Connection.SaveChanges();
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Дайти название классу!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

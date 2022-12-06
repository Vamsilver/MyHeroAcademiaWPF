using MyHeroAcademiaWPF.ADO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyHeroAcademiaWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MyHeroAcademiaEntities Connection = new MyHeroAcademiaEntities();
    }
}

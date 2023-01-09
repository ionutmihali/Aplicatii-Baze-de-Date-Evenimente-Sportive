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
using System.Windows.Shapes;

namespace TemaABD
{
    /// <summary>
    /// Interaction logic for MeniuAdmin.xaml
    /// </summary>
    public partial class MeniuAdmin : Window
    {
        public MeniuAdmin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)     //buton adaugare sport
        {
            AdugareSport adugareSport = new AdugareSport();
            adugareSport.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   //buton Log Out
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  //buton adaugare studenti
        {
            AdaugareStudent addStud = new AdaugareStudent();
            addStud.Show();
        }

    }
}
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
using static System.Net.Mime.MediaTypeNames;

namespace TemaABD
{
    /// <summary>
    /// Interaction logic for MeniuAntrenor.xaml
    /// </summary>
    public partial class MeniuAntrenor : Window
    {
        public int sport = 0;
        public MeniuAntrenor()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e) //delogare
        {
            if(MessageBox.Show("Vrei sa te deloghezi?","Delogare", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();

                var a = App.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (a != null)
                {
                    a.user.Text = string.Empty;
                    a.pass.Password = string.Empty;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //evenimente
        {
            EvenimenteAntrenor ev = new EvenimenteAntrenor();
            ev.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //clasament
        {
            ClasamentAntrenor cl = new ClasamentAntrenor();
            cl.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //detalii
        {

            DetaliiContAntrenor d = new DetaliiContAntrenor();
            d.Show();

        }
    }
}

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

namespace TemaABD
{
    /// <summary>
    /// Interaction logic for MeniuAdmin.xaml
    /// </summary>
    public partial class MeniuAdmin : Window
    {
        MainWindow m;
        public string tipSport { get; set; }    
        public MeniuAdmin(MainWindow m)
        {
            InitializeComponent();
            this.tipSport = "";
            LoadCombobox();
            this.m = m;
        }

        public void LoadCombobox()
        {

            List<string> s = new List<string>();
            s.Add("In Echipa");
            s.Add("Individual");

            TipEveniment.ItemsSource = s.ToList();
            TipEveniment.Text = "Select";

        }

        private void Button_Click(object sender, RoutedEventArgs e)     //buton adaugare sport
        {
            AdugareSport adugareSport = new AdugareSport();
            adugareSport.Show();
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   //buton Log Out
        {
            if (MessageBox.Show("Vrei sa te deloghezi?", "Delogare", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();

                m.Visibility = Visibility.Visible;
                if (m != null)
                {
                    m.user.Text = string.Empty;
                    m.pass.Password = string.Empty;
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)  //buton adaugare studenti
        {
            AdaugareStudent addStud = new AdaugareStudent();
            addStud.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)   // buton adaugare evenimente sportive
        {
            AdaugareEveniment a = new AdaugareEveniment(tipSport);
            if (tipSport == "")
                MessageBox.Show("Selectati tipul sportului!");
            else
                a.Show();
        }

        private void TipEveniment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TipEveniment.SelectedValue == "Individual")
                this.tipSport = "Individual";
            else if (TipEveniment.SelectedValue == "In Echipa")
                this.tipSport = "In Echipa";
        }
    }
}

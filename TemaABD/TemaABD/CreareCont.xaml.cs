using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CreareCont.xaml
    /// </summary>
    public partial class CreareCont : Window
    {
        private string tip;
        public CreareCont()
        {
            InitializeComponent();
            this.TipUtilizator.Items.Add("Administrator");
            this.TipUtilizator.Items.Add("Antrenor");
        }

        static void InsertExample(string u, string p,string t)
        {
            var context = new SportsEntities();
            var newUser = new Utilizatori()
            {
                username = u,
                parola = p,
                tip = t,

            };
            context.Utilizatoris.Add(newUser);
            context.SaveChanges();
        }
        private void Butoncontnou_Click(object sender, RoutedEventArgs e)
        {
            InsertExample(user.Text, pass.Password, this.tip);
            MessageBox.Show("Cont creat!");
            MainWindow m = new MainWindow();
            this.Close();
            m.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.Show();
        }

        private void TipUtilizator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TipUtilizator.SelectedValue == "Administrator")
                this.tip = "Administrator";
            else if (TipUtilizator.SelectedValue == "Antrenor")
                this.tip = "Antrenor";
            else {
                MessageBox.Show("Selecteaza tipul de utilizator.");
            }
        }
    }
}

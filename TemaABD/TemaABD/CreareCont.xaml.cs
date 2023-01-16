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
    /// Interaction logic for CreareCont.xaml
    /// </summary>
    public partial class CreareCont : Window
    {
        private string tip;
        MainWindow m;
        public CreareCont(MainWindow m)
        {
            InitializeComponent();
            this.TipUtilizator.Items.Add("Administrator");
            this.TipUtilizator.Items.Add("Antrenor");
            this.m = m;
        }
        static void InsertExample(string u, string p, string t)
        {
            var context = new SportsEntities();
            var newUser = new Utilizatori()
            {
                username = u,
                parola = p,
                tip = t

            };
            context.Utilizatoris.Add(newUser);
            context.SaveChanges();

            var results = from users in context.Utilizatoris
                          select new
                          {
                              users.IDUtilizator,
                              users.username,

                          };

            int id = 0;
            foreach (var item in results)
            {
                if (item.username == u)
                {
                    id = item.IDUtilizator;
                }
            }

            if (t == "Antrenor")
            {
                var newAntrenor = new Antrenori()
                {
                    IDUtilizator = id
                };

                context.Antrenoris.Add(newAntrenor);
                context.SaveChanges();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertExample(user.Text, pass.Password, this.tip);
            MessageBox.Show("User inserted");
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            m.Visibility = Visibility.Visible;
            this.Close();
        }

        private void TipUtilizator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TipUtilizator.SelectedValue == "Administrator")
                this.tip = "Administrator";
            else if (TipUtilizator.SelectedValue == "Antrenor")
                this.tip = "Antrenor";
            else
            {
                MessageBox.Show("Selecteaza tipul de utilizator.");
            }
        }
    }
}
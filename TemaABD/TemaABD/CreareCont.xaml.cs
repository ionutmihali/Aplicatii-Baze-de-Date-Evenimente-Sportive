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
        public CreareCont()
        {
            InitializeComponent();
        }

        static void InsertExample(string u, string p)
        {
            var context = new SportsEntities();
            var newUser = new Utilizatori()
            {
                username = u,
                parola = p,

            };
            context.Utilizatoris.Add(newUser);
            context.SaveChanges();
        }
        private void Butoncontnou_Click(object sender, RoutedEventArgs e)
        {
            InsertExample(user.Text, pass.Password);
            MessageBox.Show("User inserted!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.Show();
        }
    }
}

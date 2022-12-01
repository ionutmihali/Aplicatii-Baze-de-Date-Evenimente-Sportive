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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Butoncontnou_Click(object sender, RoutedEventArgs e)
        {
            CreareCont c = new CreareCont();
            this.Close();
            c.Show();
        }
        
        private void Butonantrenor_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SportsEntities())
            {
                var results = from c in context.Utilizatoris
                              select new
                              {
                                 c.username,
                                 c.parola
                              };

                if (user.Text == "admin")
                {
                    if (pass.Password == "admin")
                    {
                        MessageBox.Show("Eroare la autentificare");
                        return;

                    }
                }

                foreach (var item in results)
                { 
                    if (user.Text == item.username)
                    {
                        if (pass.Password == item.parola)
                        {
                            MessageBox.Show("Antrenor logat!");
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Eroare la autentificare!");
                            break;
                        }
                    }
                }
            }
        }
        
        private void Butonadmin_Click(object sender, RoutedEventArgs e)
        {
            if (user.Text == "admin")
            {
                if (pass.Password == "admin")
                {
                    MessageBox.Show("Administrator logat!");

                }
                else
                {
                    MessageBox.Show("Eroare la autentificare: Not an administrator!");

                }
            }
        }
    }


}

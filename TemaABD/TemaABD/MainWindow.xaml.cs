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

        private void Button_Click(object sender, RoutedEventArgs e)     // buton creare cont nou
        {
            CreareCont c = new CreareCont(this);
            this.Visibility = Visibility.Collapsed;
            c.Show();
        }


        private void ButonAntrenor_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SportsEntities())
            {
                var results = from c in context.Utilizatoris
                              select new
                              {
                                  c.username,
                                  c.parola,
                                  c.tip
                              };

                int flag = 0;
                foreach (var item in results)
                {
                    if (item.tip == "Antrenor")
                    {
                        if (user.Text == item.username)
                        {
                            if (pass.Password == item.parola)
                            {
                                MessageBox.Show("Antrenor logat!");
                                MeniuAntrenor m = new MeniuAntrenor(this);
                                this.Visibility = Visibility.Collapsed;
                                m.Show();
                                flag = 1;
                                break;
                            }
                            else
                            {
                                MessageBox.Show("Eroare la autentificare: verifica datele de logare!");
                                flag = 1;
                                user.Text = "";
                                pass.Password = "";
                                break;
                            }
                        }
                    }
                }
                if (flag == 0)
                    MessageBox.Show("Acces interzis!");
            }
        }

        private void ButonAdmin_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SportsEntities())
            {
                var results = from c in context.Utilizatoris
                              select new
                              {
                                  c.username,
                                  c.parola,
                                  c.tip,
                              };

                int flag = 0;
                foreach (var item in results)
                {
                    if (item.tip == "Administrator")
                    {
                        if (user.Text == item.username)
                        {
                            if (pass.Password == item.parola)
                            {
                                MessageBox.Show("Administrator logat!");
                                MeniuAdmin adminMenu = new MeniuAdmin(this);
                                this.Visibility = Visibility.Collapsed;
                                adminMenu.Show();
                                flag = 1;
                                break;
                            }
                            else
                            {
                                MessageBox.Show("Eroare la autentificare: verifica datele de logare!");
                                break;
                            }
                        }
                    }
                }

                if (flag == 0)
                    MessageBox.Show("Acces interzis!");
            }

        }
    }
}
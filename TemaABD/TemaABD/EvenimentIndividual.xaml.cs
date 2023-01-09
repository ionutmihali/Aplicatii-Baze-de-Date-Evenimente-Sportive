using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
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
    /// Interaction logic for EvenimentIndividual.xaml
    /// </summary>
    public partial class EvenimentIndividual : Window
    {
        int id_meci;
        public EvenimentIndividual(int id, string n, string e1, string e2, int s1, int s2, string sp)
        {
            InitializeComponent();
            ev.Text = n;
            j1.Text = e1;
            j2.Text = e2;
            scor1.Text = s1.ToString();
            scor2.Text = s2.ToString();
            sport.Text = sp;
            id_meci = id;
            stare.Text = "In desfasurare";

            meci.Visibility = Visibility.Collapsed;
            runda.Visibility = Visibility.Collapsed;

            var context = new SportsEntities();
            var a = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (a != null)
            {
                var res = from u in context.Utilizatoris
                          join ant in context.Antrenoris on u.IDUtilizator equals ant.IDUtilizator
                          join s in context.Sporturis on ant.IDSport equals s.IDSport
                          where u.username == a.user.Text
                          select new
                          {
                              s.denumire
                          };


                foreach (var aux in res)
                {
                    if (aux.denumire == sp)
                    {
                        meci.Visibility = Visibility.Visible;
                        runda.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void inapoi_Click(object sender, RoutedEventArgs e)
        {
            EvenimenteAntrenor ev = new EvenimenteAntrenor();
            ev.Show();
            this.Close();
        }

        private void runda_Click(object sender, RoutedEventArgs e)
        {
            var context = new SportsEntities();

            if (MessageBox.Show("Jucator 1 a castigat runda?", "Castigator runda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int s = Int32.Parse(scor1.Text.ToString());
                s++;
                scor1.Text = s.ToString();

                var aux = context.EvenimentSportivIndividuals.Find(id_meci);
                aux.scor1 = s;
                context.SaveChanges();
            }
            else
            {
                int s = Int32.Parse(scor2.Text.ToString());
                s++;
                scor2.Text = s.ToString();


                var aux = context.EvenimentSportivIndividuals.Find(id_meci);
                aux.scor2 = s;
                context.SaveChanges();
            }
        }

        private void meci_Click(object sender, RoutedEventArgs e)
        {
            var context = new SportsEntities();
            int sc1 = Int32.Parse(scor1.Text.ToString()), sc2 = Int32.Parse(scor2.Text.ToString());
            if (sc1 > sc2)
            {
                string s = j1.Text.ToString() + " a castigat meciul.";
                MessageBox.Show(s);
                stare.Text = "Final";
                var aux = context.EvenimentSportivIndividuals.Find(id_meci);
                aux.stare = "Final";
                context.SaveChanges();
                runda.Visibility = Visibility.Collapsed;
                meci.Visibility = Visibility.Collapsed;

                int id = 0;
                var results = from e1 in context.Studentis
                              join ss in context.EvenimentSportivIndividuals on e1.IDStudent equals ss.player1
                              join spo in context.SporturiStudents on e1.IDStudent equals spo.IDStudent
                              where e1.nume + " "+ e1.prenume == j1.Text.ToString()
                              select new
                              {
                                  spo.IdSporturiStudent
                              };

                foreach (var res in results)
                {
                    id = res.IdSporturiStudent;
                }

                var a = context.SporturiStudents.Find(id);
                a.scor += 3;
                context.SaveChanges();

            }
            else if (sc2 > sc1)
            {
                string s = j2.Text.ToString() + " a castigat meciul.";
                MessageBox.Show(s);
                stare.Text = "Final";
                var aux = context.EvenimentSportivIndividuals.Find(id_meci);
                aux.stare = "Final";
                context.SaveChanges();
                runda.Visibility = Visibility.Collapsed;
                meci.Visibility = Visibility.Collapsed;

                int id = 0;
                var results = from ss in context.EvenimentSportivIndividuals
                              join e2 in context.Studentis on ss.player2 equals e2.IDStudent
                              join spo in context.SporturiStudents on e2.IDStudent equals spo.IDStudent
                              where e2.nume + " "+ e2.prenume == j2.Text.ToString()
                              select new
                              {
                                  spo.IdSporturiStudent
                              };

                foreach (var res in results)
                {
                    id = res.IdSporturiStudent;
                }

                var a = context.SporturiStudents.Find(id);
                a.scor += 3;
                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Nu se poate determina un castigator. Marcati inca o runda drept castigatoare pentru una dintre echipe!");
            }
        }
    }
}

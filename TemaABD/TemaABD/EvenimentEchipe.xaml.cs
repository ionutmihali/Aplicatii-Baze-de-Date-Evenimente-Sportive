using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for EvenimentEchipe.xaml
    /// </summary>
    public partial class EvenimentEchipe : Window
    {
        int id_meci;
        MainWindow m;

        public EvenimentEchipe(int id, string n, string e1, string e2, int s1, int s2, string sp, string sta, MainWindow m)
        {
            InitializeComponent();
            ev.Text = n;
            ec1.Text = e1;
            ec2.Text = e2;
            scor1.Text = s1.ToString();
            scor2.Text = s2.ToString();
            sport.Text = sp;
            id_meci = id;

            meci.Visibility = Visibility.Collapsed;
            runda.Visibility = Visibility.Collapsed;

            using (var context = new SportsEntities())
            {
                var results = from s in context.Echipes
                              join ss in context.EchipeStudents on s.IdEchipe equals ss.IdEchipe
                              join st in context.Studentis on ss.IDStudent equals st.IDStudent
                              where s.nume == e1
                              select new
                              {
                                  Id = st.IDStudent,
                                  Nume = st.nume,
                                  Prenume = st.prenume
                              };

                gridEchipa1.ItemsSource = results.ToList();

                var results1 = from s in context.Echipes
                               join ss in context.EchipeStudents on s.IdEchipe equals ss.IdEchipe
                               join st in context.Studentis on ss.IDStudent equals st.IDStudent
                               where s.nume == e2
                               select new
                               {
                                   Id = st.IDStudent,
                                   Nume = st.nume,
                                   Prenume = st.prenume
                               };

                gridEchipa2.ItemsSource = results1.ToList();

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
                        if (aux.denumire == sp && sta != "Final")
                        {
                            stare.Text = "In desfasurare";
                            meci.Visibility = Visibility.Visible;
                            runda.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            stare.Text = "Final";
                        }
                    }
                }
            }
            this.m = m;
        }

        private void inapoi_Click(object sender, RoutedEventArgs e)
        {
            EvenimenteAntrenor ev = new EvenimenteAntrenor(m);
            ev.Show();
            this.Close();
        }

        private void runda_Click(object sender, RoutedEventArgs e)
        {
            var context = new SportsEntities();

            if (MessageBox.Show("Echipa 1 a castigat runda?", "Castigator runda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int s = Int32.Parse(scor1.Text.ToString());
                s++;
                scor1.Text = s.ToString();

                var aux = context.EvenimentSportivEchipes.Find(id_meci);
                aux.scor1 = s;
                context.SaveChanges();
            }
            else
            {
                int s = Int32.Parse(scor2.Text.ToString());
                s++;
                scor2.Text = s.ToString();


                var aux = context.EvenimentSportivEchipes.Find(id_meci);
                aux.scor2 = s;
                context.SaveChanges();
            }
        }

        private void meci_Click(object sender, RoutedEventArgs e)
        {
            var context = new SportsEntities();
            int sc1 = Int32.Parse(scor1.Text.ToString()), sc2=Int32.Parse(scor2.Text.ToString());
            if(sc1 > sc2)
            {
                string s = ec1.Text.ToString() + " a castigat meciul.";
                MessageBox.Show(s);
                stare.Text = "Final";
                var aux = context.EvenimentSportivEchipes.Find(id_meci);
                aux.stare = "Final";
                context.SaveChanges();
                runda.Visibility = Visibility.Collapsed;
                meci.Visibility = Visibility.Collapsed;
                
                int id =0;
                var results = from e1 in context.Echipes
                               join ss in context.EvenimentSportivEchipes on e1.IdEchipe equals ss.echipa1
                               where e1.nume == ec1.Text.ToString()
                               select new
                               {
                                   e1.IdEchipe
                               };

                foreach (var res in results)
                {
                    id = res.IdEchipe;
                }

                var a = context.Echipes.Find(id);
                a.scor = a.scor + 3;
                context.SaveChanges();

            }
            else if(sc2> sc1)
            {
                string s = ec2.Text.ToString() + " a castigat meciul.";
                MessageBox.Show(s);
                stare.Text = "Final";
                var aux = context.EvenimentSportivEchipes.Find(id_meci);
                aux.stare = "Final";
                context.SaveChanges();
                runda.Visibility = Visibility.Collapsed;
                meci.Visibility = Visibility.Collapsed;

                int id = 0;
                var results = from ss in context.EvenimentSportivEchipes
                               join e2 in context.Echipes on ss.echipa2 equals e2.IdEchipe
                               where e2.nume == ec1.Text.ToString()
                               select new
                               {
                                   e2.IdEchipe
                               };

                foreach (var res in results)
                {
                    id = res.IdEchipe;
                }

                var a = context.Echipes.Find(id);
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

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
    /// Interaction logic for AdaugareEveniment.xaml
    /// </summary>
    public partial class AdaugareEveniment : Window
    {
        private string tip { get; set; }

        public AdaugareEveniment(string tip)
        {
            InitializeComponent();
            this.tip = tip;
        }
        static void InsertEvenimentEchipa(string denumire, int p1, int p2)
        {
            var context = new SportsEntities();

            var neweventEchipa = new EvenimentSportivEchipe()
            {
                nume = denumire,
                echipa1 = p1, 
                echipa2 = p2,
                scor1 = 0,
                scor2 = 0,
                stare = "Activ"
            };
            context.EvenimentSportivEchipes.Add(neweventEchipa);
            context.SaveChanges();
        }

        static void InsertEvenimentIndividual(string denumire, int p1, int p2)
        {
            var context = new SportsEntities();
            var neweventIndividual = new EvenimentSportivIndividual()
            {
                nume = denumire,
                player1 = p1,
                player2 = p2,
                scor1 = 0,
                scor2 = 0,
                stare = "Activ"
            };
            context.EvenimentSportivIndividuals.Add(neweventIndividual);
            context.SaveChanges();
        }

        static int SelectStudent(string n, string p)
        {
            using (var context = new SportsEntities())
            {
                var results = from s in context.Studentis
                              select new
                              {
                                  s.IDStudent,
                                  s.nume,
                                  s.prenume
                              };


                foreach (var item in results)
                {
                    if (item.nume == n)
                    {
                        if (item.prenume == p)
                        {
                            return item.IDStudent;
                        }
                    }
                }
            }


            return -1;
        }
        private void Button_Click(object sender, RoutedEventArgs e)     //adaugare eveniment
        {
            int p1 = Int32.Parse(player1.Text);
            int p2 = Int32.Parse(player2.Text);

            if (tip == "In Echipa")
            {
                using (var context = new SportsEntities())
                {
                    var results = from ec in context.Echipes
                                 select new
                                 {
                                     ec.IdEchipe,
                                     ec.IDSport
                                 };



                    int count = 0;
                    int flag = 0;
                    foreach (var item in results)
                    {
                        if (item.IdEchipe == p1)
                        {
                            count++;
                        }

                        foreach (var item2 in results)
                        {
                            if (item2.IdEchipe == p2)
                            {
                                count++;
                            }

                            if (item.IDSport == item2.IDSport && count==2)
                            {
                                InsertEvenimentEchipa(eveniment.Text, p1, p2);
                                MessageBox.Show("Eveniment adaugat!");
                                flag = 1;
                                break;
                            }
                        }

                        if(flag==1)
                        {
                            break;
                        }

                        if (count != 2)
                        {
                            MessageBox.Show("Echipele nu exista.");
                            return;
                        }

                        if (flag == 0)
                        {
                            MessageBox.Show("Echipele nu practica acelasi sport.");
                            return;
                        }

                    }
                }
            }

            if (tip == "Individual")
            {
                using (var context = new SportsEntities())
                {
                    var results = from s in context.Studentis
                                  join ss in context.SporturiStudents on s.IDStudent equals ss.IDStudent
                                  select new
                                  {
                                      s.IDStudent,
                                      ss.IDSport
                                  };

                    int count = 0;
                    int flag = 0;
                    foreach (var item in results)
                    {
                        if (item.IDStudent == p1)
                        {
                            count++;
                        }

                        if (count == 1)
                        {
                            foreach (var item2 in results)
                            {

                                if (item2.IDStudent == p2)
                                {
                                    count++;
                                }


                                if (item.IDSport == item2.IDSport && count == 2)
                                {
                                    InsertEvenimentIndividual(eveniment.Text, p1, p2);
                                    MessageBox.Show("Eveniment adaugat!");
                                    flag = 1;
                                    break;
                                }

                            }

                            if (count != 2)
                            {
                                MessageBox.Show("Studentii nu exista.");
                                return;
                            }

                            if (flag == 1)
                            {
                                break;
                            }

                            if (flag == 0)
                            {
                                MessageBox.Show("Studentii nu practica acelasi sport.");
                                return;
                            }
                            

                            

                            
                        }
                    }
                }
            }

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var context = new SportsEntities())
            {
                var results = from s in context.Studentis
                              join ss in context.SporturiStudents on s.IDStudent equals ss.IDStudent
                              join sp in context.Sporturis on ss.IDSport equals sp.IDSport
                              select new
                              {
                                  s.IDStudent,
                                  s.nume,
                                  s.prenume,
                                  sp.denumire
                              };

                datagrid.Visibility = Visibility.Visible;
                datagrid.ItemsSource = results.ToList();
            }
        }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {
            using (var context = new SportsEntities())
            {
                var results = from s in context.Echipes
                              select new
                              {
                                  s.IdEchipe,
                                  s.nume,
                                  s.IDSport
                              };

                datagrid.Visibility = Visibility.Visible;
                datagrid.ItemsSource = results.ToList();
            }
        }
    }
}
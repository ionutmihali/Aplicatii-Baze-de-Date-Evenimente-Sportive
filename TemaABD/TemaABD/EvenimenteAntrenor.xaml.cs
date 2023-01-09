using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

namespace TemaABD
{
    /// <summary>
    /// Interaction logic for EvenimenteAntrenor.xaml
    /// </summary>
    public partial class EvenimenteAntrenor : Window
    {
        public EvenimenteAntrenor()
        {
            InitializeComponent();
            evenimentSportivEchipeDataGrid.Visibility = Visibility.Collapsed;
            evenimentSportivIndividualDataGrid.Visibility = Visibility.Collapsed;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new SportsEntities())
            {
                var results = from s in context.EvenimentSportivIndividuals
                              join p1 in context.Studentis on s.player1 equals p1.IDStudent
                              join p2 in context.Studentis on s.player2 equals p2.IDStudent
                              join ss in context.SporturiStudents on p1.IDStudent equals ss.IDStudent
                              join sport in context.Sporturis on ss.IDSport equals sport.IDSport
                              where s.stare=="Activ"
                              select new
                              {
                                  s.IdEvenimentSportivIndividual,
                                  s.nume,
                                  a = p1.nume + " " + p1.prenume,
                                  b = p2.nume + " " + p2.prenume,
                                  s.scor1,
                                  s.scor2,
                                  c = sport.denumire
                              };

                evenimentSportivIndividualDataGrid.ItemsSource = results.ToList();

                var results1 = from s in context.EvenimentSportivEchipes
                               join e1 in context.Echipes on s.echipa1 equals e1.IdEchipe
                               join e2 in context.Echipes on s.echipa2 equals e2.IdEchipe
                               join es in context.EchipeStudents on e1.IdEchipe equals es.IdEchipe
                               join st in context.Studentis on es.IDStudent equals st.IDStudent
                               join ss in context.SporturiStudents on st.IDStudent equals ss.IDStudent
                               join sport in context.Sporturis on ss.IDSport equals sport.IDSport
                               where s.stare == "Activ"
                               select new
                               {
                                   s.IdEvenimentSportivEchipe,
                                   s.nume,
                                   a = e1.nume,
                                   b = e2.nume,
                                   s.scor1,
                                   s.scor2,
                                   c = sport.denumire
                               };

                evenimentSportivEchipeDataGrid.ItemsSource = results1.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MeniuAntrenor m = new MeniuAntrenor();
            m.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            evenimentSportivIndividualDataGrid.Visibility = Visibility.Collapsed;
            evenimentSportivEchipeDataGrid.Visibility = Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            evenimentSportivEchipeDataGrid.Visibility = Visibility.Collapsed;
            evenimentSportivIndividualDataGrid.Visibility = Visibility.Visible;
        }

        private void evenimentSportivIndividualDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic @event = evenimentSportivIndividualDataGrid.CurrentCell.Item;
            EvenimentIndividual ev = new EvenimentIndividual(@event.IdEvenimentSportivIndividual, @event.nume, @event.a, @event.b, @event.scor1, @event.scor2, @event.c);
            ev.Show();
            this.Close();
        }

        private void evenimentSportivEchipeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic @event = evenimentSportivEchipeDataGrid.CurrentCell.Item;
            EvenimentEchipe ev = new EvenimentEchipe(@event.IdEvenimentSportivEchipe,@event.nume, @event.a, @event.b, @event.scor1, @event.scor2, @event.c);
            ev.Show();
            this.Close();
        }
    }
}

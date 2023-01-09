using System;
using System.Collections.Generic;
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
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new SportsEntities5())
            {
                var results = from s in context.EvenimentSportivIndividuals
                              select new
                              {
                                  s.IdEvenimentSportivIndividual,
                                  s.nume,
                                  s.player1,
                                  s.player2,
                                  s.scor1,
                                  s.scor2
                              };

                evenimentSportivIndividualDataGrid.ItemsSource = results.ToList();

                var results1 = from s in context.EvenimentSportivIndividuals
                               select new
                               {
                                   s.IdEvenimentSportivIndividual,
                                   s.nume,
                                   s.player1,
                                   s.player2,
                                   s.scor1,
                                   s.scor2
                               };

                evenimentSportivEchipeDataGrid.ItemsSource = results1.ToList();
            }
            TemaABD.SportsDataSet sportsDataSet = ((TemaABD.SportsDataSet)(this.FindResource("sportsDataSet")));
            // Load data into the table EvenimentSportivEchipe. You can modify this code as needed.
            TemaABD.SportsDataSetTableAdapters.EvenimentSportivEchipeTableAdapter sportsDataSetEvenimentSportivEchipeTableAdapter = new TemaABD.SportsDataSetTableAdapters.EvenimentSportivEchipeTableAdapter();
            sportsDataSetEvenimentSportivEchipeTableAdapter.Fill(sportsDataSet.EvenimentSportivEchipe);
            System.Windows.Data.CollectionViewSource evenimentSportivEchipeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("evenimentSportivEchipeViewSource")));
            evenimentSportivEchipeViewSource.View.MoveCurrentToFirst();
            // Load data into the table EvenimentSportivIndividual. You can modify this code as needed.
            TemaABD.SportsDataSetTableAdapters.EvenimentSportivIndividualTableAdapter sportsDataSetEvenimentSportivIndividualTableAdapter = new TemaABD.SportsDataSetTableAdapters.EvenimentSportivIndividualTableAdapter();
            sportsDataSetEvenimentSportivIndividualTableAdapter.Fill(sportsDataSet.EvenimentSportivIndividual);
            System.Windows.Data.CollectionViewSource evenimentSportivIndividualViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("evenimentSportivIndividualViewSource")));
            evenimentSportivIndividualViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MeniuAntrenor m = new MeniuAntrenor();
            m.Show();
            this.Close();
        }

        private void evenimentSportivEchipeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
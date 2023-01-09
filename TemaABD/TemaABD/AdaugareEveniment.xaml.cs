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
        static void InsertEvenimentEchipa(string denumire)
        {
            var context = new SportsEntities5();
            var neweventEchipa = new EvenimentSportivEchipe()
            {
                nume = denumire,
                scor1 = 0,
                scor2 = 0,
                stare = "Activ"
            };
            context.EvenimentSportivEchipes.Add(neweventEchipa);
            context.SaveChanges();
        }

        static void InsertEvenimentIndividual(string denumire)
        {
            var context = new SportsEntities5();
            var neweventIndividual = new EvenimentSportivIndividual()
            {
                nume = denumire,
                scor1 = 0,
                scor2 = 0,
                stare = "Activ"
            };
            context.EvenimentSportivIndividuals.Add(neweventIndividual);
            context.SaveChanges();
        }
        private void Button_Click(object sender, RoutedEventArgs e)     //adaugare eveniment
        {

            if (tip == "In Echipa")
            {
                InsertEvenimentEchipa(eveniment.Text);
                MessageBox.Show("Eveniment adaugat!");
            }

            if (tip == "Individual")
            {
                InsertEvenimentIndividual(eveniment.Text);
                MessageBox.Show("Eveniment adaugat!");
            }

            this.Close();
        }
    }
}

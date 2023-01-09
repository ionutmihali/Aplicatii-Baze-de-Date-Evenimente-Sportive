using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ClasamentAntrenor.xaml
    /// </summary>
    public partial class ClasamentAntrenor : Window
    {
        public string sp = "";
        public ClasamentAntrenor()
        {
            InitializeComponent();
            Load_ComboBox();
            clasamentEchipe.Visibility= Visibility.Collapsed;
            clasamentIndividual.Visibility= Visibility.Collapsed;
        }

        public void Load_ComboBox()
        {
            var context = new SportsEntities();

            List<Sporturi> s = context.Sporturis.ToList();
            combo.ItemsSource = s;
            combo.DisplayMemberPath = "denumire";
            combo.SelectedValuePath = "denumire";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //inapoi
        {
            MeniuAntrenor m = new MeniuAntrenor();
            m.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //clasament echipe
        {
            clasamentIndividual.Visibility = Visibility.Collapsed;
            clasamentEchipe.Visibility = Visibility.Visible;

            if (sp != "")
            {
                var context = new SportsEntities();

                var res = from s in context.Echipes
                          join se in context.EchipeStudents on s.IdEchipe equals se.IdEchipe
                          join st in context.Studentis on se.IDStudent equals st.IDStudent
                          join ss in context.SporturiStudents on st.IDStudent equals ss.IDStudent
                          join sp in context.Sporturis on ss.IDSport equals sp.IDSport
                          where sp.denumire==this.sp
                          orderby s.scor descending
                          select new
                          {
                              Nume = s.nume,
                              Puncte = s.scor
                          };
                clasamentEchipe.ItemsSource = res.ToList();
            }
            else
            {
                clasamentEchipe.Visibility = Visibility.Collapsed;
                MessageBox.Show("Alege un sport pentru care vrei sa vizualizezi clasamentul.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //clasament individual
        {
            clasamentEchipe.Visibility = Visibility.Collapsed;
            clasamentIndividual.Visibility = Visibility.Visible;

            if (sp != "")
            {
                var context = new SportsEntities();

                var res = from s in context.Studentis
                          join ss in context.SporturiStudents on s.IDStudent equals ss.IDStudent
                          join sp in context.Sporturis on ss.IDSport equals sp.IDSport
                          where sp.denumire == this.sp
                          orderby ss.scor descending
                          select new
                          {
                              Nume = s.nume,
                              Prenume = s.prenume,
                              Puncte = ss.scor
                          };
                clasamentIndividual.ItemsSource = res.ToList();
            }
            else
            {
                clasamentIndividual.Visibility = Visibility.Collapsed;
                MessageBox.Show("Alege un sport pentru care vrei sa vizualizezi clasamentul.");
            }
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clasamentEchipe.Visibility = Visibility.Collapsed;
            clasamentIndividual.Visibility = Visibility.Collapsed;
            sp = combo.SelectedValue.ToString();

        }
    }
}

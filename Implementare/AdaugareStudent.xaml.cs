using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for AdaugareStudent.xaml
    /// </summary>
    public partial class AdaugareStudent : Window
    {
        public AdaugareStudent()
        {
            InitializeComponent();

            LoadCombobox();
        }

        public void LoadCombobox()
        {

            var context = new SportsEntities();

            List<Sporturi> s = context.Sporturis.ToList();
            combo.ItemsSource = s;
            combo.DisplayMemberPath = "denumire";
            combo.SelectedValuePath = "IDSport";
            combo.Text = "Select";

        }
        static void InsertStudent(string n, string p)
        {
            var context = new SportsEntities();
            var newStudent = new Studenti()
            {
                nume = n,
                prenume = p
            };
            context.Studentis.Add(newStudent);
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

        static int SelectEchipa(string n)
        {
            using (var context = new SportsEntities())
            {
                var results = from s in context.Echipes
                              select new
                              {
                                  s.IdEchipe,
                                  s.nume,
                              };


                foreach (var item in results)
                {
                    if (item.nume == n)
                    {
                        return item.IdEchipe;
                    }
                }
            }

            return -1;
        }

        static int SelectSport(string sport)
        {
            using (var context = new SportsEntities())
            {
                var results = from s in context.Sporturis
                              select new
                              {
                                  s.IDSport,
                                  s.denumire
                              };


                foreach (var item in results)
                {
                    if (item.denumire == sport)
                    {
                        return item.IDSport;
                    }
                }
            }


            return -1;
        }
        static void InsertStudentSports(int idStud, int idSport)
        {
            var context = new SportsEntities();
            var newStudentSport = new SporturiStudent()
            {
                IDSport = idSport,
                IDStudent = idStud
            };
            context.SporturiStudents.Add(newStudentSport);
            context.SaveChanges();
        }

        static void InsertEchipeStudent(int idStud, int idEchipa)
        {
            var context = new SportsEntities();
            var newEchipaStud = new EchipeStudent()
            {
                IdEchipe = idEchipa,
                IDStudent = idStud
            };

            if (idEchipa != -1)
            {
                context.EchipeStudents.Add(newEchipaStud);
                context.SaveChanges();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            InsertStudent(nume.Text, prenume.Text);

            int idStudentAdaugat = SelectStudent(nume.Text, prenume.Text);
            int idSport = Convert.ToInt32(combo.SelectedValue);

            InsertStudentSports(idStudentAdaugat, idSport);


            int idStud = SelectStudent(nume.Text, prenume.Text);
            int idEch = SelectEchipa(Alegere.Text);

            InsertEchipeStudent(idStud, idEch);

            MessageBox.Show("Student adaugat!");
            this.Visibility = Visibility.Collapsed;
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int idSport = Convert.ToInt32(combo.SelectedValue);

            using (var context = new SportsEntities())
            {
                var results = from s in context.Echipes
                              where s.IDSport == idSport
                              select new
                              {
                                  s.nume
                              };
                Alegere.Visibility = Visibility.Visible;
                AlegereEchipa.Visibility = Visibility.Visible;
                grid.Visibility = Visibility.Visible;
                grid.ItemsSource = results.ToList();
            }
        }
    }
}
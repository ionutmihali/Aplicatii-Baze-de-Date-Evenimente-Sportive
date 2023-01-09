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
    /// Interaction logic for AdaugareStudent.xaml
    /// </summary>
    public partial class AdaugareStudent : Window
    {
        public AdaugareStudent()
        {
            InitializeComponent();
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertStudent(nume.Text, prenume.Text);
            MessageBox.Show("Student adaugat!");

            int idStudentAdaugat = SelectStudent(nume.Text, prenume.Text);
            int idSport = SelectSport(sport.Text);

            InsertStudentSports(idStudentAdaugat, idSport);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    /// Interaction logic for DetaliiContAntrenor.xaml
    /// </summary>
    public partial class DetaliiContAntrenor : Window
    {
        private string sp = "Alege sport";
        public DetaliiContAntrenor()
        {
            InitializeComponent();
            string text ="";
            var a = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (a != null)
            {
                text = a.user.Text;
            }

            LoadCombobox();
            int aux = verificaSport(text);
            if(aux==0)
            {
                combo.Visibility = Visibility.Collapsed;
            }
            text1.Text = text;
            text2.Text = sp;

        }

        public void LoadCombobox()
        {

            var context = new SportsEntities();

            List < Sporturi > s = context.Sporturis.ToList();
            combo.ItemsSource = s;
            combo.DisplayMemberPath = "denumire";
            combo.SelectedValuePath = "IDSport";
            combo.Text = "Select";

        }

        public int verificaSport(string t)
        {
            var context = new SportsEntities();
            var res = from u in context.Utilizatoris
                      join a in context.Antrenoris on u.IDUtilizator equals a.IDUtilizator
                      join s in context.Sporturis on a.IDSport equals s.IDSport
                      where u.username == t
                      select new
                      {
                          s.denumire
                      };


            foreach (var a in res)
            {
                if (a.denumire != null)
                {
                    sp = a.denumire;
                    return 0;
                }
                else
                {
                    return 1;
                }
                
            }
            return 1;
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sp = combo.SelectedValue.ToString();

            var context = new SportsEntities();
            int userID = 0, antrenorID = 0;

            var res1 = from s in context.Utilizatoris
                      where s.username == text1.Text
                      select new { s.IDUtilizator };

            foreach (var a1 in res1)
            {
                userID = a1.IDUtilizator;
            }

            var res2 = from s in context.Antrenoris
                       where s.IDUtilizator == userID
                       select new { s.IDAntrenori };

            foreach (var a2 in res2)
            {
                antrenorID = a2.IDAntrenori;
            }


            var antr = context.Antrenoris.Find(antrenorID);
            antr.IDSport = Int32.Parse(sp);
            context.SaveChanges();
            
            this.Close();

        }
    }
}

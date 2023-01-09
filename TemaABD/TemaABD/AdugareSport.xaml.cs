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
    /// Interaction logic for AdugareSport.xaml
    /// </summary>
    public partial class AdugareSport : Window
    {
        public AdugareSport()
        {
            InitializeComponent();
        }

        static void InsertSport(string s,string t)
        {
            var context = new SportsEntities5();
            var newSport = new Sporturi()
            {
                denumire = s,
                tip = t
            };
            context.Sporturis.Add(newSport);
            context.SaveChanges();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertSport(sport.Text,tipSport.Text);
            MessageBox.Show("Sport adaugat!");
            this.Visibility = Visibility.Collapsed;
        }
    }
}

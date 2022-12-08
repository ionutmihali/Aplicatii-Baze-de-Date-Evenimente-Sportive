using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            
            text1.Text = text;
            text2.Text = sp;

            LoadCombobox();
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

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sp = combo.SelectedItem.ToString();
        }
    }
}

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

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();
        public Window2()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            txt_rechercher.Text = "Rechercher";
            dgv_rechercher_reserv.ItemsSource = dbContext.Resevations.Select(r => new
            {
                r.ID_Reservation,
                r.Client.Nom,
                r.Client.Prenom,
                viture = r.Voiture.Matricule + " " + r.Voiture.Model.Libelle_Model,
                r.Avance,
                r.Date_D,
                r.Date_F,
                r.Nomber_Jours,
                r.Date_transactionR
            }).ToList();
            dgv_rechercher_reserv.Columns[0].Header = "ID Reservation";
            dgv_rechercher_reserv.Columns[1].Header = "Nom";
            dgv_rechercher_reserv.Columns[2].Header = "Prènom";
            dgv_rechercher_reserv.Columns[3].Header = "Marque";
            dgv_rechercher_reserv.Columns[4].Header = "Avance (DH)";
            dgv_rechercher_reserv.Columns[5].Header = "Date Debut";
            dgv_rechercher_reserv.Columns[6].Header = "Date Fin";
            dgv_rechercher_reserv.Columns[7].Header = "Nombre jours";
            dgv_rechercher_reserv.Columns[8].Header = "Date transaction";
        }

        private void txt_rechercher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_rechercher.Text == "Rechercher")
            {
                return;
            }
            else
            {
                dgv_rechercher_reserv.ItemsSource = dbContext.Resevations.Where(x => x.Client.Nom.Contains(txt_rechercher.Text) || x.Client.Prenom.Contains(txt_rechercher.Text) ||
                     x.Voiture.Model.Libelle_Model.Contains(txt_rechercher.Text) ||
                    x.Date_transactionR.Contains(txt_rechercher.Text))
                    .Select(f => new {
                        f.ID_Reservation,
                        f.Client.Nom,
                        f.Client.Prenom,
                        viture = f.Voiture.Matricule + " " + f.Voiture.Model.Libelle_Model,
                        f.Avance,
                        f.Date_D,
                        f.Date_F,
                        f.Nomber_Jours,
                        f.Date_transactionR
                    }).ToList();

                dgv_rechercher_reserv.Columns[0].Header = "ID Reservation";
                dgv_rechercher_reserv.Columns[1].Header = "Nom";
                dgv_rechercher_reserv.Columns[2].Header = "Prènom";
                dgv_rechercher_reserv.Columns[3].Header = "Marque";
                dgv_rechercher_reserv.Columns[4].Header = "Model";
                dgv_rechercher_reserv.Columns[5].Header = "Avance (DH)";
                dgv_rechercher_reserv.Columns[6].Header = "Date Debut";
                dgv_rechercher_reserv.Columns[7].Header = "Date Fin";
                dgv_rechercher_reserv.Columns[8].Header = "Nombre jours";
                dgv_rechercher_reserv.Columns[9].Header = "Date transaction";
                if (dgv_rechercher_reserv.Items.Count <= 0)
                {
                    txt_rechercher.Foreground = Brushes.Red;
                    txt_rechercher.FontWeight = FontWeights.Bold;
                }
                else
                {
                    txt_rechercher.Foreground = Brushes.Black;
                    txt_rechercher.FontWeight = FontWeights.Normal;
                }
            }
        }

        private void txt_rechercher_LostFocus(object sender, RoutedEventArgs e)
        {
            txt_rechercher.Text = "Rechercher";
        }

        private void txt_rechercher_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_rechercher.Text = string.Empty.Trim();
        }
        
        private void dgv_rechercher_reserv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var data = dgv_rechercher_reserv.SelectedItem;

            //string ID = (dgv_rechercher_reserv.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            //App.Current.Properties["id"] = ID;
            string Nom = (dgv_rechercher_reserv.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            App.Current.Properties["Nom"] = Nom;
            string prenom = (dgv_rechercher_reserv.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            App.Current.Properties["prenom"] = prenom;
            string Marque = (dgv_rechercher_reserv.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            App.Current.Properties["Marque"] = Marque;
            string Avance = (dgv_rechercher_reserv.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
            App.Current.Properties["Avance"] = Avance;
            string Date_D = (dgv_rechercher_reserv.SelectedCells[5].Column.GetCellContent(data) as TextBlock).Text;
            App.Current.Properties["Date_D"] = Date_D;
            string nbr_Jo = (dgv_rechercher_reserv.SelectedCells[7].Column.GetCellContent(data) as TextBlock).Text;
            App.Current.Properties["nbr_Jo"] = nbr_Jo;
            string Date_F = (dgv_rechercher_reserv.SelectedCells[6].Column.GetCellContent(data) as TextBlock).Text;
            App.Current.Properties["Date_F"] = Date_F;


            this.Close();
            //contrat c = new contrat();
            //c.btn_recharger.IsEnabled = true;
        }

     
    }
}

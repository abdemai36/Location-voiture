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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Reservation.xaml
    /// </summary>
    public partial class Reservation : Page
    {
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();
        public Reservation()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txt_rechercher.Text = "Rechercher";
            SetDataDGV();
            ClearControls();

            var cln = dbContext.Clients.Select(x => new
            {
                nom = x.Nom + " " + x.Prenom,
                ID = x.ID_Client

            }).ToList();
            cmb_nom.ItemsSource = cln;
            cmb_nom.DisplayMemberPath = "nom";
            cmb_nom.SelectedValuePath = "ID";


            cmb_marque.ItemsSource = dbContext.Voitures.Where(m=>m.Etat=="Disponible").Select(m => new {m.ID_Voiture,voi= m.Matricule + " " + m.Model.Libelle_Model }).ToList();
            cmb_marque.DisplayMemberPath = "voi";
            cmb_marque.SelectedValuePath = "ID_Voiture";



          
        }

        private void SetDataDGV()
        {
            dgv_Reservation.ItemsSource = dbContext.Resevations.Select(r => new
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
            dgv_Reservation.Columns[0].Header = "ID Reservation";
            dgv_Reservation.Columns[1].Header = "Nom";
            dgv_Reservation.Columns[2].Header = "Prènom";
            dgv_Reservation.Columns[3].Header = "Marque";

            dgv_Reservation.Columns[4].Header = "Avance (DH)";
            dgv_Reservation.Columns[5].Header = "Date Debut";
            dgv_Reservation.Columns[6].Header = "Date Fin";
            dgv_Reservation.Columns[7].Header = "Nombre jours";
            dgv_Reservation.Columns[8].Header = "Date transaction";
        }

        public void PopupMSG(string sourceIMG, string msg)
        {
            IMGPopup.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/" + sourceIMG));
            txtPopup.Text = msg;
            MsgPopup.IsOpen = true;
        }

        public void ClearControls()
        {
            if (dgv_Reservation.Items.Count <= 0)
                txt_id.Text = "1";
            else
            {
                txt_id.Text = Convert.ToString(dbContext.Resevations.Max(x => x.ID_Reservation) + 1);
            }
            txtavance.Text = string.Empty;
            txt_nbr_jour.Text = string.Empty;
            cmb_nom.SelectedIndex = -1;
            
            cmb_marque.SelectedIndex = -1;
            dtp_debut.Text = string.Empty;
            dtp_fin.Text = string.Empty;
            dtp_transaction.Text = string.Empty;
            HiddenLabelEmpty();
        }

        private void HiddenLabelEmpty()
        {
            Label[] lb = new Label[] {Label1, Label2, Label3, Label5, Label6, Label7, Label8,lbl};
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility = Visibility.Hidden;
            }
        }

        private void VisibleLabelEmpty()
        {
            Label[] lb = new Label[] { Label1, Label2, Label3,Label5, Label6, Label7, Label8 };
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility = Visibility.Visible;
            }
        }

        private void btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_nom.Text == string.Empty && cmb_marque.Text == string.Empty && txtavance.Text == string.Empty)
            {
                VisibleLabelEmpty();
                cmb_nom.Focus();
            }
            else
            {
                if (cmb_nom.Text == string.Empty)
                {
                    Label1.Visibility = Visibility.Visible;
                    
                    if (cmb_nom.Text != string.Empty)
                    {
                        Label1.Visibility = Visibility.Hidden;
                    }
                    cmb_nom.Focus();
                }
                else if (dtp_transaction.Text == string.Empty.Trim())
                {
                    Label2.Visibility = Visibility.Visible;
                    
                    if (dtp_transaction.Text != string.Empty.Trim())
                    {
                        Label2.Visibility = Visibility.Hidden;
                    }
                    dtp_transaction.Focus();
                }
                else if (txtavance.Text == string.Empty.Trim())
                {
                    Label5.Visibility = Visibility.Visible;
                    
                    if (txtavance.Text != string.Empty.Trim())
                    {
                        Label5.Visibility = Visibility.Hidden;
                    }
                    txtavance.Focus();
                }
                else if (cmb_marque.Text == string.Empty)
                {
                    Label3.Visibility = Visibility.Visible;
                    
                    if (cmb_marque.Text != string.Empty)
                    {
                        Label3.Visibility = Visibility.Hidden;
                    }
                    cmb_marque.Focus();
                }
                else if (dtp_debut.Text==string.Empty)
                {
                    Label6.Visibility = Visibility.Visible;
                    
                    if (dtp_debut.Text != string.Empty)
                    {
                        Label6.Visibility = Visibility.Hidden;
                    }
                    dtp_debut.Focus();
                }
                else if (dtp_fin.Text == string.Empty)
                {
                    Label7.Visibility = Visibility.Visible;
                    
                    if (dtp_fin.Text != string.Empty)
                    {
                        Label7.Visibility = Visibility.Hidden;
                    }
                    dtp_fin.Focus();
                }
                else if (txt_nbr_jour.Text == string.Empty)
                {
                    Label8.Visibility = Visibility.Visible;
                    
                    if (txt_nbr_jour.Text != string.Empty)
                    {
                        Label8.Visibility = Visibility.Hidden;
                    }
                    txt_nbr_jour.Focus();
                }
                else
                {
                    if (cmb_nom.SelectedValue == null)
                    {
                        PopupMSG("refuse.png", "Le client " + cmb_nom.Text + " n'exist pas !");
                    }
                    else
                    {
                        Resevation r = new Resevation();
                        r.ID_Client = int.Parse(cmb_nom.SelectedValue.ToString());
                        r.ID_Voiture = int.Parse(cmb_marque.SelectedValue.ToString());
                        if (txtavance.Text == string.Empty)
                                txtavance.Text = Convert.ToString(0);
                        r.Avance = Convert.ToDecimal(txtavance.Text);
                        r.Date_D = dtp_debut.SelectedDate.Value.ToShortDateString();
                        r.Date_F = dtp_fin.SelectedDate.Value.ToShortDateString();
                        r.Nomber_Jours = int.Parse(txt_nbr_jour.Text);
                        r.Date_transactionR = dtp_transaction.SelectedDate.Value.ToShortDateString();
                        dbContext.Resevations.Add(r);
                        dbContext.SaveChanges();
                        ClearControls();
                        PopupMSG("Successful.png", "Ajouté avec succès ");
                        SetDataDGV();
                        
                    }
                }
            }
        }

        private void Calcule_jour(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtp_debut.SelectedDate.Value > dtp_fin.SelectedDate.Value)
                {
                    PopupMSG("refuse.png", "La date de Debut doit etre Inférieur à la date fin reservation");
                }
                else
                {
                    TimeSpan date = dtp_fin.SelectedDate.Value - dtp_debut.SelectedDate.Value;
                    txt_nbr_jour.Text = (date.Days).ToString();
                }

            }
            catch (Exception)
            {
                PopupMSG("refuse.png", "saisir la date de reservation ");
            }

        }


        private void btn_nouveau_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void txtavance_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtavance.Text, @"[^0-9]"))
            {
                lbl.Visibility = Visibility.Visible;
                lbl.Content = "Seulement les Nombres";
                lbl.Visibility = Visibility.Visible;
                txtavance.Text = txtavance.Text.Remove(txtavance.Text.Length - 1);
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(txtavance.Text, @"[0-9]"))
            {
                lbl.Visibility = Visibility.Hidden;
            }
        }

        private void txt_rechercher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_rechercher.Text == "Rechercher")
            {
                return;
            }
            else
            {
                dgv_Reservation.ItemsSource = dbContext.Resevations.Where(x => x.Client.Nom.Contains(txt_rechercher.Text) || x.Client.Prenom.Contains(txt_rechercher.Text) ||
                     x.Voiture.Model.Libelle_Model.Contains(txt_rechercher.Text)||
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

                dgv_Reservation.Columns[0].Header = "ID Reservation";
                dgv_Reservation.Columns[1].Header = "Nom";
                dgv_Reservation.Columns[2].Header = "Prènom";
                dgv_Reservation.Columns[3].Header = "Marque";
                dgv_Reservation.Columns[4].Header = "Model";
                dgv_Reservation.Columns[5].Header = "Avance (DH)";
                dgv_Reservation.Columns[6].Header = "Date Debut";
                dgv_Reservation.Columns[7].Header = "Date Fin";
                dgv_Reservation.Columns[8].Header = "Nombre jours";
                dgv_Reservation.Columns[9].Header = "Date transaction";
                if (dgv_Reservation.Items.Count <= 0)
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

        private void txt_rechercher_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_rechercher.Text = string.Empty.Trim();
        }

        private void txt_rechercher_LostFocus(object sender, RoutedEventArgs e)
        {
            txt_rechercher.Text = "Rechercher";
        }

        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txt_id.Text);

            MessageBoxResult res = MessageBox.Show("Voulez vous vraiment supprimer cette reservation", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            try
            {
                if (cmb_nom.Text == string.Empty && cmb_marque.Text == string.Empty && txtavance.Text == string.Empty && 
                    dtp_debut.Text==string.Empty && dtp_fin.Text==string.Empty && dtp_transaction.Text==string.Empty)
                {
                    VisibleLabelEmpty();
                    cmb_nom.Focus();
                }
                else
                {
                    if (cmb_nom.Text == string.Empty)
                    {
                        Label1.Visibility = Visibility.Visible;

                        if (cmb_nom.Text != string.Empty)
                        {
                            Label1.Visibility = Visibility.Hidden;
                        }
                        cmb_nom.Focus();
                    }
                    else if (dtp_transaction.Text == string.Empty.Trim())
                    {
                        Label2.Visibility = Visibility.Visible;

                        if (dtp_transaction.Text != string.Empty.Trim())
                        {
                            Label2.Visibility = Visibility.Hidden;
                        }
                        dtp_transaction.Focus();
                    }
                    else if (txtavance.Text == string.Empty.Trim())
                    {
                        Label5.Visibility = Visibility.Visible;

                        if (txtavance.Text != string.Empty.Trim())
                        {
                            Label5.Visibility = Visibility.Hidden;
                        }
                        txtavance.Focus();
                    }
                    else if (cmb_marque.Text == string.Empty)
                    {
                        Label3.Visibility = Visibility.Visible;

                        if (cmb_marque.Text != string.Empty)
                        {
                            Label3.Visibility = Visibility.Hidden;
                        }
                        cmb_marque.Focus();
                    }
                    else if (dtp_debut.Text == string.Empty)
                    {
                        Label6.Visibility = Visibility.Visible;

                        if (dtp_debut.Text != string.Empty)
                        {
                            Label6.Visibility = Visibility.Hidden;
                        }
                        dtp_debut.Focus();
                    }
                    else if (dtp_fin.Text == string.Empty)
                    {
                        Label7.Visibility = Visibility.Visible;

                        if (dtp_fin.Text != string.Empty)
                        {
                            Label7.Visibility = Visibility.Hidden;
                        }
                        dtp_fin.Focus();
                    }
                    else if (txt_nbr_jour.Text == string.Empty)
                    {
                        Label8.Visibility = Visibility.Visible;

                        if (txt_nbr_jour.Text != string.Empty)
                        {
                            Label8.Visibility = Visibility.Hidden;
                        }
                        txt_nbr_jour.Focus();
                    }
                    else
                    {

                        if (res == MessageBoxResult.Yes)
                        {

                            Resevation rs = dbContext.Resevations.Where(k => k.ID_Reservation == id).First();
                            dbContext.Resevations.Remove(rs);
                            dbContext.SaveChanges();
                            PopupMSG("Successful.png", "La suppression a réussi !");
                            SetDataDGV();
                            ClearControls();

                        }
                    }
                }
            }
            catch (Exception)
            {
                PopupMSG("refuse.png", "Ce Membre n'existe pas !!");
            }
        }

        private void dgv_Reservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgv_Reservation.SelectedItem == null)
                {
                    return;
                }
                else
                {
                    var data = dgv_Reservation.SelectedItem;
                    string id = (dgv_Reservation.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                    txt_id.Text = id;
                    string nome = (dgv_Reservation.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                    string Prenom = (dgv_Reservation.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                    cmb_nom.Text = nome+" "+Prenom;
                    string marque = (dgv_Reservation.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
                    cmb_marque.Text = marque;
                    string avance = (dgv_Reservation.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
                    txtavance.Text = avance;
                    string DateD = (dgv_Reservation.SelectedCells[5].Column.GetCellContent(data) as TextBlock).Text;
                    dtp_debut.Text = DateD;
                    string DateF = (dgv_Reservation.SelectedCells[6].Column.GetCellContent(data) as TextBlock).Text;
                    dtp_fin.Text = DateF;
                    string NbrJ = (dgv_Reservation.SelectedCells[7].Column.GetCellContent(data) as TextBlock).Text;
                    txt_nbr_jour.Text = NbrJ;
                    string Datetrans = (dgv_Reservation.SelectedCells[8].Column.GetCellContent(data) as TextBlock).Text;
                    dtp_transaction.Text = Datetrans;
                }
            }
            catch (System.NullReferenceException ex)
            {
                PopupMSG("refuse.png", ex.Message);
            }

        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult res = MessageBox.Show("Voulez vous vraiment modifier", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    if (cmb_nom.Text == string.Empty && cmb_marque.Text == string.Empty && txtavance.Text == string.Empty && 
                        dtp_debut.Text == string.Empty && dtp_fin.Text == string.Empty && dtp_transaction.Text == string.Empty)
                    {
                        VisibleLabelEmpty();
                        cmb_nom.Focus();
                    }
                    else
                    {
                        if (cmb_nom.Text == string.Empty)
                        {
                            Label1.Visibility = Visibility.Visible;

                            if (cmb_nom.Text != string.Empty)
                            {
                                Label1.Visibility = Visibility.Hidden;
                            }
                            cmb_nom.Focus();
                        }
                        else if (dtp_transaction.Text == string.Empty.Trim())
                        {
                            Label2.Visibility = Visibility.Visible;

                            if (dtp_transaction.Text != string.Empty.Trim())
                            {
                                Label2.Visibility = Visibility.Hidden;
                            }
                            dtp_transaction.Focus();
                        }
                        else if (txtavance.Text == string.Empty.Trim())
                        {
                            Label5.Visibility = Visibility.Visible;

                            if (txtavance.Text != string.Empty.Trim())
                            {
                                Label5.Visibility = Visibility.Hidden;
                            }
                            txtavance.Focus();
                        }
                        else if (cmb_marque.Text == string.Empty)
                        {
                            Label3.Visibility = Visibility.Visible;

                            if (cmb_marque.Text != string.Empty)
                            {
                                Label3.Visibility = Visibility.Hidden;
                            }
                            cmb_marque.Focus();
                        }
                        else if (dtp_debut.Text == string.Empty)
                        {
                            Label6.Visibility = Visibility.Visible;

                            if (dtp_debut.Text != string.Empty)
                            {
                                Label6.Visibility = Visibility.Hidden;
                            }
                            dtp_debut.Focus();
                        }
                        else if (dtp_fin.Text == string.Empty)
                        {
                            Label7.Visibility = Visibility.Visible;

                            if (dtp_fin.Text != string.Empty)
                            {
                                Label7.Visibility = Visibility.Hidden;
                            }
                            dtp_fin.Focus();
                        }
                        else if (txt_nbr_jour.Text == string.Empty)
                        {
                            Label8.Visibility = Visibility.Visible;

                            if (txt_nbr_jour.Text != string.Empty)
                            {
                                Label8.Visibility = Visibility.Hidden;
                            }
                            txt_nbr_jour.Focus();
                        }
                        else
                        {
                            int id = int.Parse(txt_id.Text);
                            Resevation re = dbContext.Resevations.Where(l => l.ID_Reservation == id).First();
                            re.ID_Client = int.Parse(cmb_nom.SelectedValue.ToString());
                            re.ID_Voiture = int.Parse(cmb_marque.SelectedValue.ToString());
                            if (txtavance.Text == string.Empty)
                                txtavance.Text = Convert.ToString(0);
                            re.Avance = Convert.ToDecimal(txtavance.Text);
                            re.Date_D = dtp_debut.SelectedDate.Value.ToShortDateString();
                            re.Date_F = dtp_fin.SelectedDate.Value.ToShortDateString();
                            re.Nomber_Jours = int.Parse(txt_nbr_jour.Text);
                            re.Date_transactionR = dtp_transaction.SelectedDate.Value.ToShortDateString();
                            dbContext.SaveChanges();
                            PopupMSG("Successful.png", "Modifié avec succès");
                            SetDataDGV();
                            ClearControls();

                        }
                    }
                }
            }
            catch (Exception)
            {

                PopupMSG("refuse.png", "Cette reservation n'existe pas !");
            }
        }
    }
}

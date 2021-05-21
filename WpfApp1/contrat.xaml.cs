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
    /// Logique d'interaction pour contrat.xaml
    /// </summary>
    public partial class contrat : Page
    {

        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();

        public contrat()
        {
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //btn_recharger.IsEnabled = false;
            txt_rechercher.Text = "Rechercher";
            SetDataDgvContrat();
            ClearControls();            
            cmb_methode_paiement.Items.Add("Chéque");
            cmb_methode_paiement.Items.Add("Espéces");
            cmb_methode_paiement.Items.Add("Virement bancaire");

            cmb_nom_client.ItemsSource = dbContext.Clients.Select(cl => new {cl.ID_Client, nomP = cl.Nom + " " + cl.Prenom }).ToList();
            cmb_nom_client.DisplayMemberPath = "nomP";
            cmb_nom_client.SelectedValuePath = "ID_Client";

            cmb_nom_voiture.ItemsSource = dbContext.Voitures.Where(v => v.Etat == "Disponible").Select(v => new { v.ID_Voiture, nomV = v.Matricule + " " + v.Model.Libelle_Model }).ToList();
            cmb_nom_voiture.DisplayMemberPath = "nomV";
            cmb_nom_voiture.SelectedValuePath = "ID_Voiture";
        }

        private void SetDataDgvContrat()
        {
            dgv_contrat.ItemsSource = dbContext.Contrats.Select(c => new
            {
                c.ID_Contrat,
                nomComplet = c.Client.Nom + " " + c.Client.Prenom,
                viture = c.Voiture.Matricule + " " + c.Voiture.Model.Libelle_Model,
                c.Date_D_Contrat,
                c.Date_F_Contrat,
                c.Nombre_Jours,
                c.Montant,
                c.Paiement,
                c.Reste,
                c.Réglement,
                c.PrixPar_Jour
            }).ToList();

            dgv_contrat.Columns[0].Header = "ID contrat";
            dgv_contrat.Columns[1].Header = "Nom complet";
            dgv_contrat.Columns[2].Header = "Voiture";
            dgv_contrat.Columns[3].Header = "Date debut";
            dgv_contrat.Columns[4].Header = "Date fin";
            dgv_contrat.Columns[5].Header = "Nombre jours";
            dgv_contrat.Columns[6].Header = "Montant";
            dgv_contrat.Columns[7].Header = "Paiment";
            dgv_contrat.Columns[8].Header = "Reste";
            dgv_contrat.Columns[9].Header = "Type paiment";
            dgv_contrat.Columns[10].Header = "Prix par jour";
        }

        public void ClearControls()
        {
            if (dgv_contrat.Items.Count <= 0)
                txt_idContrat.Text = "1";
            else
            {
                txt_idContrat.Text =Convert.ToString(dbContext.Contrats.Max(co => co.ID_Contrat)+1);
            }
            txt_nbr_jours.Text = string.Empty;
            txt_prix_parJour.Text = string.Empty;
            txt_Montant.Text = string.Empty;
            txt_rest.Text = string.Empty;
            txt_Paiment.Text = string.Empty;
            cmb_methode_paiement.Text = string.Empty;
            cmb_nom_client.Text = string.Empty;
            cmb_nom_voiture.Text = string.Empty;
            dtp_debut.Text = string.Empty;
            dtp_fin.Text = string.Empty;
            HiddenLabelEmpty();
        }

        public void PopupMSG(string sourceIMG, string msg)
        {
            IMGPopup.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/" + sourceIMG));
            txtPopup.Text = msg;
            MsgPopup.IsOpen = true;
        }

        private void VisibleLabelEmpty()
        {
            Label[] lb = new Label[] { Label1, Label2, Label3, Label4, Label5, Label6, Label7, Label8, Label9, Label10 };
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility = Visibility.Visible;
            }
        }

        private void HiddenLabelEmpty()
        {
            Label[] lb = new Label[] { Label1, Label2, Label3, Label4, Label5, Label6, Label7, Label8, Label9, Label10};
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility = Visibility.Hidden;
            }
        }

        private void btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmb_nom_client.Text == string.Empty && cmb_nom_voiture.Text == string.Empty && cmb_methode_paiement.Text == string.Empty &&
                    txt_Montant.Text == string.Empty && txt_nbr_jours.Text == string.Empty && txt_Paiment.Text == string.Empty &&
                    txt_prix_parJour.Text == string.Empty)
                {
                    VisibleLabelEmpty();
                    cmb_nom_client.Focus();
                }
                else
                {
                    if (dtp_debut.Text == string.Empty)
                    {
                        Label1.Visibility = Visibility.Visible;

                        if (dtp_debut.Text != string.Empty)
                        {
                            Label1.Visibility = Visibility.Hidden;
                        }
                        dtp_debut.Focus();
                    }
                    else if (dtp_fin.Text == string.Empty.Trim())
                    {
                        Label2.Visibility = Visibility.Visible;

                        if (dtp_fin.Text != string.Empty.Trim())
                        {
                            Label2.Visibility = Visibility.Hidden;
                        }
                        dtp_fin.Focus();
                    }
                    else if (cmb_nom_client.Text == string.Empty.Trim())
                    {
                        Label3.Visibility = Visibility.Visible;

                        if (cmb_nom_client.Text != string.Empty.Trim())
                        {
                            Label3.Visibility = Visibility.Hidden;
                        }
                        cmb_nom_client.Focus();
                    }
                    else if (cmb_nom_voiture.Text == string.Empty)
                    {
                        Label4.Visibility = Visibility.Visible;

                        if (cmb_nom_voiture.Text != string.Empty)
                        {
                            Label4.Visibility = Visibility.Hidden;
                        }
                        cmb_nom_voiture.Focus();
                    }
                    else if (txt_Montant.Text == string.Empty)
                    {
                        Label5.Visibility = Visibility.Visible;

                        if (txt_Montant.Text != string.Empty)
                        {
                            Label5.Visibility = Visibility.Hidden;
                        }
                        txt_Montant.Focus();
                    }
                    else if (txt_prix_parJour.Text == string.Empty)
                    {
                        Label6.Visibility = Visibility.Visible;

                        if (txt_prix_parJour.Text != string.Empty)
                        {
                            Label6.Visibility = Visibility.Hidden;
                        }
                        txt_prix_parJour.Focus();
                    }
                    else if (txt_nbr_jours.Text == string.Empty)
                    {
                        Label7.Visibility = Visibility.Visible;

                        if (txt_nbr_jours.Text != string.Empty)
                        {
                            Label7.Visibility = Visibility.Hidden;
                        }
                        txt_nbr_jours.Focus();
                    }
                    else if (txt_Paiment.Text == string.Empty)
                    {
                        Label8.Visibility = Visibility.Visible;

                        if (txt_Paiment.Text != string.Empty)
                        {
                            Label8.Visibility = Visibility.Hidden;
                        }
                        txt_Paiment.Focus();
                    }
                    else if (txt_rest.Text == string.Empty)
                    {
                        Label9.Visibility = Visibility.Visible;

                        if (txt_rest.Text != string.Empty)
                        {
                            Label9.Visibility = Visibility.Hidden;
                        }
                        txt_rest.Focus();
                    }
                    else if (cmb_methode_paiement.Text == string.Empty)
                    {
                        Label10.Visibility = Visibility.Visible;

                        if (cmb_methode_paiement.Text != string.Empty)
                        {
                            Label10.Visibility = Visibility.Hidden;
                        }
                        cmb_methode_paiement.Focus();
                    }
                    else
                    {
                        Contrat contrat = new Contrat();
                        contrat.ID_client = int.Parse(cmb_nom_client.SelectedValue.ToString());
                        contrat.ID_voiture = int.Parse(cmb_nom_voiture.SelectedValue.ToString());
                        contrat.Date_D_Contrat = dtp_debut.SelectedDate.Value.ToShortDateString();
                        contrat.Date_F_Contrat = dtp_fin.SelectedDate.Value.ToShortDateString();
                        contrat.Nombre_Jours = int.Parse(txt_nbr_jours.Text);
                        contrat.Montant = int.Parse(txt_Montant.Text);
                        contrat.Paiement = int.Parse(txt_Paiment.Text);
                        contrat.Reste = int.Parse(txt_rest.Text);
                        contrat.Réglement = cmb_methode_paiement.SelectedItem.ToString();
                        contrat.PrixPar_Jour = int.Parse(txt_prix_parJour.Text);
                        dbContext.Contrats.Add(contrat);
                        dbContext.SaveChanges();
                        SetDataDgvContrat();
                        ClearControls();
                        PopupMSG("Successful.png", "Ajouté avec succès ");
                    }
                } 
            }
            catch (Exception ex)
            {

                PopupMSG("refuse.png", ex.Message);
            }
            
        }

        private void cmb_nom_voiture_DropDownClosed(object sender, EventArgs e)
        {

            if (cmb_nom_voiture.SelectedValue == null || txt_nbr_jours.Text == string.Empty )
            {
                PopupMSG("refuse.png", "Saisir le nombre des jours !");
                Label7.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                int i = int.Parse(cmb_nom_voiture.SelectedValue.ToString());
                txt_prix_parJour.Text = dbContext.Voitures.Where(v => v.ID_Voiture == i).Select(v => v.PrixPar_Jour).First().ToString();                
                int prixParJours = int.Parse(txt_prix_parJour.Text);
                int nmrJours = int.Parse(txt_nbr_jours.Text);
                int res = nmrJours * prixParJours;
                txt_Montant.Text = Convert.ToString(res);
            }
        }

        private void dtp_fin_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtp_debut.SelectedDate.Value > dtp_fin.SelectedDate.Value)
                {
                    PopupMSG("refuse.png", "La date de Debut doit etre Inférieur à la date fin reservation");
                    txt_nbr_jours.Text = "0";
                }
                else
                {
                    TimeSpan date = dtp_fin.SelectedDate.Value - dtp_debut.SelectedDate.Value;
                    txt_nbr_jours.Text = (date.Days).ToString();
                }

            }
            catch (Exception)
            {
                PopupMSG("refuse.png", "saisir la date de reservation ");
            }
        }

        private void txt_Paiment_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_Montant.Text == string.Empty || txt_Paiment.Text == string.Empty)
                {
                    return;
                }
                else
                {                    
                    int montant = int.Parse(txt_Montant.Text);
                    int paiment = int.Parse(txt_Paiment.Text);
                    if (paiment > montant)
                    {
                        PopupMSG("refuse.png", "le paiment doit être infirieur au montant !");                        
                        Label8.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        int res = montant - paiment;
                        txt_rest.Text = res.ToString();
                    }  
                }
            }
            catch (Exception)
            {

                PopupMSG("refuse.png", "Saisir Le montant et le paiment !");
            }
          
          
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmb_nom_client.Text == string.Empty && cmb_nom_voiture.Text == string.Empty && cmb_methode_paiement.Text == string.Empty &&
                    txt_Montant.Text == string.Empty && txt_nbr_jours.Text == string.Empty && txt_Paiment.Text == string.Empty &&
                    txt_prix_parJour.Text == string.Empty)
                {
                    VisibleLabelEmpty();
                    cmb_nom_client.Focus();
                }
                else
                {
                    if (dtp_debut.Text == string.Empty)
                    {
                        Label1.Visibility = Visibility.Visible;

                        if (dtp_debut.Text != string.Empty)
                        {
                            Label1.Visibility = Visibility.Hidden;
                        }
                        dtp_debut.Focus();
                    }
                    else if (dtp_fin.Text == string.Empty.Trim())
                    {
                        Label2.Visibility = Visibility.Visible;

                        if (dtp_fin.Text != string.Empty.Trim())
                        {
                            Label2.Visibility = Visibility.Hidden;
                        }
                        dtp_fin.Focus();
                    }
                    else if (cmb_nom_client.Text == string.Empty.Trim())
                    {
                        Label3.Visibility = Visibility.Visible;

                        if (cmb_nom_client.Text != string.Empty.Trim())
                        {
                            Label3.Visibility = Visibility.Hidden;
                        }
                        cmb_nom_client.Focus();
                    }
                    else if (cmb_nom_voiture.Text == string.Empty)
                    {
                        Label4.Visibility = Visibility.Visible;

                        if (cmb_nom_voiture.Text != string.Empty)
                        {
                            Label4.Visibility = Visibility.Hidden;
                        }
                        cmb_nom_voiture.Focus();
                    }
                    else if (txt_Montant.Text == string.Empty)
                    {
                        Label5.Visibility = Visibility.Visible;

                        if (txt_Montant.Text != string.Empty)
                        {
                            Label5.Visibility = Visibility.Hidden;
                        }
                        txt_Montant.Focus();
                    }
                    else if (txt_prix_parJour.Text == string.Empty)
                    {
                        Label6.Visibility = Visibility.Visible;

                        if (txt_prix_parJour.Text != string.Empty)
                        {
                            Label6.Visibility = Visibility.Hidden;
                        }
                        txt_prix_parJour.Focus();
                    }
                    else if (txt_nbr_jours.Text == string.Empty)
                    {
                        Label7.Visibility = Visibility.Visible;

                        if (txt_nbr_jours.Text != string.Empty)
                        {
                            Label7.Visibility = Visibility.Hidden;
                        }
                        txt_nbr_jours.Focus();
                    }
                    else if (txt_Paiment.Text == string.Empty)
                    {
                        Label8.Visibility = Visibility.Visible;

                        if (txt_Paiment.Text != string.Empty)
                        {
                            Label8.Visibility = Visibility.Hidden;
                        }
                        txt_Paiment.Focus();
                    }
                    else if (txt_rest.Text == string.Empty)
                    {
                        Label9.Visibility = Visibility.Visible;

                        if (txt_rest.Text != string.Empty)
                        {
                            Label9.Visibility = Visibility.Hidden;
                        }
                        txt_rest.Focus();
                    }
                    else if (cmb_methode_paiement.Text == string.Empty)
                    {
                        Label10.Visibility = Visibility.Visible;

                        if (cmb_methode_paiement.Text != string.Empty)
                        {
                            Label10.Visibility = Visibility.Hidden;
                        }
                        cmb_methode_paiement.Focus();
                    }
                    else
                    {
                        int iDc =int.Parse(txt_idContrat.Text);
                        Contrat contrat = dbContext.Contrats.Where(c => c.ID_Contrat == iDc).First();
                        contrat.ID_client = int.Parse(cmb_nom_client.SelectedValue.ToString());
                        contrat.ID_voiture = int.Parse(cmb_nom_voiture.SelectedValue.ToString());
                        contrat.Date_D_Contrat = dtp_debut.SelectedDate.Value.ToShortDateString();
                        contrat.Date_F_Contrat = dtp_fin.SelectedDate.Value.ToShortDateString();
                        contrat.Nombre_Jours = int.Parse(txt_nbr_jours.Text);
                        contrat.Montant = int.Parse(txt_Montant.Text);
                        contrat.Paiement = int.Parse(txt_Paiment.Text);
                        contrat.Reste = int.Parse(txt_rest.Text);
                        contrat.Réglement = cmb_methode_paiement.SelectedItem.ToString();
                        contrat.PrixPar_Jour = int.Parse(txt_prix_parJour.Text);
                        dbContext.SaveChanges();
                        SetDataDgvContrat();
                        ClearControls();
                        PopupMSG("Successful.png", "Modifié avec succès ");
                    }
                }
            }
            catch (Exception ex)
            {

                PopupMSG("refuse.png", ex.Message);
            }
        }

        private void dgv_contrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgv_contrat.SelectedItem == null)
                {
                    return;
                }
                else
                {
                    var data = dgv_contrat.SelectedItem;
                    string ID = (dgv_contrat.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                    txt_idContrat.Text = ID;
                    string NomP = (dgv_contrat.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                    cmb_nom_client.Text = NomP;
                    string voit = (dgv_contrat.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                    cmb_nom_voiture.Text = voit;
                    string DateD = (dgv_contrat.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
                    dtp_debut.Text = DateD;
                    string DateF = (dgv_contrat.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
                    dtp_fin.Text = DateF;
                    string nbrJ = (dgv_contrat.SelectedCells[5].Column.GetCellContent(data) as TextBlock).Text;
                    txt_nbr_jours.Text = nbrJ;
                    string Montant = (dgv_contrat.SelectedCells[6].Column.GetCellContent(data) as TextBlock).Text;
                    txt_Montant.Text = Montant;
                    string Paiment = (dgv_contrat.SelectedCells[7].Column.GetCellContent(data) as TextBlock).Text;
                    txt_Paiment.Text = Paiment;
                    string reste = (dgv_contrat.SelectedCells[8].Column.GetCellContent(data) as TextBlock).Text;
                    txt_rest.Text = reste;
                    string typePaiment = (dgv_contrat.SelectedCells[9].Column.GetCellContent(data) as TextBlock).Text;
                    cmb_methode_paiement.Text = typePaiment;
                    string PrixPJour = (dgv_contrat.SelectedCells[10].Column.GetCellContent(data) as TextBlock).Text;
                    txt_prix_parJour.Text = PrixPJour;
                }
            }
            catch (Exception ex)
            {

                PopupMSG("refuse.png", ex.Message);
            }
        }

        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmb_nom_client.Text == string.Empty && cmb_nom_voiture.Text == string.Empty && cmb_methode_paiement.Text == string.Empty &&
                    txt_Montant.Text == string.Empty && txt_nbr_jours.Text == string.Empty && txt_Paiment.Text == string.Empty &&
                    txt_prix_parJour.Text == string.Empty)
                {
                    VisibleLabelEmpty();
                    cmb_nom_client.Focus();
                }
                else
                {
                    if (dtp_debut.Text == string.Empty)
                    {
                        Label1.Visibility = Visibility.Visible;

                        if (dtp_debut.Text != string.Empty)
                        {
                            Label1.Visibility = Visibility.Hidden;
                        }
                        dtp_debut.Focus();
                    }
                    else if (dtp_fin.Text == string.Empty.Trim())
                    {
                        Label2.Visibility = Visibility.Visible;

                        if (dtp_fin.Text != string.Empty.Trim())
                        {
                            Label2.Visibility = Visibility.Hidden;
                        }
                        dtp_fin.Focus();
                    }
                    else if (cmb_nom_client.Text == string.Empty.Trim())
                    {
                        Label3.Visibility = Visibility.Visible;

                        if (cmb_nom_client.Text != string.Empty.Trim())
                        {
                            Label3.Visibility = Visibility.Hidden;
                        }
                        cmb_nom_client.Focus();
                    }
                    else if (cmb_nom_voiture.Text == string.Empty)
                    {
                        Label4.Visibility = Visibility.Visible;

                        if (cmb_nom_voiture.Text != string.Empty)
                        {
                            Label4.Visibility = Visibility.Hidden;
                        }
                        cmb_nom_voiture.Focus();
                    }
                    else if (txt_Montant.Text == string.Empty)
                    {
                        Label5.Visibility = Visibility.Visible;

                        if (txt_Montant.Text != string.Empty)
                        {
                            Label5.Visibility = Visibility.Hidden;
                        }
                        txt_Montant.Focus();
                    }
                    else if (txt_prix_parJour.Text == string.Empty)
                    {
                        Label6.Visibility = Visibility.Visible;

                        if (txt_prix_parJour.Text != string.Empty)
                        {
                            Label6.Visibility = Visibility.Hidden;
                        }
                        txt_prix_parJour.Focus();
                    }
                    else if (txt_nbr_jours.Text == string.Empty)
                    {
                        Label7.Visibility = Visibility.Visible;

                        if (txt_nbr_jours.Text != string.Empty)
                        {
                            Label7.Visibility = Visibility.Hidden;
                        }
                        txt_nbr_jours.Focus();
                    }
                    else if (txt_Paiment.Text == string.Empty)
                    {
                        Label8.Visibility = Visibility.Visible;

                        if (txt_Paiment.Text != string.Empty)
                        {
                            Label8.Visibility = Visibility.Hidden;
                        }
                        txt_Paiment.Focus();
                    }
                    else if (txt_rest.Text == string.Empty)
                    {
                        Label9.Visibility = Visibility.Visible;

                        if (txt_rest.Text != string.Empty)
                        {
                            Label9.Visibility = Visibility.Hidden;
                        }
                        txt_rest.Focus();
                    }
                    else if (cmb_methode_paiement.Text == string.Empty)
                    {
                        Label10.Visibility = Visibility.Visible;

                        if (cmb_methode_paiement.Text != string.Empty)
                        {
                            Label10.Visibility = Visibility.Hidden;
                        }
                        cmb_methode_paiement.Focus();
                    }
                    else
                    {
                        MessageBoxResult res = MessageBox.Show("voulez vous vraiment supprimer ?", "confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (res == MessageBoxResult.Yes)
                        {
                            int iDc = int.Parse(txt_idContrat.Text);
                            Contrat contrat = dbContext.Contrats.Where(c => c.ID_Contrat == iDc).First();
                            dbContext.Contrats.Remove(contrat);
                            dbContext.SaveChanges();
                            SetDataDgvContrat();
                            ClearControls();
                            PopupMSG("Successful.png", "La suppression a réussi !");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                PopupMSG("refuse.png", ex.Message);
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

        private void txt_rechercher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_rechercher.Text == "Rechercher")
            {
                return;
            }
            else
            {
                dgv_contrat.ItemsSource = dbContext.Contrats.Where(x => x.Client.Nom.Contains(txt_rechercher.Text) || x.Client.Prenom.Contains(txt_rechercher.Text) ||
                    x.Voiture.Model.Libelle_Model.Contains(txt_rechercher.Text) || x.Réglement.Contains(txt_rechercher.Text)||
                    x.Voiture.Matricule.Contains(txt_rechercher.Text))
                    .Select(f => new {
                        f.ID_Contrat,
                        nomComplet = f.Client.Nom + " " + f.Client.Prenom,
                        viture = f.Voiture.Matricule + " " + f.Voiture.Model.Libelle_Model,
                        f.Date_D_Contrat,
                        f.Date_F_Contrat,
                        f.Nombre_Jours,
                        f.Montant,
                        f.Paiement,
                        f.Reste,
                        f.Réglement,
                        f.PrixPar_Jour
                    }).ToList();

                dgv_contrat.Columns[0].Header = "ID contrat";
                dgv_contrat.Columns[1].Header = "Nom complet";
                dgv_contrat.Columns[2].Header = "Voiture";
                dgv_contrat.Columns[3].Header = "Date debut";
                dgv_contrat.Columns[4].Header = "Date fin";
                dgv_contrat.Columns[5].Header = "Nombre jours";
                dgv_contrat.Columns[6].Header = "Montant";
                dgv_contrat.Columns[7].Header = "Paiment";
                dgv_contrat.Columns[8].Header = "Reste";
                dgv_contrat.Columns[9].Header = "Type paiment";
                dgv_contrat.Columns[10].Header = "Prix par jour";
                if (dgv_contrat.Items.Count <= 0)
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

        private void btn_resrvation_Click(object sender, RoutedEventArgs e)
        {
            Window2 w = new Window2();
            w.Show();
        }

        private void btn_improssion_Click(object sender, RoutedEventArgs e)
        {
            frm_Print_contart frmPrint = new frm_Print_contart();
            frmPrint.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(App.Current.Properties["Nom"]==null || App.Current.Properties["prenom"]==null ||
               App.Current.Properties["Date_D"]==null || App.Current.Properties["Date_F"]==null ||
               App.Current.Properties["Marque"]==null || App.Current.Properties["nbr_Jo"]==null ||
                 App.Current.Properties["Avance"] == null)
            {
                return;
            }
            else
            {
                cmb_nom_client.Text = App.Current.Properties["Nom"] + " " + App.Current.Properties["prenom"];
                dtp_debut.Text = App.Current.Properties["Date_D"].ToString();
                dtp_fin.Text = App.Current.Properties["Date_F"].ToString();
                cmb_nom_voiture.Text = App.Current.Properties["Marque"].ToString();
                txt_nbr_jours.Text = App.Current.Properties["nbr_Jo"].ToString();
                txt_Paiment.Text = App.Current.Properties["Avance"].ToString();
            }
            
        }

        private void btn_nouveau_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

      
    }
}

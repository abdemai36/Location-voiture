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
    /// Logique d'interaction pour client.xaml
    /// </summary>
    public partial class client : Page
    {
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();

        public client()
        {
            InitializeComponent();            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txt_rechercher.Text = "Rechercher";
            txt_rechercher.Foreground = Brushes.Black;
            HiddenLabelNmbrString();
            HiddenLabelEmpty();
            SetDataDGV();
            cmb_piece.Items.Add("CIN");
            cmb_piece.Items.Add("Passeport");
            ClearControls();
           
        }
        //Message popup
        public void PopupMSG(string sourceIMG, string msg)
        {
            IMGPopup.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/" + sourceIMG));
            txtPopup.Text = msg;
            MsgPopup.IsOpen = true;
        }
        //show all labels Which control if input textbox equal text or not  
        private void VisibleLabelNmbrString()
        {
            Label[] lb = new Label[] {label11, label12, label13,label14,label15,label16,label17,label18,label19,label20 };
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility = Visibility.Visible;
            }
        }
        //show all labels Which control if a textbox  empty or not
        private void VisibleLabelEmpty()
        {
            Label[] lb = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10};
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility=Visibility.Visible;
            }
        }
        //hide all labels Which control if input textbox equal text or not
        private void HiddenLabelNmbrString()
        {
            Label[] lb = new Label[] { label11, label12, label13, label14, label15, label16, label17, label18, label19, label20 };
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility = Visibility.Hidden;
            }
        }
        //hide all labels Which control if a textbox  empty or not
        private void HiddenLabelEmpty()
        {
            Label[] lb = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13
                ,label14,label15,label16,label17,label18,label19,label20 };
            for (int i = 0; i < lb.Length; i++)
            {
                lb[i].Visibility = Visibility.Hidden;
            }
        }
        //charge datagridview
        private void SetDataDGV()
        {
            dgv_clients.ItemsSource = dbContext.Clients.Select(c => new
            {
                c.ID_Client,c.Nom,c.Prenom,c.Adresse,c.DateNaissance,c.Num_Permis,
                c.Date_Validite,c.Telephone,c.Piece_Identite,c.Num_Identite, c.Date_Transaction
            }).ToList();

            dgv_clients.Columns[0].Header = "ID Client";
            dgv_clients.Columns[1].Header = "Nom";
            dgv_clients.Columns[2].Header = "Prènom";
            dgv_clients.Columns[3].Header = "Adresse";
            dgv_clients.Columns[4].Header = "Date Naissance";
            dgv_clients.Columns[5].Header = "Numéro Pérmis";
            dgv_clients.Columns[6].Header = "Date validité";
            dgv_clients.Columns[7].Header = "Téléphone";
            dgv_clients.Columns[8].Header = "Piéce identite";
            dgv_clients.Columns[9].Header = "Numéro identite";
            dgv_clients.Columns[10].Header = "Date Transaction";
        }

        public void ClearControls()
        {
            txt_nom.Focus();
            if (dgv_clients.Items.Count <= 0)
                txt_id.Text = "1";
            else
            {
                txt_id.Text = Convert.ToString(dbContext.Clients.Max(x => x.ID_Client) + 1);
            }

            txt_nom.Text = string.Empty.Trim();
            txt_prenom.Text=string.Empty.Trim();
            txt_adresse.Text = string.Empty.Trim();
            TMQ_naissance.Text = string.Empty.Trim();
            TMQ_transaction.Text = string.Empty.Trim();
            TMQ_validite.Text = string.Empty.Trim();
            txt_num_permis.Text = string.Empty.Trim();
            txt_telephone.Text = string.Empty.Trim();
            cmb_piece.SelectedIndex = -1;
            txt_num_ident.Text = string.Empty.Trim();
            VisibleLabelEmpty();
            HiddenLabelEmpty();
        }

        private void btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_nom.Text == string.Empty.Trim() && txt_prenom.Text == string.Empty.Trim() && txt_adresse.Text == string.Empty.Trim()
             && TMQ_naissance.Text == string.Empty.Trim() && TMQ_transaction.Text == string.Empty.Trim() && TMQ_validite.Text == string.Empty.Trim()
             && txt_num_permis.Text == string.Empty.Trim() && txt_telephone.Text == string.Empty.Trim() && cmb_piece.SelectedIndex == -1 && txt_num_ident.Text == string.Empty.Trim())
                {
                    VisibleLabelEmpty();
                }
                else
                {

                    if (txt_nom.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label1.Visibility = Visibility.Visible;
                        txt_nom.Focus();
                        if (txt_nom.Text != string.Empty.Trim())
                        {
                            label1.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_prenom.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label2.Visibility = Visibility.Visible;
                        txt_prenom.Focus();
                        if (txt_prenom.Text != string.Empty.Trim())
                        {
                            label2.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_adresse.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label3.Visibility = Visibility.Visible;
                        txt_adresse.Focus();
                        if (txt_adresse.Text != string.Empty.Trim())
                        {
                            label3.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_naissance.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label4.Visibility = Visibility.Visible;
                        TMQ_naissance.Focus();
                        if (TMQ_naissance.Text != string.Empty.Trim())
                        {
                            label4.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_num_permis.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label5.Visibility = Visibility.Visible;
                        txt_num_permis.Focus();
                        if (txt_num_permis.Text != string.Empty.Trim())
                        {
                            label5.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_validite.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label6.Visibility = Visibility.Visible;
                        TMQ_validite.Focus();
                        if (TMQ_validite.Text != string.Empty.Trim())
                        {
                            label6.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_telephone.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label7.Visibility = Visibility.Visible;
                        txt_telephone.Focus();
                        if (txt_telephone.Text != string.Empty.Trim())
                        {
                            label7.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (cmb_piece.SelectedIndex == -1)
                    {
                        HiddenLabelEmpty();
                        label8.Visibility = Visibility.Visible;
                        cmb_piece.Focus();
                        if (cmb_piece.SelectedIndex != -1)
                        {
                            label8.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_num_ident.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label9.Visibility = Visibility.Visible;
                        txt_num_ident.Focus();
                        if (txt_num_ident.Text != string.Empty.Trim())
                        {
                            label9.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_transaction.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label10.Visibility = Visibility.Visible;
                        TMQ_transaction.Focus();
                        if (TMQ_transaction.Text != string.Empty.Trim())
                        {
                            label10.Visibility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        Client cln = new Client();
                        cln.Nom = txt_nom.Text;
                        cln.Prenom = txt_prenom.Text;
                        cln.Adresse = txt_adresse.Text;
                        cln.DateNaissance = TMQ_naissance.SelectedDate.Value.ToShortDateString();
                        cln.Num_Permis = int.Parse(txt_num_permis.Text);
                        cln.Date_Validite = TMQ_validite.SelectedDate.Value.ToShortDateString();
                        cln.Telephone = txt_telephone.Text;
                        cln.Piece_Identite = cmb_piece.SelectedItem.ToString();
                        cln.Num_Identite = txt_num_ident.Text;
                        cln.Date_Transaction = TMQ_transaction.SelectedDate.Value.ToShortDateString();
                        dbContext.Clients.Add(cln);
                        dbContext.SaveChanges();
                        PopupMSG("Successful.png", "Ajouté avec succès ");
                        SetDataDGV();
                        ClearControls();
                    }
                }

            }
            catch (Exception ex )
            {
                PopupMSG("refuse.png", ex.Message);
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(dbContext.Clients.Max(x => x.ID_Client)+"");
            VisibleLabelEmpty();
        }

        private void btn_Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            SetDataDGV();
        }

        private void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if (txt_nom.Text == string.Empty.Trim() && txt_prenom.Text == string.Empty.Trim() && txt_adresse.Text == string.Empty.Trim()
             && TMQ_naissance.Text == string.Empty.Trim() && TMQ_transaction.Text == string.Empty.Trim() && TMQ_validite.Text == string.Empty.Trim()
             && txt_num_permis.Text == string.Empty.Trim() && txt_telephone.Text == string.Empty.Trim() && cmb_piece.SelectedIndex == -1 && txt_num_ident.Text == string.Empty.Trim())
                {
                    VisibleLabelEmpty();
                }
                else
                {

                    if (txt_nom.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label1.Visibility = Visibility.Visible;
                        txt_nom.Focus();
                        if (txt_nom.Text != string.Empty.Trim())
                        {
                            label1.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_prenom.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label2.Visibility = Visibility.Visible;
                        txt_prenom.Focus();
                        if (txt_prenom.Text != string.Empty.Trim())
                        {
                            label2.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_adresse.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label3.Visibility = Visibility.Visible;
                        txt_adresse.Focus();
                        if (txt_adresse.Text != string.Empty.Trim())
                        {
                            label3.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_naissance.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label4.Visibility = Visibility.Visible;
                        TMQ_naissance.Focus();
                        if (TMQ_naissance.Text != string.Empty.Trim())
                        {
                            label4.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_num_permis.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label5.Visibility = Visibility.Visible;
                        txt_num_permis.Focus();
                        if (txt_num_permis.Text != string.Empty.Trim())
                        {
                            label5.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_validite.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label6.Visibility = Visibility.Visible;
                        TMQ_validite.Focus();
                        if (TMQ_validite.Text != string.Empty.Trim())
                        {
                            label6.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_telephone.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label7.Visibility = Visibility.Visible;
                        txt_telephone.Focus();
                        if (txt_telephone.Text != string.Empty.Trim())
                        {
                            label7.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (cmb_piece.SelectedIndex == -1)
                    {
                        HiddenLabelEmpty();
                        label8.Visibility = Visibility.Visible;
                        cmb_piece.Focus();
                        if (cmb_piece.SelectedIndex != -1)
                        {
                            label8.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_num_ident.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label9.Visibility = Visibility.Visible;
                        txt_num_ident.Focus();
                        if (txt_num_ident.Text != string.Empty.Trim())
                        {
                            label9.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_transaction.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label10.Visibility = Visibility.Visible;
                        TMQ_transaction.Focus();
                        if (TMQ_transaction.Text != string.Empty.Trim())
                        {
                            label10.Visibility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        MessageBoxResult res = MessageBox.Show("Voules vous vraiment supprimer ce Client ?", "Confimation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (res == MessageBoxResult.Yes)
                        {
                            int idC = int.Parse(txt_id.Text);
                            Client cl = dbContext.Clients.Where(c => c.ID_Client == idC).First();
                            dbContext.Clients.Remove(cl);
                            dbContext.SaveChanges();
                            PopupMSG("Successful.png", "La suppression a réussi ");
                            SetDataDGV();
                            ClearControls();
                        }
                    }
                }
            //}
            //catch ( Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //   // PopupMSG("refuse.png", ex.Message);
            //}
        }

        private void grv_clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgv_clients.SelectedItem == null)
                {
                    return;
                }
                else
                {
                    var data = dgv_clients.SelectedItem;
                    string ID = (dgv_clients.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                    txt_id.Text = ID;
                    string Nom = (dgv_clients.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                    txt_nom.Text = Nom;
                    string Prenom = (dgv_clients.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                    txt_prenom.Text = Prenom;
                    string Adresse = (dgv_clients.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
                    txt_adresse.Text = Adresse;
                    string DateNais = (dgv_clients.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
                    TMQ_naissance.Text = DateNais;
                    string NmrPirmis = (dgv_clients.SelectedCells[5].Column.GetCellContent(data) as TextBlock).Text;
                    txt_num_permis.Text = NmrPirmis;
                    string DateValid = (dgv_clients.SelectedCells[6].Column.GetCellContent(data) as TextBlock).Text;
                    TMQ_validite.Text = DateValid;
                    string Tele = (dgv_clients.SelectedCells[7].Column.GetCellContent(data) as TextBlock).Text;
                    txt_telephone.Text = Tele;
                    string PieceID = (dgv_clients.SelectedCells[8].Column.GetCellContent(data) as TextBlock).Text;
                    cmb_piece.Text = PieceID;
                    string NmrID = (dgv_clients.SelectedCells[9].Column.GetCellContent(data) as TextBlock).Text;
                    txt_num_ident.Text = NmrID;
                    string DateTrans = (dgv_clients.SelectedCells[10].Column.GetCellContent(data) as TextBlock).Text;
                    TMQ_transaction.Text = DateTrans;
                }
            }
            catch (Exception ex)
            {

                PopupMSG("refuse.png", ex.Message);
            }
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_nom.Text == string.Empty.Trim() && txt_prenom.Text == string.Empty.Trim() && txt_adresse.Text == string.Empty.Trim()
             && TMQ_naissance.Text == string.Empty.Trim() && TMQ_transaction.Text == string.Empty.Trim() && TMQ_validite.Text == string.Empty.Trim()
             && txt_num_permis.Text == string.Empty.Trim() && txt_telephone.Text == string.Empty.Trim() && cmb_piece.SelectedIndex == -1 && txt_num_ident.Text == string.Empty.Trim())
                {
                    VisibleLabelEmpty();
                }
                else
                {

                    if (txt_nom.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label1.Visibility = Visibility.Visible;
                        txt_nom.Focus();
                        if (txt_nom.Text != string.Empty.Trim())
                        {
                            label1.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_prenom.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label2.Visibility = Visibility.Visible;
                        txt_prenom.Focus();
                        if (txt_prenom.Text != string.Empty.Trim())
                        {
                            label2.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_adresse.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label3.Visibility = Visibility.Visible;
                        txt_adresse.Focus();
                        if (txt_adresse.Text != string.Empty.Trim())
                        {
                            label3.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_naissance.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label4.Visibility = Visibility.Visible;
                        TMQ_naissance.Focus();
                        if (TMQ_naissance.Text != string.Empty.Trim())
                        {
                            label4.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_num_permis.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label5.Visibility = Visibility.Visible;
                        txt_num_permis.Focus();
                        if (txt_num_permis.Text != string.Empty.Trim())
                        {
                            label5.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_validite.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label6.Visibility = Visibility.Visible;
                        TMQ_validite.Focus();
                        if (TMQ_validite.Text != string.Empty.Trim())
                        {
                            label6.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_telephone.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label7.Visibility = Visibility.Visible;
                        txt_telephone.Focus();
                        if (txt_telephone.Text != string.Empty.Trim())
                        {
                            label7.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (cmb_piece.SelectedIndex == -1)
                    {
                        HiddenLabelEmpty();
                        label8.Visibility = Visibility.Visible;
                        cmb_piece.Focus();
                        if (cmb_piece.SelectedIndex != -1)
                        {
                            label8.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (txt_num_ident.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label9.Visibility = Visibility.Visible;
                        txt_num_ident.Focus();
                        if (txt_num_ident.Text != string.Empty.Trim())
                        {
                            label9.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (TMQ_transaction.Text == string.Empty.Trim())
                    {
                        HiddenLabelEmpty();
                        label10.Visibility = Visibility.Visible;
                        TMQ_transaction.Focus();
                        if (TMQ_transaction.Text != string.Empty.Trim())
                        {
                            label10.Visibility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        int idC = int.Parse(txt_id.Text);
                        Client cln = dbContext.Clients.Where(m => m.ID_Client == idC).First();
                        cln.Nom = txt_nom.Text;
                        cln.Prenom = txt_prenom.Text;
                        cln.Adresse = txt_adresse.Text;
                        cln.DateNaissance = TMQ_naissance.SelectedDate.Value.ToShortDateString();
                        cln.Num_Permis = int.Parse(txt_num_permis.Text);
                        cln.Date_Validite = TMQ_validite.SelectedDate.Value.ToShortDateString();
                        cln.Telephone = txt_telephone.Text;
                        cln.Piece_Identite = cmb_piece.SelectedItem.ToString();
                        cln.Num_Identite = txt_num_ident.Text;
                        cln.Date_Transaction = TMQ_transaction.SelectedDate.Value.ToShortDateString();
                        dbContext.SaveChanges();
                        PopupMSG("Successful.png", "Modifié avec succès ");
                        HiddenLabelEmpty();
                        SetDataDGV();
                        ClearControls();
                    }
                }
            }
            catch (Exception ex)
            {

                PopupMSG("refuse.png", ex.Message);
            }
        }
        //Control if the text of textbox has numbers or not
        public void ControlsTextBoxInt(TextBox txt, Label lb)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"[^0-9]"))
            {
                lb.Visibility = Visibility.Visible;
                lb.Content = "Seulement les Nombres";
                lb.Visibility = Visibility.Visible;
                txt.Text = txt.Text.Remove(txt.Text.Length - 1);
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"[0-9]"))
            {
                lb.Visibility = Visibility.Hidden;
            }
        }

        //Control if the text of textbox has Letters or not
        public void ControlsTextBoxString(TextBox txt,Label lb)
        {
            if (txt.Text == " ")
            {
                PopupMSG("refuse.png", "Les espèces ne peuvent pas être ajoutés !");
                txt.Text = txt.Text.Remove(txt.Text.Length - 1);
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"\d"))
            {
                lb.Visibility = Visibility.Visible;
                PopupMSG("refuse.png", "seulement nombres !!");
                //lb.Content = "seulement les Lettres ";
                lb.Visibility = Visibility.Visible;
                txt.Text = txt.Text.Remove(txt.Text.Length - 1);
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, "[a-zA-Z_]*$"))
            {
                lb.Visibility = Visibility.Hidden;
            }
        }

        private void txt_nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            ControlsTextBoxString(this.txt_nom, label12);
        }

        private void txt_rechercher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_rechercher.Text == "Rechercher")
            {
                return;
            }
            else
            {
                dgv_clients.ItemsSource = dbContext.Clients.Where(x => x.Nom.Contains(txt_rechercher.Text) || x.Num_Identite.Contains(txt_rechercher.Text)||
                    x.Telephone.Contains(txt_rechercher.Text) || x.Prenom.Contains(txt_rechercher.Text))
                    .Select(f=> new { f.ID_Client, f.Nom, f.Prenom, f.Adresse, f.DateNaissance, f.Num_Permis,
                    f.Date_Validite, f.Telephone, f.Piece_Identite, f.Num_Identite, f.Date_Transaction }).ToList();

                dgv_clients.Columns[0].Header = "ID Client";
                dgv_clients.Columns[1].Header = "Nom";
                dgv_clients.Columns[2].Header = "Prènom";
                dgv_clients.Columns[3].Header = "Adresse";
                dgv_clients.Columns[4].Header = "Date Naissance";
                dgv_clients.Columns[5].Header = "Numéro Pérmis";
                dgv_clients.Columns[6].Header = "Date validité";
                dgv_clients.Columns[7].Header = "Téléphone";
                dgv_clients.Columns[8].Header = "Piéce identite";
                dgv_clients.Columns[9].Header = "Numéro identite";
                dgv_clients.Columns[10].Header = "Date Transaction";
                if (dgv_clients.Items.Count <= 0)
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

        private void txt_prenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            ControlsTextBoxString(this.txt_prenom, label12);
        }

        private void txt_num_permis_TextChanged(object sender, TextChangedEventArgs e)
        {
            ControlsTextBoxInt(this.txt_num_permis, label15);
        }
    }  
}

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
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Comptes.xaml
    /// </summary>
    public partial class Comptes : Page
    {
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();
        public Comptes()
        {
            InitializeComponent();
        }

        //public string Crypto(string passwoer)
        //{
        //    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //    UTF8Encoding uTF8Encoding = new UTF8Encoding();
        //    byte[] data = md5.ComputeHash(uTF8Encoding.GetBytes(passwoer));

        //    return Convert.ToBase64String(data);
        //}

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Login l = new Login();
                if (txt_mdp.Password == string.Empty.Trim() && txt_nom.Text == string.Empty.Trim())
                {
                    lbl_pass.Visibility = Visibility.Visible;
                    lbl_Nom.Visibility = Visibility.Visible;
                    txt_nom.Focus();
                }
                else
                {
                    if (txt_mdp.Password == string.Empty.Trim())
                    {
                        lbl_pass.Visibility = Visibility.Visible;
                        txt_mdp.Focus();
                        if (txt_nom.Text != string.Empty.Trim())
                        {
                            lbl_Nom.Visibility = Visibility.Hidden;
                        }
                        return;
                    }
                    else if (string.IsNullOrEmpty(txt_nom.Text))
                    {
                        lbl_Nom.Visibility = Visibility.Visible;
                        lbl_Nom_Exist.Visibility = Visibility.Hidden;
                        txt_nom.Focus();
                        return;
                    }
                    else
                    {
                        if (dbContext.Logins.Where(x => x.Nom == txt_nom.Text.ToLower()).ToList().Count > 0)
                        {
                            PopupMSG("refuse.png", txt_nom.Text+" déjà existe !");
                            txt_nom.Focus();
                        }
                        else
                        {
                            txt_nom.Text = txt_nom.Text.Replace(" ", "");
                            l.Nom = txt_nom.Text.Trim();
                            l.Mot_de_passe = txt_mdp.Password.Trim();
                            if (RB_Admin.IsChecked == true)
                            {
                                l.Admin_O_No = RB_Admin.Content.ToString();
                            }
                            else
                            {
                                l.Admin_O_No = RB_NoAdmin.Content.ToString();
                            }
                            dbContext.Logins.Add(l);
                            dbContext.SaveChanges();
                            lbl_Nom.Visibility = Visibility.Hidden;
                            lbl_pass.Visibility = Visibility.Hidden;
                            ClearControls();
                            PopupMSG("Successful.png", "Ajouté avec succès");
                            ChargerDGVComptes();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                PopupMSG("refuse.png", ex.Message);
            }
            
               
            
        }

        public void PopupMSG(string sourceIMG,string msg)
        {
            IMGPopup.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/"+ sourceIMG));
            txtPopup.Text = msg;
            MsgPopup.IsOpen = true;
        }
        
        public void ClearControls()
        {
            txt_mdp.Password = string.Empty;
            txt_nom.Text = string.Empty;
            passwordTxtBox.Text = string.Empty;
            lbl_Nom.Visibility = Visibility.Hidden;
            lbl_pass.Visibility = Visibility.Hidden;
            lbl_Nom_Exist.Visibility = Visibility.Hidden;
            if (dtg_comptes.Items.Count <= 0)
                txt_id.Text = "1";
            else
            {
                txt_id.Text = Convert.ToString(dbContext.Logins.Max(m => m.ID_Member) + 1);
            }
            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txt_nom.Focus();
            lbl_Nom.Visibility = Visibility.Hidden;
            lbl_pass.Visibility = Visibility.Hidden;
            lbl_Nom_Exist.Visibility = Visibility.Hidden;
            ChargeDGVDetail();
            ChargerDGVComptes();
            ClearControls();
            txt_recherche_compte.Text = "Rechercher";
            txt_recherche_compte.Foreground = Brushes.Black;
            txt_recherche_detail.Text = "Rechercher";
            txt_recherche_detail.Foreground = Brushes.Black;
        }

        private void ChargerDGVComptes()
        {
            dtg_comptes.ItemsSource = dbContext.Logins.ToList();
            dtg_comptes.Columns[0].Header = "ID membre";
            dtg_comptes.Columns[1].Header = "Nom utilisateur";
            dtg_comptes.Columns[2].Header = "Mot de passe";
            dtg_comptes.Columns[3].Header = "Statut";
        }

        private void ChargeDGVDetail()
        {
            dtg_details.ItemsSource = (from d in dbContext.Details orderby d.time_Entree descending select d).ToList();
            dtg_details.Columns[0].Header = "ID Detail";
            dtg_details.Columns[1].Header = "Nom utilisateur";
            dtg_details.Columns[2].Header = "Date d'entrer";
            dtg_details.Columns[3].Header = "Heur d'entrer";
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txt_id.Text);

            MessageBoxResult res = MessageBox.Show("Voulez vous vraiment supprimer ce Mmebre", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            try
            {
                if (txt_mdp.Password == string.Empty.Trim() && txt_nom.Text == string.Empty.Trim())
                {
                    lbl_pass.Visibility = Visibility.Visible;
                    lbl_Nom.Visibility = Visibility.Visible;
                    txt_nom.Focus();
                }
                else
                {
                    if (txt_mdp.Password == string.Empty.Trim())
                    {
                        lbl_pass.Visibility = Visibility.Visible;
                        txt_mdp.Focus();
                        if (txt_nom.Text != string.Empty.Trim())
                        {
                            lbl_Nom.Visibility = Visibility.Hidden;
                        }
                        return;
                    }
                    else if (txt_nom.Text == string.Empty.Trim())
                    {
                        lbl_Nom.Visibility = Visibility.Visible;
                        lbl_Nom_Exist.Visibility = Visibility.Hidden;
                        txt_nom.Focus();
                        return;
                    }
                    else
                    {

                        if (res == MessageBoxResult.Yes)
                        {

                            Login lg = dbContext.Logins.Where(k => k.ID_Member == id).First();
                            dbContext.Logins.Remove(lg);
                            dbContext.SaveChanges();
                            PopupMSG("Successful.png", "La suppression a réussi !");
                            ChargerDGVComptes();
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

        private void comptes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtg_comptes.SelectedItem == null)
                {
                    return;
                }
                else
                {
                    var data = dtg_comptes.SelectedItem;
                    string id = (dtg_comptes.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                    txt_id.Text = id;
                    txt_id.IsEnabled = false;
                    string nome = (dtg_comptes.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                    txt_nom.Text = nome;
                    string password = (dtg_comptes.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                    txt_mdp.Password = password;
                    passwordTxtBox.Text = password;
                    string AdminOuNon = (dtg_comptes.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
                    if ((string)RB_Admin.Content == AdminOuNon)
                    {
                        RB_Admin.IsChecked = true;
                    }
                    else if ((string)RB_NoAdmin.Content == AdminOuNon)
                    {
                        RB_NoAdmin.IsChecked = true;
                    }
                }

            }
            catch (System.NullReferenceException ex)
            {
                PopupMSG("refuse.png", ex.Message);
            }
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordTxtBox.Text = txt_mdp.Password;
            txt_mdp.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
        }

        private void check_Pass_Unchecked(object sender, RoutedEventArgs e)
        {
            txt_mdp.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            txt_mdp.Visibility = Visibility.Visible;
        }

        private void btn_Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            ChargerDGVComptes();
            ChargeDGVDetail();
        }

        private void passwordTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_pass.Visibility = Visibility.Hidden;
        }

        //Control if the text of textbox has Letters or not
        public void ControlsTextBoxString(TextBox txt, Label lb)
        {
            if (txt.Text==" ")
            {
                PopupMSG("refuse.png", "Les espèces ne peuvent pas être ajoutés !");
                txt.Text = txt.Text.Remove(txt.Text.Length - 1);
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"\d"))
            {
                PopupMSG("refuse.png", "seulement les Lettres !");

                txt.Text = txt.Text.Remove(txt.Text.Length - 1);
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, "[a-zA-Z_]*$"))
            {
                lb.Visibility = Visibility.Hidden;
            }
            
        }

        private void txt_nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            //client c = new client();
           ControlsTextBoxString(this.txt_nom, lbl_Nom_Exist);
            //lbl_Nom.Visibility = Visibility.Hidden;
        }

        private void txt_mdp_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lbl_pass.Visibility = Visibility.Hidden;
        }

        private void txt_recherche_compte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_recherche_compte.Text == "Rechercher")
            {
                return;
            }
            else
            {
                dtg_comptes.ItemsSource = dbContext.Logins.Where(lg => lg.Nom.Contains(txt_recherche_compte.Text)).ToList();
                dtg_comptes.Columns[0].Header = "ID membre";
                dtg_comptes.Columns[1].Header = "Nom utilisateur";
                dtg_comptes.Columns[2].Header = "Mot de passe";
                dtg_comptes.Columns[3].Header = "Statut";
                if (dtg_comptes.Items.Count <= 0)
                {
                    txt_recherche_compte.Foreground = Brushes.Red;
                    txt_recherche_compte.FontWeight = FontWeights.Bold;
                }
                else
                {
                    txt_recherche_compte.Foreground = Brushes.Black;
                    txt_recherche_compte.FontWeight = FontWeights.Normal;
                }
            }
            
        }

        private void txt_recherche_compte_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_recherche_compte.Text = "";
        }

        private void txt_recherche_compte_LostFocus(object sender, RoutedEventArgs e)
        {       
                txt_recherche_compte.Text = "Rechercher";
        }

        private void txt_recherche_detail_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_recherche_detail.Text = "";
        }

        private void txt_recherche_detail_LostFocus(object sender, RoutedEventArgs e)
        {
            txt_recherche_detail.Text = "Rechercher";
        }

        private void txt_recherche_detail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_recherche_detail.Text == "Rechercher")
            {
                return;
            }
            else
            {
                dtg_details.ItemsSource = dbContext.Details.Where(d => d.Nom.Contains(txt_recherche_detail.Text)|| d.Date_Entree.Contains(txt_recherche_detail.Text)).ToList();
                dtg_details.Columns[0].Header = "ID Detail";
                dtg_details.Columns[1].Header = "Nom utilisateur";
                dtg_details.Columns[2].Header = "Date d'entrer";
                dtg_details.Columns[3].Header = "Heur d'entrer";
                if (dtg_details.Items.Count <= 0)
                {
                    txt_recherche_detail.Foreground = Brushes.Red;
                    txt_recherche_detail.FontWeight = FontWeights.Bold;

                }
                else
                {
                    txt_recherche_detail.Foreground = Brushes.Black;
                    txt_recherche_detail.FontWeight = FontWeights.Normal;
                }
                    
            }
            
        }

        private void Modiffier_Click(object sender, RoutedEventArgs e)
        {
            if (txt_mdp.Password == string.Empty.Trim())
            {
                lbl_pass.Visibility = Visibility.Visible;
                txt_mdp.Focus();
                if (txt_nom.Text != string.Empty.Trim())
                {
                    lbl_Nom.Visibility = Visibility.Hidden;
                }
                return;
            }
            else if (txt_nom.Text == string.Empty.Trim())
            {
                lbl_Nom.Visibility = Visibility.Visible;
                lbl_Nom_Exist.Visibility = Visibility.Hidden;
                txt_nom.Focus();
                return;
            }
            else
            {
                try
                {
                    MessageBoxResult res = MessageBox.Show("Voulez vous vraiment supprimer ce Mmebre", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        int id = int.Parse(txt_id.Text);
                        Login lg = dbContext.Logins.Where(l => l.ID_Member == id).First();
                        lg.Nom = txt_nom.Text;
                        lg.Mot_de_passe = txt_mdp.Password;
                        lg.Mot_de_passe = passwordTxtBox.Text;
                        if (RB_Admin.IsChecked == true)
                        {
                            lg.Admin_O_No = RB_Admin.Content.ToString();
                        }
                        else if (RB_NoAdmin.IsChecked == true)
                        {
                            lg.Admin_O_No = RB_NoAdmin.Content.ToString();
                        }
                        dbContext.SaveChanges();
                        PopupMSG("Successful.png", "Modifié avec succès");
                        ChargerDGVComptes();
                        ClearControls();
                    }

                }
                catch (Exception )
                {

                    PopupMSG("refuse.png", "Ce Membre n'existe pas !");
                }
                
            }
           
            
        }

    }
}

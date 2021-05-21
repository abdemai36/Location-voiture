using System;
using System.Collections.Generic;
using System.Globalization;
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
using WpfApp1.Properties;
 



namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            label_Check.Visibility=Visibility.Hidden;
            txt_user_name.Focus();
            
        }
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                
                if(txt_user_name.Text==string.Empty)
                {
                    txt_user_name.Focus();
                    label_Check.Content = "Entrer votre nom ";
                    label_Check.Visibility = Visibility.Visible;
                }else if (txt_password.Password == string.Empty)
                {
                    txt_password.Focus();
                    label_Check.Content = "Entrer votre mot de passe ";
                    label_Check.Visibility = Visibility.Visible;
                }
                else
                {
                    if (dbContext.Logins.Where(r => r.Nom == txt_user_name.Text && r.Mot_de_passe == txt_password.Password).Count() > 0)
                    {
                        App.Current.Properties["username"] = "Bonjour " + txt_user_name.Text;
                        App.Current.Properties["etat"] = dbContext.Logins.Where(z => z.Nom == txt_user_name.Text).Select(z => z.Admin_O_No).SingleOrDefault();
                        this.Hide();
                        Window1 w = new Window1();
                        w.Show();
                        Detail d = new Detail();
                        d.Date_Entree = DateTime.Now.ToShortDateString();
                        d.time_Entree = DateTime.Now.ToString("HH:mm");
                        d.Nom = txt_user_name.Text;
                        dbContext.Details.Add(d);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        label_Check.Content = "Nom ou mot de passe incorrect";
                        label_Check.Visibility = Visibility.Visible;
                       
                    }
                }
               
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur en niveau de base de donnée", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }

        private void txt_user_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_user_name.Text, @"[^a-z-A-Z]"))
            {
                label_Check.Visibility = Visibility.Visible;
                label_Check.Content="S'il vous plait saisir just des carracters.";
                txt_user_name.Text = txt_user_name.Text.Remove(txt_user_name.Text.Length - 1);
            }
            else if(System.Text.RegularExpressions.Regex.IsMatch(txt_user_name.Text, @"[a-z-A-Z]"))
            {
                label_Check.Visibility = Visibility.Hidden;
            }
        }

        
    }
}

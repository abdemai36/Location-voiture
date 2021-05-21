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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            username.Content = App.Current.Properties["username"];
           
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            
                logo.Visibility = Visibility.Hidden;
                parent.Content = new Dashboard();
                titre.Content = "Statistique generales :";
           
        }

        private void label_nme_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
            parent.Content = new contrat();
            titre.Content = "Gestion de contrat :";
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            
            logo.Visibility = Visibility.Hidden;
            parent.Content = new Reservation();
            titre.Content = "Gestion de reservation :";
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            if (App.Current.Properties["etat"].ToString() == "Admin")
            {
                logo.Visibility = Visibility.Hidden;
                parent.Content = new Comptes();
                titre.Content = "Gestion de comptes";
            }
            else
            {
                eror.IsOpen = true;
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
            parent.Content = new client();
            titre.Content = "Gestion de clients";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow w = new MainWindow();
            w.Show();


        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
            parent.Content = new Voitures();
        }
    }
}

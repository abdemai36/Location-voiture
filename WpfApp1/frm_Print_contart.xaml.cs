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
using WpfApp1.Data;
using WpfApp1.Data.DataSet_ContTableAdapters;
//using WpfApp1.Data;

using WpfApp1.Rapportes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour frm_Print_contart.xaml
    /// </summary>
    public partial class frm_Print_contart : Window
    {
        public frm_Print_contart()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_search.Text = "Rechercher";
        }

     

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (txt_search.Text == "Rechercher" || txt_search.Text==string.Empty)
            //{
            //    return;
            //}
            //else {
            //    DataSet_Cont ds = new DataSet_Cont();
            //    ListContratTableAdapter da = new ListContratTableAdapter();
            //    da.Fill(ds.ListContrat, int.Parse(txt_search.Text));
            //    CrystalReport_contra cryC = new CrystalReport_contra();
            //    cryC.SetDataSource(ds);
            //    crystalviewerContrat.ViewerCore.ReportSource = cryC;
            //}
            
        }


        private void txt_search_LostFocus_1(object sender, RoutedEventArgs e)
        {
            //txt_search.Text = "Rechercher";
        }

        private void txt_search_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txt_search.Text = string.Empty.Trim();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            DataSet_Cont ds = new DataSet_Cont();
            ListContratTableAdapter da = new ListContratTableAdapter();
            da.Fill(ds.ListContrat, int.Parse(txt_search.Text));
            CrystalReport_contra cryC = new CrystalReport_contra();
            cryC.SetDataSource(ds);
            crystalviewerContrat.ViewerCore.ReportSource = cryC;
        }
    }
}

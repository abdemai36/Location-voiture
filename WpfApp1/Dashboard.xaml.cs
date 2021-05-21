using LiveCharts;
using LiveCharts.Wpf;
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
using Separator = LiveCharts.Wpf.Separator;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
 
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();
        public Dashboard()
        {
            InitializeComponent();
            louer.Content = dbContext.Voitures.Where(r => r.Etat == "non").Count();
            dispo.Content = dbContext.Voitures.Where(r => r.Etat == "oui").Count();
            reserv.Content = dbContext.Resevations.Count();
            var data = dbContext.Contrats.Select(p => new { p.Date_D_Contrat, p.Client });
            //reservation.ItemsSource = dbContext.Resevations.Select(s => new { s.ID_Reservation, s.Client.Nom, s.Client.Prenom, s.Voiture.Marque, s.Avance });

            //ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<int>(), LabelPoint = point => point.Y.ToString() };
            //Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };
            //foreach(var x in data)
            //{
            //    col.Values.Add(x.Client);
            //    ax.Labels.Add(x.Date_D_Contrat.ToString()); 
            //}

            //CartesianChart.Series.Add(col);
            //CartesianChart.AxisX.Add(ax);
            //CartesianChart.AxisY.Add(new Axis {
            //    LabelFormatter = value Separator()


            //}) ;
        }
    }
}

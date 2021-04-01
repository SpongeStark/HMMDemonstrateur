using System;
using LiveCharts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LiveCharts.Wpf;

namespace Demonstrateur
{
    /// <summary>
    /// Logique d'interaction pour Graphique.xaml
    /// </summary>
    public partial class Graphique : UserControl
    {
        public Graphique()
        {
            InitializeComponent();

            Series = new SeriesCollection();

            DataContext = this;
        }

        public void AddLineSerie(string title, double[] values)
        {
            Series.Add(new LineSeries
            {
                Title = title,
                Values = new ChartValues<double>(values),
                LineSmoothness = 0
            });
        }

        public SeriesCollection Series { get; set; }

        private void ListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(ListBox, (DependencyObject)e.OriginalSource) as ListBoxItem;
            if (item == null) return;

            var series = (LineSeries)item.Content;
            series.Visibility = series.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;
        }
    }
}

using CsvHelper;
using LiveCharts;
using LiveCharts.Wpf;
using RewardCalculator;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace LiveRewards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();
        public Configuration Configuration       { get; set; } 

        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(Constants.OutputDirectory))
                Directory.CreateDirectory(Constants.OutputDirectory);

            Configuration = Configuration.FromIniData(new IniFileParser.IniFileParser()
                .ReadFile(Constants.IniFile));

            DataContext = this;
        }

        private void CalculateSeries()
        {
            var collections = Calculate(Configuration);
            var chartValues = new ChartValues<int>();

            if(collections.Item2.Count > 2)
                collections.Item2
                    .ForEach(value => chartValues.Add(value.Bfp));
            else 
                collections.Item1
                    //.Where((value, index) => index % 5 == 0)
                    .ToList()
                    .ForEach(value => chartValues.Add(value.Bfp));

            SeriesCollection.Add(new LineSeries {
                Title = "Test",
                Values = chartValues
            });
        }

        private void CalculateSeries(object sender, RoutedEventArgs e) => CalculateSeries();

        private (ValueCollection, ValueCollection) Calculate(Configuration configuration) => RewardCalculator.RewardCalculator.CreateValueCollection(configuration);

        private void WriteToFile((ValueCollection, ValueCollection) collections)
        {
            if (!Directory.Exists(Constants.OutputDirectory))
                Directory.CreateDirectory(Constants.OutputDirectory);

            using var writer = new StreamWriter(Path.Combine(Constants.OutputDirectory, Constants.CsvAllPath));
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(collections.Item1);

            using var dailyWriter = new StreamWriter(Path.Combine(Constants.OutputDirectory, Constants.CsvDailyPath));
            using var dailycsvWriter = new CsvWriter(dailyWriter, CultureInfo.InvariantCulture);
            dailycsvWriter.WriteRecords(collections.Item2);
        }
    }
}
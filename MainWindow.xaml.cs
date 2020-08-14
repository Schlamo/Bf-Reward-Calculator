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
        public static int Minutes { get; } = 1440;
        public static string CsvAllPath { get; } = "all.csv";
        public static string CsvDailyPath { get; } = "daily.csv";
        public static string OutputDirectory { get; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CSV");
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();
        public Configuration Configuration { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);

            Configuration = Configuration.FromIniData(new IniFileParser.IniFileParser()
                .ReadFile(Constants.IniPath));

            CalculateSeries();
            DataContext = this;
        }

        private void CalculateSeries()
        {
            (ValueCollection, ValueCollection) collections = Calculate(Configuration);

            var chartValues = new ChartValues<int>();
            collections.Item1.ToList().ForEach(value => chartValues.Add(value.Bfp));

            SeriesCollection.Add(new LineSeries
            {
                Title = "Test",
                Values = chartValues
            });
        }

        private void CalculateSeries(object sender, RoutedEventArgs e)
        {
            CalculateSeries();
        }

        private (ValueCollection, ValueCollection) Calculate(Configuration configuration)
        {
            return RewardCalculator.RewardCalculator.CreateValueCollection(configuration);
        }

        private void WriteToFile((ValueCollection, ValueCollection) collections)
        {
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);
            using var writer = new StreamWriter(Path.Combine(OutputDirectory, CsvAllPath));
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(collections.Item1);

            using var dailyWriter = new StreamWriter(Path.Combine(OutputDirectory, CsvDailyPath));
            using var dailycsvWriter = new CsvWriter(dailyWriter, CultureInfo.InvariantCulture);
            dailycsvWriter.WriteRecords(collections.Item2);
        }
    }
}
//using CsvHelper;
//using LiveCharts;
//using LiveCharts.Wpf;
//using RewardCalculator;
//using System;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Windows;

//namespace LiveRewards
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        private static int MaxValues { get; } = 120;
//        private static int Minutes { get; } = 1440;
//        private static string CsvAllPath { get; } = "all.csv";
//        private static string CsvDailyPath { get; } = "daily.csv";
//        private static string OutputDirectory { get; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CSV");
//        private SeriesCollection SeriesCollection { get; set; }
//        private Configuration Configuration { get; set; }

//        public MainWindow()
//        {
//            InitializeComponent();

//            Configuration = Configuration.FromIniData(new IniFileParser.IniFileParser()
//                .ReadFile(Constants.IniPath));

//            UpdateCalculations();

//            var collections = RewardCalculator.RewardCalculator.CreateValueCollection(Configuration);

//            var chartValues = new ChartValues<int>();

//            collections.Item1.ToList().ForEach(value => chartValues.Add(value.Bfp));
//            var series = new LineSeries
//            {
//                Title = "Test",
//                Values = chartValues
//            };

//            SeriesCollection = new SeriesCollection {
//                series
//            };
//            DataContext = this;
//        }

//        private void UpdateCalculations()
//        {
//        }
//    }
//}
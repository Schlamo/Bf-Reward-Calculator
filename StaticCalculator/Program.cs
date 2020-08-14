using System;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Reflection;

namespace RewardCalculator
{
    public class Program
    {
        public static readonly int _minutes = 1440;
        public static readonly string _csvAllPath = "all.csv";
        public static readonly string _csvDailyPath = "daily.csv";
        public static readonly string _outputDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CSV");
        public static void Main()
        {
            //if (!Directory.Exists(_outputDirectory))
            //    Directory.CreateDirectory(_outputDirectory);

            //var Configuration.FromIniData(new IniFileParser.IniFileParser()
            //    .ReadFile(Constants.IniPath));

            //var collection = RewardCalculator.CreateValueCollection(Configuration.GeneralTotalDays, Configuration.GeneralMinutesPerDay);

            //using var writer = new StreamWriter(Path.Combine(_outputDirectory, _csvAllPath));
            //using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            //csvWriter.WriteRecords(collection.Item1);

            //using var dailyWriter = new StreamWriter(Path.Combine(_outputDirectory, _csvDailyPath));
            //using var dailycsvWriter = new CsvWriter(dailyWriter, CultureInfo.InvariantCulture);
            //dailycsvWriter.WriteRecords(collection.Item2);
        }

    }

}

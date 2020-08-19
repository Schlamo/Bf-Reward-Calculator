namespace RewardCalculator
{
    public class MeasuredValue
    {
        public int Bfp          { get; set; }
        public int DailyPool    { get; set; }
        public int QuestsLeft   { get; set; }
        public int ReservePool  { get; set; }
        public int Minute       { get; set; }
        public int Day          { get; set; }
        
        public MeasuredValue() 
        {
        }

        public static MeasuredValue Empty(Configuration configuration) => new MeasuredValue {
            Minute      = 0,
            Bfp         = 0,
            DailyPool   = configuration.DailyMaxGain,
            ReservePool = configuration.ReservePoolSize,
            QuestsLeft  = 0,
            Day         = 1
        };
    }
}

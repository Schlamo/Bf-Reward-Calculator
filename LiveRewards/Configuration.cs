using IniFileParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RewardCalculator
{
    public class Configuration
    {
        #region Daily Pool
        public int DailyPoolSize         { get; set; } = 0;
        public int DailyMinGain          { get; set; } = 0;
        public int DailyMaxGain          { get; set; } = 0;
        public int DailyDuration         { get; set; } = 0;
        #endregion

        #region Reserve Pool
        public int ReservePoolSize       { get; set; } = 0;
        public int ReserveDrainPerMinute { get; set; } = 0;
        public int ReserveMinutesPerGain { get; set; } = 0;
        #endregion

        #region Quests 
        public int QuestsPoolSize        { get; set; } = 0;
        public int QuestsDailyGain       { get; set; } = 0;
        public int QuestsBfpPerQuest     { get; set; } = 0;
        public int QuestsMinutesPerQuest { get; set; } = 0;
        #endregion

        #region General 
        public int GeneralMinutesPerDay  { get; set; } = 0;
        public int GeneralTotalDays      { get; set; } = 0;
        public int GeneralTotalMinutes   { get; set; } = 0;
        public int GeneralAllXDays       { get; set; } = 0;
        #endregion

        public static Configuration FromIniData(IniData iniData)
            => new Configuration {
                DailyPoolSize = int.Parse(iniData["DailyPool"]["PoolSize"]),
                DailyMinGain = int.Parse(iniData["DailyPool"]["MinGain"]),
                DailyMaxGain = int.Parse(iniData["DailyPool"]["MaxGain"]),
                DailyDuration = int.Parse(iniData["DailyPool"]["Duration"]),

                ReservePoolSize = int.Parse(iniData["ReservePool"]["PoolSize"]),
                ReserveDrainPerMinute = int.Parse(iniData["ReservePool"]["DrainPerMinute"]),
                ReserveMinutesPerGain = int.Parse(iniData["ReservePool"]["MinutesPerGain"]),


                QuestsPoolSize = int.Parse(iniData["Quests"]["PoolSize"]),
                QuestsDailyGain = int.Parse(iniData["Quests"]["DailyGain"]),
                QuestsBfpPerQuest = int.Parse(iniData["Quests"]["BfpPerQuest"]),
                QuestsMinutesPerQuest = int.Parse(iniData["Quests"]["MinutesPerQuest"]),


                GeneralMinutesPerDay = int.Parse(iniData["General"]["MinutesPerDay"]),
                GeneralTotalDays = int.Parse(iniData["General"]["TotalDays"]),
                GeneralTotalMinutes = int.Parse(iniData["General"]["TotalMinutes"]),
                GeneralAllXDays = int.Parse(iniData["General"]["AllXDays"])
            };
    }
}

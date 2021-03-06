﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RewardCalculator
{
    public static class RewardCalculator
    {
        public static (ValueCollection, ValueCollection) CreateValueCollection(Configuration configuration)
        {
            var collection = new ValueCollection {
                MeasuredValue.Empty(configuration)
            };

            var dailyCollection = new ValueCollection {
                MeasuredValue.Empty(configuration)
            };
            
            var reserveTimer = 1;
            var reservePool  = configuration.ReservePoolSize;
            var questTimer   = 1;
            var dailyPool    = 0;
            var questPool    = 0;
            var dayCount     = configuration.GeneralAllXDays;

            for (int day = 1; day <= configuration.GeneralTotalDays; day++) {
                dailyPool += dailyPool == 0
                    ? configuration.DailyMaxGain
                    : configuration.DailyMinGain;

                if (questPool < configuration.QuestsPoolSize) 
                    questPool = Math.Min(questPool + configuration.QuestsDailyGain, configuration.QuestsPoolSize);

                if (dayCount == configuration.GeneralAllXDays) {
                    dayCount = 1;

                    for (int minute = 1; minute < Math.Min(1440, configuration.GeneralTotalMinutes + 1); minute++)
                    {
                        var oldValue = collection.Last();

                        var newValue = new MeasuredValue
                        {
                            ReservePool = oldValue.ReservePool,
                            QuestsLeft  = oldValue.QuestsLeft,
                            DailyPool   = dailyPool,
                            Minute      = minute + ((day - 1) * 1440),
                            Bfp         = oldValue.Bfp,
                            Day         = day
                        };

                        if (minute < configuration.GeneralMinutesPerDay + 1)
                        {
                            if (dailyPool > 0)
                            {
                                var gainFromDaily = dailyPool / (configuration.DailyDuration - minute + 1);
                                newValue.Bfp      += gainFromDaily;
                                dailyPool         -= gainFromDaily;
                            }
                            else
                            {
                                if (reservePool >= configuration.ReserveDrainPerMinute)
                                {
                                    newValue.Bfp += configuration.ReserveDrainPerMinute;
                                    reservePool  -= configuration.ReserveDrainPerMinute;
                                }
                                else if (reservePool > 0)
                                {
                                    newValue.Bfp += reservePool;
                                    reservePool  = 0;
                                }
                            }
                        }

                        //Reserve Gain
                        reserveTimer++;
                        if (reserveTimer == configuration.ReserveMinutesPerGain)
                        {
                            if (reservePool < configuration.ReservePoolSize)
                                reservePool++;

                            reserveTimer = 0;
                        }

                        //Quests 
                        if (questPool > 0)
                        {
                            questTimer++;

                            if (questTimer == configuration.QuestsMinutesPerQuest)
                            {
                                questPool -= 1;
                                newValue.Bfp += configuration.QuestsBfpPerQuest;
                                questTimer = 0;
                            }
                        }

                        newValue.DailyPool = dailyPool;
                        newValue.ReservePool = reservePool;
                        newValue.QuestsLeft = questPool;

                        collection.Add(newValue);

                    }
                }
                else dayCount++;

                dailyCollection.Add(collection.Last());
            }
            return (collection, dailyCollection);
        }
    }
}

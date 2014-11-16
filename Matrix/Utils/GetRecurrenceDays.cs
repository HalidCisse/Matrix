using System;
using System.Collections.Generic;

namespace Matrix.Utils
{
    public class Scheduler 
    {

        public List<DayOfWeek> GetRecurrenceDays(int Bin)
        {
            var DayList = new List<DayOfWeek>();


            if (Bin == 2)
            {
                DayList.Add(DayOfWeek.Monday);
            }
            else if (Bin == 6)
            {
                DayList.Add (DayOfWeek.Monday);
                DayList.Add (DayOfWeek.Tuesday);
            }
            else if(Bin == 14)
            {
                DayList.Add (DayOfWeek.Monday);
                DayList.Add (DayOfWeek.Tuesday);
                DayList.Add (DayOfWeek.Wednesday);
            }
            else if(Bin == 30)
            {
                DayList.Add (DayOfWeek.Monday);
                DayList.Add (DayOfWeek.Tuesday);
                DayList.Add (DayOfWeek.Wednesday);
                DayList.Add (DayOfWeek.Thursday);
            }
            else if(Bin == 30)
            {
                DayList.Add (DayOfWeek.Monday);
                DayList.Add (DayOfWeek.Tuesday);
                DayList.Add (DayOfWeek.Wednesday);
                DayList.Add (DayOfWeek.Thursday);
                DayList.Add (DayOfWeek.Friday);
            }


            //var WeekDays = new List<DayOfWeek>();
            //WeekDays.Add (DayOfWeek.Monday);
            //WeekDays.Add (DayOfWeek.Tuesday);
            //WeekDays.Add (DayOfWeek.Wednesday);
            //WeekDays.Add (DayOfWeek.Thursday);
            //WeekDays.Add (DayOfWeek.Friday);
            //WeekDays.Add (DayOfWeek.Saturday);
            //WeekDays.Add (DayOfWeek.Sunday);

            var WeekDays = new List<int>{2,4,8,16,32,64,128};

            foreach(var D1 in WeekDays)
            {
                foreach(var D2 in WeekDays)
                {
                    foreach(var D3 in WeekDays)
                    {
                        foreach(var D4 in WeekDays)
                        {
                            foreach(var D5 in WeekDays)
                            {
                                foreach(var D6 in WeekDays)
                                {
                                    foreach(var D7 in WeekDays)
                                    {
                                        //
                                    }
                                }
                            }
                        }
                    }
                }
            }








            return DayList;
        }







    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoServices.Utilites
{
    public class WorkHoursCalculation
    {
        public static int GetWorkHoursSum(DateTime startDate, DateTime endDate, TimeSpan workStartTime, TimeSpan workEndTime)
        {
            List<double> result = new List<double>();

            if (startDate >= endDate)
            {
                return 0;
            }

            DateTime currentDate = startDate.Date;

            while (currentDate <= endDate.Date)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    TimeSpan realStartTime = currentDate == startDate.Date ? (startDate.TimeOfDay < workStartTime ? workStartTime : startDate.TimeOfDay) : workStartTime;
                    TimeSpan realEndTime = currentDate == endDate.Date ? (endDate.TimeOfDay < workEndTime ? endDate.TimeOfDay : workEndTime) : workEndTime;

                    if (realStartTime >= workEndTime || realEndTime <= workStartTime)
                    {
                        result.Add(0);
                    }
                    else
                    {
                        result.Add((realEndTime - realStartTime).TotalHours);
                    }
                }
                currentDate = currentDate.AddDays(1);
            }

            return (int)result.Sum();
        }
    }
}

using AirlinesReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Helper
{
    public class MoneyHelper
    {
        private static Model1 db = new Model1();
        public static string showVND(int money)
        {
            var currency = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(currency, "{0:c0}", money);
        }

        public static int TakeMoney(int id)
        {
            FlightSchedule flightSchedule = db.FlightSchedules.Where(s => s.id == id).FirstOrDefault();

            if (flightSchedule == null)
            {
                return 0;
            }
            else
            {
                return flightSchedule.cost;
            }

        }
    }
}
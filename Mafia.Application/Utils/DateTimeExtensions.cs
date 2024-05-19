using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Application.Utils
{
    /// <summary>
    /// Класс содержит логику работу с функциями связанными со временем
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Возврат возраста пациента в днях при указании даты рождения
        /// </summary>
        /// <param name="dob"> Дата рождения </param>
        /// <returns></returns>
        public static int ToAgeString(this DateTime dob)
        {
            DateTime today = DateTime.Today;

            int months = today.Month - dob.Month;
            int years = today.Year - dob.Year;

            if (today.Day < dob.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (today - dob.AddMonths((years * 12) + months)).Days;

            return days;
        }
    }
}

using System;

namespace GuestBook.Domain.Helpers
{
    public class TimeConverter
    {
        /// <summary>
        /// Базовое время окуда начинается отсчет.
        /// </summary>
        private static DateTime _baseTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// Перевод даты в Unix формат из DateTime.
        /// </summary>
        /// <param name="time">Дата в DateTime.</param>
        /// <returns>Дата в Unix-формате.</returns>
        public static long? ToUnixTime(DateTime? time)
        {
            if (time == null)
            {
                return null;
            }

            var interval = time.Value.Subtract(_baseTime);

            return (long)interval.TotalSeconds;
        }

        /// <summary>
        /// Перевод даты из Unix формата в DateTime.
        /// </summary>
        /// <param name="date">Дата в Unix-формате.</param>
        /// <returns>Дата в DateTime.</returns>
        public static DateTime? FromUnixTime(long? date)
        {
            return date == null ? (DateTime?)null : _baseTime
                .AddSeconds(date.Value);
        }
    }
}

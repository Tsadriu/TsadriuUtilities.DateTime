using System.Globalization;

namespace DateTimeHelper
{
    /// <summary>
    /// Class that helps on dealing with <b><see cref="DateTime"/></b>.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// All of the date styles possible from the <b><see cref="DateTimeStyles"/></b> enum.
        /// </summary>
        private static readonly Array DateStyles = Enum.GetValues(typeof(DateTimeStyles));

        /// <summary>
        /// This method attempts to convert a date from a <see cref="string"/> to a type of <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The date represented as a <see cref="string"/>.<br/>
        /// Examples: "23/03/2012", "11-02-2001 23:12:04", "06.12.2019".</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> of the date.</param>
        /// <param name="dateTimeStyle">The date time style of the date.</param>
        /// <param name="formats">The date format.<br/>
        /// Examples: "dd/MM/yyyy", "MM/dd/yyyy".</param>
        /// <returns>If the parsing was successful, returns <see cref="DateTime"/>.<br/>
        /// If the parsing fails, it throws an <b><see cref="ArgumentException"/></b>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <b><paramref name="date"/></b> is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the <b><paramref name="date"/></b> could not be converted into a <b><see cref="DateTime"/></b> object.</exception>
        public static DateTime ToDateTime(this string date, CultureInfo cultureInfo, DateTimeStyles? dateTimeStyle, params string[] formats)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                throw new ArgumentNullException(nameof(date), "The date cannot be empty");
            }

            Array currentDateStyles = dateTimeStyle == null ? DateStyles : new[] { dateTimeStyle };

            foreach (string format in formats)
            {
                for (int i = 0; i < currentDateStyles.Length; i++)
                {
                    bool successConversion = DateTime.TryParseExact(date, format, cultureInfo, (DateTimeStyles)i, out DateTime result);

                    if (!successConversion)
                    {
                        successConversion = DateTime.TryParse(date, cultureInfo, (DateTimeStyles)i, out result);
                    }

                    if (successConversion)
                    {
                        return result;
                    }
                }
            }

            throw new ArgumentException($"Could not parse the date '{date}'. Please check if the format of the date is provided in the formats parameter");
        }

        /// <summary>
        /// This method attempts to convert a date from a <see cref="string"/> to a type of <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The date represented as a <see cref="string"/>.<br/>
        /// Examples: "23/03/2012", "11-02-2001 23:12:04", "06.12.2019".</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> of the date.</param>
        /// <param name="formats">The date format. Examples: "dd/MM/yyyy", "MM/dd/yyyy".</param>
        /// <returns>If the parsing was successful, returns <see cref="DateTime"/>.<br/>
        /// If the parsing fails, returns <see cref="DateTime.MinValue"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <b><paramref name="date"/></b> is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the <b><paramref name="date"/></b> could not be converted into a <b><see cref="DateTime"/></b> object.</exception>
        public static DateTime ToDateTime(this string date, CultureInfo cultureInfo, params string[] formats)
        {
            return date.ToDateTime(cultureInfo, null, formats);
        }

        /// <summary>
        /// This method attempts to convert a date from a <see cref="string"/> to a nullable <see cref="DateTime"/>.<br/>
        /// </summary>
        /// <param name="date">The date represented as a <see cref="string"/>.<br/>
        /// Examples: "23/03/2012", "11-02-2001 23:12:04", "06.12.2019".</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> of the date.</param>
        /// <param name="dateTimeStyle">The date time style of the date.</param>
        /// <param name="formats">The date format.<br/>
        /// Examples: "dd/MM/yyyy", "MM/dd/yyyy".</param>
        /// <returns>If the parsing was successful, returns <see cref="DateTime"/>.<br/>
        /// If the parsing fails (or the <b><paramref name="date"/></b> parameter is null or empty), it returns null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <b><paramref name="date"/></b> is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the <b><paramref name="date"/></b> could not be converted into a <b><see cref="DateTime"/></b> object.</exception>
        public static DateTime? ToNullableDateTime(this string date, CultureInfo cultureInfo, DateTimeStyles? dateTimeStyle, params string[] formats)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return null;
            }

            Array currentDateStyles = dateTimeStyle == null ? DateStyles : new[] { dateTimeStyle };

            foreach (string format in formats)
            {
                for (int i = 0; i < currentDateStyles.Length; i++)
                {
                    bool successConversion = DateTime.TryParseExact(date, format, cultureInfo, (DateTimeStyles)i, out DateTime result);

                    if (!successConversion)
                    {
                        successConversion = DateTime.TryParse(date, cultureInfo, (DateTimeStyles)i, out result);
                    }

                    if (successConversion)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// This method attempts to convert a date from a <see cref="string"/> to a nullable <see cref="DateTime"/>.<br/>
        /// </summary>
        /// <param name="date">The date represented as a <see cref="string"/>.<br/>
        /// Examples: "23/03/2012", "11-02-2001 23:12:04", "06.12.2019".</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> of the date.</param>
        /// <param name="formats">The date format.<br/>
        /// Examples: "dd/MM/yyyy", "MM/dd/yyyy".</param>
        /// <returns>If the parsing was successful, returns <see cref="DateTime"/>.<br/>
        /// If the parsing fails (or the <b><paramref name="date"/></b> parameter is null or empty), it returns null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <b><paramref name="date"/></b> is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the <b><paramref name="date"/></b> could not be converted into a <b><see cref="DateTime"/></b> object.</exception>
        public static DateTime? ToNullableDateTime(this string date, CultureInfo cultureInfo, params string[] formats)
        {
            return date.ToNullableDateTime(cultureInfo, null, formats);
        }

        /// <summary>
        /// Parses the date to return with the last day of the month.
        /// </summary>
        /// <param name="date">Date to be checked on.</param>
        /// <returns>Date with the last day of the month.</returns>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Sets the <paramref name="day"/> in the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">Current date.</param>
        /// <param name="day">Day to set. In case <paramref name="day"/> is higher than the month's max days, it will be clamped to the month's max day.</param>
        /// <returns>Date with the specified <paramref name="day"/>.</returns>
        public static DateTime SetDay(this DateTime date, int day)
        {
            int clampedDay = day <= DateTime.MinValue.Day ? DateTime.MinValue.Day : day >= DateTime.DaysInMonth(date.Year, date.Month) ? DateTime.DaysInMonth(date.Year, date.Month) : day;
            return new DateTime(date.Year, date.Month, clampedDay);
        }

        /// <summary>
        /// Sets the <paramref name="month"/> in the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">Current date.</param>
        /// <param name="month">Month to set. In case <paramref name="month"/> is higher than the year's max months, it will be clamped to the year's max month.</param>
        /// <returns>Date with the specified <paramref name="month"/>.</returns>
        public static DateTime SetMonth(this DateTime date, int month)
        {
            int clampedMonth = month <= DateTime.MinValue.Month ? DateTime.MinValue.Month : month >= DateTime.MaxValue.Month ? DateTime.MaxValue.Month : month;
            return new DateTime(date.Year, clampedMonth, 1).SetDay(date.Day);
        }

        /// <summary>
        /// Sets the <paramref name="year"/> in the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">Current date.</param>
        /// <param name="year">Year to set.</param>
        /// <returns>Date with the specified <paramref name="year"/>.</returns>
        public static DateTime SetYear(this DateTime date, int year)
        {
            int clampedYear = year <= DateTime.MinValue.Year ? DateTime.MinValue.Year : year >= DateTime.MaxValue.Year ? DateTime.MaxValue.Year : year;
            return new DateTime(clampedYear, 1, 1).SetMonth(date.Month).SetDay(date.Day);
        }

        /// <summary>
        /// Removes a specified number of days from <paramref name="date"/>.
        /// </summary>
        /// <param name="date">Current date.</param>
        /// <param name="days">Number of days to remove.</param>
        /// <returns>Date with the specified number of days removed.</returns>
        public static DateTime RemoveDays(this DateTime date, int days)
        {
            return date.AddDays(-Math.Abs(days));
        }


        /// <summary>
        /// Removes a specified number of months from <paramref name="date"/>.
        /// </summary>
        /// <param name="date">Current date.</param>
        /// <param name="months">Number of months to remove.</param>
        /// <returns>Date with the specified number of months removed.</returns>
        public static DateTime RemoveMonths(this DateTime date, int months)
        {
            return date.AddMonths(-Math.Abs(months));
        }

        /// <summary>
        /// Removes a specified number of years from <paramref name="date"/>.
        /// </summary>
        /// <param name="date">Current date.</param>
        /// <param name="years">Number of years to remove.</param>
        /// <returns>Date with the specified number of years removed.</returns>
        public static DateTime RemoveYears(this DateTime date, int years)
        {
            return date.AddYears(-Math.Abs(years));
        }
    }
}

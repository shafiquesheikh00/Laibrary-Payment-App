using System;

namespace Laibrary_Payment_App.Services
{
    public static class MembershipDateCalculator
    {
        /// <summary>
        /// Calculates the inclusive End Date for a membership given a start date, duration value, and duration unit.
        /// Handles Day, Week, Month, Quarter, Half-Yearly, and Year units correctly.
        /// </summary>
        public static DateTime CalculateEndDate(DateTime startDate, int durationValue, string durationUnit)
        {
            if (durationValue <= 0)
            {
                durationValue = 1;
            }

            var start = startDate.Date;
            var normalizedUnit = (durationUnit ?? string.Empty).Trim().ToLowerInvariant();

            return normalizedUnit switch
            {
                "day" or "days" => start.AddDays(durationValue - 1),
                "week" or "weeks" => start.AddDays(durationValue * 7 - 1),
                "month" or "months" => start.AddMonths(durationValue).AddDays(-1),
                "quarter" or "quarters" => start.AddMonths(durationValue * 3).AddDays(-1),
                "half-yearly" or "half yearly" or "half-year" or "halfyear" => start.AddMonths(durationValue * 6).AddDays(-1),
                "year" or "years" => start.AddYears(durationValue).AddDays(-1),
                _ => start.AddDays(durationValue - 1)
            };
        }
    }
}

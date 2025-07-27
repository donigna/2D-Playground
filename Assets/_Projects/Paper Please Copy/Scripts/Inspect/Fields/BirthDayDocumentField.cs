using System;

namespace com.Kuwiku
{
    public class BirthDayDocumentField : DocumentField
    {
        public int day;
        public int month;
        public int year;

        protected override void _LocalStart()
        {
            DisplayText($"{day} - {month} - {year}");
        }

        public int GetAge()
        {
            DateTime birthday = new DateTime(year, month, day);
            DateTime today = DateTime.Today;
            int years = today.Year - birthday.Year;
            int months = today.Month - today.Month;
            int days = today.Day - today.Day;

            if (months < 0)
            {
                years--;
                return years;
            }

            if (days < 0)
            {
                years--;
                return years;
            }

            return years;
        }
    }
}
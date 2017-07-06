﻿using System;
using System.Text;

namespace SupportBank.DataTypes
{
    public class Money
    {
        public const string NumberFormatMessage = "The string passed to create a money is not a valid number: ";

        private int amount;

        public Money(int value)
        {
            amount = value;
        }

        public Money(string value)
        {
            char[] separators = {'.'};
            string[] splitValue = value.Split(separators);

            if (splitValue.Length == 2)
            {
                amount = ParsePoundsPence(splitValue[0], splitValue[1]);
            }
            else if (splitValue.Length == 1)
            {
                amount = ParsePounds(splitValue[0]);
            }
            else
            {
                throw new ArgumentException(NumberFormatMessage + value);
            }
        }

        private int ParsePoundsPence(string poundsString, string penceString)
        {
            int pounds = 0;
            int pence = 0;

            if (poundsString.Length > 0)
                pounds = ParsePounds(poundsString);

            pence = ParsePence(penceString);

            return (pounds + pence);
        }

        private int ParsePounds(string poundsString)
        {
            int pounds = Int32.Parse(poundsString);
            return (pounds * 100);
        }

        private int ParsePence(string penceString)
        {
            int pence = Int32.Parse(penceString);
            //If there is only one pence number, it's actually a multiple of 10
            if (penceString.Length == 1)
                pence *= 10;
            return pence;
        }

        public int GetAmount()
        {
            return amount;
        }

        public override string ToString()
        {
            int absAmount = Math.Abs(amount);

            int pounds = absAmount / 100;
            int pence = absAmount % 100;

            StringBuilder stringBuilder = new StringBuilder();

            if (amount < 0)
            {
                stringBuilder.Append("-");
            }

            stringBuilder.Append("£" + pounds.ToString("0") + "." + pence.ToString("00"));

            return stringBuilder.ToString();
        }

        public static Money operator +(Money m1, Money m2)
        {
            return new Money(m1.GetAmount() + m2.GetAmount());
        }

        public static Money operator -(Money m1, Money m2)
        {
            return new Money(m1.GetAmount() - m2.GetAmount());
        }
    }
}

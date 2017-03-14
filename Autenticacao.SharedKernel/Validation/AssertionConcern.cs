using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Autenticacao.SharedKernel.Events;
using Autenticacao.SharedKernel.Helpers;

namespace Autenticacao.SharedKernel.Validation
{
    public static class AssertionConcern
    {
        public static bool IsSatisfiedBy(params DomainNotification[] validations)
        {
            var notificationsNotNull = validations.Where(validation => validation != null);
            NotifyAll(notificationsNotNull);

            return notificationsNotNull.Count().Equals(0);
        }

        private static void NotifyAll(IEnumerable<DomainNotification> notifications)
        {
            notifications.ToList().ForEach(validation =>
            {
                DomainEvent.Raise<DomainNotification>(validation);
            });
        }

        public static DomainNotification AssertLength(string stringValue, int minimum, int maximum, string message,
            string key = null)
        {
            int length = stringValue.Trim().Length;

            return (length < minimum || length > maximum)
                ? new DomainNotification(key ?? "AssertArgumentLength", message)
                : null;
        }

        public static DomainNotification AssertMatches(string pattern, string stringValue, string message, string key = null)
        {
            Regex regex = new Regex(pattern);

            return (!regex.IsMatch(stringValue))
                ? new DomainNotification(key ?? "AssertArgumentLength", message)
                : null;
        }

        public static DomainNotification AssertNotEmpty(string stringValue, string message, string key = null)
        {
            return (string.IsNullOrEmpty(stringValue))
                ? new DomainNotification(key ?? "AssertArgumentNotEmpty", message)
                : null;
        }

        public static DomainNotification AssertNotNullOrWhiteSpace(string stringValue, string message, string key = null)
        {
            return (string.IsNullOrWhiteSpace(stringValue))
                ? new DomainNotification(key ?? "AssertArgumentNotEmpty", message)
                : null;
        }

        public static DomainNotification AssertNotNull(object object1, string message, string key = null)
        {

            return (object1 == null)
                ? new DomainNotification(key ?? "AssertArgumentNotNull", message)
                : null;
        }

        public static DomainNotification AssertNull(object object1, string message, string key = null)
        {

            return (object1 != null)
                ? new DomainNotification(key ?? "AssertArgumentNull", message)
                : null;
        }
        public static DomainNotification AssertNullOrWhiteSpace(string stringValue, string message, string key = null)
        {
            return (!string.IsNullOrWhiteSpace(stringValue))
                ? new DomainNotification(key ?? "AssertArgumentNotEmpty", message)
                : null;
        }

        public static DomainNotification AssertTrue(bool boolValue, string message, string key = null)
        {
            return (!boolValue)
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }
        public static DomainNotification AssertFalse(bool boolValue, string message, string key = null)
        {
            return (boolValue)
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertNotAreEquals(string value, string match, string message)
        {
            return (value == match)
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }
        public static DomainNotification AssertNotAreEquals(int value, int match, string message)
        {
            return (value == match)
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertAreEqualsNoCase(string value, string match, string message)
        {
            return
                (!((value != null && match != null && value.ToUpper() == match.ToUpper()) ||
                   (value == null && match == null)))
                    ? new DomainNotification("AssertArgumentTrue", message)
                    : null;
        }

        public static DomainNotification AssertAreEquals(string value, string match, string message, string key = null)
        {
            return (!(value == match))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertAreEquals(int value, int match, string message, string key = null)
        {
            return (!(value == match))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertAreEquals(long value, long match, string message, string key = null)
        {
            return (!(value == match))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertAreEquals(bool value, bool match, string message, string key = null)
        {
            return (!(value == match))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterThan(int value1, int value2, string message, string key = null)
        {
            return (!(value1 > value2))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterThan(long value1, long value2, string message)
        {
            return (!(value1 > value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsBiggerThan(DateTime value1, DateTime value2, string message)
        {
            return ((value1 > value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsBiggerThan(long value1, long value2, string message)
        {
            return ((value1 > value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsBiggerThan(int value1, int value2, string message)
        {
            return ((value1 > value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterThan(decimal value1, decimal value2, string message)
        {
            return (!(value1 > value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsLessOrEqualThan(int value1, int value2, string message,
            string key = null)
        {
            return (!(value1 <= value2))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterOrEqualThan(int value1, int value2, string message)
        {
            return (!(value1 >= value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterOrEqualThan(long value1, long value2, string message)
        {
            return (!(value1 >= value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertCpfIsValid(string value, string message, string key = null)
        {
            return (!value.ValidaCpf())
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertEmailIsValid(string value, string message, string key = null)
        {
            return (!(value.ValidaEmail()))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertDataIsValid(string value, string message, string key = null)
        {
            return (!(value.ValidaData()))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertDataNotIsMinValueIsValid(DateTime value, string message, string key = null)
        {
            return !(value > DateTime.MinValue)
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertDataNotIsMinValueDataBaseIsValid(DateTime value, string message, string key = null)
        {
            return !(value.Ticks > new DateTime(1754, 01, 01).Ticks)
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertStatusIsValid(string value, string message, string key = null)
        {
            return (!(value.ValidaStatus()))
                ? new DomainNotification(key ?? "AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertRegexMatch(string value, string regex, string message)
        {
            return (!Regex.IsMatch(value, regex, RegexOptions.IgnoreCase))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertUrlIsValid(string url, string message)
        {
            // Do not validate if no URL is provided
            // You can call AssertNotEmpty before this if you want
            if (String.IsNullOrEmpty(url))
                return null;

            var regex = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

            return (!Regex.IsMatch(url, regex, RegexOptions.IgnoreCase))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertOr(DomainNotification value1, DomainNotification value2, string message)
        {
            return (value1 != null || value2 != null)
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertAnd(DomainNotification value1, DomainNotification value2, string message)
        {
            return (value1 == null || value2 == null)
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsDate(string value1,  string message)
        {

            try
            {
                DateTime resultado = DateTime.Parse(value1);
                return null;

            }
            catch
            {
                return new DomainNotification("AssertArgumentTrue", message);
            }

            

            //return (DateTime.TryParse(value1, out resultado))
            //    ? new DomainNotification("AssertArgumentTrue", message)
            //    : null;


            
        }

    }
}

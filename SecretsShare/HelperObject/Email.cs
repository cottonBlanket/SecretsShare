using System;
using System.Text.RegularExpressions;

namespace SecretsShare.HelperObject
{
    public class Email
    {
        public string Value;

        public Email(string value)
        {
            if (IsValid(value))
                Value = value;
            else
                throw new ArgumentException("Invalid format value");
        }
        
        private bool IsValid(object? value)
        {
            var r = new Regex(@"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(?:aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$");
            if (value is not string)
                return false;
            return r.IsMatch(value.ToString());
        }
    }
}
using System;
using System.Text;
namespace TestAPI.Utilities
{
    public class USPhoneNumberFormat : IPhoneNumberFormat
    {
        public USPhoneNumberFormat()
        {
        }

        public string countryCodeFormat(string number)
        {
            throw new NotImplementedException();
        }

        public bool isValidNumber(string number)
        {
            throw new NotImplementedException();
        }

        public string userFriendlyFormat(string number)
        {
            StringBuilder formattedNumber = new StringBuilder(number.Substring(0,1));
            formattedNumber.Append("-(");
            formattedNumber.Append(number.Substring(1,3));
            formattedNumber.Append(")-");
            formattedNumber.Append(number.Substring(4,3));
            formattedNumber.Append("-");
            formattedNumber.Append(number.Substring(7, 4));
			return formattedNumber.ToString();
        }
    }
}

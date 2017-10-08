using System;
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
            throw new NotImplementedException();
        }
    }
}

using System;
namespace TestAPI.Utilities
{
    public interface IPhoneNumberFormat
    {

        Boolean isValidNumber(string number);
        string countryCodeFormat(string number);
        string userFriendlyFormat(string number);

    }
}

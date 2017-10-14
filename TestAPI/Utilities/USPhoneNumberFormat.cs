using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.Extensions.Options;
using TestAPI.DataStore;
using Twilio;
using Twilio.Rest.Lookups.V1;
using Twilio.Types;
namespace TestAPI.Utilities
{
    public class USPhoneNumberFormat : IPhoneNumberFormat
    {
        private int _countryCode;
        private readonly TwilioApiCredentials _credentials;

        public USPhoneNumberFormat(IOptions<TwilioApiCredentials> credentials)
        {
            _countryCode = 1;
            _credentials = credentials.Value;
        }

        public string countryCodeFormat(string number)
        {
            StringBuilder normalizedPhoneNumber=new StringBuilder();
            string numberFormat = formatInputPhoneNumber(number);
			if (numberFormat[0]=='1' && numberFormat.Length == 11)
			{
                normalizedPhoneNumber.Append(numberFormat);
			}
			else if (numberFormat.Length == 10)
			{
                normalizedPhoneNumber.Append(_countryCode);
                normalizedPhoneNumber.Append(numberFormat);

			}
            return normalizedPhoneNumber.ToString();
        }

        private Boolean thirdPartyValidation(string phoneNumber)
        {
			Boolean isValid = false;
			TwilioClient.Init(_credentials.User, _credentials.Token);
            try
            {
				var phoneNumberDetails = PhoneNumberResource.Fetch(new PhoneNumber(phoneNumber), "US");
                isValid = true;
            }
            catch(Twilio.Exceptions.ApiException ex)
            {
                //log the invalid phone number
            }
            return isValid;
        }

        private string formatInputPhoneNumber(string number)
        {
            StringBuilder formattedStr = new StringBuilder();
            string[] numberParts=number.Split('-');
            int i;
            string part;
            int j;
            for (i = 0; i < numberParts.Length;i++)
            {
                part = numberParts[i];
                if(part.Equals("1") && i==0)
                {
                    formattedStr.Append(part);
                }
                else if (i!=numberParts.Length-1)
				{
                    string numPart=part.Replace("(","");
                    numPart = numPart.Replace(")", "");
                    if (int.TryParse(numPart, out j))
                    {
                        formattedStr.Append(numPart);
                    }
				}
                else if(part.Length==4)
                {
                    if (int.TryParse(part, out j))
                    {
                        formattedStr.Append(part);
                    }
                }
            }
            return formattedStr.ToString();
        }

        public bool isValidNumber(string number)
        {
            string numberFormat = formatInputPhoneNumber(number);
            if(String.IsNullOrEmpty(numberFormat))
            {
                return false;
            }
            else
            {
				if (numberFormat[0] == '1' && numberFormat.Length == 11)
				{
                    return thirdPartyValidation(number);
				}
				else if (numberFormat.Length == 10)
				{
                    return thirdPartyValidation(number);
				}
				else
				{
					return false;
				}
            }
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

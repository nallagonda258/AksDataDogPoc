using System;
using System.Text;
namespace TestAPI.Utilities
{
    public class USPhoneNumberFormat : IPhoneNumberFormat
    {
        private int _countryCode;

        public USPhoneNumberFormat()
        {
            _countryCode = 1;
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

        private string formatInputPhoneNumber(string number)
        {
            /*
            StringBuilder formattedStr = new StringBuilder(number);
			for (int i = 0; i < formattedStr.Length; i++)
			{
                if (formattedStr[i].Equals('-') || formattedStr[i].Equals('(')||formattedStr[i].Equals(')'))
				{
					formattedStr.Remove(i, 1);
				}
			}
            return formattedStr.ToString();
            */
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
					return true;
				}
				else if (numberFormat.Length == 10)
				{
					return true;
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

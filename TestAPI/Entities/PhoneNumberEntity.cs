using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Entities
{
	/// <summary>
	/// Represents all the phone numbers for a Customer
	/// </summary>
	public class PhoneNumberEntity
    {
		[Key]
        public int CustomerId { get; set; }
        public List<string> PhoneNumbers { get; set; }



		public PhoneNumberEntity()
		{

		}

        public PhoneNumberEntity(int cId,List<string> phoneNumberList)
        {
            this.CustomerId = cId;
            this.PhoneNumbers = phoneNumberList;
        }

        /// <summary>
		/// Add a phone number to an existing Customers phone number list
		/// </summary>
		public void AddContent(string phNumber)
		{
            PhoneNumbers.Add(String.Copy(phNumber));
		}


		/// <summary>
		/// Modify phone numbers in the Customers phone number list
		/// </summary>
		public void SetContent(List<string> newPhoneNumberList)
        {
            PhoneNumbers.Clear();
            foreach(string phoneNumber in newPhoneNumberList)
            {
                PhoneNumbers.Add(String.Copy(phoneNumber));
            }
        }
    }
}

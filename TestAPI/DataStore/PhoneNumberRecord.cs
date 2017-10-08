using System;
using System.ComponentModel.DataAnnotations;
namespace TestAPI.DataStore
{
    public class PhoneNumberRecord
    {
		public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }

        public PhoneNumberRecord(int id,string number)
        {
            CustomerId = id;
            PhoneNumber = number;
        }
    }
}

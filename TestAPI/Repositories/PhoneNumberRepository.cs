using System;
using System.Linq;
using System.Collections.Generic;
using TestAPI.Entities;
using TestAPI.DataStore;

namespace TestAPI.Repositories
{
    public class PhoneNumberRepository: IPhoneNumberRepository
    {
        private ApiContext _context;


		public PhoneNumberRepository(ApiContext context)
        {
            _context = context;
        }
        /*
        public void Add(PhoneNumberEntity phoneNumber)
        {
            int customerId = phoneNumber.CustomerId;
            if(_phoneNumbers.ContainsKey(customerId))
            {
                List<string> oldPhoneNums = _phoneNumbers[customerId];
                _phoneNumbers.Add(customerId,oldPhoneNums.Concat(phoneNumber.PhoneNumbers).ToList());
            }
        }
         */
        public List<string> GetAll(int id)
        {
            return _context.customerPhoneNumber.Where(m => m.CustomerId == id).Select(m => m.PhoneNumber).ToList();
        }


		public void Add(PhoneNumberEntity phoneNumber)
		{
            foreach (string number in phoneNumber.PhoneNumbers)
			{
				//data.AddContent(numberList.PhoneNumbers[i]);
                _context.customerPhoneNumber.Add(new PhoneNumberRecord(phoneNumber.CustomerId,number));
			}
            //_context.customerPhoneNumbers.Add(phoneNumber);
            _context.SaveChanges();
		}
/*
		public IEnumerable<List<string>> GetAll(int id)
		{
            var phoneNumbers = _context.customerPhoneNumbers.Where(p => p.CustomerId == id).Select(x => x.PhoneNumbers).ToAsyncEnumerable().ToEnumerable();
            return phoneNumbers;
		}
*/
	}
}

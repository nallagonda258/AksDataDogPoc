using System;
using System.Linq;
using System.Collections.Generic;
using TestAPI.Entities;
using TestAPI.DataStore;

namespace TestAPI.Repositories
{
    public class PhoneNumberRepository: IPhoneNumberRepository
    {
		readonly Dictionary<int, List<string>> _phoneNumbers = new Dictionary<int, List<string>>();
        private ApiContext _context;

        /*
		public PhoneNumberRepository(ApiContext context)
        {
            _context = context;
        }
        */

        public void Add(PhoneNumberEntity phoneNumber)
        {
            int customerId = phoneNumber.CustomerId;
            if(_phoneNumbers.ContainsKey(customerId))
            {
                List<string> oldPhoneNums = _phoneNumbers[customerId];
                _phoneNumbers.Add(customerId,oldPhoneNums.Concat(phoneNumber.PhoneNumbers).ToList());
            }
        }

        public IEnumerable<List<string>> GetAll(int id)
        {
            return _phoneNumbers.Select(x => x).Where(k => k.Key == id).Select(k => k.Value);
        }

/*
		public void Add(PhoneNumberEntity phoneNumber)
		{
            _context.customerPhoneNumbers.Add(phoneNumber);
            _context.SaveChanges();
		}

		public IEnumerable<List<string>> GetAll(int id)
		{
            var phoneNumbers = _context.customerPhoneNumbers.Where(p => p.CustomerId == id).Select(x => x.PhoneNumbers).ToAsyncEnumerable().ToEnumerable();
            return phoneNumbers;
		}
*/
    }
}

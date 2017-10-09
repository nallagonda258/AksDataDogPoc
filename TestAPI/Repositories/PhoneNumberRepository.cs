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

        public List<string> GetAll(int id)
        {
            return _context.customerPhoneNumber.Where(m => m.CustomerId == id).Select(m => m.PhoneNumber).ToList();
        }


		public void Add(PhoneNumberEntity phoneNumber)
		{
            foreach (string number in phoneNumber.PhoneNumbers)
			{
                if(!phoneNumberExists(phoneNumber.CustomerId,number))
                {
					_context.customerPhoneNumber.Add(new PhoneNumberRecord(phoneNumber.CustomerId, number));
                }
			}
            _context.SaveChanges();
		}

        private Boolean phoneNumberExists(int customerId,string number)
        {
            return _context.customerPhoneNumber.Any(cp => (cp.CustomerId == customerId && cp.PhoneNumber == number));
        }
	}
}

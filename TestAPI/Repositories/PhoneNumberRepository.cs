using System;
using System.Linq;
using System.Collections.Generic;
using TestAPI.Entities;
using TestAPI.DataStore;
using TestAPI.Utilities;
using Newtonsoft.Json;

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
            Serilog.ILogger Logger = LoggerExtensions.Logger();
            Logger.Information("Phone number with Id :{@account} requested", id);
            var number = _context.customerPhoneNumber.Where(m => m.CustomerId == id).Select(m => m.PhoneNumber).ToList();
            Logger.Information("Phone numbers retrived from database :{@account}", JsonConvert.SerializeObject(number));
            return number;
        }


		public void Add(PhoneNumberEntity phoneNumber)
		{
            foreach (string number in phoneNumber.PhoneNumbers)
			{
                if(!phoneNumberExists(phoneNumber.CustomerId,number))
                {
                    Serilog.ILogger Logger = LoggerExtensions.Logger();
                    Logger.Information("Add phone number request received :{@account}", phoneNumber);
                    _context.customerPhoneNumber.Add(new PhoneNumberRecord(phoneNumber.CustomerId, number));
                    Logger.Information("Phone number Added :{@account} Added to database", phoneNumber);
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

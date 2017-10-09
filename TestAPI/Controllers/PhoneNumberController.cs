using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Model;
using TestAPI.Repositories;
using TestAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using TestAPI.DataStore;
using TestAPI.Utilities;

namespace TestAPI.Controllers
{
    [Route("api/users/{id}/phone-numbers")]
    public class PhoneNumberController : Controller
    {


        private readonly IPhoneNumberRepository _phoneNumberRepository;
        private IPhoneNumberFormat _phoneNumberFormatter;

		public PhoneNumberController(IPhoneNumberRepository phoneRepository, IPhoneNumberFormat formatter)
		{
			_phoneNumberRepository = phoneRepository;
            _phoneNumberFormatter = formatter;
		}


		[HttpGet(Name = "GetPhoneNumbers")]
        public IActionResult GetPhoneNumbers(int id)
		{

            List<string> customerPhoneNumbers = _phoneNumberRepository.GetAll(id);
            PhoneNumber phNumberDTO = new PhoneNumber();
            int i = 0;
            phNumberDTO.PhoneNumbers = customerPhoneNumbers.ToArray();
            foreach(string phNumber in customerPhoneNumbers)
            {
                phNumberDTO.PhoneNumbers[i] = _phoneNumberFormatter.userFriendlyFormat(phNumber);
				i++;
            }
            return Ok(phNumberDTO);                      
		}

        [HttpPost]
        public IActionResult Post(int id,[FromBody]PhoneNumber numberList)
        {
            
            if (numberList == null)
			{
				return BadRequest("No Phone number were passed");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
            PhoneNumberEntity data = new PhoneNumberEntity(id,new List<string>());
            //can use automapper here

            for (int i = 0; i < numberList.PhoneNumbers.Length;i++)
            {
                if (_phoneNumberFormatter.isValidNumber(numberList.PhoneNumbers[i]))
                {
                    data.AddContent(_phoneNumberFormatter.countryCodeFormat(numberList.PhoneNumbers[i]));
                }
            }

            _phoneNumberRepository.Add(data);

			//variable to handle DB errors if connecting to a database
            bool result = true;
			if (!result)
			{
				throw new Exception("something went wrong when adding a new entry");
			}
            return CreatedAtRoute("GetPhoneNumbers",new { id = id} ,data);
        }
    }
}

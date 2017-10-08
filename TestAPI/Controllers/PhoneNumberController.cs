using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Model;
using TestAPI.Repositories;
using TestAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using TestAPI.DataStore;

namespace TestAPI.Controllers
{
    [Route("api/users/{id}/phone-numbers")]
    public class PhoneNumberController : Controller
    {


        private readonly IPhoneNumberRepository _phoneNumberRepository;

		public PhoneNumberController(IPhoneNumberRepository phoneRepository,ApiContext context)
		{
			_phoneNumberRepository = phoneRepository;
		}


		[HttpGet(Name = "GetPhoneNumbers")]
        public IActionResult GetPhoneNumbers(int id)
		{

            List<string> customerPhoneNumbers = _phoneNumberRepository.GetAll(id);
            PhoneNumber phNumberDTO = new PhoneNumber();
            phNumberDTO.PhoneNumbers = customerPhoneNumbers.ToArray();
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
                data.AddContent(numberList.PhoneNumbers[i]);
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

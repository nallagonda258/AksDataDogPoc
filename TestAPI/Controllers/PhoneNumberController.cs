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
        private ApiContext _context;

		public PhoneNumberController(IPhoneNumberRepository phoneRepository,ApiContext context)
		{
			_phoneNumberRepository = phoneRepository;
            _context = context;
		}


		[HttpGet(Name = "GetPhoneNumbers")]
		public List<string> GetPhoneNumbers(int id)
		{
            /*
            var customerPhoneNumbers = _phoneNumberRepository.GetAll(id);

            var allEntitysDto = customerPhoneNumbers.Select(x => Mapper.Map<HouseDto>(x));

            Response.Headers.Add("X-Pagination",
            JsonConvert.SerializeObject(new { totalCount = _houseRepository.Count() }));

            var customerPhoneNumbers = _context.customerPhoneNumbers.Where(p => p.CustomerId == id).Select(x => x.PhoneNumbers.ToList()).ToArray();

            return Ok(customerPhoneNumbers);
            */
            List<string> y = _context.customerPhoneNumber.Where(m => m.CustomerId==id).Select(m=>m.PhoneNumber).ToList();
            return y;
                        
		}
        /*
        // GET api/values
        [HttpGet(Name = "GetPhoneNumbers")]
        public IEnumerable<string> GetPhoneNumbers(int id)
        {
            return new string[] { "value1", "value2"};
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST api/values


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
                _context.customerPhoneNumber.Add(new PhoneNumberRecord(id,numberList.PhoneNumbers[i]));
            }

            _phoneNumberRepository.Add(data);

			//variable to handle DB errors if connecting to a database
            bool result = true;
			if (!result)
			{
				throw new Exception("something went wrong when adding a new House");
			}
			
			_context.SaveChanges();
            return CreatedAtRoute("GetPhoneNumbers",new { id = id} ,data);
        }
    }
}

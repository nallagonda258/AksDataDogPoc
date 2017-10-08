using System;
using System.Linq;
using TestAPI.Entities;
using System.Collections.Generic;

namespace TestAPI.Repositories
{
    public interface IPhoneNumberRepository
    {
      void Add(PhoneNumberEntity phoneNumber);
      List<string> GetAll(int id);
    }
}

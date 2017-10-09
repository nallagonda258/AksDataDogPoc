using System;
namespace TestAPI.Messages.ErrorHandling
{
    public class InvalidPhoneNumber:ErrorMessage
    {
        public string InvalidNumber { get; set; }
        public InvalidPhoneNumber()
        {
        }
    }
}

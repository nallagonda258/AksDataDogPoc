using System;
namespace TestAPI.Messages
{
    public class ErrorMessage: StatusMessage
    {
		public int ErrorCode { get; set; }
		public ErrorMessage()
		{

		}
    }
}

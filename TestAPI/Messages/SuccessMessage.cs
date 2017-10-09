using System;
namespace TestAPI.Messages
{
    public class SuccessMessage: StatusMessage
    {
		public object result { get; set; }
		public SuccessMessage()
		{
		}
    }
}

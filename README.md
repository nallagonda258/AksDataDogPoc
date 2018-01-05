API project in C#

1. POST API to save phone numbers.

http://localhost/api/users/{userId}/phone-numbers

Request Body

{
 "phoneNumbers" : [
					"888-888-8881",
					"(888)-888-8882",
					"1-(888)-888-8883",
					"FooFoo123",
					"BarBar987",
					"1-(888)-888-88"
                  ]
}

Response

[
    {
        "result": "888-888-8881",
        "message": "Success"
    },
    {
        "result": "(888)-888-8882",
        "message": "Success"
    },
    {
        "result": "1-(888)-888-8883",
        "message": "Success"
    },
    {
        "invalidNumber": "FooFoo123",
        "errorCode": 5124,
        "message": "Invalid Phone Number"
    },
    {
        "invalidNumber": "BarBar987",
        "errorCode": 5124,
        "message": "Invalid Phone Number"
    },
    {
        "invalidNumber": "1-(888)-888-88",
        "errorCode": 5124,
        "message": "Invalid Phone Number"
    }
]

2. GET API

http://localhost:5000/api/users/{userId}/phone-numbers

Response

{
    "phoneNumbers": [
        "1-(888)-888-8881",
        "1-(888)-888-8882",
        "1-(888)-888-8883"
    ]
}



To use Twilio validation you need to replace the User and Token fields in appsettings.Development.json with Account Id and Auth Token as shown below,


  "Twilio": {
    "User": "YourUserID",
    "Token": "YourUserToken"
  }

# SecBank Web API

This is a .NET web API that allows third-party users to post single transactions and multiple transactions. It also has an endpoint to generate a JWT token and to fetch all transactions from a SQLite database. The API was built using .NET and Entity Framework. It has a Windows worker service to process recurrent transactions.

## Getting Started

To get started with this project, clone the repository and open it in Visual Studio. You will need to have .NET installed on your machine.

## Endpoints

The following endpoints are available:

- `/api/transaction/posttransaction` - POST a single transaction
- `/api/transaction/posttransactions` - POST multiple transactions
- `/api/transaction/gettoken` - GET a JWT token
- `/api/transaction/getalltransactions` - GET all transactions from the SQLite database

## Usage

To use this API, you will need to generate a token for authorisation using the "getToken" endpoint. Once you have a token, you can use the endpoints to post transactions and generate a JWT token.

## Built With

- [.NET](https://dotnet.microsoft.com/) - A free, cross-platform, open-source developer platform for building many different types of applications.
- [Entity Framework](https://docs.microsoft.com/en-us/ef/) - A free, open-source object-relational mapping (ORM) framework for .NET.
- [SQLite](https://www.sqlite.org/index.html) - A free, open-source SQL database engine.

## Authors

- Victor Madu(https://github.com/thevictormadu)

## Acknowledgments

- Hat tip to FirstBank for giving me this assignment.

Please let me know if you have any questions!

May your code live forever.

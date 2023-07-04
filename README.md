# LSAApi
.NET CRUD API for app to monitor ethernet devices

This project is an API server written in .NET 6.0 for an application that is used to monitor Ethernet switches. 
Authentication makes by JwtBearer. The project uses the repository pattern and provides an interface for communicating with the MS SQL database. 
The project also includes unit tests that are written using tools such as FakeItEasy, FluentAssertions and xUnit.

### Configuration

Before launching the project, configure the Connection String in the appsettings.json file.

Delete the Migrations folder and then run the commands:
```
Add-Migration Init
Update-Database
```

You can use a prepared [SQL Insert Query](https://github.com/b0r55uk/LSAApi/blob/master/SQLInsertQuery.sql) to populate the database with test data



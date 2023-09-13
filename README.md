# LSAApi
.NET CRUD API for app to monitor ethernet devices

This project is an API server written in .NET 6.0 for an application that is used to monitor Ethernet switches. 
Authentication makes by JwtBearer. The project uses the repository pattern and provides an interface for communicating with the MS SQL database. 
The project also includes unit tests that are written using tools such as FakeItEasy, FluentAssertions and xUnit.

## NuGet Packages
### LSAApi
```
AutoMapper v12.0.1
AutoMapper.Extensions.Microsoft.DependencyInjection v12.0.1
Microsoft.AspNetCore.Authentication.JwtBearer v6.0.19
Microsoft.EntityFrameworkCore.Design v7.0.8
Microsoft.EntityFrameworkCore.SqlServer v7.0.8
Microsoft.EntityFrameworkCore.Tools v7.0.8
Swashbuckle.AspNetCore v6.2.3
System.IdentityModel.Tokens.Jwt v6.31.0
System.Security.Claims v4.3.0
```
### LSAApi.Tests
```
FakeItEasy v7.4.0
FluentAssertions v6.11.0
Microsoft.EntityFrameworkCore.InMemory v7.0.8
Microsoft.NET.Test.Sdk v17.3.2
xunit v2.4.2
xunit.runner.visualstudio v2.4.5
```

## Configuration

Before launching the project, configure the Connection String in the appsettings.json file.

You can use a prepared [SQL Insert Query](https://github.com/b0r55uk/LSAApi/blob/master/SQLInsertQuery.sql) to populate the database with test data



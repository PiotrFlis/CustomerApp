#Introduction
The solution has a few projects:
 - CustomerApp.Model
   Data Model
 - CustomerApp.Model.InMemory
   Implementation of data access using EF and in-memory database
 - CustomerApp.WebApp.Tests
   Integration tests of data access
 - CustomerWebApp
   .Net Asp Core web app providing WebAPI to allow operations on customers

Due to lack of time I did not implement client application

#Test
1. In order to test the data access layer you can run the tests from within Visual Studio
Test > Run > All tests

2. WebAPI can be tested using REST client like RESTClient for Firefox or Postman for Chrome
Run the solution 
Setup REST client to include in header: Content-Type: application/json

#WebAPI
-----------------
- List all customers - action added for testin purposes

GET
http://localhost:60852/api/customers

3 records in received JSON response (the application seeds the database with 3 records)

-----------------
- Add new record
POST

body:
{"name":"Steve","surname":"White","telephoneNumber":"+48 324234","address":"Warsaw"}

-----------------
- Get customer with id 3

GET
http://localhost:60852/api/customers/3

received response:
{
  "id": 3,
  "name": "Jane",
  "surname": "Smith",
  "telephoneNumber": "+1 12211234",
  "address": "Chicago"
}
-----------------

- Update customer 3 with new address
PUT
http://localhost:60852/api/customers

body:
{"id":3,"name":"Jane","surname":"Smith","address":"Sydney"}

-----------------
- Remove customer Id 2

DELETE
http://localhost:60852/api/customers/2

-----------------

- Remove customer Jane Smith
DELETE
http://localhost:60852/api/customers
{"id":3,"name":"Jane","surname":"Smith","address":"Sydney"}

-----------------

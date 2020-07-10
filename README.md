# Inventorify
A web app that allows you to view and manage your inventory thorugh a UI or API.
To run the web app, clone the project and open Inventorify.sln in Visual Studio, from there, build and run the app.

### Seeding the database
The database does not need to be manually seeded. If the database is empty the databse will automatically seed with 9 data entries. <br/>
If you want to reseed the database, set `const bool ReSeed = true;` in Inventorify\Models\ReSeedDb.cs or delete all data entries and the database will reset.

### Troubleshooting database
If the site loads an SqlException `SqlException: Cannot open database "Inventorify" requested by the login` then the database has not been created.
TO fix this, open the Nuget Package Manager Console and type: <br/>
`add-migration AddInventorifyToDb` <br/>
followed by <br/>
`update-database` <br/>
This will create the database the site is trying to access.

### Testing
The Api test file is located in InventorifyApiTests\InventoryItemsTestController.cs 
On Visual Studio, use the terminal to navigate to InventorifyApiTests then type the command "dotnet test" to run the 7 tests. The tests should pass on a newly seeded database.

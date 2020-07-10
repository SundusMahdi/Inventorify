# Inventorify
A web app that allows you to view and manage your inventory thorugh a UI or API. <br/>
To run the web app, clone the project and open Inventorify.sln in Visual Studio. Insure you have Sql Server Installed. from Visual Studio, build and run the app. You will likely need to initialize the database. To initialize the database see Troubleshooting database.

### Seeding the database
The database does not need to be manually seeded. If the database is empty the databse will automatically seed with 9 data entries. <br/>
If you want to reseed the database, set `const bool ReSeed = true;` in Inventorify\Models\ReSeedDb.cs or delete all data entries and the database will reset.

### Troubleshooting database
If the site loads the SqlException: <br/>
`SqlException: Cannot open database "Inventorify" requested by the login` <br/>
then the database has not been created.
To fix this, open the NuGet Package Manager Console by clicking Tools > NuGet Package Manager > Package Manager Console <br/> 
On the Package Manager Console then type: <br/>
`update-database` <br/>
This will create the database the site is trying to access.

### Testing
The Api test file is located in InventorifyApiTests\InventoryItemsTestController.cs
Use any terminal to navigate to InventorifyApiTests on your machine then type the command "dotnet test" to run the 7 tests. The tests should pass on a newly seeded database.

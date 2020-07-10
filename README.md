# Inventorify
A web app that allows you to view and manage your inventory thorugh a UI or API.

# Seeding the database
If the database is empty the databse will automatically seed with 9 data entries.
If you want to reseed the database, change ReSeed to true in Inventorify\Models\ReSeedDb.cs or delete all data entries and the database will reset.

# Testing
The Api test file is located in InventorifyApiTests\InventoryItemsTestController.cs 
On Visual Studio, use the terminal to navigate to InventorifyApiTests then type the command "dotnet test" to run the 7 tests. The tests should pass on a newly seeded database.

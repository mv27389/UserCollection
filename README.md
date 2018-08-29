#UserCollection
UserCollection is a web api application to manage user contacts. 

Install Guidelines:
1. Download the project.
2. Open thhe solution is Visual Studio. Change the mode to "Release" and build the solution.
3. In Solution Explorer, expand folder "DataAccess". Right click on "UserCollection.Database" project and click on "Publish".
4. Set target databse connection. Set Database name as "UserCollection.Database". and click on "Publish" button.
5. In Solution Explorer, right click on the "UserCollection" project. Click on "Publish".
6. Select the Folder option and enter the path of output directory and click on "Publish" button.
7. Open IIS, either create a new application pool or use existing one.
8. Go to "Default Web Site" under "Sites". Right click on "Default Web Site" and select "Add Application".
9. Give an alias to the application. Select application pool and browse to the output directory, set in Step 6 and click "Ok".

Directory Structure
Project has one Web API project (UserCollection) and 3 Solution folders, namely DataAccess, Services and UnitTests.

UserCollection project contains the UserController which has all methods to manage user contacts.

DataAccess folder contains Database project, which will create the database in SQL Server.
It also contains repository projects which provide abstraction to NHibernate.

Services folder contains service which communicates with repository project to perform CRUD operations. It also contains project for logging.

UnitTests contains project which has methods to run test cases for the services.




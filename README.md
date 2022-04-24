# UHPS
Courier/Post office database: Gather data concerning the packages going in and out of the post office, where they are going, and who is sending them. Additionally, the team would have to build a database of customers in  relation to what packages they are shipping out.The database should have the functionality to provide ‘tracking’ history for packages both for customers and employees. 
This is a Database Systems course project to be graded by Professor Ramamurthy at University of Houston. Team members include Son La, Morrison Fowlks, Andy Nguyen, Josh Opie, Bader Salem. 

Contributors:
La, Son : 3467150404 shiloh1712@gmail.com
Opie, Josh: 8323535360  blob28895@gmail.com
Fowlks, Morrison: 8329958009, morrisonfowlks1@gmail.com
Nguyen, Andy: 8329705903, andynguyen7835@gmail.com
Salem, Bader: 7134473104, badersalem12@gmail.com

This is a web application project to be submitted to Professor Uma Ramamurthy. This web application allows customers to track sent at the store, get a quote before shipping, browse shipping items, view their sent/recived shipments,
and employees to manage (create, edit, view, delete) shop items, shipments, sales, stores, customers,... Anonymous user with a valid tracking number can track their shipments

Installation/Localhost:
1. Install Visual Studio Community and SQL server (and set up local database)
2. Clone/Download the repository/Google Drive
3. Open UHPS\UHPostalService\UHPostalService.sln
4. Open Package Manager Console: Tools > Nuget Package Manager > Package Manager Console
5. run "update-database"
6. Run: Debug > Start Debugging

Credits:
.NET Razor Pages Crash Course:
https://www.youtube.com/watch?v=eru2emiqow0
Database Models
https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-6.0&tabs=visual-studio#scaffold-student-pages
Triggers:
https://www.sqlshack.com/learn-sql-sql-triggers/
Authentication:
https://www.tektutorialshub.com/asp-net-core/user-registration-login-using-cookie-authentication-asp-net-core/
Using raw sql in .NET Razor Pages:
https://docs.microsoft.com/en-us/ef/core/querying/raw-sql

To improve:
1. A new employee (store unassigned) should not have access to pages that an official employee can (e.g. create shop item)
2. An employee should not be able to edit their own role to supervisor/admin nor edit their workplace (storeid)
3. An employee should only see shipments that are in their store at the moment and shipments expected to arrive
4. Should have another table containing sale items for each sale
5. 
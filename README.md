# AppBookRazorPages


Project Setup Instructions (ASP.NET Core + SQL Server)
⚙️ Requirements
Before you begin, make sure the following are installed:

.NET SDK 8.0+
SQL Server
Visual Studio 2022 or Visual Studio Code (with the C# extension)

🔧 How to Run the Project
1. Clone the Repository
git clone https://github.com/sayron2332/AppBookRazorPages
cd AppBookRazorPages


Then run the following command to apply migrations and create the database:
dotnet ef database update --project Chapter02.Infrastructure --startup-project Chapter02

build the Project Api project
cd Chapter02
dotnet build

and than you can the project 
dotnet run

🔑 Admin Login Credentials
By default, the project seeds an admin user when it starts:
Email: admin@example.com
Password: Admin123!
These can be changed in the SeedData.cs or DbInitializer.cs file if needed.

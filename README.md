![image](https://github.com/user-attachments/assets/6a2e3ba5-039b-4701-949b-e0fd85499186)
![image](https://github.com/user-attachments/assets/6aec5d1f-3409-4b9f-8055-7a30242c0ca8)# AppBookRazorPages


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

build the Project project
dotnet build

and than you can run the project 
cd Chapter02
dotnet run

🔑 Admin Login Credentials
By default, the project seeds an admin user when it starts:
Email: admin@example.com
Password: Admin123!
These can be changed in the SeedData.cs or DbInitializer.cs file if needed.

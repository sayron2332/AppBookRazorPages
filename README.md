# AppBookRazorPages
📚 Project Description 
This web application is an online Bookstore, where users can:

🔐 Register, log in (authentication), and access role-based features (authorization)

📖 Browse the list of books

➕ Add new books, authors, genres, and users

📝 Edit and delete existing records (full CRUD support for all entities)

🔎 Search for books by title

🛠 Access the admin panel to manage all data
![image](https://github.com/user-attachments/assets/6a2e3ba5-039b-4701-949b-e0fd85499186)
![image](https://github.com/user-attachments/assets/6aec5d1f-3409-4b9f-8055-7a30242c0ca8)


Project Setup Instructions (ASP.NET Core + SQL Server)
⚙️ Requirements
Before you begin, make sure the following are installed:

.NET SDK 8.0+
SQL Server
Visual Studio 2022 or Visual Studio Code (with the C# extension)

🔧 How to Run the Project
1. Clone the Repository
git clone https://github.com/sayron2332/AppBookRazorPages
and open folder with project: cd AppBookRazorPages


Then run the following command to apply migrations and create the database:
dotnet ef database update --project Chapter02.Infrastructure --startup-project Chapter02

and than you need to open Web project: cd Chapter02

and finish run: dotnet run

🔑 Admin Login Credentials
By default, the project seeds an admin user when it starts:
Email: admin@example.com
Password: Admin123!
These can be changed in the SeedData.cs or DbInitializer.cs file if needed.

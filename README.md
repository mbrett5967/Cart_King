<div align="center">
  <h1>🛒 Cart King</h1>
  
  <p>
    <strong>A modern e-commerce platform built with ASP.NET Core MVC</strong>
  </p>

  <p>
    <img src="https://img.shields.io/badge/.NET-9.0-blue?style=for-the-badge&logo=dotnet" alt=".NET 9"/>
    <img src="https://img.shields.io/badge/Entity%20Framework%20Core-9.0-512BD4?style=for-the-badge&logo=dotnet" alt="EF Core"/>
    <img src="https://img.shields.io/badge/PostgreSQL-18-336791?style=for-the-badge&logo=postgresql" alt="PostgreSQL"/>
  </p>
</div>

---
<img src="https://img.shields.io/badge/WIP-8A2BE2" alt="WIP"/>

<h3>All, including README subject to change</h3>


>(24/03/2026): Successfully setup Email servicing using MailTrap Sandbox. Now have functional email sending for authentication and registration.
> 
> (15/04/2026) : Added IdentityUserId linked forms for profile dashboard. Now have Delivery address data saved and persistent for users


<img width="1475" height="764" alt="image" src="https://github.com/user-attachments/assets/af997f21-b43a-4f48-9ff7-0ea94264b6b5" />
<h2>My Delivery Address Partial</h2>

* Uses professional ViewModel flow, recieved data updates main model etc
* Calls data from model for "Welcome @Model.FirstName" greeting, in dashboard header
* Saves the form data and returns saved state
* TempData used to notify of success or error of saved data
  
<img width="1904" height="806" alt="image" src="https://github.com/user-attachments/assets/aed28348-016b-4e32-8065-411c7abbcf18" />
<h2>Working Email services for Authentication</h2>

* Initial implementation using Mailtrap SMTP Sandbox
* Using Default IdentityUser for password resets and account registration

<img width="1381" height="662" alt="image" src="https://github.com/user-attachments/assets/5a97f33a-2949-4773-aa7c-aa48debd2aa9" />
<img width="1891" height="894" alt="image" src="https://github.com/user-attachments/assets/95621774-b7c7-49a8-bff4-490d75570407" />
<img width="1904" height="956" alt="image" src="https://github.com/user-attachments/assets/bfff015f-83fb-4d49-b764-7717095e5f29" />


 

 


## 🚀 Overview
Cart King is a technical showcase of **Relational Database Management** and **MVC Architecture**. It features a robust backend powered by PostgreSQL and Entity Framework Core, handling everything from product categorisation to simulated order processing using business rules, and dynamic content rather than UI design.

The aim of this project is to showcase my progression as a Junior developer, displaying my ability to interact with a modern tech stack and hopefully cement confidence in my productivity and suitability to a development team.



Aims

* Understanding design-time tooling for Ef core migrations to run using an admin connection and app user
* Database Schema Design - Implementing C# classes that define the database structure
* Developing experience using Queries,building ViewComponent logic
* Experience in Bootstrap classes and CSS styling, such as hover states, Google fonts , imported icons and much more
* Database seeding using OnModelCreating, implementing this with time oriented Views
* and so much more - honestly the amount I have learned is monumental and I have made sure to include everything in my notes for future projects 



## ✨ Planned Features

- 🛍️ Product listing with rich descriptions, multiple photos & categories
- 👤 User registration & authentication (email confirmation, password reset)
- 🛒 Shopping cart & persistent cart (saved between sessions)
- 💳 Checkout process (currently simulated – ready for payment gateway integration)
- ⭐ Buyer & seller ratings / reviews
- 🔍 Advanced search & filtering (category, price range, condition, location...)
- 📱 Responsive design 
- 🛡️ Basic security features (anti-forgery, input validation, HTTPS ready)
- 👑 Admin panel (product moderation, user management, statistics)

Tech Stack
* .NET 9
* ASP.NET Core MVC
* Entity Framework Core
* PostgreSQL
* pgAdmin 4
* Npgsql EF Core Provider









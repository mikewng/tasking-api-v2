# Introduction
This is a simple and low-weight tasking application designed for keeping track of work items, chores, activities, etc. Users can create their own account, create multiple boards and tasks in those boards. There is also an option to connect to their preferred calendar application, which allows them 
to forward these tasks either manually or toggle to have it be automatic.

# Application Structure
This application attempts to follow Clean Architecture and SOLID Principles as close as possible. It uses Controllers for API Endpoints
 - Controller Endpoints
 - Infrastructure
   - Contexts - Shared data and functionality across the application
       - AppDbContext.cs - Provides EF model creations and low-level CRUD on SQL database.
   - Extensions - Scoped Injections of Interfaces
       - ProgramExtensions.cs
 - Main
   - Secure Layer - User Authorization Layer
       - IUserSecure.cs
   - Service Layer - Business Logic
       - IBoardService.cs
       - IBoardTaskService.cs
       - ICalendarService.cs
       - IUserService.cs
   - Data (Repository) Layer - Database Queries and Manipulation
       - IBoardRepository.cs
       - IBoardTaskRepository.cs
   - Models - Entity Framework Models translated from SQL to C#, Request Bodies

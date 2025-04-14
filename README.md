# Receipt Tracker

A web application that allows university employees to submit receipts for reimbursement. Built with .NET 8, ASP.NET Core Web API, and Blazor WebAssembly.

## Project Overview

Receipt Tracker is a simple application designed to help university employees submit and track their reimbursement requests. Users can upload receipts with details such as date, amount, and description. The application stores this information in a database and provides a convenient way to view submitted receipts.

## Time Estimation

- Estimated Time: 20 hours
- Actual Time Spent: 17 hours

*The additional time was spent debugging CORS issues and file upload functionality, also ran into issue HTTP requests not going through due to old database being used before another parameter was added to models. Time was also spent figuring out how to link Web API and Blazor together*

## Technology Stack

- **Backend**: ASP.NET Core Web API (.NET 8)
- **Frontend**: Blazor WebAssembly (.NET 8)
- **Database**: SQLite
- **ORM**: Entity Framework Core 8

### Reasons for Choosing This Tech Stack

1. **.NET 8**: I chose the latest version of .NET for improved performance and modern language features, also just most familiar with it from a Full Stack development course I took
2. **Blazor WebAssembly**: Web app is what was wanted for this task and this was a .NET area I hadnt used before and wanted to learn and have created HTML pages before so that transferred to this.
3. **SQLite**: Lightweight database that requires no installation and is perfect for demonstration purposes, used MS SQL in .NET before but not SQLite in .NET so wanted to learn it. Also if MS SQL was used the connection would be unique to my MS SQL server on my computer making cloning it and using it difficult (just how I personally learned it).
4. **Entity Framework Core**: Simplifies data access and provides a consistent API for database operations.

## Features

- Submit receipt information (date, amount, description)
- Upload receipt images/PDFs
- View list of submitted receipts
- Download uploaded receipt files
- Delete receipts

## Prerequisites

- Visual Studio 2022 (or later)
- .NET 8 SDK
- Git (optional, for cloning the repository)

## Installation and Setup

1. Clone or download the repository: https://github.com/jholl1886/ReceiptTracker_2025.git
2. Open the solution file `ReceiptTracker.sln` in Visual Studio.

3. Restore NuGet packages: ( I didnt have to do this on my other computer but just in case)
- Right-click on the solution in Solution Explorer and select "Restore NuGet Packages"
- Alternatively, run `dotnet restore` in the terminal at the solution root

4. Set multiple startup projects:
- Right-click on the solution in Solution Explorer
- Select "Set Startup Projects..."
- Choose "Multiple startup projects"
- Set both `ReceiptTracker.API` and `ReceiptTracker.Client` to "Start"

5. Build the solution:
- Press Ctrl+Shift+B or select "Build > Build Solution" from the menu

6. Run the application:
- Press F5 or click the "Start" button to run the application

## Usage

1. Navigate to the home page
2. Click on "Add Receipt" in the navigation menu
3. Fill in the form with receipt details:
- Date of purchase
- Amount
- Description
- Upload receipt file (image or PDF)
4. Click "Submit" to save the receipt
5. View all receipts on the "Receipts" page
6. Download or delete receipts as needed

## Project Structure

- **ReceiptTracker.API**: Backend Web API service
- Controllers: Handle HTTP requests
- Services: Business logic layer,used interfaces
- Data: Database context and configuration

- **ReceiptTracker.Client**: Blazor WebAssembly frontend
- Views: UI components
- ViewModels: State management and business logic
- Services: API communication, again used interfaces

- **ReceiptTracker.Shared**: Common models and DTOs shared between client and server

## Assumptions and Design Decisions

1. **Security**: For this demonstration, authentication and authorization were omitted. In a production environment, proper identity management would be implemented.
2. **File Storage**: Files are stored both in the database (for portability) and in the filesystem (for performance). In a production environment, a blob storage solution might be more appropriate.
3. **Validation**: Basic validation is implemented for form inputs. More comprehensive validation would be needed in a production system.
4. **MVVM Pattern**: The application follows the Model-View-ViewModel pattern to separate concerns and improve testability, what I learned in my Full Stack Development course instead of more standard MVC.

## Challenges and Solutions

- **General Learning**: First time creating a Blazor Web app and also first time using SQLite in a .NET application. Relearning how to relink client and server side solutions
- **CORS Configuration**: Ensured proper CORS policy setup to allow communication between the client and server.
- **File Upload Handling**: Implemented proper file upload handling on both client and server sides.
- **Database Integration**: Set up Entity Framework Core with SQLite for persistent storage.

## Highlights

- Clean architecture with separation of concerns
- Proper use of DTOs for data transfer
- Asynchronous programming throughout
- Responsive UI with validation feedback
- File upload and download functionality

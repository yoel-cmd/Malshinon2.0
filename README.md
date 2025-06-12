# Malshinon2.0
Malshinon 2.0 - Reporting System
Project Description
Malshinon 2.0 is a reporting management system that allows users to add, update, and track reports on individuals in a database. The system features a simple console interface and utilizes MySQL for data management. It supports creating individuals, managing statuses (e.g., Informer, Target, Both, PotentialAgent), identifying potential agents, and detecting high-risk targets.
Key Features

Add reports including first name, last name, and report text.
Identify potential agents (based on more than 10 reports with an average length over 100 characters).
Detect high-risk targets (based on 20 or more reports, or 3 or more reports in the last 15 minutes).
Manage individual statuses (Informer, Target, Both, PotentialAgent).
Log actions and errors in a log table.

System Requirements

Development Environment: .NET Framework or .NET Core.
Database: MySQL Server.
NuGet Packages:
MySql.Data for MySQL connection management.


Database: A database named malshinon2.0 with the tables people, reports, and Logs.

Installation Instructions

Install MySQL:

Install MySQL Server and configure a user (e.g., root with no password, or update the connection string accordingly).
Create a database named malshinon2.0 and run the following schema to create tables:

CREATE TABLE people (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    SecretCode VARCHAR(6),
    ReportCount INT DEFAULT 0,
    Status VARCHAR(20),
    MentionCount INT DEFAULT 0
);

CREATE TABLE reports (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    InformerId INT,
    ReportedId INT,
    ReportText TEXT,
    ReportLength INT,
    ReportDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (InformerId) REFERENCES people(Id),
    FOREIGN KEY (ReportedId) REFERENCES people(Id)
);

CREATE TABLE Logs (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Info TEXT,
    LogDate DATETIME DEFAULT CURRENT_TIMESTAMP
);


Project Setup:

Clone the repository or download the files.
Open the project in Visual Studio (or a compatible IDE).
Add the MySql.Data package via NuGet:dotnet add package MySql.Data


Ensure the connection string in Program.cs and servis.cs matches your local settings:"server=localhost;username=root;password=;database=malshinon2.0"




Running the Project:

Build the project and run it via IDE or using the command:dotnet run





Usage Instructions

Running the Program:

Run the program to see a menu with 4 options:
1: Add a report.
2: View potential agents.
3: View high-risk targets.
4: Exit.




Adding a Report (Option 1):

Enter the first name and last name of the informer.
Enter a report in the format: first_name last_name report_text.
Example: John Doe acting suspiciously.


Viewing Potential Agents (Option 2):

The system displays individuals with the PotentialAgent status.


Viewing High-Risk Targets (Option 3):

The system displays individuals reported on frequently.


Exit (Option 4):

Closes the program and terminates the database connection.



Database Structure

Table people:

Id: Primary key, unique identifier.
FirstName: First name.
LastName: Last name.
SecretCode: Random secret code (6 characters).
ReportCount: Number of reports submitted by the individual.
Status: Status (Informer, Target, Both, PotentialAgent).
MentionCount: Number of times the individual was reported.


Table reports:

Id: Primary key.
InformerId: ID of the informer.
ReportedId: ID of the reported individual.
ReportText: Report text.
ReportLength: Length of the report.
ReportDate: Date and time of the report.


Table Logs:

Id: Primary key.
Info: Description of the action or error.
LogDate: Date and time of the log entry.



Known Limitations

A single database connection may cause DataReader errors if not handled properly (fixed in this version).
No strict input validation (e.g., empty or invalid names).
The potentialAgen method is redundant and similar to UpdateStatus.

Developer Notes

Consider adding advanced error handling, such as input validation.
Consider moving the connection string to an external configuration file.
Replacing return null with empty lists (new List<people>()) could improve stability.

Credits

Developed by [Developer Name, if applicable].

License
This project is private and has no public license.

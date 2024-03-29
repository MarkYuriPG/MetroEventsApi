# Setting Up Local SQL Server for MetroEvents

To set up the local SQL Server for MetroEvents, you'll need to follow these steps:

## Prerequisites

Ensure you have the following installed:

1. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. [Microsoft SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
3. Visual Studio with the following workloads:
    - [ASP.NET and web development](https://visualstudio.microsoft.com/vs/features/net-web/)
    - [.NET desktop development](https://visualstudio.microsoft.com/vs/features/net-desktop/)
    - [Desktop development with C++](https://visualstudio.microsoft.com/vs/features/desktop/)
    - [Data storage and processing](https://visualstudio.microsoft.com/vs/features/data-tools/)

## Installation Steps

1. **SQL Server Installation:**
   - Download SQL Server from [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
   - Follow the installation instructions provided by Microsoft.

2. **Microsoft SQL Server Management Studio (SSMS) Installation:**
   - Download SSMS from [here](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16).
   - Install SSMS by following the provided instructions.

3. **Restoring MetroEvents Database:**
   - Ensure you have a backup file (`*.bak`) of the MetroEvents database.
   - You can download the backup file from [here](https://drive.google.com/file/d/1iM9kjhzP8r51TyThuKx6cnkzo60Bw6PZ/view).
   - Open SSMS and connect to your SQL Server instance.
   - Right-click on "Databases" in the Object Explorer.
   - Select "Restore Database" and choose the MetroEvents backup file.
   - Follow the prompts to complete the database restoration process.

4. **Configuring MetroEventsApi:**
   - Open `appsettings.json` file in your `MetroEventsApi` solution.
   - Change the `Data Source` in the `DefaultConnection` according to the Server Name set up in the SQL Server.
![image](https://github.com/MarkYuriPG/MetroEventsApi/assets/123946546/70019b66-ca09-470c-ab38-619ffb71007d)

## Usage

After completing the setup, you can start using the MetroEvents database for your application.

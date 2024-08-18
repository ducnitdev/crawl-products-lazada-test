
# 🛒 Product Crawler

Welcome to the **Product Crawler** project! This .NET application is designed to crawl product information from the Lazada website and store it in a MySQL database. It leverages Selenium WebDriver for web scraping and Entity Framework Core for database operations.

## 🌐 Live Demo

- **Products List Page**: [View Demo](http://13.54.146.219/)
- **Crawled From**: [Lazada Store](https://www.lazada.vn/locklock-flagship-store/?q=All-Products&from=wangpu&langFlag=vi&pageTypeId=2)

## 📂 Project Structure

- **`Program.cs`**: The entry point of the application, initializing the product crawler and saving crawled data to the database.
- **`WebDriverFactory.cs`**: Responsible for creating and configuring the Selenium WebDriver instance.
- **`ProductCrawler.cs`**: Manages the logic of crawling product information from the Lazada website.
- **`ProductRepository.cs`**: Handles saving the crawled product data to the MySQL database.
- **`ProductContext.cs`**: The Entity Framework `DbContext` class used for interacting with the MySQL database.
- **`Product.cs`**: The entity class representing a product.

## ⚙️ Setup Guide

### Prerequisites

- .NET Framework 4.8 or later
- MySQL Server
- Google Chrome (for Selenium WebDriver)
- ChromeDriver (ensure the version matches your installed version of Chrome)

### Configuration Steps

1. **Database Setup**: 

   Create a MySQL database and a `products` table using the following SQL script:

   ```sql
   CREATE TABLE `products` (
       `id` int NOT NULL AUTO_INCREMENT,
       `name` varchar(255) DEFAULT NULL,
       `url` varchar(255) DEFAULT NULL,
       `price` varchar(255) DEFAULT NULL,
       `image` varchar(255) DEFAULT NULL,
       PRIMARY KEY (`id`)
   );
   ```

2. **Connection String**:

   Update the connection string in your `App.config` file with your MySQL database details:

   ```xml
   <connectionStrings>
       <add name="ProductDatabase" connectionString="server=YOUR_SERVER;database=YOUR_DATABASE;uid=YOUR_USERNAME;pwd=YOUR_PASSWORD;" providerName="MySql.Data.MySqlClient" />
   </connectionStrings>
   ```

3. **Install Dependencies**:

   Run the following commands to install the necessary NuGet packages:

   ```sh
   dotnet add package Selenium.WebDriver
   dotnet add package Selenium.WebDriver.ChromeDriver
   dotnet add package Microsoft.EntityFrameworkCore
   dotnet add package Pomelo.EntityFrameworkCore.MySql
   dotnet add package System.Configuration.ConfigurationManager
   ```

## 🚀 Running the Application

### Build the Project

Compile the project to ensure all dependencies are correctly installed.

### Execute the Application

Run the application to start crawling product data from Lazada and save it to your database.

## 🔧 Customization Options

- **Crawling Pages**: Modify the `totalPages` variable in `Program.cs` to control how many pages are crawled.
- **Target URL**: Adjust the `GenerateUrl` method in `ProductCrawler.cs` to change the store or product category being crawled.

## 🛠 Troubleshooting Tips

- **WebDriver Issues**: Ensure that the ChromeDriver version is compatible with your installed version of Chrome.
- **Database Connection Problems**: Double-check your connection string and confirm that the MySQL server is running.
- **Performance**: Fine-tune the scroll duration and sleep times in the `NavigateToUrl` method to optimize crawling speed and avoid rate limiting.

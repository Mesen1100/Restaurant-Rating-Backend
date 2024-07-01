# Restaurant Rating Backend

## Overview

Welcome to our Restaurant Rating App Backend! This is part of our Software Engineering course project, where we developed a backend system to manage restaurant ratings and reviews. I worked on the backend using .NET Core.

## Features

- **User Management:** Register and authenticate users.
- **Restaurant Management:** Add, edit, and delete restaurant details.
- **Menu Management:** Add, edit, and delete menu details.
- **Food Management:** Add, edit, and delete food details.
- **Rating and Reviews:** Users can rate and review restaurants.


## Challenges Faced

- **Database Management:** Ensuring efficient data storage and retrieval.
- **API Design:** Creating a robust and scalable API structure.
- **Security:** Implementing secure authentication and authorization mechanisms.
- **Gmail Integration:** Integrating Gmail for user notifications and confirmations.
- **Blob Storage:** Managing and storing images and other large files using Blob Storage.


## Installation

Want to run this project locally? Here’s how:

1. **Clone the repository:**

    ```sh
    git clone https://github.com/Mesen1100/restaurant-rating-backend.git
    ```

2. **Navigate to the project directory:**

    ```sh
    cd restaurant-rating-backend
    ```

3. **Restore the required packages:**

    ```sh
    dotnet restore
    ```

4. **Create a `appsettings.json` file in the root of the project and add the following configuration:**

    ```json
    {
      "UseInMemoryDatabase": false,
      "ConnectionStrings": {
        "DefaultConnection": "Sql Connection String"
      },
      "Serilog": {
        "Using": [],
        "MinimumLevel": {
          "Default": "Information",
          "Override": {
            "Microsoft": "Warning",
            "System": "Warning"
          }
        },
        "WriteTo": [
          {
            "Name": "Console"
          }
        ],
        "Enrich": [
          "FromLogContext",
          "WithMachineName",
          "WithProcessId",
          "WithThreadId"
        ],
        "Properties": {
          "ApplicationName": "Serilog.WebApplication"
        }
      },
      "JWTSettings": {
        "Key": "Jwt Key ",
        "Issuer": "Jwt Issuer",
        "Audience": "Jwt Audience",
        "DurationInMinutes": Time
      },
      "BlobStorageSettings": {
        "ConnectionString": "Blob Storage Connection String",
        "BlobName": "Blob Name",
        "ContainerName": "Container Name"
      },
      "AllowedHosts": "*"
    }
    ```

5. **Update the database:**

    ```sh
    dotnet ef database update
    ```
6.**Fill Email Service Information:**

    Watch [https://www.youtube.com/watch?v=lk5dhDzfzsU] and then fill in the details in `EmailService.cs` at lines 58 and 59.
    
7. **Run the application:**

    ```sh
    dotnet run
    ```

## Usage

Once you have it up and running, the backend server will be running at `https://localhost:9001` (or `http://localhost:5000`).

## Contributing

I’m open to comments and suggestions to improve my coding skills. If you have any feedback or find any issues, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the `LICENSE` file for details.

## Contact

If you have any questions or feedback, don't hesitate to get in touch with me via [mustafaesen1100@gmail.com].


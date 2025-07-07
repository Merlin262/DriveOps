# DriveOps – Programming Exercise

This repository contains my solution to the programming exercise for the Entry-Level Software Engineer position at Volvo.

## Getting Started

Follow the steps below to run the project locally:

### 1. Clone the repository

```bash
git clone https://github.com/Merlin262/DriveOps.git
cd DriveOps
```

### 2. Authenticate with your Volvo account

You need to log in with your Volvo account in order to access required resources and permissions.

You can do this either via **Visual Studio** or using the **Azure CLI**:

```bash
az login
```

> **Important:** Access to this project has already been granted to **Heron Dantas** and **Rodrigo Marques**.  
> If anyone else needs access, please reach out to me via **Teams** or email: `joao.merlin@volvo.com`.

### 3. Run the application

Use the following command to build and run the application:

```bash
dotnet run --project DriveOps
```

Once the project is up and running, you can access the Swagger UI at:

> [https://localhost:7022/swagger/index.html](https://localhost:7022/swagger/index.html)

---

## Public Deployment

You can also access the Swagger UI through the deployed instance in Azure Container Apps at the following address:

> [https://driveops.salmonsand-fdbddf14.eastus2.azurecontainerapps.io/swagger/index.html](https://driveops.salmonsand-fdbddf14.eastus2.azurecontainerapps.io/swagger/index.html)

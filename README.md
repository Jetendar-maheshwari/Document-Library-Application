# Document Library Applicatoion

A Web application developed in ASP.Net, C# and React using Visual Studio and .NET for efficiently managing attachments.

## Overview

The Document Library Application provides a user-friendly interface for uploading multiple types of attachments and displaying a list of all uploaded attachments.

## Features

- Upload Attachment (Images, PDF, Words, Excell, Power-Point)
- List and overview of all uploaded attachments
- Download the uploaded attachment
- Share the attachment via the link
- Preview the attachment
- The application follows coding standards, incorporates design patterns, and includes comprehensive testing cases

## Installation

1. Clone the repository from GitHub
2. Open the application in the visual studio

## Run Database Schema

3. Run the database schema which is available inside the Document library application.
   Database folder - (Available DocLib/Document library application/Database/document_library.sql)

4. Change the database configuration inside the appsetting.json file, Change DefaultConnection
    - "DefaultConnection": "Server=127.0.0.1;Port=8889;Uid=root;Pwd=root;Database=document_library;"
    - change it with your database configuration.
      
## Run Application Using Visual Studio:

5. Build the application
6. Run the Application - Intially Program.cs file is executed.

## Testing NUnit tests

1. Navigate to the Project directory in your terminal.

2. To run tests use the following command:

```
dotnet test

```

This will execute unit tests for the backend repositories code.

## Technologies Used

- React Framework: React was chosen for its efficiency in building interactive user interfaces.
- Component Modularity: Component was structured into reusable pieces to foster modularity and ease of maintenance.
- Bootstrap for Responsive Design: Material UI was implemented to achieve responsive design across different devices.
- MySQL Database is used for save the data of attachment e.g: attachment path, time and some other attributes.
- Nunit test is used for robustness and reliability of the application, validating the functionality of Repository.
- ASP.Net, C# is used for the backend

## Architecture and Design Patterns

The system follows architecture and design patterns, emphasizing:
- Testability
- Extendibility

It includes:

### Service Layer and Repository Interface

The system includes a service layer and repository interface for better code organisation.

### Models (Attachment and  ShareAttachment)

Represents attachment and shares attachment entities with properties like ID, fileName and others.

### Repository Interface (IAttachmentRepository and IShareAttachmentRepository)

Defines methods to add attachments and retrieve a list, of uploaded attachments, delete attachments etc.

### Repository (AttachmentRepository and ShareAttachmentRepository,)

Implements `IAttachmentRepository`, persisting attachment data.

### Service (AttachmentService and SharedAttachmentService)

Serves as an intermediary, implementing `IAttachmentRepository` and delegating calls to `AttachmentRepository` for application logic same for Share Attachment.

### Utility (AttachmentHelper)

Some other basic functions

### Testing (RepositoryTest)

NUnit tests for `AttachmentRepository` functionality:

- Test case for the AttachmentRepository class to ensure that attachments can be added successfully.
- It verifies the AddAttachment method by mocking the logger and testing with a valid attachment object.

### Application (Document Library Application)

The main entry point where the user interacts with the Attachment Upload home page System through a web application:

- Options include uploading an attachment, listing all attachments, sharing, downloading previewing and deleting the exiting attachment.



# Document Library Applicatoion

A Web application developed in ASP.Net, C# and REACT using Visual Studio and .NET for efficiently managing attachments.

# How to Run the Application

To run the Document library application, follow these steps:

1. Open the application in the visual studo
1. Run the database schema which is avaible inside the Document library application, Database folder
2. Change the database configuration inside the appsetting.json file, Change DefaultConnection
    - "DefaultConnection": "Server=127.0.0.1;Port=8889;Uid=root;Pwd=root;Database=document_library;"
    - change it with your database configuration.
3. Build the application
4. Run the Application
5. Intially Program.cs file is execated.

# Features

- Upload Attachment
- List and overview of all uploaded attachment
- Download the uploaded attachment
- Share the attachment via link
- Preview the attachment
- The application follows coding standards, incorporates design patterns, and includes comprehensive testing cases.


### Using Visual Studio:

3. If you have Visual Studio installed, you can open the project in Visual Studio and set `Program.cs` as the startup file.
4. Build the project.
5. Run the project, and the console application will start.


## Architecture and Design Patterns

The system follows architecture and design patterns, emphasizing:
- Testability
- Extendibility

It includes:

### Service Layer and Repository Interface

For better code organization, the system includes a service layer and repository interface.

## File Structure

In the `DocLib` file structure: Document Library Application that contain

    - Clinet App that contain client side code in React inside this
        - scr folder that contain component that are user in the application
            - component
    - Controllers
    - Modes
    - Repository
    - Services
    - upload_attachments
- test
    - Contain folder RespostoryTest with AttachmentRepositoryTest


### Models (Attachment and  ShareAttachment)

Represents attachment and share attachment entities with properties like ID, fileName some others.

### Repository Interface (IAttachmentRepository and IShareAttachmentRepository)

Defines methods to add attachments and retrieve a list, of uploaded attachment, delete attachments etc.

### Repository (AttachmentRepository and ShareAttachmentRepository,)

Implements `IAttachmentRepository`, persisting attachment data.

### Service (AttachmentService and SharedAttachmentService)

Serves as an intermediary, implementing `IAttachmentRepository` and delegating calls to `AttachmentRepository` for application logic same for Share Attachment.

### Utility (AttachmentHelper)

Some other basic funtion

### Testing (RepositoryTest)

NUnit tests for `AttachmentRepository` functionality:

- Test case for the AttachmentRepository class to ensure that attachments can be added successfully.
- It verifies the AddAttachment method by mocking the logger and testing with a valid attachment object.

### Application (Document Library Application)

The main entry point where the user interacts with the Attachment Upload home page System through a web application:

- Options include upload a attachment, listing all attachment, shareing, downlaoding and preview and delete the exiting attachment.



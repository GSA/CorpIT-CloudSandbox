
# Cloud Sandbox API
This is a starter API based on the code and instructions from this Microsoft tutorial: https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-vsc

## .NET Project

The API itself is a .NET CORE 2.0 application, and should be able to run with the .NET CORE 2.0 SDK. (I don't have this installed, so I can't verify this.) The project file is here: [AccessRequestsAPI.csproj](AccessRequestsAPI.csproj).

## Building and Pushing to Cloud.gov

For cloud.gov, the manifest file is used: [manifest.yml](manifest.yml). This includes the app name.

Here is the command to push to cloud.gov (had to specify the buildpack in the push commmand because otherwise there were build errors):

`cf push -b https://github.com/cloudfoundry/dotnet-core-buildpack`

## Testing the API

Currently three endpoints have been verified to work:

### Get all Access Requests
[GET]
https://accessmanagement.app.cloud.gov/ears/v0/accessrequests/

### Get one Access Request
[GET]
https://accessmanagement.app.cloud.gov/ears/v0/accessrequests/{id}

### Create one Access Request
[POST]
https://accessmanagement.app.cloud.gov/ears/v0/accessrequests/

HTTP Payload:
`{
    "sample_field_1":"test data",
    "sample_field_2":"test data"
}`

### SOAUPUI project
A SOAPUI project for testing these endpoints is available here:
[Tools/REST-CloudSandbox-soapui-project.xml](Tools/REST-CloudSandbox-soapui-project.xml)

## Database

The data is stored in a PostgreSQL database hosted in cloud.gov.

CREATE TABLE syntax is as follows:

`
create table "AccessRequests" 
(                                                                                                                   
"Id" serial primary key,
"Sample_Field_1" varchar(20),
"Sample_Field_2" varchar(20)
);
`
## Running API locally

If you have a PostgreSQL database running locally, the API can run if you first add the appropriate LOCAL_CONNECTION_STRING environment variable.

On Mac, here is the syntax:

`export LOCAL_CONNECTION_STRING="Username={username};Password={password};Host=localhost;Port=5432;Database=accessrequestdb;Pooling=true;"`






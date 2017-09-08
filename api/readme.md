
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
https://ryan-access-request-api.app.cloud.gov/v0/accessrequests/

### Get one Access Request
[GET]
https://ryan-access-request-api.app.cloud.gov/v0/accessrequests/{id}

### Create one Access Request
[POST]
https://ryan-access-request-api.app.cloud.gov/v0/accessrequests/

HTTP Payload:
`{
    "sample_field_1":"test data",
    "sample_field_2":"test data"
}`

### SOAUPUI project
A SOAPUI project for testing these endpoints is available here:
[Tools/REST-CloudSandbox-soapui-project.xml](Tools/REST-CloudSandbox-soapui-project.xml)

## A Note About the Data

This API is using an in-memory database. So the POST command does store data that can be retrieved with the GET. However, I'm not sure how long the data lasts in memory. It definitely is wiped every time the app is restarted, if not before then.





# HelloEARS app

Demonstration app using the sample Access Request API on cloud.gov.

## Testing the Web App

### Home Page

https://helloears.app.cloud.gov/

### View All Access Requests
https://helloears.app.cloud.gov/access

### View One Access Request
https://helloears.app.cloud.gov/access/{id}

## API
Code for the API can be found in the `/api` folder of this repository.

## Run the app locally

1. Install ASP.NET Core .
+ cd into the app directory and then `src/WebApplication`
+ Run `dotnet restore`
+ Run `dotnet run`
+ Access the running app in a browser at <http://localhost:5000>

It should also work in VS 2017 with the run command.

## Building and Pushing to Cloud.gov

For cloud.gov, the manifest file is used: [manifest.yml](manifest.yml). This includes the app name.

Here is the command to push to cloud.gov (had to specify the buildpack in the push commmand because otherwise there were build errors):

`cf push -b https://github.com/cloudfoundry/dotnet-core-buildpack`

Uses the CloudFoundry ASP.NET Core buildpack:

https://github.com/cloudfoundry-incubator/dotnet-core-buildpack

# HelloEARS app

Demonstration app using the sample Acces Request API on cloud.gov.

## API
Code for the API can be found in the `/api` folder of this repository.

## Run the app locally

1. Install ASP.NET Core by following the [Getting Started][] instructions
+ cd into the app directory and then `src/WebApplication`
+ Run `dotnet restore`
+ Run `dotnet run`
+ Access the running app in a browser at <http://localhost:5000>

Uses the CloudFoundry ASP.NET Core buildpack:

https://github.com/cloudfoundry-incubator/dotnet-core-buildpack

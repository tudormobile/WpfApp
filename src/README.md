# WPF Application Container

## Build and Test
The *WpfApp* library can be built locally using the '**build.cmd**' batch file located in the root of the repository. The following actions are taken:
- *'dotnet test'* will compile a Debug build of all projects in the soluction (including sample code) and run all unit tests found in the repo.
- *'dotnet build'* is performed on the 'Release' configuration.
- *'dotnet tool update* is used to update or install **docfx**.
- *'docfx'* is used to create API documentation from code comments and static files.

You can also use Visual Studio to load the solution, build, and run unit tests.

## Github Actions
- ***'Build and Deploy'*** - Triggered whenever a push or pull request is made on the main (default) branch of the repo. This action, as the name implies, builds and deploys the library nuget package (on push), and builds and tests the code on pull request.
- ***'Publish Docs'*** - Triggered whenever a push is made on the main (default) branch. Builds documentation and publishes as a static web site to Github Pages.

## Source Code Structure

### Solution and Projects
- **WpfApp.sln** - Visual Studio solution
- **src\WpfApp.csproj** - Project file
- **src\WpfApp.APITests\WpfApp.APITests.csproj** - Unit tests for the **public** API
- **src\WpfApp.Tests\WpfApp.Test.csproj** - Unit tests covering internal members and other non-public code.

### Library and Package Version
The *major* and *minor* version numbers for the library (and nuget package) are contained in the file '***version.txt***', which is located in the root of the repo. The major version number is incremented when breaking changes are introducted, or when a significant enhancement is included. The minor version is incremented for each release.

The *build* and *revision* numbers are coded using an automation scheme that indicates the date and type of build performed. The build number is included in the nuget package version.

### Documentation
Documentation generated from code comments, along with samples and additional api information contained within the 'docs' folder. The content is partially generated via '***docfx***' tool, and deployed to Github Pages for distribution.

### Sample Code / Applications
Sample code is contained within the '***Samples/'*** folder. Some of the sample code is also included in the generated documentation.


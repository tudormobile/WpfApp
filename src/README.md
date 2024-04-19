# WPF Application Container
## Source Code Structure

### Solution and Projects
- src\WpfApp.sln - Visual Studio solution
- src\WpfApp.csproj - Project file
- src\WpfApp.APITests\WpfApp.APITests.csproj - Unit tests for the **public** API
- src\WpfApp.Tests\WpfApp.Test.csproj - Unit tests covering internal members

### Versioning
Application version follows a convention *Major.Minor.Build.Revison* where:
- Major - increments with breaking changes or significant addtions
- Minor - increments with non-breaking changes
- Build - build number coded for year and month
- Revision - revision number coded for build type and day

### Documentation
Documentation generated from code comments, along with samples and additional api information is contained within the 'docs' folder and is partially generated via 'docfx' tool.

### Sample Code / Applications
Sample code is contained within the '*Samples/'* folder. Some of the sample code is also included in the generated documentation.


dotnet test
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet build -c Release
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet tool update -g docfx
if %errorlevel% neq 0 exit /b %errorlevel%
docfx docs/docfx.json
if %errorlevel% neq 0 exit /b %errorlevel%
del docs\_site\*.ico

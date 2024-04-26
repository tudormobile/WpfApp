dotnet test --logger trx;LogFilePrefix=output --collect "Code Coverage" 
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet build -c Release
if %errorlevel% neq 0 exit /b %errorlevel%
rem dotnet tool update -g docfx
if %errorlevel% neq 0 exit /b %errorlevel%
docfx docs/docfx.json
if %errorlevel% neq 0 exit /b %errorlevel%
del docs\_site\*.ico
powershell -command compress-archive -Path src\bin\release\*.nupkg -Destination package.zip -Force
set /p ver=<src\bin\release\ver.txt
echo version=%ver%
rem dotnet tool update -g dotnet-Coverage
dotnet-coverage merge **\*.coverage --remove-input-files --output-format xml --output output.xml
rem dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"output.xml" -targetdir:"output" -reporttypes:"MarkdownSummaryGithub"

echo Test Results
powershell -command "Get-ChildItem -Recurse -Filter *.trx | ForEach-Object {get-content $_.FullName | select-string '<Counters (.+)passedButRunAborted' | ForEach-Object {$_.Matches[0].Groups[1].Value}}"

echo Code Coverage
powershell -command "get-content output\SummaryGithub.md | select-string '<summary>(.+%)</summary>' | ForEach-Object {$_.Matches[0].Groups[1].Value}"
del output.xml
del package.zip

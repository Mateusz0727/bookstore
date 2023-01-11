@echo off
set DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
set DOTNET_CLI_TELEMETRY_OPTOUT=true
cd ..\Bookshop.Data
dotnet tool restore
dotnet ef dbcontext scaffold "Server=DESKTOP-6J70549\SQLEXPRESS;Database=Bookshop;User Id=admin;Password=admin;MultipleActiveResultSets=False; Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -o Model -c BaseContext -f 
cd ..\.tools
pause

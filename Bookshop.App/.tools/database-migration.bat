@echo off
set DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
@echo off

dotnet fm list migrations -p sqlserver -c "Server=DESKTOP-6J70549\SQLEXPRESS;Database=Bookshop;User Id=admin;Password=admin;MultipleActiveResultSets=False; Encrypt=False;" -a "..\Bookshop.Migration\bin\Debug\net6.0\Bookshop.Migration.dll" --allowDirtyAssemblies
dotnet fm migrate -a "..\Bookshop.Migration\bin\Debug\net6.0\Bookshop.Migration.dll" -p sqlserver --allowDirtyAssemblies  -c "Server=DESKTOP-6J70549\SQLEXPRESS;Database=Bookshop;User Id=admin;Password=admin;MultipleActiveResultSets=False; Encrypt=False;" -V
dotnet fm list migrations -p sqlserver -c "Server=DESKTOP-6J70549\SQLEXPRESS;Database=Bookshop;User Id=admin;Password=admin;MultipleActiveResultSets=False; Encrypt=False;" -a "..\Bookshop.Migration\bin\Debug\net6.0\Bookshop.Migration.dll" --allowDirtyAssemblies

pause
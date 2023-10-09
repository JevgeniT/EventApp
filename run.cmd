echo off
echo -----"running ...."-----
dotnet ef database update 0 --project EventApp.Repository --startup-project EventApp
dotnet ef migrations remove --project EventApp.Repository --startup-project EventApp
dotnet ef migrations add Init --project EventApp.Repository --startup-project EventApp
dotnet ef database update --project EventApp.Repository --startup-project EventApp
echo -----"seems legit"-----
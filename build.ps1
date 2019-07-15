## BUILDING DOTNET
dotnet restore
Start-Sleep -s 1
dotnet build
Start-Sleep -s 1
dotnet publish

## DOCKER BUILDING

docker-compose build
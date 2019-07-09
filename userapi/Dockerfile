FROM mcr.microsoft.com/dotnet/core/aspnet:latest
 
WORKDIR /app
COPY bin/Debug/netcoreapp2.1/publish .
 
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
 
ENTRYPOINT ["dotnet", "userapi.dll"]
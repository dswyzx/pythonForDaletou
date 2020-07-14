FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY . /app

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "AutoGetDLT.dll"]
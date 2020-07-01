FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic
WORKDIR /app
COPY . /app
ENV TZ=Asia/Shanghai
ENV DEBIAN_FRONTEND=noninteractive
ENV ASPNETCORE_URLS=http://+:80

RUN apt-get update -y && apt-get install tzdata -y
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
ENTRYPOINT ["dotnet", "DotNetCoreTest.dll"]
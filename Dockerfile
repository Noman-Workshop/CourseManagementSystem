#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0
LABEL author="Noman5237"

ENV ASPNETCORE_URLS=http://*:5000
ENV DOT_NET_USE_POLLING_FILE_WATCHER=1

WORKDIR /app
EXPOSE 5000
CMD ["/bin/bash", "-c", "dotnet restore && dotnet watch run --launch-profile CourseManagementSystem"]
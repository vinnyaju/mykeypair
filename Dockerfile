FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /source

COPY *.sln . 
COPY mykeypair-api/* ./mykeypair-api/


WORKDIR /source/paste-trash-api
RUN dotnet restore
RUN dotnet publish -c Release -o /app --no-restore


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

WORKDIR /app
COPY --from=build /app ./
EXPOSE $PORT

CMD ASPNETCORE_URLS=http://*:$PORT dotnet mykeypair-api.dll
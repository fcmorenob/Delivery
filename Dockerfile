FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ENV ASPNETCORE_ENVIRONMENT=Desarrollo
WORKDIR /app

COPY *.sln ./
COPY . .

RUN dotnet restore "Delivery.sln"
RUN dotnet publish "Delivery.sln" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /publish .

ENTRYPOINT ["dotnet", "Delivery.dll"]
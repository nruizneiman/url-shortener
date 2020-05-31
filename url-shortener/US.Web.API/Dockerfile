#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["US.Web.API/US.Web.API.csproj", "US.Web.API/"]
COPY ["US.IService/US.IService.csproj", "US.IService/"]
COPY ["US.Application.Validation/US.Application.Validation.csproj", "US.Application.Validation/"]
COPY ["US.Domain/US.Domain.csproj", "US.Domain/"]
COPY ["US.Application.IoC/US.Application.IoC.csproj", "US.Application.IoC/"]
COPY ["US.Domain.Repository/US.Domain.Repository.csproj", "US.Domain.Repository/"]
COPY ["US.Domain.Base/US.Domain.Base.csproj", "US.Domain.Base/"]
COPY ["US.Infrastructure/US.Infrastructure.csproj", "US.Infrastructure/"]
COPY ["US.Application.Connection/US.Application.Connection.csproj", "US.Application.Connection/"]
COPY ["US.Infrastructure.Repository/US.Infrastructure.Repository.csproj", "US.Infrastructure.Repository/"]
COPY ["US.Infrastructure.Base/US.Infrastructure.Base.csproj", "US.Infrastructure.Base/"]
COPY ["US.Service/US.Service.csproj", "US.Service/"]
COPY ["US.Service.Mapper/US.Service.Mapper.csproj", "US.Service.Mapper/"]
RUN dotnet restore "US.Web.API/US.Web.API.csproj"
COPY . .
WORKDIR "/src/US.Web.API"
RUN dotnet build "US.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "US.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "US.Web.API.dll"]
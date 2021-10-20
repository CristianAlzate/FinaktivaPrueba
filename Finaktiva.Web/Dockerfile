#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Finaktiva.Web/Finaktiva.Web.csproj", "Finaktiva.Web/"]
COPY ["Finaktiva.Services/Finaktiva.Services.csproj", "Finaktiva.Services/"]
COPY ["Finaktiva.Repository/Finaktiva.Repository.csproj", "Finaktiva.Repository/"]
RUN dotnet restore "Finaktiva.Web/Finaktiva.Web.csproj"
COPY . .
WORKDIR "/src/Finaktiva.Web"
RUN dotnet build "Finaktiva.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Finaktiva.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finaktiva.Web.dll"]
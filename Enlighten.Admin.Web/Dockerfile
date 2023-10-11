#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Enlighten.Admin.Web/Enlighten.Admin.Web.csproj", "Enlighten.Admin.Web/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
COPY ["Enlighten.Admin.Core/Enlighten.Admin.Core.csproj", "Enlighten.Admin.Core/"]
COPY ["Enlighten.Core/Enlighten.Core.csproj", "Enlighten.Core/"]
COPY ["Enlighten.Data/Enlighten.Data.csproj", "Enlighten.Data/"]
COPY ["Enlighten.Gpt.Client/Enlighten.Gpt.Client.csproj", "Enlighten.Gpt.Client/"]
COPY ["Enlighten.Study.Core/Enlighten.Study.Core.csproj", "Enlighten.Study.Core/"]
RUN dotnet restore "Enlighten.Admin.Web/Enlighten.Admin.Web.csproj"
COPY . .
WORKDIR "/src/Enlighten.Admin.Web"
RUN dotnet build "Enlighten.Admin.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Enlighten.Admin.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enlighten.Admin.Web.dll"]
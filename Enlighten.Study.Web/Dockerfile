#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Enlighten.Study.Web/Enlighten.Study.Web.csproj", "Enlighten.Study.Web/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
COPY ["Enlighten.Gpt.Client/Enlighten.Gpt.Client.csproj", "Enlighten.Gpt.Client/"]
COPY ["Enlighten.Study.Core/Enlighten.Study.Core.csproj", "Enlighten.Study.Core/"]
COPY ["Enlighten.Core/Enlighten.Core.csproj", "Enlighten.Core/"]
COPY ["Enlighten.Data/Enlighten.Data.csproj", "Enlighten.Data/"]
RUN dotnet restore "Enlighten.Study.Web/Enlighten.Study.Web.csproj"
COPY . .
WORKDIR "/src/Enlighten.Study.Web"
RUN dotnet build "Enlighten.Study.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Enlighten.Study.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enlighten.Study.Web.dll"]
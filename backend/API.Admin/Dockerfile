FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5061

ENV ASPNETCORE_URLS=http://+:5061
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /backend

# Copia o arquivo .csproj para o diret rio de trabalho atual no cont iner
COPY backend/API.Admin/API.Admin.csproj .
COPY backend/Common.Net8/Common.Net8.csproj .

# Restaura as depend ncias do projeto
RUN dotnet restore API.Admin.csproj

# Copia o restante dos arquivos do projeto para o cont iner
COPY . .

# Define o diret rio de trabalho e constr i o projeto
WORKDIR /backend
RUN dotnet build backend/API.Admin/API.Admin.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish backend/API.Admin/API.Admin.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Admin.dll"]
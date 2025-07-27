# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar y restaurar el proyecto
COPY TiendaVirtual.Client/TiendaVirtual.Client.csproj ./TiendaVirtual.Client/
RUN dotnet restore ./TiendaVirtual.Client/TiendaVirtual.Client.csproj

# Copiar todo y publicar
COPY . .
WORKDIR /app/TiendaVirtual.Client
RUN dotnet publish -c Release -o /app/out

# Etapa final con Nginx
FROM nginx:alpine
COPY --from=build /app/out/wwwroot /usr/share/nginx/html
COPY --from=build /app/TiendaVirtual.Client/nginx.conf /etc/nginx/conf.d/default.conf
    
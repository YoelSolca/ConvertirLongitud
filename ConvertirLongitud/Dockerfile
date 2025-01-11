FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR app

# Exponer el puerto 80 para los requests HTTP
EXPOSE 5024

# Copia los archivos del proyecto
COPY ./*.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0 
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ConvertirLongitud.dll"]

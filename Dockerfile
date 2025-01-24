# Etapa de build: Usando a imagem do SDK do .NET 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Defina o diretório de trabalho no container
WORKDIR /app

# Copiar o arquivo .csproj e restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o código fonte para dentro do container
COPY . ./

# Publicar o projeto
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime: Usando a imagem do runtime do .NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# Definir o comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "Files.dll"]
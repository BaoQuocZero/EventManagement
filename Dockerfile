# Sử dụng image .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy và restore project
COPY demo_02/demo_02/*.csproj ./demo_02/
WORKDIR /app/demo_02
RUN dotnet restore

# Copy toàn bộ source code và build
COPY demo_02/ ./demo_02/
WORKDIR /app/demo_02
RUN dotnet publish -c Release -o /out

# Dùng runtime image để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Mở cổng 80 và 443
EXPOSE 80
EXPOSE 443

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "demo_02.dll"]

# Sử dụng image SDK của .NET để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Thiết lập thư mục làm việc
WORKDIR /app

# Sao chép file csproj và khôi phục phụ thuộc
COPY *.csproj ./
RUN dotnet restore

# Sao chép toàn bộ mã nguồn vào container
COPY . ./

# Build ứng dụng
RUN dotnet publish -c Release -o out

# Sử dụng image runtime của .NET để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

WORKDIR /app
COPY --from=build /app/out .

# Thiết lập port cho ứng dụng
EXPOSE 80

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "BlogBackend.dll"]

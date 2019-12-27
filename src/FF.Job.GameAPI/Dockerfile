FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY . .
EXPOSE 80
VOLUME ["/app/logs"]

ENTRYPOINT ["dotnet", "FF.Job.GameAPI.dll"]

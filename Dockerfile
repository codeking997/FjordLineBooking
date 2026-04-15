# Use runtime only (no build stage) 
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY out/ .

# 👇 ADD THIS LINE HERE
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 8080

ENTRYPOINT ["dotnet", "FjordLineBooking.dll"]
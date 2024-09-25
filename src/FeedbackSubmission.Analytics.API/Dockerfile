FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FeedbackSubmission.Analytics.API/FeedbackSubmission.Analytics.API.csproj", "src/FeedbackSubmission.Analytics.API/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "./src/FeedbackSubmission.Analytics.API/FeedbackSubmission.Analytics.API.csproj"
COPY . .
WORKDIR "/src/src/FeedbackSubmission.Analytics.API"
RUN dotnet build "./FeedbackSubmission.Analytics.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FeedbackSubmission.Analytics.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeedbackSubmission.Analytics.API.dll"]
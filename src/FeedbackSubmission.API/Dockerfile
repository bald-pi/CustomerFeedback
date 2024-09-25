FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 6000
EXPOSE 6001


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FeedbackSubmission.API/FeedbackSubmission.API.csproj", "src/FeedbackSubmission.API/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "./src/FeedbackSubmission.API/FeedbackSubmission.API.csproj"
COPY . .
WORKDIR "/src/src/FeedbackSubmission.API"
RUN dotnet build "./FeedbackSubmission.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FeedbackSubmission.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeedbackSubmission.API.dll"]
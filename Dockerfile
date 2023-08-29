# Set the base image as the .NET 7.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
WORKDIR /app
COPY . ./
RUN dotnet publish ./Kemocade.Unity.Coverage.Action/Kemocade.Unity.Coverage.Action.csproj -c Release -o out --no-self-contained

# Label the container
LABEL maintainer="Dustuu <dustuu@kemocade.com>"
LABEL repository="https://github.com/kemocade/Kemocade.Unity.Coverage.Action"
LABEL homepage="https://github.com/kemocade/Kemocade.Unity.Coverage.Action"

# Label as GitHub action
LABEL com.github.actions.name="Kemocade Unity Coverage Action"
# Limit to 160 characters
LABEL com.github.actions.description="Kemocade Unity Coverage Action"
# See branding:
# https://docs.github.com/actions/creating-actions/metadata-syntax-for-github-actions#branding
LABEL com.github.actions.icon="check"
LABEL com.github.actions.color="green"

# Relayer the .NET Runtime, anew with the build output
FROM mcr.microsoft.com/dotnet/runtime:7.0
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "/Kemocade.Unity.Coverage.Action.dll" ]
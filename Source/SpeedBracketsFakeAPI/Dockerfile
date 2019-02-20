#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016
WORKDIR /src
EXPOSE 80

COPY SpeedBracketsFakeAPI/ SpeedBracketsFakeAPI/
RUN dotnet restore SpeedBracketsFakeAPI/SpeedBracketsFakeAPI.csproj

WORKDIR /src/SpeedBracketsFakeAPI
RUN dotnet build SpeedBracketsFakeAPI.csproj -c Release -o /app

WORKDIR /app
ENTRYPOINT ["dotnet", "SpeedBracketsFakeAPI.dll"]

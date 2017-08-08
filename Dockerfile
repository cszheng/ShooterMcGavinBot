FROM microsoft/dotnet:latest

#create directory
RUN mkdir /src
WORKDIR /src
FROM microsoft/dotnet:2.0.0-sdk

#create directory and copy source
RUN mkdir /src
COPY ./src/* /src/
WORKDIR /src

ENTRYPOINT ["/bin/bash", "./build_project.sh"]
###### Run container with: 
# docker build -t cszheng/shooter-mcgavin-bot .
# docker run cszheng/shooter-mcgavin-bot
# docker run -it -v $pwd/src:/src --name=shooter-bot-container-1 cszheng/discord-shooter-bot /bin/bash
# docker start shooter-bot-container-1

###### While in the src/ShooterMcGavinBot directory, run app with:
# dotnet restore
# dotnet build
# dotnet run
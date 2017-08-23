FROM microsoft/dotnet:2.0.0-sdk

#create directory and copy source
RUN mkdir /src
COPY ./src /src
WORKDIR /src

#give permissions to scripts
RUN chmod a+x build.sh
RUN chmod a+x run.sh
RUN chmod a+x test.sh

#restore and build the dotnet packages
RUN ./build.sh

CMD ["/bin/bash", "./run.sh"]


# ***Build container with:***
# docker build -t cszheng/shooter-mcgavin-bot .

# ***Run container with:***
# powershell: 
#   docker run -e DOTNETCORE_ENVIRONMENT=$Env:DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$Env:DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot
# inactive mode without running run.sh
#   docker run -it -e DOTNETCORE_ENVIRONMENT=$Env:DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$Env:DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot /bin/bash
# non-interactive without running run.sh
#   docker run -e DOTNETCORE_ENVIRONMENT=$Env:DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$Env:DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot echo test
# -------------------------------------------------------------------------------------------------------------------------------------------------------
# bash:
#   docker run -e DOTNETCORE_ENVIRONMENT=$DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot
# inactive mode without running run.sh
#   docker run -it -e DOTNETCORE_ENVIRONMENT=$DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot /bin/bash
# non-interactive without running run.sh
#   docker run -e DOTNETCORE_ENVIRONMENT=$DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot echo test

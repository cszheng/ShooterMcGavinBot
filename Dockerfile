FROM microsoft/dotnet:2.0.0-sdk

#create directory and copy source
RUN mkdir /src
COPY ./src/* /src/
WORKDIR /src

CMD ["/bin/bash", "./BuildAndRun.sh"]


# ***Build container with:***
# docker build -t cszheng/shooter-mcgavin-bot .

# ***Run container with:***
# powershell: 
#   docker run -e DOTNETCORE_ENVIRONMENT=$Env:DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$Env:DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot
# inactive mode without running BuildAndRun.sh
#   docker run -it -e DOTNETCORE_ENVIRONMENT=$Env:DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$Env:DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot /bin/bash
# non-interactive without running BuildAndRun.sh
#   docker run -e DOTNETCORE_ENVIRONMENT=$Env:DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$Env:DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot echo test
# -------------------------------------------------------------------------------------------------------------------------------------------------------
# bash:
#   docker run -e DOTNETCORE_ENVIRONMENT=$DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot
# inactive mode without running BuildAndRun.sh
#   docker run -it -e DOTNETCORE_ENVIRONMENT=$DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot /bin/bash
# non-interactive without running BuildAndRun.sh
#   docker run -e DOTNETCORE_ENVIRONMENT=$DOTNETCORE_ENVIRONMENT -e DISCORDBOT_TOKEN=$DISCORDBOT_TOKEN cszheng/shooter-mcgavin-bot echo test

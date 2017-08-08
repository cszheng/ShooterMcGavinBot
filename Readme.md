# Shooter McGavin Discord Bot

Bot with Shooter McGavin Quotes 
Will add more to readme later...

###### Run container with: 
* docker build -t cszheng/discord-shooter-bot .
* docker run -it -v $pwd/src:/src --name=shooter-bot-container-1 cszheng/discord-shooter-bot /bin/bash
* docker start shooter-bot-container-1

###### While in the MainApp directory, run app with:
* dotnet restore
* dotnet build
* dotnet run
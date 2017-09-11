#!/bin/bash
#create a release tag for the current build
DOCKER_USER=$1
DOCKER_PASSWORD=$2
DOCKER_URL=$3
DOCKER_IMAGE=$4
RELEASE_TAG=$5

echo "REGISTERY_URL=https://[DOCKER_URL]"
REGISTERY_URL=https://$DOCKER_URL    

echo "docker login -u [DOCKER_USER] -p [DOCKER_PASSWORD] [REGISTERY_URL]"
docker login -u $DOCKER_USER -p $DOCKER_PASSWORD $REGISTERY_URL

echo "docker tag [DOCKER_USER]/[DOCKER_IMAGE] [DOCKER_USER]/[DOCKER_IMAGE]:[RELEASE_TAG]"
docker tag $DOCKER_USER/$DOCKER_IMAGE $DOCKER_USER/$DOCKER_IMAGE:$RELEASE_TAG

echo "docker push [DOCKER_USER]/[DOCKER_IMAGE]"
docker push $DOCKER_USER/$DOCKER_IMAGE

echo "docker logout"
docker logout
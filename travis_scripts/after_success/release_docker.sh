#!/bin/bash

echo "docker login -u [DOCKER_USER] -p [DOCKER_PASSWORD] [DOCKER_URL]"
docker login -u $DOCKER_USER -p $DOCKER_PASSWORD $DOCKER_URL

echo "docker tag [DOCKER_USER]/[DOCKER_IMAGE] [DOCKER_USER]/[DOCKER_IMAGE]:[RELEASE_TAG]"
docker tag $DOCKER_USER/$DOCKER_IMAGE $DOCKER_USER/$DOCKER_IMAGE:$RELEASE_TAG

echo "docker push [DOCKER_USER]/[DOCKER_IMAGE]"
docker push $DOCKER_USER/$DOCKER_IMAGE

echo "docker logout"
docker logout
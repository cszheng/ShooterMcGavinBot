#!/bin/bash
echo "docker build -t [DOCKER_USER]/[DOCKER_IMAGE] ." 
docker build -t $DOCKER_USER/$DOCKER_IMAGE .
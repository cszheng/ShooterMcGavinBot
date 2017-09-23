#!/bin/bash
gcloudssh () {
    gcloud compute ssh $GCLOUD_INSTANCE_USER@$GCLOUD_INSTANCE --zone=$GCLOUD_ZONE --ssh-key-file=gcloud-instance-key --command="$1"    
}

echo "if [ ! -d \"[HOME]/google-cloud-sdk/bin\" ]; then"
if [ ! -d "$HOME/google-cloud-sdk/bin" ]; then

    echo "rm -rf [HOME]/google-cloud-sdk"
    rm -rf $HOME/google-cloud-sdk

    echo "export CLOUDSDK_CORE_DISABLE_PROMPTS=1"
    export CLOUDSDK_CORE_DISABLE_PROMPTS=1

    echo "https://sdk.cloud.google.com | bash"
    curl https://sdk.cloud.google.com | bash

echo "fi"
fi

echo "gcloud auth activate-service-account --key-file gcloud_credentials.json"
gcloud auth activate-service-account --key-file gcloud_credentials.json

echo "gcloudssh \"sudo docker stop [DOCKER_IMAGE]-container-1\""
gcloudssh "sudo docker stop $DOCKER_IMAGE-container-1"

echo "gcloudssh \"sudo docker rm [DOCKER_IMAGE]-container-1\""
gcloudssh "sudo docker rm $DOCKER_IMAGE-container-1"

echo "gcloudssh \"sudo docker login -u [DOCKER_USER] -p [DOCKER_PASSWORD] [DOCKER_URL]\""
gcloudssh "sudo docker login -u $DOCKER_USER -p $DOCKER_PASSWORD $DOCKER_URL"

echo "gcloudssh \"sudo docker pull [DOCKER_USER]/[DOCKER_IMAGE]\""
gcloudssh "sudo docker pull $DOCKER_USER/$DOCKER_IMAGE $DOCKER_IMAGE"

echo "gcloudssh \"sudo docker run -d -e DOTNETCORE_ENVIRONMENT=[DOTNETCORE_ENVIRONMENT] [DOCKER_USER]/[DOCKER_IMAGE] --name [DOCKER_IMAGE]-container-1\""
gcloudssh "sudo docker run -d -e DOTNETCORE_ENVIRONMENT=$DOTNETCORE_ENVIRONMENT $DOCKER_USER/$DOCKER_IMAGE --name $DOCKER_IMAGE-container-1"

echo "gcloudssh \"sudo docker logout\""
gcloudssh "sudo docker logout"
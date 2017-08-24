#!/bin/bash
#create a release tag for the current build
RELEASE_TAG=$1
GIT_USER=$2
GIT_TOKEN=$3
GIT_URL=$4
GIT_BRANCH=$5
REMOTE_URL=https://$GIT_USER:$GIT_TOKEN@$GIT_URL     
git remote rm origin
git remote add origin $REMOTE_URL
git tag $RELEASE_TAG -am "Generated tag from TravisCI build $RELEASE_TAG"
git push origin $GIT_BRANCH --tags
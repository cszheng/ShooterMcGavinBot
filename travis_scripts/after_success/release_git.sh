#!/bin/bash

echo "REMOTE_URL=https://[GIT_USER]:[GIT_TOKEN]@[GIT_URL]"   
REMOTE_URL=https://$GIT_USER:$GIT_TOKEN@$GIT_URL   

echo "git remote rm origin"
git remote rm origin

echo "git remote add origin [REMOTE_URL]"
git remote add origin $REMOTE_URL

echo "git tag [RELEASE_TAG] -am \"Generated tag from TravisCI build [RELEASE_TAG]\""
git tag $RELEASE_TAG -am "Generated tag from TravisCI build $RELEASE_TAG"

echo "git push origin [GIT_BRANCH] --tags"
git push origin $GIT_BRANCH --tags
#!/bin/bash
#create a release tag for the current build
RELEASE_TAG=$RELEASE_VERSION.$TRAVIS_BUILD_NUMBER
REMOTE_URL=https://$GIT_OAUTH_USER:$GIT_OAUTH_TOKEN@$GIT_REMOTE_URL     
git remote rm origin
git remote add origin $REMOTE_URL
git tag $RELEASE_TAG -am "Generated tag from TravisCI build $RELEASE_TAG"
git push origin $TRAVIS_BRANCH --tags
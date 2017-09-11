#!/bin/bash

RELEASE_TAG=v$RELEASE_MAJOR.$RELEASE_MINOR.$RELEASE_BUGFIXES.$TRAVIS_BUILD_NUMBER

if [ "$TRAVIS_BRANCH" == "master" ] && [ "$RELEASE" == "true" ]; then 
    ./travis_script/release_git.sh $RELEASE_TAG $GIT_USER $GIT_TOKEN $GIT_URL $TRAVIS_BRANCH; 
fi
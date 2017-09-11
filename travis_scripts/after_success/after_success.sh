#!/bin/bash
echo "RELEASE_TAG=v[RELEASE_MAJOR].[RELEASE_MINOR].[RELEASE_BUGFIXES].[TRAVIS_BUILD_NUMBER]"
RELEASE_TAG=v$RELEASE_MAJOR.$RELEASE_MINOR.$RELEASE_BUGFIXES.$TRAVIS_BUILD_NUMBER

echo "if [ \"[TRAVIS_BRANCH]\" == \"master\" ] && [ \"[RELEASE]\" == \"true\" ]; then"
if [ "$TRAVIS_BRANCH" == "master" ] && [ "$RELEASE" == "true" ]; then

    echo "./travis_scripts/after_success/release_git.sh [GIT_USER] [GIT_TOKEN] [GIT_URL] [RELEASE_TAG] [TRAVIS_BRANCH];"     
    ./travis_scripts/after_success/release_git.sh $GIT_USER $GIT_TOKEN $GIT_URL $RELEASE_TAG $TRAVIS_BRANCH; 

echo "fi"
fi
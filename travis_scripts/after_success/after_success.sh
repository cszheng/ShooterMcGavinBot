#!/bin/bash
echo "export RELEASE_TAG=v[RELEASE_MAJOR].[RELEASE_MINOR].[RELEASE_BUGFIXES].[TRAVIS_BUILD_NUMBER]"
export RELEASE_TAG=v$RELEASE_MAJOR.$RELEASE_MINOR.$RELEASE_BUGFIXES.$TRAVIS_BUILD_NUMBER

echo "if [ \"[TRAVIS_BRANCH]\" == \"master\" ] && [ \"[RELEASE]\" == \"true\" ]; then"
if [ "$TRAVIS_BRANCH" == "master" ] && [ "$RELEASE" == "true" ]; then

    echo "./travis_scripts/after_success/release_git.sh"     
    ./travis_scripts/after_success/release_git.sh

    echo "./travis_scripts/after_success/release_docker.sh"
    ./travis_scripts/after_success/release_docker.sh

    echo "./travis_scripts/after_success/release_gcloud.sh"
    ./travis_scripts/after_success/release_gcloud.sh

echo "fi"
fi
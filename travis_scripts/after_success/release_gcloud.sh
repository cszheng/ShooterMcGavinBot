#!/bin/bash
echo "curl http://[GCLOUD_URL]/?token=[GCLOUD_TOKEN]&hook=[GCLOUD_HOOK]"
curl http://$GCLOUD_URL/?token=$GCLOUD_TOKEN&hook=$GCLOUD_HOOK
#!/bin/bash

echo "Unzip files..."
unzip tmp/COUCHMUSIC-v07.zip -d tmp/ 
echo "Done."

echo "Create cluster and buckets..."
/opt/couchbase/bin/couchbase-cli cluster-init -c localhost:8091 --cluster-username Administrator --cluster-password password --services data,query,index --cluster-index-ramsize 256 --cluster-ramsize 768 --cluster-name "Couchify Cluster"
/opt/couchbase/bin/couchbase-cli bucket-create -c localhost:8091 --username Administrator --password password --bucket couchmusic1 --bucket-type couchbase --bucket-ramsize 128 --bucket-replica 0 --enable-flush 1
/opt/couchbase/bin/couchbase-cli bucket-create -c localhost:8091 --username Administrator --password password --bucket couchmusic2 --bucket-type couchbase --bucket-ramsize 128 --bucket-replica 0 --enable-flush 1
/opt/couchbase/bin/couchbase-cli bucket-create -c localhost:8091 --username Administrator --password password --bucket couchmusic3 --bucket-type couchbase --bucket-ramsize 128 --bucket-replica 0 --enable-flush 1
echo "Done."

echo "Sleep for 15 seconds, in order to give time to cluster and buckets initialization..."
sleep 15
echo "Done."

echo "Create scope and collections..."
cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE SCOPE \`couchmusic2\`.couchify;"
cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.country"
cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.\`sub-region\`"
cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.userprofile"
cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.track"
cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.playlist"
echo "Done."


echo "Try to import data into collection: couchmusic1..."
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic1/couchmusic1-tracks.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic1/couchmusic1-countries.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic1/couchmusic1-playlists.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic1/couchmusic1-userprofiles.zip -t 2
echo "Done."

echo "Try to import data into collection: couchmusic2..."
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://tmp/COUCHMUSIC-v07/couchmusic2/couchmusic2-countries.json -t 2 -g %type%::%countryCode% --scope-collection-exp couchify.%type%
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://tmp/COUCHMUSIC-v07/couchmusic2/couchmusic2-tracks.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type%
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://tmp/COUCHMUSIC-v07/couchmusic2/couchmusic2-subregions.json -t 2 -g %type%::%region-number% --scope-collection-exp couchify.%type%
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://tmp/COUCHMUSIC-v07/couchmusic2/couchmusic2-userprofiles.json -t 2 -g %type%::%username% --scope-collection-exp couchify.%type%
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://tmp/COUCHMUSIC-v07/couchmusic2/couchmusic2-playlists-1.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type%
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://tmp/COUCHMUSIC-v07/couchmusic2/couchmusic2-playlists-2.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type%
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://tmp/COUCHMUSIC-v07/couchmusic2/couchmusic2-playlists-3.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type%
echo "Done."

echo "Try to import data into collection: couchmusic3..."
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-countries.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-subregions.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-tracks.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-userprofiles.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-playlists-1.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-playlists-2.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-playlists-3.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://tmp/COUCHMUSIC-v07/couchmusic3/couchmusic3-playlists-4.zip -t 2
echo "Done."

echo "Create user..."
couchbase-cli user-manage -c 127.0.0.1:8091 -u Administrator -p password --set --rbac-username cbuser --rbac-password 123456 --roles bucket_full_access[couchmusic1],bucket_full_access[couchmusic2],bucket_full_access[couchmusic3] --auth-domain local
echo "Done."

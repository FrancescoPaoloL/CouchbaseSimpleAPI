#!/bin/bash

# enter in bash mode
docker exec -t -i a6bab94331ba /bin/bash


# create cluster and buckets
/opt/couchbase/bin/couchbase-cli cluster-init -c localhost:8091 --cluster-username Administrator --cluster-password password --services data,query,index --cluster-index-ramsize 256 --cluster-ramsize 768 --cluster-name "Couchify Cluster"
/opt/couchbase/bin/couchbase-cli bucket-create -c localhost:8091 --username Administrator --password password --bucket couchmusic1 --bucket-type couchbase --bucket-ramsize 128 --bucket-replica 0 --enable-flush 1
/opt/couchbase/bin/couchbase-cli bucket-create -c localhost:8091 --username Administrator --password password --bucket couchmusic2 --bucket-type couchbase --bucket-ramsize 128 --bucket-replica 0 --enable-flush 1
/opt/couchbase/bin/couchbase-cli bucket-create -c localhost:8091 --username Administrator --password password --bucket couchmusic3 --bucket-type couchbase --bucket-ramsize 128 --bucket-replica 0 --enable-flush 1

# create scope and collections
./cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE SCOPE \`couchmusic2\`.couchify;"
./cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.country"
./cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.\`sub-region\`"
./cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.userprofile"
./cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.track"
./cbq -u Administrator -p password -e "http://localhost:8091" --script="CREATE COLLECTION \`couchmusic2\`.couchify.playlist"

# try to import data into collection: couchmusic1
cd /tmp/COUCHMUSIC-v07/couchmusic1
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://couchmusic1-tracks.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://couchmusic1-countries.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://couchmusic1-playlists.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic1 -f sample -d file://couchmusic1-userprofiles.zip -t 2

# try to import data into collection: couchmusic2
cd /tmp/COUCHMUSIC-v07/couchmusic2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://couchmusic2-countries.json -t 2 -g %type%::%countryCode% --scope-collection-exp couchify.%type%
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://couchmusic2-tracks.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type% \
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://couchmusic2-subregions.json -t 2 -g %type%::%region-number% --scope-collection-exp couchify.%type% \
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://couchmusic2-userprofiles.json -t 2 -g %type%::%username% --scope-collection-exp couchify.%type% \
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://couchmusic2-playlists-1.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type% \
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://couchmusic2-playlists-2.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type% \
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic2 -f list -d file://couchmusic2-playlists-3.json -t 2 -g %type%::%id% --scope-collection-exp couchify.%type%

# try to import data into collection: couchmusic3
RUN cd /tmp/COUCHMUSIC-v07/couchmusic3
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-countries.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-subregions.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-tracks.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-userprofiles.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-playlists-1.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-playlists-2.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-playlists-3.zip -t 2
cbimport json -c couchbase://127.0.0.1 -u Administrator -p password -b couchmusic3 -f sample -d file://couchmusic3-playlists-4.zip -t 2

# create user
couchbase-cli user-manage -c 127.0.0.1:8091 -u Administrator -p password --set --rbac-username cbuser --rbac-password 123456 --roles bucket_full_access[couchmusic1],bucket_full_access[couchmusic2],bucket_full_access[couchmusic3] --auth-domain local


-----------------------------------------

IN ORDER TO START A CONTAINER:
	- sudo docker container ls -a
	- sudo docker start a6bab94331ba
	- sudo docker exec -t -i a6bab94331ba /bin/bash
	[do something: copy files, create new ones etc...]
	- docker commit [containerId]
	- docker tag [newimageId] <name>*
	- sudo docker rm [oldcontainerId]
	- sudo docker run -d --name <putnamehere> -p 8091-8096:8091-8096 \
		-p 11210-11211:11210-11211 \
		<putnamehere>:latest
	- sudo docker rmi [oldimagename]


COPYING FILES FROM DOCKER CONTAINER TO LOCAL MACHINE
	(start in local machine, no container)
	sudo docker cp a6bab94331ba:/tmp/ /media/fpl/Data/Working_Directory/tmp/t (it copies the entire folder)



1. create cluster
couchbase-cli cluster-init -c localhost:8091 \
--cluster-username Administrator --cluster-password password \
--services data,query,index \
--cluster-index-ramsize 256 \
--cluster-ramsize 768 \
--cluster-name "Couchify Cluster"

	Search is 512 MB



2. create buckets
couchbase-cli bucket-create -c localhost:8091 \
--username Administrator --password password \
--bucket couchmusic1 \
--bucket-type couchbase \
--bucket-ramsize 128 \
--bucket-replica 0 \
--enable-flush 1

couchbase-cli bucket-create -c localhost:8091 \
--username Administrator --password password \
--bucket couchmusic2 \
--bucket-type couchbase \
--bucket-ramsize 128 \
--bucket-replica 0 \
--enable-flush 1

couchbase-cli bucket-create -c localhost:8091 \
--username Administrator --password password \
--bucket couchmusic3 \
--bucket-type couchbase \
--bucket-ramsize 128 \
--bucket-replica 0 \
--enable-flush 1



3. Create scope...
./cbq -u Administrator -p password -e "http://localhost:8091" \
--script="CREATE SCOPE \`couchmusic2\`.couchify;"


3.1 ... and collections
./cbq -u Administrator -p password -e "http://localhost:8091" \
--script="CREATE COLLECTION \`couchmusic2\`.couchify.country"

./cbq -u Administrator -p password -e "http://localhost:8091" \
--script="CREATE COLLECTION \`couchmusic2\`.couchify.\`sub-region\`"

./cbq -u Administrator -p password -e "http://localhost:8091" \
--script="CREATE COLLECTION \`couchmusic2\`.couchify.userprofile"

./cbq -u Administrator -p password -e "http://localhost:8091" \
--script="CREATE COLLECTION \`couchmusic2\`.couchify.track"

./cbq -u Administrator -p password -e "http://localhost:8091" \
--script="CREATE COLLECTION \`couchmusic2\`.couchify.playlist"



4.1 Load the data for the couchmusic1 bucket
cbimport json -c couchbase://127.0.0.1 \
-u Administrator -p password \
-b couchmusic1 \
-f sample \
-d file://couchmusic1-tracks.zip -t 2

cbimport json -c couchbase://127.0.0.1 \
-u Administrator -p password \
-b couchmusic1 \
-f sample \
-d file://couchmusic1-countries.zip -t 2

cbimport json -c couchbase://127.0.0.1 \
-u Administrator -p password \
-b couchmusic1 \
-f sample \
-d file://couchmusic1-playlists.zip -t 2

cbimport json -c couchbase://127.0.0.1 \
-u Administrator -p password \
-b couchmusic1 \
-f sample \
-d file://couchmusic1-userprofiles.zip -t 2


4.2 Load the data for the couchmusic2 bucket
cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic2 \
-f list \
-d file://couchmusic2-countries.json -t 2 -g %type%::%countryCode% \
--scope-collection-exp couchify.%type%

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic2 \
-f list \
-d file://couchmusic2-tracks.json -t 2 -g %type%::%id% \
--scope-collection-exp couchify.%type%

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic2 \
-f list \
-d file://couchmusic2-subregions.json -t 2 -g %type%::%region-number% \
--scope-collection-exp couchify.%type%

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic2 \
-f list \
-d file://couchmusic2-userprofiles.json -t 2 -g %type%::%username% \
--scope-collection-exp couchify.%type%

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic2 \
-f list \
-d file://couchmusic2-playlists-1.json -t 2 -g %type%::%id% \
--scope-collection-exp couchify.%type%

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic2 \
-f list \
-d file://couchmusic2-playlists-2.json -t 2 -g %type%::%id% \
--scope-collection-exp couchify.%type%

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic2 \
-f list \
-d file://couchmusic2-playlists-3.json -t 2 -g %type%::%id% \
--scope-collection-exp couchify.%type%



5. Load the data for the couchmusic3 bucket
cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-countries.zip -t 2

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-subregions.zip -t 2

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-tracks.zip -t 2

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-userprofiles.zip -t 2

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-playlists-1.zip -t 2

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-playlists-2.zip -t 2

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-playlists-3.zip -t 2

cbimport json -c couchbase://127.0.0.1 -u \
Administrator -p password \
-b couchmusic3 \
-f sample \
-d file://couchmusic3-playlists-4.zip -t 2



6. create user
couchbase-cli user-manage -c 127.0.0.1:8091 \
-u Administrator -p password \
--set \
--rbac-username cbuser --rbac-password 123456 \
--roles bucket_full_access[couchmusic1],bucket_full_access[couchmusic2],bucket_full_access[couchmusic3] \
--auth-domain local


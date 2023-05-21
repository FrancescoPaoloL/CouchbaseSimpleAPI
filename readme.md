# Couchbase Simple Web Service

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

![Couchbase](https://img.shields.io/badge/Couchbase-EA2328?style=for-the-badge&logo=couchbase&logoColor=white)

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

# Note
This simple project is based on this <a href="http://e-learning-labs.s3-website-us-west-2.amazonaws.com/cb130cs-lab-instructions/index.html">couchbase lab</a>.

## What I used
* Couchbase Client
* FluentAssertions
* Xunit

## Preliminary Setup if you want use a docker image

NB: if you under Ubuntu, most probably, you have to use **sudo** for each docker command. We have used: **Docker version: 20.10.17** and **bash version: 4.4.20**.

1. Download and run couchbase docker image
```
docker run -d --name couchmusic -p 8091-8096:8091-8096 \
-p 11210-11211:11210-11211 \
registry.gitlab.com/couchbasesamples/couchbase-training:1.5
```

3. Once container is created, take note its CONTAINER ID, in this example: *625ac7047fd3*
```
docker container ls

CONTAINER ID   IMAGE                                                         COMMAND                  CREATED          STATUS          PORTS                                                                                                                                                              NAMES
625ac7047fd3   registry.gitlab.com/couchbasesamples/couchbase-training:1.5   "/entrypoint.sh couc…"   8 seconds ago   Up 6 seconds   0.0.0.0:8091-8096->8091-8096/tcp, :::8091-8096->8091-8096/tcp, 11207/tcp, 0.0.0.0:11210-11211->11210-11211/tcp, :::11210-11211->11210-11211/tcp, 18091-18096/tcp   couchmusic
```

3. Go to *CouchbaseSimpleAPI/src* folder and copy script folder into container
```
docker cp script/ 625ac7047fd3:/script/
```

4. Enter in bash mode
```
docker exec -t -i 625ac7047fd3 /bin/bash
```

5. Run the script (Please wait! 'cause it would take a while!)
```
./script/runme.sh
```

7. Once it has terminated, press **ctrl-p and ctrl-q** and go to *couchbase web console main page*
```
http://localhost:8091/ui/index.html
```

8. Insert "Administrator" + "password" and check if all is properly created.

![Dashboard](./img/dashboard.png)


Last thing: before doing anything, wait for all the buckets to be green and available!

##  TO DO:
* Complete APIs
* Add more tests


## License
[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

<hr>

## Connect with me
<p align="left">
<a href="https://www.linkedin.com/in/francescopl/" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/linked-in-alt.svg" alt="francescopaololezza" height="20" width="30" /></a>
<a href="https://www.kaggle.com/francescopaolol" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/kaggle.svg" alt="francescopaololezza" height="20" width="30" /></a>
</p>




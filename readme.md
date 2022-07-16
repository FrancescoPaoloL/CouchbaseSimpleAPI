# Couchbase Simple Web Service

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

![Couchbase](https://img.shields.io/badge/Couchbase-EA2328?style=for-the-badge&logo=couchbase&logoColor=white)

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

# Note
This simple project is base on this <a href="http://e-learning-labs.s3-website-us-west-2.amazonaws.com/cb130cs-lab-instructions/index.html">couchbase lab</a>.

## What I used
* Couchbase Client
* FluentAssertions
* Xunit

## Preliminary Setup
1. Download and run couchbase docker image
```
docker run -d --name couchmusic -p 8091-8096:8091-8096 \
-p 11210-11211:11210-11211 \
registry.gitlab.com/couchbasesamples/couchbase-training:1.5
```

2. Once container is created, take note its Id

3. Copy script... (it copies the entire folder)
```
sudo docker cp [IdContainer]/tmp/script ./script
```

4. ... enter bash
```
sudo docker exec -t -i [IdContainer] /bin/bash
```

5. ... go to folder and run script
```
#####
```
6. ... exit crtl-p crtl-q

7. ... go to couchbase web console main page
```
	http://localhost:8091/ui/index.html
```

8. run tests



##  TO DO:
* Complete APIs
* Add more unit tests


## License
[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

<hr>

## Connect with me
<p align="left">
<a href="https://www.linkedin.com/in/francescopl/" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/linked-in-alt.svg" alt="francescopaololezza" height="20" width="30" /></a>
<a href="https://www.kaggle.com/francescopaolol" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/kaggle.svg" alt="francescopaololezza" height="20" width="30" /></a>
</p>




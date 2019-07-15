
# web-api-lab

  
### AppVeyor
[![Build status](https://img.shields.io/appveyor/ci/HumanAftrAll/web-api-lab.svg?style=flat-square)](https://ci.appveyor.com/project/HumanAftrAll/web-api-lab) [![Master status](https://img.shields.io/appveyor/ci/HumanAftrAll/web-api-lab/master.svg?style=flat-square&label=master)](https://ci.appveyor.com/project/HumanAftrAll/web-api-lab/branch/master) [![Test Status](https://img.shields.io/appveyor/tests/HumanAftrAll/web-api-lab.svg?style=flat-square)](https://ci.appveyor.com/project/HumanAftrAll/web-api-lab/branch/master/tests)
### Travis CI
[![Build Status](https://travis-ci.org/ruirizzi/web-api-lab.svg?branch=master)](https://travis-ci.org/ruirizzi/web-api-lab)

>Lab repository to test out Docker webapi develoment, testing and CI.

  

## Why?

I've created this repo as a laboratory to test how I could develop a simple container image composed of a **WebApi** and a **WebServer**. **Unit tests** and **CI** and **Redis** are a plus.

The chosen technologies where [**.NET Core**](https://dotnet.microsoft.com/) as framework and [**nginx**](http://nginx.org/) as webserver. I've also implemented [**xUnit**](https://xunit.net/) tests and [**AppVeyor**](https://www.appveyor.com/) for continuous integration and [**Redis**](https://redis.io/) as caching layer.

  

[**Docker**](https://www.docker.com/) was also used, obviously.

  

## Getting Started  
This small guide should help you run the Docker image and run tests.

### Build the Code
This WebAPI was developed in `ASPNet Core` so in order to build, publish and run tests, you must have `dotNet Core` installed on your environment. 

>Check out their [download page](https://dotnet.microsoft.com/download) and follow the installation instructions for your system.
>
After installing, open a terminal window and enter `dotnet --version`to check if the installation was successful:
![dotnet version command result](https://imgur.com/fCFooub.png)

Now you should:
```bash
dotnet restore
dotnet build
dotnet publish
```
This will allow you to correctly build the WebAPI Docker container.

### Runing the Docker Containers

In order to run the Docker image, you **must** have Docker installed in you [**Windows**](https://download.docker.com/win/stable/Docker%20for%20Windows%20Installer.exe), [**Linux**](https://docs.docker.com/install/linux/docker-ce/debian/) or [**Mac**](https://download.docker.com/mac/stable/Docker.dmg).
After installing Docker on your system and certifying that it is running, you should clone this repository and then:
```bash
~\web-api-lab\docker-compile build
~\web-api-lab\docker-compile up
```
The result should look like this:

![docker-compile up result](https://imgur.com/xzvBiF4.png)

After that is done, you can reach the ``/user`` endpoint on your ``localhost/api/user`` and get this (I'm using [Postman](https://www.getpostman.com/) to test it):

![user list response](https://imgur.com/kWENhZa.png)

You're done! You can now run `GET`, `POST`, `PUT` and `DELETE` data from/to your database though the **WebApi**.
>For more info and examples on the API usage, please visit it's [Postman Documenter Page](https://documenter.getpostman.com/view/8122691/SVSHr9nV).

### Building and publishing in a single step
Alternatively to the aforementioned building steps, you can simply run one of the scripts on the project's root folder.

>On **Windows**, run the `PowerShell` script `build.ps1`.

>On **Linux** and **Mac**, run the `bash` script `build`.

After runing one of the scripts, `docker-compose up` to start all containers.
### Runing tests
All the tests are on the ``user-webapitests`` project inside the folder with the same name.

If using Visual Studio 2017+, you can open the whole solution and run tests on the ``user-webapitests`` project:

![how to run tests from solution explorer](https://imgur.com/TiVIaTm.png)

Alternatively, you can run the tests simply by ``dotnet test`` from the project's root folder (``user-web-api``)

## Cache

### Setting Cache Data
I used **Redis** to create a caching layer that can be easily set by making a `GET` request:
>localhost/api/user/SetUsersCacheData

If successful, this will return a `status`response set to `true`:

![status = true result](https://imgur.com/SORqEac.png)

### Retrieving Cached Data

Likewise, you can retrieve cached data by making a `GET`request:
>localhost/api/user/GetUsersCacheData

If successful, this will return a `string` containing all cached data:

![user list string result](https://imgur.com/OFo4Hna.png)

To get an specific entry, you can `GET`to `GetUserCacheDataById` adding the desire `id` at the URI end:
>localhost/api/user/GetUserCacheDataById/1

The result is a `string` of the desired entry: 

![user string result](https://imgur.com/YFybD8i.png)

In case a not-existing `id` is sent, the result will be:

![id not found result](https://imgur.com/XQGfUNo.png)

### Removing Cached Data

To remove the cached data, `GET`to: 
>localhost/api/user/RemoveUsersCacheData

The expected result is a `Boolean` set to `true`:

![true result](https://imgur.com/nLiGZw7.png)

## References
-  [NGINX + ASPNet Core containers](https://www.sep.com/sep-blog/2017/02/27/nginx-reverse-proxy-to-asp-net-core-separate-docker-containers)
-  [ASPNet Core WebAPI HelloWord](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)
-  [ASPNet Core WebAPI Testing 1](https://github.com/mmacneil/ApiIntegrationTestSamples/tree/0b3f268b7f300bbf4cb7772b27836f61326850a5)
-  [ASPNet Core WebAPI Testing 2](https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api)
-  [ASPNet Core WebAPI Testing 3](https://www.c-sharpcorner.com/article/crud-operations-unit-testing-in-asp-net-core-web-api-with-xunit/)
-  [ASPNet Core WebAPI Example](http://www.mukeshkumar.net/articles/dotnetcore/crud-operation-in-asp-net-core-web-api-with-entity-framework-core)
-  [How to use AppVeyor](https://codeshare.co.uk/blog/how-to-set-up-continuous-deployment-for-mvc-and-umbraco-using-appveyor/)
-  [How to setup AppVeyor](https://www.dannyallegrezza.com/blog/2019-03-03-configuring-net-core-2-1-projects-on-appveyor/)
-  [Deploy REDIS](https://medium.com/volosoft/docker-web-farm-example-with-using-redis-haproxy-and-asp-net-core-web-api-8e3f81217fd2)

  

## License
[![badge](https://img.shields.io/github/license/ruirizzi/web-api-lab.svg?color=blue&style=popout-square)](https://github.com/ruirizzi/web-api-lab/blob/master/LICENSE)

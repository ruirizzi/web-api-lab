# web-api-lab

[![Build status](https://img.shields.io/appveyor/ci/HumanAftrAll/web-api-lab.svg?style=flat-square)](https://ci.appveyor.com/project/HumanAftrAll/web-api-lab) [![Master status](https://img.shields.io/appveyor/ci/HumanAftrAll/web-api-lab/master.svg?style=flat-square&label=master)](https://ci.appveyor.com/project/HumanAftrAll/web-api-lab/branch/master) [![Test Status](https://img.shields.io/appveyor/tests/HumanAftrAll/web-api-lab.svg?style=flat-square)](https://ci.appveyor.com/project/HumanAftrAll/web-api-lab/branch/master/tests)
>Lab repository to test out Docker webapi develoment, testing and CI.

## Why?
I've created this repo as a laboratory to test how I could develop a simple container image composed of a **WebApi** and a **WebServer**. **Unit tests** and **CI** are a plus.

The chosen technologies where [**.NET Core**](https://dotnet.microsoft.com/) as framework and [**nginx**](http://nginx.org/) as webserver. I've also implemented [**xUnit**](https://xunit.net/) tests and [**AppVeyor**](https://www.appveyor.com/) for continuous integration. 

I've also used [**Docker**](https://www.docker.com/), obviously.

## Getting Started

This small guide should help you run the Docker image and run tests.

### Runing the Docker Containers

In order to run the Docker image, you **must** have Docker installed in you [**Windows**](https://download.docker.com/win/stable/Docker%20for%20Windows%20Installer.exe), [**Linux**](https://docs.docker.com/install/linux/docker-ce/debian/) or [**Mac**](https://download.docker.com/mac/stable/Docker.dmg).

After installing Docker on your system and certifying that it is running, you should clone this repository and then:

```bash
~\web-api-lab\docker-compile build
~\web-api-lab\docker-compile up
```
The result should look like this:

![docker-compile up result](https://imgur.com/APgic2E.png)

After that is done, you can reach the ``/user`` endpoint on your ``localhost/api/user`` and get this (I'm using Postman to test it):

![enter image description here](https://i.imgur.com/3QAZfDB.png)

You're done! You can now run `GET`, `POST`, `PUT` and `DELETE` data from/to your database though the **WebApi**.

>For more info and examples on the API usage, please visit it's [Postman Documenter Page](https://documenter.getpostman.com/view/8122691/SVSHr9nV).


### Runing tests

All the tests are on the ``user-webapitests`` project inside the folder with the same name.

If using Visual Studio 2017+, you can open the whole solution and run tests on the ``user-webapitests`` project:

![how to run tests from solution explorer](https://i.imgur.com/jGGFtGL.png)

Alternatively, you can run the tests simply by ``dotnet test`` from the project's root folder (``user-web-api``)
## References
- [NGINX + ASPNet Core containers](https://www.sep.com/sep-blog/2017/02/27/nginx-reverse-proxy-to-asp-net-core-separate-docker-containers)
- [ASPNet Core WebAPI HelloWord](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)
- [ASPNet Core WebAPI Testing 1](https://github.com/mmacneil/ApiIntegrationTestSamples/tree/0b3f268b7f300bbf4cb7772b27836f61326850a5)
- [ASPNet Core WebAPI Testing 2](https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api)
- [ASPNet Core WebAPI Testing 3](https://www.c-sharpcorner.com/article/crud-operations-unit-testing-in-asp-net-core-web-api-with-xunit/)
- [ASPNet Core WebAPI Example](http://www.mukeshkumar.net/articles/dotnetcore/crud-operation-in-asp-net-core-web-api-with-entity-framework-core)
- [How to use AppVeyor](https://codeshare.co.uk/blog/how-to-set-up-continuous-deployment-for-mvc-and-umbraco-using-appveyor/)
- [How to setup AppVeyor](https://www.dannyallegrezza.com/blog/2019-03-03-configuring-net-core-2-1-projects-on-appveyor/)

## License
[![badge](https://img.shields.io/github/license/ruirizzi/web-api-lab.svg?color=blue&style=popout-square)](https://github.com/ruirizzi/web-api-lab/blob/master/LICENSE)

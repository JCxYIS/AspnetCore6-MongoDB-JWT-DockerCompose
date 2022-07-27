Asp.net core 6 + MongoDB + JWT + Docker Compose

## Setup
- Clone this project
- Modify env file in `env_mongo.env`
- Run the equivalent commands to start the program
#### Development 
Open Visual Studio and click the mf ▶️ button *(Recommended)*,
or run manully: 
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
#### Production
```
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d
```


## Volumes
Runtime data will be stored in `.runtime` folder, including
- `.runtime/db/` 

## References
- 如何在 ASP.NET Core 6 使用 Token-based 身份認證與授權 (JWT) https://blog.miniasp.com/post/2022/02/13/How-to-use-JWT-token-based-auth-in-aspnet-core-60
- Tutorial: Create a multi-container app with Docker Compose https://docs.microsoft.com/en-us/visualstudio/containers/tutorial-multicontainer?view=vs-2022
- Microservices Using ASP.NET Core, MongoDB and Docker Container https://medium.com/aspnetrun/build-catalog-microservice-using-asp-net-core-mongodb-and-docker-container-88b8fd4d5040

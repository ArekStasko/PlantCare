# PlantCare
PlantCare is a system created for clear purpose, to manage your plants in comfortable and efficient way.
It allows me to quickly check soil moisture level of individual plant or read some notes about it.
I have a couple of plants in my room so i wanted to make generic module which can be use with any plant, here we have 
the ESP32 module with soil moisture sensor, it is responsible for registering soil moisture level and communicating with 
my backend ( here you can read more about it -> [PlantCare Module](https://github.com/ArekStasko/PlantCare_Module) ). 

# PlantCare.API 
Plantcare API is written in CQRS architecture via MediatR, queries and commands logic are fully separated also on database side, i have
Read and Write database controlled over ConsistencyManager which takes care of keeping information consistent between these two databases.

ConsistencyManager keeps data consistent via RabbitMQ, if command changes state of Write Database, consistency manager will send message that 
will be consumed by proper service to update data in Read Database. 

There is also Redis caching to keep performence on good level, caching is located in repository decorators which allows me to cache data in smooth
way, by hiding implementation of repository.

API runs on docker containers, it allows me to easily update and scale my application, i run them on my home server, on which i also keep docker images 
in docker repository.

# IDP Client Package
Idp client package is responsible for authentication in my plantcare.ui app, it allows user to register and login, also it tracks token lifetime etc. 
what is more it gives me ready to use page with login/register form so i dont have to implement it on my own.

# IDP Client API package
Idp CLient API package is responsible for authentication in my Plantcare.API project and it gives me authentication middleware that checks if token sent from
plantcare.ui with request is valid.

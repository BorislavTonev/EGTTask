Prerequisites 

-VS 2019

-.net 5  

-latest Docker for windows version

Steps to  run  
1.Download and build solution
2.Open cmd and execute this  command
  docker run --name myrediscache -p 5000:6379 -d redis
  
  This command will  create a redis instance that will be used in the demo.
  Check in docker if the container is up  and running.
  
3.Open another cmd window and execute this command
 
  docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
  
  Do not close this cmd window while using the demo!
  This will create a rabbitmq instance that will be used in the demo.
  
4.After executing the previous steps successfully, open the solution in  VS2019 and setup  the 
  multiple starting projects(WebAPI and WokerApp) from  solution properties.
  
5.Open Package management console and select  WorkerApp project.
  Run this command in PMC :
  
  update-database
  
  This will set up the SQL database that will  be used to store values.
  
 6.Use postman make calls to first add new items to  redis, then make a call  to  notify the worker that there is an item added.
  
 7.Postman Sample requests are added in  the repo.
  
Implementation  includes all  the above listed  technologies.
I’ve used “Clean Architecture ” while creating the solution. WebApi  handles all the requests for adding and getting values from the redis cache. I’ve used ready  docker images for both the Redis instance and RabbitMQ. AddItemsAsync endpoint is used to  add new items to  Redis cache and the GetItemAsync endpoint is used t o extract them by  passing the correct  cache key  value. NotifyWorker is used to notify  the Worker app that a value is added to  the cache. Redis cache key is required here also.

The Worker app is responsible for handling the process of consuming the notification  from  rabbitmq and inserting the values in the SQL database.In this project for consuming the MQ notifications, MassTransit.RabitMQ is used.

The database side is Code first approach, and the creation of the db is handled by  migration scripts.

Application and Domain project are shared between the API  and Worker projects.They are responsible for storing entities and application logic implementation.

 

Prerequisites:
-VS 2019
-.net 5  
- latest Docker for windows version

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
  
4. After executing the previous steps successfully, open the solution in  VS2019 and setup  the 
  multiple starting projects(WebAPI and WokerApp) from  solution properties.
  
5.Open Package management console and select  WorkerApp project.
  Run this command in PMC :
  
  update-database
  
  This will set up the SQL database that will  be used to store values.
  
 6.Use postman make calls to first add new items to  redis, then make a call  to  notify the worker that there is an item added.
  
  Postman Sample requests are added in  the repo

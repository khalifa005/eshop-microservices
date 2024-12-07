# eshop-microservices

postgres ports issue
https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/learn/lecture/42551780#questions/21733428


we used vs to create catalog api docker file - then to override docker compose

to override the app settings .json
      - ConnectionStrings__Database=Server=catalogdb;Port=5432;Database=CatalogDb;User Id=postgres;Password=postgres;Include Error Detail=true

containers on the same network can comunicates by  their names 
depends_on:
  - catalogdb



  https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/learn/lecture/42551956#questions/21852622

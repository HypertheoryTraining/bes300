# Project Info

This is a simple API - it uses some logging (probably can leave the serilog stuff out) and a simple thing for connecting to a database.

It also demonstrates some of using *Autmapper* and, in particular, the **QueryableExtensions** and `ProjectTo<T>`



## Build the docker image:

```
docker build -t shopping:latest -f "SimpleAPI\Dockerfile" .
```

Running the docker image: !!THIS WILL NOT WORK CANNOT GET TO THE LOCAL MACHINE!!

```

docker run -p 8080:80 -e ConnectionStrings__shopping="server=172.17.69.145\sqlexpress;database=library;user=boss;password=Tokyo_Joe_138!!" 401c

```

## Docker-Compose

Show how to create a docker compose (included) - add the code to migrate the SQL Server stuff.

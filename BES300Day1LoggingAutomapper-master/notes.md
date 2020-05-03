Build the docker image:

```
docker build -t shopping:latest -f "SimpleAPI\Dockerfile" .
```

Running the docker image: !!THIS WILL NOT WORK CANNOT GET TO THE LOCAL MACHINE!!

```

docker run -p 8080:80 -e ConnectionStrings__shopping="server=172.17.69.145\sqlexpress;database=library;user=boss;password=Tokyo_Joe_138!!" 401c

```
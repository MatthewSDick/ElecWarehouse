docker build -t elecwarehouse-image .

docker tag elecwarehouse-image registry.heroku.com/elecwarehouse/web

docker push registry.heroku.com/elecwarehouse/web

heroku container:release web -a elecwarehouse
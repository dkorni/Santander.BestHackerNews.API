# Santander.BestHackerNews
**Santander.BestHackerNews** is the service that provides the best stories from the Firebase source https://hacker-news.firebaseio.com/v0/beststories.json. The service implements a Read-Through pattern by using Redis as a distributed cache.

**Santander.BestHackerNews** was designed in a clean architectural way.

There are 2 main interfaces in the application layer: IHackerNewsProvider and IHackerNewsLiveManager. The first is created for retrieving stories. The second is responsible for updating the data in the cache.

IHackerNewsProvider has 2 implementations on the persistence layer: *OriginHackerNewsProvider* and *RedisHackerNewsProvider*. The OriginHackerNewsProvider returns data from the origin firebase source. The RedisHackerNewsProvider returns data from the Redis cache server and it decorates OriginHackerNewsProvider.

*FirebaseHackerNewsLiveManager* is the implementation of IHackerNewsLiveManager. The main task of this class is to subscribe to Firebase source updates and rewrite the story array in the Redis when it gets updated.

The story array is stored in the cache as sorted by their score in descending order.

# How to run
## Windows
1. Clone the repository:
```
git clone https://github.com/dkorni/Santander.BestHackerNews.API
```
2. Generate certificate and configure local machine:
```
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\BestHackerNews.pfx"  -p password
dotnet dev-certs https --trust
```
3. In command prompt change the location to the root of the cloned repository and run the command:
```
docker compose up
```
4. Make HTTP get request to return 10 stories:
```
https://localhost:49206/BestStories?count=10
```

## Mac and Linux
1. Clone the repository:
```
git clone https://github.com/dkorni/Santander.BestHackerNews.API
```
2. Generate certificate and configure local machine:
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/BestHackerNews.pfx -p password
dotnet dev-certs https --trust
```
3. In command prompt change the location to the root of the cloned repository and run the command:
```
docker compose up
```
4. Make HTTP get request to return 10 stories:
```
https://localhost:49206/BestStories?count=10
```

# Performance tests
1. [Sequantial stories fetching from origin source](Screenshots/DirectSequantialReadingOfBestStories.png) ~30000 ms
2. [Parallel stories fetching from origin source](Screenshots/ParallelReadingOfBestStories.png) ~2000 ms
3. [Stories fetching from cache](Screenshots/ResponseFromCache.png) ~20 ms

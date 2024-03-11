# Santander.BestHackerNews
**Santander.BestHackerNews** is the service that provides the best stories from the Firebase source https://hacker-news.firebaseio.com/v0/beststories.json. The service implements a Cache-Aside pattern by using Redis as a distributed cache.

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
2. Delete dotnet https dev certificate if it exists:
```
dotnet dev-certs https --clean
```
3. Generate certificate and configure local machine:
```
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"  -p password
dotnet dev-certs https --trust
```
4. In command prompt change the location to the root of the cloned repository and run the command:
```
docker compose up
```
6. Make HTTP get request to return 10 stories:
```
https://localhost:49206/BestStories?count=10
```

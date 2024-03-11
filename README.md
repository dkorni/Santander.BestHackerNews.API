# Santander.BestHackerNews
**Santander.BestHackerNews** is the service that provides the best stories from the Firebase source https://hacker-news.firebaseio.com/v0/beststories.json. The service implements a Cache-Aside pattern by using Redis as a distributed cache.

**Santander.BestHackerNews** was designed in a clean architectural way.

There are 2 main interfaces in the application layer: IHackerNewsProvider and IHackerNewsLiveManager. The first is created for retrieving stories. The second is responsible for updating the data in the cache.

IHackerNewsProvider has 2 implementations on the persistence layer: *OriginHackerNewsProvider* and *RedisHackerNewsProvider*. The OriginHackerNewsProvider returns data from the origin firebase source. The RedisHackerNewsProvider returns data from the Redis cache server and it decorates OriginHackerNewsProvider.

*FirebaseHackerNewsLiveManager* is the implementation of IHackerNewsLiveManager. The main task of this class is to subscribe to Firebase source updates and rewrite the story array in the Redis when it gets updated.

The story array is stored as sorted by their score in a descending order.

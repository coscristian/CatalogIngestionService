# CatalogIngestionService

A background worker service that periodically fetches content from external providers (e.g., TMDB), maps it to internal DTOs and forwards it to an internal REST API.

### How it works (high level)
***

- The Worker (a BackgroundService) triggers a polling loop.
- For each tick, the worker delegates to an ingestion service which:
    - Calls one or more IContentProvider implementations (e.g., TmdbProvider) to fetch external content.
    - Maps provider responses into internal DTOs (Models/*).
    - Sends the mapped DTOs to the internal REST API via a typed HTTP client (e.g., InternalApiClient, which extends HttpApiClientBase).
- Common HTTP logic (base URL, auth header handling, generic Get/Post helpers) is factored into HttpApiClientBase. Concrete typed clients implement domain-specific endpoints and payloads.

### Class Diagram (In progress)
![Class Diagram](https://www.plantuml.com/plantuml/svg/hPBFJW8n4CRlVOen7iGeBs0sX3ycWec94HBFGtTS6fQMT6PNZVBkbYuirUNe762stpVVjh_ja0MzL9jINSYADWIrMNgqejGMzHeBWZdvscXY-59gTrgUYcn7MpCf3La96eJ29Oj7BQ1BPFvj6IDJj3NLZseZGVENQ276s08UtsLEsfEeLhliflQIf5NRti9iukoRAIgFOfm5dJvTju4ti41UPui1F92C-DFgqFVEwS-E4mCaZlRSjJCvp4DE9QifOKu-66CrzftQmGdX10LBLyniGRpVL4FwIAbX-EhycZnasJWC5TvLDZ_O1eDWFAHs1j8laspqZ_WVv2AM9rSRuWYd5_kP5V8MotjniDalf6Ek9kzDJcTn7JaffQxWXShuKbRTkvHzz_kTqzu1VGZPdDKm_8Ph_GC0)

***
### Important configuration keys

- ApiClientConfig:{service}:Url base URL for a client (e.g., tmdb, internal).
- ApiClientConfig:{service}:SecretKey token/secret used by a client (if applicable).
- Polling:IntervalSeconds — worker polling interval in seconds.
***
### Next steps

- Complete class diagram.
- Map Response from TMDB.
- Make REST POST Request to Internal Streambit Service. 
- Implement Docker.
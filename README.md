# Creation Sharing Platform
The API is designed to be used by a web and mobile application, allowing users to manage their creations and discover new ones, and rate their uniqueness and creativity.

## Service Overview

### Hosting
This service is designed to run in a serverless fashion, using AWS Lambda. This allows for easy scaling and management of the service.gateway
dndpoints are implemented in .NET 8 using C# and are designed to be stateless. This allows for easy scaling and management of the service. The service is designed to be highly available and scalable.

### Data Storage
This service is designed to use a PostgreSQL for data storage(WIP). Hosting is done using AWS RDS. The database is chosen for high availability and scalability.
To maintain the data, the service is meant to use Entity Framework Core. The service uses a code-first approach to create the database. This allows for easy management of the database schemas and migrations.
The data is stored in a relational format, allowing for easy querying and manipulation of data.
For storing images and other files, the service uses AWS S3. This allows for easy storage and retrieval of files.

### Monitoring
This service uses ElasticSearch, Kibana and Logstash (ELK) for monitoring and logging. This allows for easy searching and visualization of logs. To enhance the observability of the service, we use OpenTelemetry to collect and export telemetry data to the ELK stack. This allows for easy monitoring and debugging of the service.

### Versioning
This service uses semantic versioning to manage the versioning of the API. This allows for easy tracking of changes and updates to the API. The versioning is done using the URL path, allowing for easy access to different versions of the API.

### API Documentation
This service uses OpenAPI to document the API. This allows for easy generation of documentation and client libraries. The API is designed to be RESTful, allowing for easy integration with other services.
    * The endpoints handling the entities are meant to be User specific, meaning only they users who owns the Event, talk etc. should be able to access them
    * Admin API for super users, who are able to handle all the entities of the exposed APIs

### Diagrams

* [C4 Context diagram](Docs/context.md)
* [C4 Container diagram](Docs/container.md)

## Legal considerations

The service consumes personal data, which should be protected with proper user access and security measures.

### User rights (GDPR)

* Information store regarding the user (Right to Access)
  * The service exposes endpoints sufficient the collect out all information regarding the user, if they so choose
* Control of the information stored regarding the user (Right to Rectification, Right to Erasure)
  * The services provides functionality to update user data, if they user so chooses. Admins are also able to handle this data, if provided with the correct rights.
  * The service provides functionality that deletes data stored regarding the user, if they so choose.
    * Roadmap includes the proof of deletion, returning what data was deleted.
* The service does not take age into consideration
  * Consumers should make sure that users are able to consent to what data they are sending the service
  * Future implementations could segregated different age groups where children should have parental consent.

# Security

## Identity Provider

* Uses Auth0 as a Identity Provider
  * Decentralized login facilitating authorization independent of who ever uses them
  * Uses OpenID Protocol to authorize the user
    * Uses JSON Web Tokens to handle the claims between the service and Auth0
      * Helps create a stateless service, for easier scalability
      * Signed by Auth0 to prove the authenticity of the Token
      * Provides information about the user
        * Unique ID
        * Claims and Permissions
  * The service uses Auth0 to maintain roles of users, to make it easier to give them the right permissions
    * Permissions are enforced through Annotations on the endpoints. Currently not working as expected, fallback created, but missing proper validation
    * Eg. a person giving a Talk should be able to create a talk and associate it with an event, but participants should only be able to register to an event
    * Admins have special roles also divided into different roles depending on what they should administer eg. Events
* Should run with a TLS/SSL enabled connection to ensure traffic sniffers can't hijack tokens
  * This should be enforced by the host provider, in this case Azure

# Things to improve on
Currently there are no tests for the service, but it is planed to use the trophy test shape. This is to ensure that the service is tested in a way that is easy to understand and maintain. The tests should be run on every commit to ensure that the service is always in a working state. To ensure that the tests are run they should be executed during the CI/CD pipeline.

    C4Context
      title System Context diagram for Internet Banking System

        Person(CustomerA, "Creation Platform User", "A user who has made creations they want to share, or discover and rate other creations")

        Boundary(b0, "API") {
            System(SystemAPI, "Creation Sharing API hosting on AWS", "API to facilitate CRUD operations on creations, searching and more")
        }
        Boundary(b1, "Consumer of the API") {
            System_Ext(SystemConsumer, "Consumer of API (Web or mobile app)", "Frontend for sharing and finding NPU creations")
        }
        Boundary(b2, "IDP") {
            System_Ext(SystemIDP, "Identity Provider", "Auth0 for handling user authentication and authorization")
        }
        Boundary(b3, "Observability") {
            System_Ext(SystemELK, "Elasticsearch, Kibana and Logstash", "For making it easier to store logs for monitoring service health.")
        }
        Rel(CustomerA, SystemConsumer, "Uses")
        Rel(SystemConsumer, SystemAPI, "Uses")
        Rel(SystemAPI, SystemIDP, "Uses")
        Rel(SystemConsumer, SystemIDP, "Uses")
        Rel(SystemAPI, SystemELK, "Stores log messages")

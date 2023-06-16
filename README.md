# CardProductAPI

Dotnet Version 7.0

# Message Broker: RabbitMQ
**Docker img:** rabbitmq

**Url:**
http://localhost:15672/

**Default Login:**

* **user** -> guest
* **password** -> guest

When card is created a message is published to a queue. It is received in the same project (only for testing purpose)

# Authentication

Authenticate using sign-in endpoint to get the token that will be use to interact with the API.

`curl -X 'POST' \
'http://localhost:5121/api/Users/sign-in' \
-H 'accept: */*' \
-H 'Content-Type: application/json' \
-d '{
"email": "vernik23@gmail.com",
"password": "2222222"
}'`

The client must send this Bearer Token in the Authorization header on every request it makes to obtain a protected resource.

# Database Migration

dotnet ef migrations add ModelRevisionsProduct --context CardProductContext

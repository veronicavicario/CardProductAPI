# CardProductAPI

Dotnet Version 7.0

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

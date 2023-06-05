# CardProductAPI
Learning .NET with a basic API for card products! :)

Dotnet Version 7.0

Card Product API allows to create:

# Contracts:
Available endpoints:

Create:

`curl -X 'POST' \
    'http://localhost:5121/api/Contracts' \
    -H 'accept: */*' \
    -H 'Content-Type: application/json' \
    -d '{
        "contract": {
                    "type": "Credit",
                    "code": "365475347",
                    "contractNumber": "3465456",
                    "userId": 1,
                    "state": "Active",
                    "createdAt": "2023-06-05T02:59:24.588Z",
                    "country": "AR",
                    "account": "345346"
                    }
}'`

Get all contracts:

http://localhost:5121/api/Contracts

Get contract by Id and delete:

http://localhost:5121/api/Contracts/{contractId}




# Cards:

Create a card. Contract must exists

`curl -X 'POST' \
        'http://localhost:5121/api/Cards' \
        -H 'accept: */*' \
        -H 'Content-Type: application/json' \
        -d '{
            "card": {
                    "type": "Virtual",
                    "state": "Active",
                    "token": "989438759",
                    "userId": 2,
                    "code": "98798",
                    "contractId": 1,
                    "createdAt": "2023-06-05",
                    "country": "CO"
            }
    }'`

Get all the cards:
http://localhost:5121/api/Cards


Get cards by user id:
http://localhost:5121/api/Cards/userId/1

Delete:
http://localhost:5121/api/Cards/1


# Database Migration

dotnet ef migrations add ModelRevisionsProduct --context CardProductContext

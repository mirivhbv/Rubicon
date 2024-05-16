# Introduction

Rubicon - Rectangle intersecting checking app.

# Additinoal notes

Just a single endpoint served via minimal API.
The database is Postgres with PostGIS extension to ease the job on spatial indices.
Initially, I wrote a straightforward service that handled all possible edge cases. Afterward, I moved to varying "ready" solutions and ultimately opted for a Postgres solution. I added a simple, plain in-memory caching layer (I didn't want to go deep into redising here).
Note: Running a project by docker-compose directly will handle applying DB migrations - there is no need for manual intervention here.

Thank you!

# Getting Started

1. Installation process
   Clone the repo into local machine. Open terminal in the root directory.
   Run `docker compose build` and `docker compose up -d` subsequently or `docker compose up -d --build` alone.
   Now, Rubicon app is up on http://localhost:5000 port.
2. For swagger endpoint please refer to: http://localhost:5000/swagger/index.html

# Build and Test

1.  Via docker:
    Run `docker compose build` and `docker compose up -d`.
2.  Via dotnet:
    Run `dotnet run -p ./Rubicon`
3.  To validate tests, run `dotnet test ./Rubicon.Tests`

# Database / EF Core

- To add new migration run `dotnet ef migrations add Initial --project Rubicon --startup-project Rubicon` from solution directory
- To upadte `dotnet ef database update --project Rubicon --startup-project Rubicon`
- To generate idempotent script `dotnet ef migrations script --idempotent -p Rubicon -o ./sql/rubicon-api.sql`

# Contribute

Just go ahead and alter as you wish :)

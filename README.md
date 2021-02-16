# OTY Kratos Demo

## Get it running

1. Start the ORY Kratos Server

   ```bash
   docker-compose -f env/docker-compose.yaml up
   ```

2. Start the ASP.NET Core backend

   ```bash
   cd src/server
   dotnet run
   ```

3. Start the React Frontend

   ```bash
   cd src/client
   yarn start
   ```

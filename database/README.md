# SyncVR Developer Assignment - Database Migrations Tool

This folder contains a database migrations tool. In a nutshell, this is a .NET 6 console application that takes a bunch of `.sql` files (or _migrations_), checks with the database which migrations are already applied, and executes only the ones that are not yet applied.

Migrations are stored in the `src/SyncVR.Database/Migrations` folder and use the following naming conventions `YYYY-MM-DD-SEQ-[migration-title].sql`. For example, `2022-11-09-01-add-queries-table.sql`. The date format and sequence number work with text sorting, so migrations will always appear in oldest-to-newest order.

- `YYYY` - year, eg. `2022`
- `MM` - month, eg. `01` or `11`
- `DD` - day, eg. `01` or `20`
- `SEQ` - daily sequence, start from `01`; useful when there are more than 1 migrations per day

## Running the program

The migrations are applied by running the dotnet console application and providing the database config as parameters.

```shell
# change into the project folder
cd src/SyncVR.Database

# see available parameters and description
dotnet run --help

# execute migrations on a local database
# in what-if mode (does not apply migrations)
dotnet run
    --host localhost
    --port 5432
    --username postgres
    --pasword postgres
    --database demo
    --what-if
```

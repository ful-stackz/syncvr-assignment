# Backend Project - SyncVR Developer Assignment

This project contains the server logic:

- calculating fibonacci numbers
- storing and retrieving historical queries

## Controllers/FibonacciController

Exposes two endpoints - to calculate fibonacci numbers and to retrieve historical data.

- `GET /fibonacci/calculate/{position}`
  - calculate fibonacci number at `{position}` using Services/Fibonacci
  - store query for historical data using Stores/QueriesStore
  - returns the resulting query
- `GET /fibonacci/history`
  - retrieves all historical queries using Stores/QueriesStore
  - returns them

## Services/Fibonacci

Calculates Fibonacci numbers at a specified position.

1. Check if number is already calculated using Stores/FibonacciStore
2. If calculated return calculated value
3. Else calculate using an iterative approach
4. Store the calculated (position, value) pair using Stores/FibonacciStore
5. Return the calculated value

## Stores/FibonacciStore

Creates and retrieves Fibonacci entries within the database.

- `AddEntryIfNotExistsAsync(position: int, value: ulong)`
  - Checks if an entry at the given `position` exists
  - If it exists - returns the entry
  - Else adds a new entry with the given `position` and `value`
- `GetEntryByPositionAsync(position: int)`
  - Returns the entry for the given `position`
  - Or `null` if it does not exist

## Stores/QueriesStore

Creates and retrieves historical query data within the database.

- `GetQueriesAsync()`
  - Retrieves and returns all historical queries
- `AddQueryAsync()`
  - Adds a new entry for the given query

## Database/DbContext

Configures Entity Framework Core to work with PostgreSQL and sets up the entities. We configure the entities manually, because we are using custom migrations solution instead of EF migrations.

## Database/Models

Contains the models that represent table entries within the database.

- `FibonacciEntry`
  - represents a Fibonacci number at a given `position` and the `value` of that number
  - one to many relation with `FibonacciQuery`
- `FibonacciQuery`
  - represents a user query for calculating the Fibonacci number at a given `position`
  - stores the timestamp of creation, (anonymized) client id and has a many to one relation with `FibonacciEntry`
  - used to serve historical data about queries

## appsettings.json

Contains the configuration needed to run the application. More precisely, contains placeholders for the configuration values, which will be inserted during the CI/CD build steps.

In order to run locally you'll have to make a copy of this file and name it `appsettings.Development.json`. Edit the placeholders in this file with actual values and voila!

The reason for having two config files is to be able to safely commit the production `appsettings.json` with Git and have the placeholders in place for painless substitution when building in the CI/CD steps. At the same time it's easy to have a local `*.Development.*` copy that remains untouched and unpublished.

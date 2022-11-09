using CommandLine;
using DbUp;

return Parser.Default
    .ParseArguments<Options>(args)
    .MapResult(
        (Options options) => {
            var success = Migrator.ExecuteMigrations(options);
            return success ? 0 : -1;
        },
        _ => {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to parse provided options.");
            Console.ResetColor();
            return -1;
        }
    );

public record Options {
    [Option("connection-string", SetName = "conn-str", HelpText = "The database connection string.")]
    public string ConnectionString { get; init; } = string.Empty;

    [Option('h', "host", SetName = "builder", Default = "localhost", HelpText = "The host where the database is located.")]
    public string Host { get; init; } = string.Empty;

    [Option('p', "port", SetName = "builder", Default = 5432, HelpText = "The port where the database is located.")]
    public int Port { get; init; }

    [Option('u', "username", SetName = "builder", Default = "postgres", HelpText = "The username to access the database.")]
    public string Username { get; init; } = string.Empty;

    [Option("password", SetName = "builder", Default = "postgres", HelpText = "The password to access the database.")]
    public string Password { get; init; } = string.Empty;

    [Option("database", SetName = "builder", HelpText = "The name of the database to target.")]
    public string Database { get; init; } = string.Empty;

    [Option("what-if", Required = false, Default = false, HelpText = "Execute scripts and then rollback all changes.")]
    public bool WhatIf { get; init; }
}

public static class Migrator {
    public static bool ExecuteMigrations(Options options) {
        var connectionString = string.IsNullOrEmpty(options.ConnectionString) == false
            ? options.ConnectionString
            : $"Host={options.Host};Port={options.Port};Username={options.Username};Password={options.Password};Database={options.Database}";
        var dbup = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .JournalToPostgresqlTable("public", "migration_history")
            .WithScriptsEmbeddedInAssembly(
                assembly: typeof(Migrator).Assembly,
                filter: script => script.Contains("Migrations") && script.EndsWith(".sql")
            )
            .LogToConsole();

        if (options.WhatIf) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Running in WhatIf mode. Any changes will be rolled back.");
            Console.ResetColor();
            dbup = dbup.WithTransactionAlwaysRollback();
        } else {
            dbup = dbup.WithTransactionPerScript();
        }

        var upgrade = dbup.Build().PerformUpgrade();
        if (!upgrade.Successful) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(upgrade.Error);
            Console.ResetColor();
            return false;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return true;
    }
}

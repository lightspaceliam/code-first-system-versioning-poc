# .NET Core Migrations - Code First & System Versioned Temporal Tables

Whilst currently not natively supported, implementing native SQL system versioning can be accomplished with Code First Migrations. To demonstrate this concept, I have created a Person entity and migrations. The two migrations demonstrate that system versioned temporal tables will reflect changes applied to their concrete tables as the schama is modified over time. 

Migrations include:
- **{timestamp}_People.cs** Initial migration including the configuration of the system versioning table
- **{timestamp}_PeopleAge.cd** Change the Person schema to test the change is reflected in the system versioning table


Procedure for adding a new table that is backed by SYSTEM_VERSIONING:

1. Create an entity in C# 
2. Add constraints (Data Annotations / Fluent API / both)
3. Create the migration as normal
4. Configure both Up & Down methods in the migration to the extension methods:   AddSystemVersioningSupport & RemoveSystemVersioningSupport
5. Apply the migration to the database and you're done.

In the /Entities/Scripts/SeedExample.sql script, there are a couple of TSQL commands to:
1. Insert a single Person
2. Update that person
3. Query both dbo.People & History.People tables

Notice how History.People automatically populates after the initial inserted record is updated. This behavior is native to the database. No extra implementation to your data access layer is required.


## Pros & Cons

| Pros | Cons |
| --- | --- |
| No code required to handle updates to data | Entity Framework knows nothing of the system-versioning temporary tables
| There will always be accurate version data | The database will continuously persist changes, adding size to the database
| As migrations modify the schema, changes are also reflected in the system-versioning temporary tables | Historical data resides on the database. You cannot mitigate storage to a more cost effective alternative

## Installation 
This example requires:

- [SQL Server](https://www.microsoft.com/en-au/sql-server/sql-server-downloads)
- [.NET Core 5.0.6](https://dotnet.microsoft.com/download)

## Clone
Clone the repository from: 
```
git clone https://github.com/lightspaceliam/code-first-system-versioning-poc.git
```
**The connection string is configured to:**

- Server: localhost
- Database: SystemVersioningDbContext

Change if required.
## Run
1. CMD/Terminal, navigate  to ../code-first-system-versioning-poc/Entities
2. Run command:
```
dotnet ef database update
```
3. Execute the TSQ sript in ../code-first-system-versioning-poc/Entities/Scripts/SeedExample.sql

You will be able to see the data inserted into dbo.People and History.People.


### Reference:
- [Creating a system-versioned temporal table](https://docs.microsoft.com/en-us/sql/relational-databases/tables/creating-a-system-versioned-temporal-table?view=sql-server-ver15)

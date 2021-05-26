using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class MigrationBuilderExtensions
    {
        public static void AddSystemVersioningSupport(this MigrationBuilder builder, string tableName, string schema = "dbo", string historySchema = "History")
        {
            //  Add schema
            builder.Sql($@"
                IF NOT EXISTS ( SELECT  *
                                FROM    sys.schemas
                                WHERE   name = N'{historySchema}' )
	                BEGIN
		                EXEC('CREATE SCHEMA [{historySchema}]');
	                END
                ");

            //  Add appropriate Versioning columns to table.
            builder.Sql($@"
                ALTER TABLE [{schema}].[{tableName}] 
                ADD     [ValidFrom] datetime2(7) GENERATED ALWAYS AS ROW START NOT NULL
                        , [ValidTo] datetime2(7) GENERATED ALWAYS AS ROW END NOT NULL
                        , PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo]);
            ");

            //  Apply system versioning to table.
            builder.Sql($@"ALTER TABLE {schema}.{tableName} 
                SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [{historySchema}].[{tableName}]));");
        }

        public static void RemoveSystemVersioningSupport(this MigrationBuilder builder, string tableName, string schema = "dbo", string historySchema = "History")
        {
            //  Set system versioning to off.
            builder.Sql($@"
                ALTER TABLE [{schema}].[{tableName}] 
                SET     (SYSTEM_VERSIONING = OFF)
            ");

            //  Drop primary table.
            builder.Sql($@"
                DROP TABLE [{schema}].[{tableName}]
            ");

            //  Drop system versioning table.
            builder.Sql($@"
                DROP TABLE [{historySchema}].[{tableName}]
            ");

        }
    }
}

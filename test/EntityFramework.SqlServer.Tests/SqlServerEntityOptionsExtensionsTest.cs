// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data.SqlClient;
using System.Linq;
using Xunit;

namespace Microsoft.Data.Entity.SqlServer.Tests
{
    public class SqlServerEntityOptionsExtensionsTest
    {
        [Fact]
        public void Can_add_extension_with_max_batch_size()
        {
            var optionsBuilder = new EntityOptionsBuilder();
            optionsBuilder.UseSqlServer("Database=Crunchie").MaxBatchSize(123);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().Single();

            Assert.Equal(123, extension.MaxBatchSize);
        }

        [Fact]
        public void Can_add_extension_with_command_timeout()
        {
            var optionsBuilder = new EntityOptionsBuilder();
            optionsBuilder.UseSqlServer("Database=Crunchie").CommandTimeout(30);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().Single();

            Assert.Equal(30, extension.CommandTimeout);
        }

        [Fact]
        public void Can_add_extension_with_ambient_transaction_warning_suppressed()
        {
            var optionsBuilder = new EntityOptionsBuilder();
            optionsBuilder.UseSqlServer("Database=Crunchie").SuppressAmbientTransactionWarning();

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().Single();

            Assert.Equal(false, extension.ThrowOnAmbientTransaction);
        }

        [Fact]
        public void Can_add_extension_with_connection_string()
        {
            var optionsBuilder = new EntityOptionsBuilder();
            optionsBuilder.UseSqlServer("Database=Crunchie");

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().Single();

            Assert.Equal("Database=Crunchie", extension.ConnectionString);
            Assert.Null(extension.Connection);
        }

        [Fact]
        public void Can_add_extension_with_connection_string_using_generic_options()
        {
            var optionsBuilder = new EntityOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer("Database=Whisper");

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().Single();

            Assert.Equal("Database=Whisper", extension.ConnectionString);
            Assert.Null(extension.Connection);
        }

        [Fact]
        public void Can_add_extension_with_connection()
        {
            var optionsBuilder = new EntityOptionsBuilder();
            var connection = new SqlConnection();

            optionsBuilder.UseSqlServer(connection);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().Single();

            Assert.Same(connection, extension.Connection);
            Assert.Null(extension.ConnectionString);
        }

        [Fact]
        public void Can_add_extension_with_connection_using_generic_options()
        {
            var optionsBuilder = new EntityOptionsBuilder<DbContext>();
            var connection = new SqlConnection();

            optionsBuilder.UseSqlServer(connection);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().Single();

            Assert.Same(connection, extension.Connection);
            Assert.Null(extension.ConnectionString);
        }
    }
}

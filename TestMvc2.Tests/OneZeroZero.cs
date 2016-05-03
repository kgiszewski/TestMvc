using DAL;
using NUnit.Framework;
using TestMvc2.DAL;
using TestMvc2.Migrations;

namespace TestMvc2.Tests
{
    [Category("Migrations")]
    [TestFixture]
    public class OneZeroZero
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            PetaPocoUnitOfWork.ConnectionString = "testDb";

            using (var uow = new PetaPocoUnitOfWork())
            {
                //remove current tables
                if (uow.Database.DoesTableExist("Sample"))
                {
                    uow.Database.Execute(@"DROP TABLE [Sample]");
                }

                //add post v0.1.0 table
                uow.Database.Execute(@"
                    CREATE TABLE [dbo].[Sample](
	                    [id] [int] NULL,
	                    [domain] [nvarchar](max) NULL,
	                    [path] [nvarchar](max) NULL
                    )
                ");

                uow.Commit();
            }
        }

        [Test]
        public void Can_Migrate_To_One_Zero_Zero()
        {
            //assert table exists
            using (var uow = new PetaPocoUnitOfWork())
            {
                Assert.That(uow.Database.DoesTableExist("Sample"));
            }

            var migration = new TargetVersionOneZeroZeroMigration();

            migration.Excecute();

            using (var uow = new PetaPocoUnitOfWork())
            {
                Assert.That(uow.Database.DoesTableExist("Sample"));

                var sql = @"
                    SELECT CHARACTER_MAXIMUM_LENGTH
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE 
                        TABLE_NAME = 'Sample' AND 
                        COLUMN_NAME = 'domain'
                ";

                var result = uow.Database.ExecuteScalar<int>(sql);

                Assert.AreEqual(75, result);

                sql = @"
                    SELECT CHARACTER_MAXIMUM_LENGTH
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE 
                        TABLE_NAME = 'Sample' AND 
                        COLUMN_NAME = 'path'
                ";

                result = uow.Database.ExecuteScalar<int>(sql);

                Assert.AreEqual(255, result);
            }
        }
    }
}

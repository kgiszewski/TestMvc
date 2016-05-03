using DAL;
using NUnit.Framework;
using TestMvc2.DAL;
using TestMvc2.Migrations;

namespace TestMvc2.Tests
{
    [Category("Migrations")]
    [TestFixture]
    public class ZeroOneZero
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

                //add v0.0.1 table
                uow.Database.Execute(@"
                    CREATE TABLE [dbo].[sample](
	                    [id] [int] NULL,
	                    [domain] [nvarchar](max) NULL,
	                    [path] [nvarchar](max) NULL,
	                    [legacy] [nchar](10) NULL
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

            var migration = new TargetVersionZeroOneZeroMigration();

            migration.Excecute();

            using (var uow = new PetaPocoUnitOfWork())
            {
                Assert.That(uow.Database.DoesTableExist("Sample"));

                var result = uow.Database.ExecuteScalar<int?>(@"
                    SELECT object_id FROM sys.columns 
                    WHERE Name = N'legacy' AND Object_ID = Object_ID(N'sample')
                ");

                Assert.That(result == null);
            }
        }
    }
}

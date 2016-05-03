using System;
using DAL;

namespace TestMvc2.Migrations
{
    public class TargetVersionOneZeroZeroMigration : IMigration
    {
        public Version TargetVersion
        {
            get { return new Version("1.0.0"); }
        }

        public void Excecute()
        {
            using (var uow = new PetaPocoUnitOfWork())
            {
                uow.Database.Execute(@"
                    ALTER TABLE [Sample]
                    ALTER COLUMN domain NVARCHAR(75)
                ");

                uow.Database.Execute(@"
                    ALTER TABLE [Sample]
                    ALTER COLUMN path NVARCHAR(255)
                ");

                uow.Commit();
            }
        }
    }
}
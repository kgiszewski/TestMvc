using System;
using DAL;

namespace TestMvc2.Migrations
{
    public class TargetVersionZeroOneZeroMigration : IMigration
    {
        public System.Version TargetVersion
        {
            get { return new Version("0.1.0"); }
        }

        public void Excecute()
        {
            using (var uow = new PetaPocoUnitOfWork())
            {
                uow.Database.Execute(@"
                    ALTER TABLE [Sample]
                    DROP COLUMN legacy
                ");

                uow.Commit();
            }
        }
    }
}
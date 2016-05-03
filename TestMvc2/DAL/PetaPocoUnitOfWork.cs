using System;
using PetaPoco;

namespace DAL
{
    public class PetaPocoUnitOfWork : IDisposable
    {
        private readonly Transaction _petaTransaction;
        private readonly Database _database;
        public static string ConnectionString = "testMvc";

        public PetaPocoUnitOfWork(string connectionString = "")
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                ConnectionString = connectionString;
            }

            _database = new Database(ConnectionString);
            _petaTransaction = new Transaction(_database);
        }

        public void Dispose()
        {
            _petaTransaction.Dispose();
        }

        public Database Database
        {
            get { return _database; }
        }

        public void Commit()
        {
            _petaTransaction.Complete();
        }
    }
}
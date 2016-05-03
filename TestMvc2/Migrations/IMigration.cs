using System;

namespace TestMvc2.Migrations
{
    /// <summary>
    /// Class that represents a base migration.
    /// </summary>
    public interface IMigration
    {
        Version TargetVersion { get; }

        void Excecute();
    }
}
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace TestMvc2.Migrations
{
    /// <summary>
    /// Class that is used to facilitate the migrations.
    /// </summary>
    public class MigrationHelper
    {
        /// <summary>
        /// Handles the migrations by using reflection to grab instances of MigrationBase.
        /// </summary>
        /// <param name="currentVersion">The current version.</param>
        public static void HandleMigrations(Version currentVersion)
        {
            var migrations = typeof(IMigration)
                .Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IMigration)))
                .Select(t => (IMigration)Activator.CreateInstance(t))
                .OrderBy(x => x.TargetVersion);

            foreach (var migration in migrations)
            {
                if (migration.TargetVersion > currentVersion)
                {
                    try
                    {
                        migration.Excecute();
                    }
                    catch (Exception ex)
                    {
                        //log the error
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Helper that gets the DLL version.
        /// </summary>
        /// <returns></returns>
        public static string GetDllVersion()
        {
            var asm = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(asm.Location);

            return fvi.FileVersion;
        }
    }
}
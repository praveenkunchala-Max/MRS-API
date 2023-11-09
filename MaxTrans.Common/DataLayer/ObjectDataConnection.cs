using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Augadh.SecurityMonitoring.Common.DataLayer
{
    public class ObjectDataConnection
    {
        Database db;
        public string objectConnect { get; set; }
        public string ipConnect { get; set; }

        public string gkConnect { get; set; }
        public IConfiguration configuration { get; set; }

        public ObjectDataConnection(string connString)
        {
            this.objectConnect = connString;
            this.ipConnect = connString;
            this.gkConnect = connString;
        }

        public ObjectDataConnection()
        {

            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(System.IO.Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .Build();

            this.objectConnect = Microsoft
              .Extensions
              .Configuration
              .ConfigurationExtensions
              .GetConnectionString(config, "DefaultConnection");

            this.ipConnect = Microsoft
              .Extensions
              .Configuration
              .ConfigurationExtensions
              .GetConnectionString(config, "IPConnection");

            this.gkConnect = Microsoft
              .Extensions
              .Configuration
              .ConfigurationExtensions
              .GetConnectionString(config, "GKDefaultConnection");
        }

        public Database GetSecurityMonitoringDBConnection(string product)
        {
            Database objDBConnection = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(product.Equals("maxtrans") ? this.objectConnect : this.objectConnect);
            return objDBConnection;
        }
        public Database GetObjectDBConnection()
        {
            // Database objDBConnection = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(product.Equals("maxtrans") ? this.objectConnect : this.objectConnect);
            Database objDBConnection = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(this.objectConnect);
            return objDBConnection;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookpadScraping
{
	public static class DbConnectionFactory
	{
		public static DbConnection Create(string connectionName)
		{
			var connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

			var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
			var con = factory.CreateConnection();
			if (con != null) con.ConnectionString = connectionString;

			return con;
		}
	}
}

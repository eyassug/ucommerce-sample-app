using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace SampleApp.Web
{
	public partial class About : System.Web.UI.Page
	{
		/// <summary>
		/// Loads uCommerce version and the database schema version. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			string binPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
			var assemblyName = AssemblyName.GetAssemblyName(string.Format("{0}\\uCommerce.dll", binPath));
			uCommerceVersion.Text = assemblyName.Version.ToString();

			SchemaVersion.Text = GetSchemaVersion().ToString();
		}

		protected int GetSchemaVersion()
		{
			var schemaVersion = -1;
			var connectionStrings = ConfigurationManager.ConnectionStrings;
			foreach (var connectionString in connectionStrings)
			{
				if (schemaVersion != -1)
					break;

				using (var conn = new SqlConnection(connectionString.ToString()))
				{
					var cmd = new SqlCommand("SELECT SchemaVersion FROM uCommerce_SystemVersion", conn);

					try
					{
						conn.Open();
						schemaVersion = (int)cmd.ExecuteScalar();
					}
					catch (SqlException exception)
					{
						
					}
					finally
					{
						conn.Close();
					}
				}
			}
			return schemaVersion;
		}
	}
}
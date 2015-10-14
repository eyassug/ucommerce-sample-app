using System;
using System.Reflection;

namespace SampleApp.Web
{
	public partial class About : System.Web.UI.Page
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			string binPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
			var assemblyName = AssemblyName.GetAssemblyName(string.Format("{0}\\uCommerce.dll", binPath));
			Version.Text = assemblyName.Version.ToString();
		}
	}
}
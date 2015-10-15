using System;
using System.Web.UI;
using SampleApp.Extensions.Api;
using UCommerce.EntitiesV2;

namespace SampleApp.Web
{
	public partial class AboutUserControl : UserControl, INamed
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			uCommerceVersion.Text = SampleApi.uCommerceVersion();

			SchemaVersion.Text = SampleApi.SchemaVersion();
		}

		private string _name = "About";

		string INamedReadOnly.Name 
		{ 
			get { return _name; }
		}

		string INamed.Name
		{
			get { return _name; }
			set { _name = value; }
		}
	}
}
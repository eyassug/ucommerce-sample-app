using System;
using System.Web.UI;
using SampleApp.Extensions.Api;
using SampleApp.Extensions.Model;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace SampleApp.Web
{
	public partial class AboutUserControl : UserControl, INamed
	{
		private readonly TabConfiguration _configuration;

		public AboutUserControl(TabConfiguration configuration)
		{
			_configuration = configuration;
		}

		public AboutUserControl() : this(ObjectFactory.Instance.Resolve<TabConfiguration>())
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			uCommerceVersionHeader.Visible = _configuration.ShowUCommerceVersion;
			SchemaVersionHeader.Visible = _configuration.ShowShemaVersion;

			uCommerceVersion.Text = SampleApi.uCommerceVersion();

			SchemaVersion.Text = SampleApi.SchemaVersion();
		}

		private string _name = "About, INamed inferface";

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
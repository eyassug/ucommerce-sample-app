﻿using System;
using System.Web.UI;
using SampleApp.Extensions.Api;
using SampleApp.Extensions.Model;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace SampleApp.Web
{
	/// <summary>
	/// User control that controls what content to show on the tab
	/// </summary>
	/// <remarks>
	/// Implements the INamed interface so uCommerce can get the title of tab.
	///  </remarks>
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

		/// <summary>
		/// Uses the TabConfiguration to determine what to show and loads the uCommerce version and the SchemaVersion 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			uCommerceVersionHeader.Visible = _configuration.ShowUCommerceVersion;
			SchemaVersionHeader.Visible = _configuration.ShowShemaVersion;

			uCommerceVersion.Text = SampleApi.uCommerceVersion();

			SchemaVersion.Text = SampleApi.SchemaVersion();
		}

		private string _name = "About";

		/// <summary>
		/// Used the get the title of tab
		/// </summary>
		string INamedReadOnly.Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Used the get the title of tab
		/// </summary>
		string INamed.Name
		{
			get { return _name; }
			set { _name = value; }
		}
	}
}
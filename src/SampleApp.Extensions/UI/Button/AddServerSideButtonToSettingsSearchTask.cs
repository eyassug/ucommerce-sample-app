using System;
using System.Linq;
using System.Web.UI.WebControls;
using UCommerce;
using UCommerce.Infrastructure.Globalization;
using UCommerce.Pipelines;
using UCommerce.Presentation;
using UCommerce.Presentation.UI;
using UCommerce.Search.Indexers;

namespace SampleApp.Extensions.UI.Button
{
	/// <summary>
	/// Adds a Server side button to the Settings search view. 
	/// Which index everything from scratch. 
	/// </summary>
	public class AddServerSideButtonToSettingsSearchTask : IPipelineTask<SectionGroup>
	{
		private readonly ScratchIndexer _scratchIndexer;

		public AddServerSideButtonToSettingsSearchTask(ScratchIndexer scratchIndexer)
		{
			_scratchIndexer = scratchIndexer;
		}

		public PipelineExecutionResult Execute(SectionGroup subject)
		{
			if (subject.GetViewName() != Constants.UI.Pages.Settings.Search) return PipelineExecutionResult.Success;

			var section = subject.Sections.FirstOrDefault(s => s.Name == "Common" && s.OriginalName == "IndexFromScratch.ascx");

			if (section == null) return PipelineExecutionResult.Success;

			section.Menu.AddMenuButton(CreateServerSideButton());

			return PipelineExecutionResult.Success;
		}

		public ImageButton CreateServerSideButton()
		{
			var serverSideButton = new ImageButton();
			serverSideButton.ImageUrl = Resources.Images.Menu.Sort;
			serverSideButton.CausesValidation = false;

			serverSideButton.Attributes.Add("onclick", "if (confirm('Are you sure you want to index everything from scratch?')) { return true; } else return false;");

			serverSideButton.Click += IndexEverythingFromSratchMethod;
			return serverSideButton;
		}

		protected void IndexEverythingFromSratchMethod(object sender, EventArgs e)
		{
			_scratchIndexer.Index();		
		}
	}
}

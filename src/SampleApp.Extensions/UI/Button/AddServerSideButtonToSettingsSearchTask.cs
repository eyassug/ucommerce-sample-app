using System;
using System.Linq;
using System.Web.UI.WebControls;
using UCommerce.Infrastructure;
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
		private readonly IResourceManager _resourceManager;

		public AddServerSideButtonToSettingsSearchTask(ScratchIndexer scratchIndexer, IResourceManager resourceManager)
		{
			_scratchIndexer = scratchIndexer;
			_resourceManager = resourceManager;
		}

		public PipelineExecutionResult Execute(SectionGroup subject)
		{
			//Finds the right section by filtering on Name and OriginalName 
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

			var translatedConfirmText = _resourceManager.GetLocalizedText("SampleApp", "confirmScratchIndexing");
			serverSideButton.Attributes.Add("onclick", "if (confirm('" + translatedConfirmText + "')) { return true; } else return false;");

			serverSideButton.Click += IndexEverythingFromSratchMethod;
			return serverSideButton;
		}

		protected void IndexEverythingFromSratchMethod(object sender, EventArgs e)
		{
			_scratchIndexer.Index();
		}
	}
}

using System;
using System.Linq;
using System.Web.UI.WebControls;
using UCommerce.Infrastructure;
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
			serverSideButton.ToolTip = "Index everything from scratch";
			serverSideButton.Attributes.Add("onclick", "if (confirm('Are you sure you want to index everything from scratch?')) { return true; } else return false;");
			serverSideButton.Click += IndexEverythingFromSratchMethod;
			return serverSideButton;
		}

		protected void IndexEverythingFromSratchMethod(object sender, EventArgs e)
		{
			ObjectFactory.Instance.Resolve<ScratchIndexer>().Index();
		}
	}
}

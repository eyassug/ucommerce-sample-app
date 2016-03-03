#Test Article

{CODE-START:csharp /}
	public ImageButton CreateServerSideButton()
	{
		var serverSideButton = new ImageButton();
		serverSideButton.ImageUrl = Resources.Images.Menu.Sort;
		serverSideButton.CausesValidation = false;

		//The client side command which executes on right click.
		var translatedConfirmText = _resourceManager.GetLocalizedText("SampleApp", "confirmScratchIndexing");
		serverSideButton.Attributes.Add("onclick", "if (confirm('" + translatedConfirmText + "')) { return true; } else return false;");

		//The server side command which executes on right click.
		serverSideButton.Click += IndexEverythingFromSratchMethod;
		return serverSideButton;
	}
{CODE-END /}
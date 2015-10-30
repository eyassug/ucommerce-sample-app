namespace SampleApp.Extensions.Model
{
	/// <summary>
	/// This is an example on how you can create configuration component that you can configure in the castle windsors config.
	/// </summary>
	public class TabConfiguration
	{
		public bool ShowTab { get; set; }

		public bool ShowUCommerceVersion { get; set; }
		
		public bool ShowShemaVersion { get; set; }

	}
}

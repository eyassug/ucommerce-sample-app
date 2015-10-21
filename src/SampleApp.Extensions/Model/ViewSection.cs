using UCommerce.EntitiesV2.UI;

namespace SampleApp.Extensions.Model
{
	public class ViewSection : IViewSection
	{
		public string View { get; set; }
		public string ResourceKey { get; set; }
		public bool MultiLingual { get; set; }
		public bool HasSaveButton { get; set; }
		public bool HasDeleteButton { get; set; }
	}
}

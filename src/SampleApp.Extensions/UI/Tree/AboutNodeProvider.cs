using System.Collections.Generic;
using UCommerce.Tree;
using UCommerce.Tree.Impl;

namespace SampleApp.Extensions.UI.Tree
{
	/// <summary>
	/// A content provider is a child node provider for supported node types.
	/// </summary>
	public class AboutNodeProvider : ITreeContentProvider
	{
		private readonly string _nodeName;
		private readonly IList<string> _supportTypes;

		/// <summary>
		/// 
		/// </summary>
		public AboutNodeProvider(string nodeName, IList<string> supportTypes)
		{
			_nodeName = nodeName;
			_supportTypes = supportTypes;
		}

		/// <summary>
		/// Given a node type and possibly an id, the provider returnes a list of child nodes.
		/// </summary>
		/// <param name="nodeType">The type of the node.</param>
		/// <param name="id">The id of the node.</param>
		/// <returns>A list of child nodes for the node.</returns>
		public IList<ITreeNodeContent> GetChildren(string nodeType, string id)
		{
			return new List<ITreeNodeContent>
			{
				new TreeNodeContent("version", -1)
				{
					Name = _nodeName,
					HasChildren = false,
					Action = "/Apps/SampleApp/About.aspx",
					Icon = "/Apps/SampleApp/Media/about.png"
				}
			};
		}

		/// <summary>
		/// Returns true, if the provider supports a specific tree node type.
		/// </summary>
		/// <param name="nodeType">The type of the node.</param>
		/// <returns>true, if the node type is supported by the provider.</returns>
		public bool Supports(string nodeType)
		{
			return _supportTypes.Contains(nodeType);
		}
	}
}

using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	/// <summary>
	/// TextMesh entity component to hold parent unit
	/// </summary>
	[Serializable]
	public struct DisplayParentComponent : IComponentData
	{
		public Entity value;
	}
}

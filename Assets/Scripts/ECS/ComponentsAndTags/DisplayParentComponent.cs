using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct DisplayParentComponent : IComponentData
	{
		public Entity value;
	}
}

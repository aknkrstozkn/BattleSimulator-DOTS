using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct DisplayComponent : IComponentData
	{
		public Entity value;
	}
}

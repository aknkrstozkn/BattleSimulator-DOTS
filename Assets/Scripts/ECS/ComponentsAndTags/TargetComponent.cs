using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct TargetComponent : IComponentData
	{
		public Entity value;
	}
}
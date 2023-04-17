using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct AttackRangeComponent : IComponentData
	{
		public float value;
	}
}
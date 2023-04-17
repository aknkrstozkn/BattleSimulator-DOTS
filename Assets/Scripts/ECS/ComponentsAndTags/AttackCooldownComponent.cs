using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct AttackCooldownComponent : IComponentData
	{
		public float value;
		public float remainingTime;
	}
}

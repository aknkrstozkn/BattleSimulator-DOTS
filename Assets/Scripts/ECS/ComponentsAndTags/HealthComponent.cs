using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct HealthComponent : IComponentData
	{
		public int currentHealth;
	}
}
using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct AttackDamageComponent : IComponentData
	{
		public float value;
	}
}
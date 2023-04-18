using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	/// <summary>
	/// Controls delay of the attacks 
	/// </summary>
	[Serializable]
	public struct AttackCooldownComponent : IComponentData
	{
		public float value;
		public float remainingTime;
	}
}

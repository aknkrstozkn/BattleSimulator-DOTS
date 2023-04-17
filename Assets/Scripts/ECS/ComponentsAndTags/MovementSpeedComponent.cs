using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public struct MovementSpeedComponent : IComponentData
	{
		public float value;
	}
}

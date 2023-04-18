using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	/// <summary>
	/// Unit component to hold target entity
	/// </summary>
	[Serializable]
	public struct TargetComponent : IComponentData
	{
		public Entity value;
	}
}
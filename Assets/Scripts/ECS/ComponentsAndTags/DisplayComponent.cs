using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	/// <summary>
	/// A Unit component to hold TextMesh entity
	/// </summary>
	[Serializable]
	public struct DisplayComponent : IComponentData
	{
		public Entity value;
	}
}

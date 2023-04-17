using System;
using Unity.Entities;
namespace ECS.ComponentsAndTags
{
	[Serializable]
	public enum Team
	{
		Blue,
		Red
	}
	
	[Serializable]
	public struct TeamComponent : IComponentData
	{
		public Team value;
	}
}
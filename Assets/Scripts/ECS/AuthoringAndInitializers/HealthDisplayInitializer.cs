using ECS.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
namespace ECS.AuthoringAndInitializers
{
	/// <summary>
	/// Inits TextMesh entities component data
	/// </summary>
	public static class HealthDisplayInitializer
	{
		public static void Init(ref Entity entity, ref Entity targetEntity, ref EntityCommandBuffer ecb)
		{
			ecb.AddComponent(entity, new DisplayParentComponent() { value = targetEntity});
			ecb.AddComponent(entity, new Parent(){Value = targetEntity});
			ecb.AddComponent<LocalToParent>(entity);
			ecb.AddComponent<LocalToWorld>(entity);
			ecb.SetComponent(entity, new Translation() { Value = new float3(0f, 1.1f, 0f)});
			ecb.AddComponent(entity, new Rotation() {Value = Quaternion.Euler(new Vector3(90f, 0f, 0f))});
		}
	}
}

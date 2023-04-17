using ECS.ComponentsAndTags;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
namespace ECS.Systems
{
	[UpdateAfter(typeof(InitializeUnitsSystem))]
	public partial class UpdateHealthDisplaySystem : SystemBase
	{
		private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			_endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
		}
		
		protected override void OnUpdate()
		{
			var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
			NativeArray<Entity> entities = EntityManager.GetAllEntities(Allocator.TempJob); 
			Entities
				.ForEach((Entity entity, int entityInQueryIndex, TextMesh textMesh, in DisplayParentComponent parent) =>
				{
					if (!entities.Contains(parent.value))
					{
						ecb.DestroyEntity(entityInQueryIndex, entity);
						return;
					}
					textMesh.text = EntityManager.GetComponentData<HealthComponent>(parent.value).currentHealth.ToString();
				})
				.WithoutBurst()
				.Run();
		}
	}
}
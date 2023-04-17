using ECS.ComponentsAndTags;
using Unity.Entities;
namespace ECS.Systems
{
	[UpdateAfter(typeof(ECS.Systems.AttackSystem))]
	public partial class DeathSystem : SystemBase
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

			Entities
				.WithAll<HealthComponent>()
				.ForEach((Entity entity, int entityInQueryIndex, in HealthComponent health, in DisplayComponent display) =>
				{
					if (health.currentHealth <= 0)
					{
						ecb.DestroyEntity(entityInQueryIndex, entity);
						ecb.DestroyEntity(display.value.Index, display.value);
					}
				}).ScheduleParallel();

			_endSimulationEcbSystem.AddJobHandleForProducer(Dependency);
		}
	}
}
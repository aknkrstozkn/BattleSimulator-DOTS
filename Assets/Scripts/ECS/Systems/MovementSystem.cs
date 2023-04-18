using ECS.ComponentsAndTags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
namespace ECS.Systems
{
	[UpdateAfter(typeof(ECS.Systems.AssignTargetSystem))]
	public partial class MovementSystem : SystemBase
	{
		EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			Enabled = false;
			m_EndSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
			GameManager.GameStarted += OnGameStarted;
			GameManager.GameReloaded += OnGameReloaded;
		}
		private void OnGameReloaded()
		{
			Enabled = false;
		}

		private void OnGameStarted()
		{
			Enabled = true;
		}
		
		protected override void OnUpdate()
		{
			var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
			float deltaTime = Time.DeltaTime;

			var entities = EntityManager.GetAllEntities(Allocator.TempJob);
			
			NativeArray<Translation> translations = new NativeArray<Translation>(entities.Length, Allocator.TempJob);
			Entities
				.ForEach((Entity entity, in Translation translation) =>
				{
					translations[entities.IndexOf(entity)] = translation;
				}).WithoutBurst().Run();

			Entities
				.WithReadOnly(translations)
				.ForEach((Entity entity, int entityInQueryIndex, ref Translation position, in MovementSpeedComponent movementSpeed, in TargetComponent target, in AttackRangeComponent attackRange) =>
				{
					if (!entities.Contains(target.value))
					{
						return;
					}
					
					float3 targetPosition = translations[entities.IndexOf(target.value)].Value;
					targetPosition.y = position.Value.y;
					if (math.distance(targetPosition, position.Value) <= attackRange.value)
					{
						return;
					}
					
					float3 direction = math.normalize(targetPosition - position.Value);
					ecb.SetComponent(entityInQueryIndex, entity, new Translation()
					{
						Value = position.Value +  direction * movementSpeed.value * deltaTime
					});
					
				}).ScheduleParallel();
		}
	}
}
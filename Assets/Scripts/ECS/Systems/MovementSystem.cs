using ECS.ComponentsAndTags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
namespace ECS.Systems
{
	/// <summary>
	/// After AssignTargetSystems starts working this systems move units to their targets. 
	/// </summary>
	[UpdateAfter(typeof(ECS.Systems.AssignTargetSystem))]
	public partial class MovementSystem : SystemBase
	{
		EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			Enabled = false;
			_endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
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
			var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
			float deltaTime = Time.DeltaTime;

			var entities = EntityManager.GetAllEntities(Allocator.TempJob);

			Entities
				.ForEach((Entity entity, int entityInQueryIndex, in Translation position, in MovementSpeedComponent movementSpeed, in TargetComponent target, in AttackRangeComponent attackRange) =>
				{
					//Check if entity exist, different way to check if target entity is valid
					if (!entities.Contains(target.value) || !HasComponent<Translation>(target.value))
					{
						return;
					}
					
					//Check target units distance, if close stop
					float3 targetPosition = GetComponent<Translation>(target.value).Value;
					targetPosition.y = position.Value.y;
					if (math.distance(targetPosition, position.Value) <= attackRange.value)
					{
						return;
					}
					
					// If target unit is not in range, move to target
					float3 direction = math.normalize(targetPosition - position.Value);
					ecb.SetComponent(entityInQueryIndex, entity, new Translation()
					{
						Value = position.Value +  direction * movementSpeed.value * deltaTime
					});
					
				}).ScheduleParallel();
			_endSimulationEcbSystem.AddJobHandleForProducer(Dependency);
		}
	}
}
using ECS.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
namespace ECS.Systems
{
	[UpdateAfter(typeof(MovementSystem))]
	public partial class AttackSystem : SystemBase
	{
		private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			_endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{
			float deltaTime = Time.DeltaTime;
			var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
			
			Entities
				.ForEach((Entity entity, int entityInQueryIndex, ref AttackCooldownComponent attackCooldown, in TargetComponent target, in AttackRangeComponent attackRange, in AttackDamageComponent attackDamage) =>
				{
					if (HasComponent<Translation>(target.value) && HasComponent<HealthComponent>(target.value))
					{
						float3 targetPosition = GetComponent<Translation>(target.value).Value;
						float distanceToTarget = math.distance(GetComponent<Translation>(entity).Value, targetPosition);

						if (distanceToTarget <= attackRange.value)
						{
							attackCooldown.remainingTime-= deltaTime;

							if (attackCooldown.remainingTime <= 0)
							{
								attackCooldown.remainingTime = attackCooldown.value;
								int newHealth = (int)(GetComponent<HealthComponent>(target.value).currentHealth - attackDamage.value);
								ecb.SetComponent(entityInQueryIndex, target.value, new HealthComponent { currentHealth = newHealth });
							}
						}
					}
				}).ScheduleParallel();

			_endSimulationEcbSystem.AddJobHandleForProducer(Dependency);
		}
	}
}
using ECS.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
namespace ECS.Systems
{
	/// <summary>
	/// This systems handles units attacks
	/// </summary>
	[UpdateAfter(typeof(MovementSystem))]
	public partial class AttackSystem : SystemBase
	{
		private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

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
			float deltaTime = Time.DeltaTime;
			//Creating a buffer to handle multi threading
			var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
			
			Entities
				.ForEach((Entity entity, int entityInQueryIndex, ref AttackCooldownComponent attackCooldown, in TargetComponent target, in AttackRangeComponent attackRange, in AttackDamageComponent attackDamage) =>
				{
					//Basically checking if the target entity is an active Unit,
					//I used different approaches through of the project.
					if (HasComponent<Translation>(target.value) && HasComponent<HealthComponent>(target.value))
					{
						float3 targetPosition = GetComponent<Translation>(target.value).Value;
						float distanceToTarget = math.distance(GetComponent<Translation>(entity).Value, targetPosition);

						//Check if target in range
						if (distanceToTarget <= attackRange.value)
						{
							attackCooldown.remainingTime-= deltaTime;

							//Check if attack cooldown is zero
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
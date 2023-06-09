﻿using ECS.ComponentsAndTags;
using Unity.Entities;
namespace ECS.Systems
{
	/// <summary>
	/// This systems checks if an entities hp below or equal zero to destroy it.
	/// </summary>
	[UpdateAfter(typeof(ECS.Systems.AttackSystem))]
	public partial class DeathSystem : SystemBase
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
			var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

			Entities
				.WithAll<HealthComponent>()
				.ForEach((Entity entity, int entityInQueryIndex, in HealthComponent health, in DisplayComponent display) =>
				{
					if (health.currentHealth <= 0)
					{
						ecb.DestroyEntity(entityInQueryIndex, entity);
						// Destroying Health Text also
						ecb.DestroyEntity(display.value.Index, display.value);
					}
				}).ScheduleParallel();

			_endSimulationEcbSystem.AddJobHandleForProducer(Dependency);
		}
	}
}
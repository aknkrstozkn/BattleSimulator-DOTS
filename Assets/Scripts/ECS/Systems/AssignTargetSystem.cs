﻿using ECS.ComponentsAndTags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
namespace ECS.Systems
{
	/// <summary>
	/// This system matches random rival units
	/// </summary>
	[UpdateAfter(typeof(InitializeUnitsSystem))]
	public partial class AssignTargetSystem : SystemBase
	{
		protected override void OnCreate()
		{
			base.OnCreate();
			Enabled = false;
			GameManager.GameStarted += OnGameStarted;
			GameManager.GameReloaded += OnGameReloaded;
		}
		
		private void OnGameReloaded()
		{
			Enabled = false;
		}
		
		/// <summary>
		/// Before the game start we don't this system to match any unit
		/// </summary>
		private void OnGameStarted()
		{
			Enabled = true;
		}

		protected override void OnUpdate()
		{
			var random = new Random((uint)UnityEngine.Random.Range(1, int.MaxValue));
			
			EntityQuery query = EntityManager.CreateEntityQuery(typeof(TeamComponent));
			NativeArray<Entity> entities = query.ToEntityArray(Allocator.TempJob);
			
			// Create a NativeList to store entities with a specific component
			// This doesn't need to be parallel
			var redUnits = new NativeList<Entity>(Allocator.TempJob);
			var blueUnits = new NativeList<Entity>(Allocator.TempJob);
			Entities
				.ForEach((Entity entity, in TeamComponent teamComponent) =>
				{
					switch (teamComponent.value)
					{
						case Team.Blue:
							blueUnits.Add(entity);
							break;
						case Team.Red:
							redUnits.Add(entity);
							break;
						default:
							break;
					}
				}).Run();

			// Using async programming to handle random unit match
			Entities
				.WithAll<TargetComponent>()
				.WithAll<TeamComponent>()
				.WithReadOnly(redUnits)
				.WithReadOnly(blueUnits)
				.ForEach((Entity entity, int entityInQueryIndex, ref TargetComponent target, in TeamComponent team) =>
				{
					if (entities.Contains(target.value))
					{
						return;
					}

					int targetCount;
					int randomIndex;
					switch (team.value)
					{
						case Team.Blue:
							if (redUnits.Length <= 0)
							{
								return;
							}
							targetCount = redUnits.Length;
							randomIndex = random.NextInt(0, targetCount);
							target.value = redUnits[randomIndex];
							break;
						case Team.Red:
							if (blueUnits.Length <= 0)
							{
								return;
							}
							targetCount = blueUnits.Length;
							randomIndex = random.NextInt(0, targetCount);
							target.value = blueUnits[randomIndex];
							break;
						default:
							break;
					}
					
				}).ScheduleParallel();
		}
	}
}
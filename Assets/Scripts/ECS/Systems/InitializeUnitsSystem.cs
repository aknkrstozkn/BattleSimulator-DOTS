﻿using ECS.AuthoringAndInitializers;
using ECS.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
namespace ECS.Systems
{
	/// <summary>
	/// This is the core system. Other systems waits for this system.
	/// This systems generates Entities and Destroys Them.
	/// </summary>
	public partial class InitializeUnitsSystem : SystemBase
	{
		private EntityCommandBufferSystem _ecbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			_ecbSystem = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
			PrefabsToEntityConverter.PrefabsConverted += OnPrefabsConverted;
			TeamButton.TeamButtonClicked += OnTeamButtonClicked;
			GameManager.GameReloaded += OnGameReloaded;
		}

		protected override void OnUpdate()
		{
			
		}

		private void OnGameReloaded()
		{
			DestroyUnits();
			OnPrefabsConverted();
		}
		
		/// <summary>
		/// Destroys All Unit entities with their health text entities
		/// </summary>
		private void DestroyUnits()
		{
			var ecb = _ecbSystem.CreateCommandBuffer();
			Entities
				.ForEach((Entity entity, int entityInQueryIndex, in TeamComponent team, in DisplayComponent display) =>
				{
					ecb.DestroyEntity(entity);
					ecb.DestroyEntity(display.value);
				}).WithoutBurst().Run();
		}
		
		/// <summary>
		/// Destroy specified team, I used this to change teams when I clicked team buttons
		/// </summary>
		private void DestroyTeam(Team targetTeam)
		{
			var ecb = _ecbSystem.CreateCommandBuffer();
			Entities
				.ForEach((Entity entity, int entityInQueryIndex, in TeamComponent team, in DisplayComponent display) =>
				{
					if (team.value.Equals(targetTeam))
					{
						ecb.DestroyEntity(entity);
						ecb.DestroyEntity(display.value);
					}
				}).WithoutBurst().Run();
		}
		
		private void OnTeamButtonClicked(TeamData teamData)
		{
			DestroyTeam(teamData.Team);
			InstantiateTeam(teamData);
		}

		private void InstantiateTeam(TeamData teamData)
		{
			var ecb = _ecbSystem.CreateCommandBuffer();

			var instantiationEntity = PrefabsToEntityConverter.TeamsEntityDic[teamData.Team];
			var slotIndexUnitDic = teamData.GetSlotIndexUnitDic();
			for (int i = 0; i < DataManager.TeamsSpawnPoints[teamData.Team].Length; i++)
			{
				// Check if slot acquired
				if (!slotIndexUnitDic.ContainsKey(i))
				{
					continue;
				}
				var unitData = slotIndexUnitDic[i];
				unitData.Position = DataManager.TeamsSpawnPoints[teamData.Team][i];
				Entity unit = ecb.Instantiate(instantiationEntity);
				Entity unitHealthDisplay = ecb.Instantiate(PrefabsToEntityConverter.UnitHealthDisplay);
				unitData.DisplayEntity = unitHealthDisplay;
				
				UnitInitializer.Init(ref unit, ref ecb, ref unitData);
				HealthDisplayInitializer.Init(ref unitHealthDisplay, ref unit, ref ecb);	
			}
		}
		
		/// <summary>
		/// When games starts, after prefab entities instantiated
		/// Creates default(first) teams.
		/// </summary>
		private void OnPrefabsConverted()
		{
			foreach (var tEntity in PrefabsToEntityConverter.TeamsEntityDic)
			{
				var team = tEntity.Key;
				var teamData = DataManager.TeamsData[team][0];
				InstantiateTeam(teamData);
			}
		}
	}
}
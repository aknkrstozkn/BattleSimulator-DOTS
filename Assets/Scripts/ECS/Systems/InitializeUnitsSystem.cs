using ECS.AuthoringAndInitializers;
using ECS.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
namespace ECS.Systems
{
	public partial class InitializeUnitsSystem : SystemBase
	{
		private EntityCommandBufferSystem _ecbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			_ecbSystem = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
			PrefabsToEntityConverter.PrefabsConverted += OnPrefabsConverted;
		}
		
		protected override void OnUpdate()
		{
			
		}
		
		private void OnPrefabsConverted()
		{
			var ecb = _ecbSystem.CreateCommandBuffer();

			// Blue team units
			for (int i = 0; i < 10; i++)
			{
				var unitData = new Unit();
				unitData.TestInit(Team.Blue);
				//TODO get pos from config file
				unitData.Position = new float3(-5, 1, -5 + i);
				//------------*
				
				Entity unit = ecb.Instantiate(PrefabsToEntityConverter.BlueTeamUnit);
				Entity unitHealthDisplay = ecb.Instantiate(PrefabsToEntityConverter.UnitHealthDisplay);
				unitData.DisplayEntity = unitHealthDisplay;
				
				UnitInitializer.Init(ref unit, ref ecb, ref unitData);
				HealthDisplayInitializer.Init(ref unitHealthDisplay, ref unit, ref ecb);
			}

			// Red team units
			for (int i = 0; i < 10; i++)
			{
				var unitData = new Unit();
				unitData.TestInit(Team.Red);
				//TODO get pos from config file
				unitData.Position = new float3(5, 1, -5 + i);
				//------------*
				
				Entity unit = ecb.Instantiate(PrefabsToEntityConverter.RedTeamUnit);
				Entity unitHealthDisplay = ecb.Instantiate(PrefabsToEntityConverter.UnitHealthDisplay);
				unitData.DisplayEntity = unitHealthDisplay;
				
				UnitInitializer.Init(ref unit, ref ecb, ref unitData);
				HealthDisplayInitializer.Init(ref unitHealthDisplay, ref unit, ref ecb);
			}
			
			// Destroy InitializeUnits entity
			_ecbSystem.AddJobHandleForProducer(Dependency);
		}
	}
}
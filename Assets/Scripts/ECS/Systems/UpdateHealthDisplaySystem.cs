using ECS.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;
namespace ECS.Systems
{
	/// <summary>
	/// Updated health texts of units
	/// </summary>
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
			// This is not performant,
			// We are not event using BursCompiler
			// This is simply a basic for loop but...
			// If you wanna use TextMesh, we can't have parallel programming
			// at least for this part.
			Entities
				.ForEach((Entity entity, int entityInQueryIndex, TextMesh textMesh, in DisplayParentComponent parent) =>
				{
					textMesh.text = EntityManager.GetComponentData<HealthComponent>(parent.value).currentHealth.ToString();
				})
				.WithoutBurst()
				.Run();
		}
	}
}
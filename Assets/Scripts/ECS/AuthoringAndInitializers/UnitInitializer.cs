using ECS.ComponentsAndTags;
using Unity.Entities;
using Unity.Transforms;
namespace ECS.AuthoringAndInitializers
{
	/// <summary>
	/// Inits Unit entities component data
	/// </summary>
	public static class UnitInitializer
	{
		public static void Init(ref Entity entity, ref EntityCommandBuffer ecb, ref UnitData data)
		{
			ecb.AddComponent<LocalToWorld>(entity);
			
			ecb.AddComponent(entity, new Translation { Value = data.Position });
			
			ecb.AddComponent(entity, new DisplayComponent()
			{
				value = data.DisplayEntity
			});
			
			ecb.AddComponent(entity, new HealthComponent()
			{
				currentHealth = data.Health
			});
			
			ecb.AddComponent(entity, new AttackDamageComponent()
			{
				value = data.AttackDamage
			});
			
			ecb.AddComponent(entity, new AttackRangeComponent()
			{
				value = data.AttackRange
			});
			
			ecb.AddComponent(entity, new AttackCooldownComponent()
			{
				value = data.AttackCooldown,
				remainingTime = data.AttackCooldown
			});
			
			ecb.AddComponent(entity, new MovementSpeedComponent()
			{
				value = data.MovementSpeed
			});
			
			ecb.AddComponent(entity, new TeamComponent()
			{
				value = data.Team
			});

			ecb.AddComponent(entity, new TargetComponent());
		}
	}
}

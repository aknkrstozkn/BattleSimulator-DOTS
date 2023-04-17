using ECS.ComponentsAndTags;
using Unity.Entities;
namespace ECS.Systems
{
    [UpdateAfter(typeof(ECS.Systems.DeathSystem))]
    public partial class VictorySystem : SystemBase
    {
        protected override void OnUpdate()
        {
            int blueTeamCount = 0;
            int redTeamCount = 0;

            Entities
                .ForEach((in TeamComponent teamComponent) =>
                {
                    if (teamComponent.value == Team.Blue)
                    {
                        blueTeamCount++;
                    }
                    else if (teamComponent.value == Team.Red)
                    {
                        redTeamCount++;
                    }
                }).Run();

            if (blueTeamCount == 0 || redTeamCount == 0)
            {
                Team winningTeam = blueTeamCount > 0 ? Team.Blue : Team.Red;
                /*EntityManager.AddComponent<StopMovingTag>(GetSingletonEntity<TeamComponent>());*/

                // You can implement additional logic here to display a victory message or trigger other events.
            }
        }
    }
}
/*using ECS.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;
namespace ECS.Systems
{
    [UpdateAfter(typeof(ECS.Systems.DeathSystem))]
    public partial class VictorySystem : SystemBase
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
        
        private void OnGameStarted()
        {
            Enabled = true;
        }

        protected override void OnUpdate()
        {
            int blueTeamCount = 0;
            int redTeamCount = 0;

            Entities
                .WithAll<DisplayComponent>()
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

            Debug.Log("blue " + blueTeamCount);
            Debug.Log("red " + redTeamCount);
            if (blueTeamCount == 0 && redTeamCount == 0)
            {
                GameManager.RaiseGameDraw();
                return;
            }

            if (blueTeamCount == 0 || redTeamCount == 0)
            {
                if (blueTeamCount == 0)
                {
                    GameManager.RaiseGameLost();
                }
                else
                {
                    GameManager.RaiseGameWon();
                }
            }
        }
    }
}*/
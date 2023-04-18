using ECS.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Basically checks for end game scenarios. 
/// It need to be MonoBehaviour not system because of the draw scenarios.
/// When Draw scenario happens, system updates automatically shuts down
/// And we can pre-know if there is no entity left
/// </summary>
public class EndGameChecker : MonoBehaviour
{
	private bool _checkForDraw = false;
	private void Awake()
	{
		SignUpEvents();
	}

	private void SignUpEvents()
	{
		GameManager.GameStarted += OnGameStarted;
		GameManager.GameReloaded += OnGameReloaded;
		GameManager.GameLost += OnGameLost;
		GameManager.GameWon += OnGameWon;
		GameManager.GameDraw += OnGameDraw;
	}

	[BurstCompile]
	private void Update()
	{
		if (!_checkForDraw)
		{
			return;
		}
        
		var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
		var entities= entityManager.CreateEntityQuery(typeof(TeamComponent)).ToEntityArray(Allocator.Temp);
		if (entities.Length <= 0)
		{
			GameManager.RaiseGameDraw();
			return;
		}
		int blueTeamCount = 0;
		int redTeamCount = 0;
		foreach (var entity in entities)
		{
			var teamComponent = entityManager.GetComponentData<TeamComponent>(entity);
			if (teamComponent.value.Equals(Team.Blue))
			{
				blueTeamCount++;
			}
			else
			{
				redTeamCount++;
			}
		}
		if (blueTeamCount == 0)
		{
			GameManager.RaiseGameLost();
		}
		else if(redTeamCount == 0)
		{
			GameManager.RaiseGameWon();
		}
	}

	private void OnGameDraw()
	{
		_checkForDraw = false;
	}
	
	private void OnGameWon()
	{
		_checkForDraw = false;
	}
	
	private void OnGameLost()
	{
		_checkForDraw = false;
	}
	
	private void OnGameReloaded()
	{
		_checkForDraw = false;
	}

	private void OnGameStarted()
	{
		_checkForDraw = true;
	}
}

using System;
using System.Collections.Generic;
using ECS.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// Using struct for better visualization on inspector
/// </summary>
[Serializable]
public struct TeamUnitPrefabData
{
	public Team team;
	public GameObject unitPrefab;
}

/// <summary>
/// Simply creates entity prefabs from game object prefabs.
/// </summary>
public class PrefabsToEntityConverter : MonoBehaviour
{
	public static event Action PrefabsConverted;

	[SerializeField] private TeamUnitPrefabData[] teamsUnitPrefab;
	[SerializeField] private GameObject unitHealthDisplayPrefab;

	public static Dictionary<Team, Entity> TeamsEntityDic { get; private set; }
	public static Entity UnitHealthDisplay { get; private set; }
	private void Awake()
	{
		TeamsEntityDic = new Dictionary<Team, Entity>();
		
		var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
		foreach (var teamUnitPrefab in teamsUnitPrefab)
		{
			TeamsEntityDic.Add(teamUnitPrefab.team, GameObjectConversionUtility.ConvertGameObjectHierarchy(teamUnitPrefab.unitPrefab, settings));
		}
		UnitHealthDisplay = GameObjectConversionUtility.ConvertGameObjectHierarchy(unitHealthDisplayPrefab, settings);

		PrefabsConverted?.Invoke();
	}

	private void OnDestroy()
	{
		PrefabsConverted = null;
		TeamsEntityDic = null;
	}
}
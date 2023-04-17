using System;
using Unity.Entities;
using UnityEngine;

public class PrefabsToEntityConverter : MonoBehaviour
{
	public static event Action PrefabsConverted;

	[SerializeField] private GameObject blueTeamUnitPrefab;
	[SerializeField] private GameObject redTeamUnitPrefab;
	[SerializeField] private GameObject unitHealthDisplayPrefab;

	public static Entity BlueTeamUnit { get; private set; }
	public static Entity RedTeamUnit { get; private set; }
	public static Entity UnitHealthDisplay { get; private set; }
	private void Awake()
	{
		var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
		BlueTeamUnit = GameObjectConversionUtility.ConvertGameObjectHierarchy(blueTeamUnitPrefab, settings);
		RedTeamUnit = GameObjectConversionUtility.ConvertGameObjectHierarchy(redTeamUnitPrefab, settings);
		UnitHealthDisplay = GameObjectConversionUtility.ConvertGameObjectHierarchy(unitHealthDisplayPrefab, settings);

		PrefabsConverted?.Invoke();
	}

	private void OnDestroy()
	{
		PrefabsConverted = null;
	}
}
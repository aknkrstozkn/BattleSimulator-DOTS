using System;
using System.Collections.Generic;
using ECS.ComponentsAndTags;
using UnityEngine;

[Serializable]
public struct UnitSlotData
{
	public int slotIndex;
	public UnitData unitData;
}

[CreateAssetMenu(fileName = "TeamData", menuName = "ScriptableObjects/TeamData", order = 1)]
public class TeamData : ScriptableObject
{
	[SerializeField] private string teamName;
	[SerializeField] private Team team;
	[SerializeField] private UnitSlotData[] unitsSlotsData;

	public string TeamName => teamName;
	public Team Team => team;

	public Dictionary<int, UnitData> GetSlotIndexUnitDic()
	{
		var slotIndexUnitDic = new Dictionary<int, UnitData>();
		foreach (var unitSlotData in unitsSlotsData)
		{
			if (slotIndexUnitDic.ContainsKey(unitSlotData.slotIndex))
			{
#if UNITY_EDITOR
				Debug.LogWarning($"{teamName} team data has different units on same slot index!");
#endif
				continue;
			}
			if (unitSlotData.slotIndex >= 9)
			{
#if UNITY_EDITOR
				Debug.LogWarning($"{teamName} team data has an unit that slot index is bigger than max slot index 8!");
#endif
				continue;
			}
			
			slotIndexUnitDic.Add(unitSlotData.slotIndex, unitSlotData.unitData);
		}

		return slotIndexUnitDic;
	}
}
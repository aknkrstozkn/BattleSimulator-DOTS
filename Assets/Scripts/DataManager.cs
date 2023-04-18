using System;
using System.Collections.Generic;
using ECS.ComponentsAndTags;
using UnityEngine;

[Serializable]
public struct TeamSpawnTransformsData
{
	public Team team;
	public Transform[] spawnTransforms;
}

public class DataManager : MonoBehaviour
{
	[SerializeField] private TeamSpawnTransformsData[] teamsSpawnData;
	[SerializeField] private TeamData[] teamsData;
	
	public static Dictionary<Team, Vector3[]> TeamsSpawnPoints { get; private set; }
	public static Dictionary<Team, List<TeamData>> TeamsData { get; private set; }

	private void OnDestroy()
	{
		TeamsSpawnPoints = null;
		TeamsData = null;
	}

	private void Awake()
    {
	    Init();
    }

    private void Init()
    {
	    InitSpawnPoints();
	    InitTeams();
    }

    private void InitTeams()
    {
	    TeamsData = new Dictionary<Team, List<TeamData>>();
	    
	    foreach (var teamData in teamsData)
	    {
		    if (!TeamsData.ContainsKey(teamData.Team))
		    {
			    TeamsData.Add(teamData.Team, new List<TeamData>());
		    }
		    TeamsData[teamData.Team].Add(teamData);
	    }
    }
    
    private void InitSpawnPoints()
    {
	    TeamsSpawnPoints = new Dictionary<Team, Vector3[]>();
	    

        foreach (var spawnData in teamsSpawnData)
        {
	        var spawnPoints = new Vector3[9];
	        if (spawnData.spawnTransforms.Length != 9)
	        {
#if UNITY_EDITOR
		        Debug.LogError("Team Spawn Transform Count Must Be 9");
#endif
	        }
	        for (int i = 0; i < 9; i++)
	        {
		        var spawnTransform = spawnData.spawnTransforms[i];
		        spawnPoints[i] = new Vector3(spawnTransform.position.x, 1f, spawnTransform.position.z);
	        }
	        TeamsSpawnPoints.Add(spawnData.team, spawnPoints);
        }
    }
}

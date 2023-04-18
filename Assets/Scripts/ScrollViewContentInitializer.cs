using ECS.ComponentsAndTags;
using UnityEngine;

public class ScrollViewContentInitializer : MonoBehaviour
{
    [SerializeField] public Team team;
    [SerializeField] public TeamButton teamButtonPrefab;

    private void Start()
    {
        var teamsData = DataManager.TeamsData[team];
        foreach (var teamData in teamsData)
        {
            var newButton = Instantiate(teamButtonPrefab, transform);
            newButton.Init(teamData);
        }
    }
}

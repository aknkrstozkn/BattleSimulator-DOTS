using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Basic button class that holds data to forward when its clicked. 
/// </summary>
public class TeamButton : MonoBehaviour
{
	public static event Action<TeamData> TeamButtonClicked;

	private Button _button;
	private TeamData _teamData;
	
	public void Init(TeamData teamData)
	{
		_teamData = teamData;
	
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnClick);

		transform.GetComponentInChildren<Text>().text = teamData.TeamName;
	}

	private void OnClick()
	{
		TeamButtonClicked?.Invoke(_teamData);
	}

	private void OnDestroy()
	{
		TeamButtonClicked = null;
	}
}

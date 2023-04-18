using UnityEngine;

public class PlatformController : MonoBehaviour
{
	[SerializeField] private GameObject[] teamsPlatforms;
	
	private void Awake()
	{
		SignUpEvents();
	}

	private void SignUpEvents()
	{
		GameManager.GameStarted += OnGameStarted;
		GameManager.GameReloaded += OnGameReloaded;
	}
	
	private void OnGameReloaded()
	{
		foreach (var teamsPlatform in teamsPlatforms)
		{
			teamsPlatform.SetActive(true);
		}
	}

	private void OnGameStarted()
	{
		foreach (var teamsPlatform in teamsPlatforms)
		{
			teamsPlatform.SetActive(false);
		}
	}
}
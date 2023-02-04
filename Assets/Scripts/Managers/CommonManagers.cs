using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CommonManagers : MonoBehaviour
{
	[SerializeField]
	protected PlayableDirector _gameFromMainMenuPlayable;
	[SerializeField]
	protected PlayableDirector _mainMenuFromGamePlayable;

	private static CommonManagers _instance;
	public static CommonManagers Instance => _instance;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public void OnFinishedGoToCreditsFromMainMenu()
	{
		SceneManager.UnloadSceneAsync("MainMenu");
	}

	public void GoToCreditsFromMainMenu()
	{
		SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
		// TODO: Transition to Credits
	}

	public void OnFinishedGoToMainMenuFromCredits()
	{
		SceneManager.UnloadSceneAsync("Credits");
	}

	public void GoToMainMenuFromCredits()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
		// TODO: Transition to MainMenu
	}

	public void OnFinishedGoToGameFromMainMenu()
	{
		SceneManager.UnloadSceneAsync("MainMenu");
		GameManager.Instance.StartGame();
	}

	public void GoToGameFromMainMenu()
	{
		_gameFromMainMenuPlayable.enabled = false;
		_gameFromMainMenuPlayable.enabled = true;
		_gameFromMainMenuPlayable.Play();
	}

	public void OnFinishedGoToMainMenuFromGame()
	{
		MainMenuManager menuManager = FindObjectOfType<MainMenuManager>();
		if (menuManager != null)
		{
			menuManager.Return();
		}
	}

	public void GoToMainMenuFromGame()
	{
		IEnumerator GoToMainMenuFromGameCoroutine()
		{
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
			yield return null;
			MainMenuManager menuManager = FindObjectOfType<MainMenuManager>();
			if (menuManager != null)
			{
				menuManager.InstantHide();
			}
			_mainMenuFromGamePlayable.enabled = false;
			_mainMenuFromGamePlayable.enabled = true;
			_mainMenuFromGamePlayable.Play();
		}

		StartCoroutine(GoToMainMenuFromGameCoroutine());
	}
}

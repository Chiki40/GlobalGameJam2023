using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CommonManagers : Singleton
{
	[SerializeField]
	protected PlayableDirector transitionPlayable;
	private void Start()
	{
		if (!SceneManager.GetSceneByName("Game").isLoaded)
		{
			SceneManager.LoadScene("Game", LoadSceneMode.Additive);
		}
	}


	public void GoToCreditsFromMainMenu()
	{
		IEnumerator GoToCoroutine()
		{
			yield return null;
			SceneManager.UnloadSceneAsync("MainMenu");
		}

		SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
		StartCoroutine(GoToCoroutine());
	}

	public void GoToMainMenuFromCredits()
	{
		IEnumerator GoToCoroutine()
		{
			yield return null;
			SceneManager.UnloadSceneAsync("Credits");
		}

		SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
		StartCoroutine(GoToCoroutine());
	}

	public void OnFinishedGoToGameFromMainMenu()
	{
		SceneManager.UnloadSceneAsync("MainMenu");
	}

	public void GoToGameFromMainMenu()
	{
		transitionPlayable.Play();
	}

	public void GoToMainMenuFromGame()
	{
		IEnumerator GoToCoroutine()
		{
			yield return null;
		}

		SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
		StartCoroutine(GoToCoroutine());
	}
}

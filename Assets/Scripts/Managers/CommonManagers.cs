using System.Collections;
using UnityEngine.SceneManagement;

public class CommonManagers : Singleton
{
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

	public void GoToGameFromMainMenu()
	{
		IEnumerator GoToCoroutine()
		{
			yield return null;
			SceneManager.UnloadSceneAsync("MainMenu");
		}

		StartCoroutine(GoToCoroutine());
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

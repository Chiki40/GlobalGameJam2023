using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public void OnStart()
	{
		SceneManager.LoadScene("Game");
	}

	public void OnCredits()
	{
		SceneManager.LoadScene("Credits");
	}

	public void OnExit()
	{
#if !UNITY_EDITOR
		Application.Quit();
#else
		UnityEditor.EditorApplication.ExitPlaymode();
#endif
	}
}

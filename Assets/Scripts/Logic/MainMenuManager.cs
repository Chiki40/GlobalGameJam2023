using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public void OnStart()
	{
		((CommonManagers)CommonManagers.Instance).GoToGameFromMainMenu();
	}

	public void OnCredits()
	{
		((CommonManagers)CommonManagers.Instance).GoToCreditsFromMainMenu();
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

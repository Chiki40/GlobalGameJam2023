using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
	public void OnExit()
	{
		SceneManager.LoadScene("MainMenu");
	}
}

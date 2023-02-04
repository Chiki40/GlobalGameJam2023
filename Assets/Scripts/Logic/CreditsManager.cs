using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnExit();
		}
	}
	public void OnExit()
	{
		CommonManagers.Instance.GoToMainMenuFromCredits();
	}
}

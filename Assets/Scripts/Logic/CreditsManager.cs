using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
	public void OnExit()
	{
		CommonManagers.Instance.GoToMainMenuFromCredits();
	}
}

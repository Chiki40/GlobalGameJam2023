using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
	public void OnExit()
	{
		((CommonManagers)CommonManagers.Instance).GoToMainMenuFromCredits();
	}
}

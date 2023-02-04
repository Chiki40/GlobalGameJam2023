using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CommonManagers : MonoBehaviour
{
	private const int kLowPriorityCam = 1000;
	private const int kHighPriorityCam = 1001;

	[SerializeField]
	protected PlayableDirector _gameFromMainMenuPlayable;
	[SerializeField]
	protected PlayableDirector _mainMenuFromGamePlayable;
	[SerializeField]
	protected PlayableDirector _creditsFromMainMenuPlayable;
	[SerializeField]
	protected PlayableDirector _mainMenuFromCreditsPlayable;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _gameFromMainMenuCam;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _mainMenuFromGameCam;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _creditsFromMainMenuCam;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _mainMenuFromCreditsCam;

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
		CreditsManager creditsManager = FindObjectOfType<CreditsManager>();
		if (creditsManager != null)
		{
			creditsManager.ShowCredits();
		}
	}

	public void GoToCreditsFromMainMenu()
	{
		IEnumerator GoToCreditsFromMainMenuCoroutine()
		{
			SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
			yield return null;
			CreditsManager creditsManager = FindObjectOfType<CreditsManager>();
			if (creditsManager != null)
			{
				creditsManager.InstantHide();
			}
			_gameFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
			_mainMenuFromGameCam.Priority = kLowPriorityCam;
			_creditsFromMainMenuCam.Priority = kHighPriorityCam;
			_creditsFromMainMenuPlayable.Play();
		}

		StartCoroutine(GoToCreditsFromMainMenuCoroutine());
	}

	public void OnFinishedGoToMainMenuFromCredits()
	{
		SceneManager.UnloadSceneAsync("Credits");
		MainMenuManager menuManager = FindObjectOfType<MainMenuManager>();
		if (menuManager != null)
		{
			menuManager.Return();
		}
	}

	public void GoToMainMenuFromCredits()
	{
		IEnumerator GoToMainMenuFromCreditsCoroutine()
		{
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
			yield return null;
			MainMenuManager menuManager = FindObjectOfType<MainMenuManager>();
			if (menuManager != null)
			{
				menuManager.InstantHide();
			}
			_creditsFromMainMenuCam.Priority = kLowPriorityCam;
			_gameFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromGameCam.Priority = kLowPriorityCam;
			_mainMenuFromCreditsCam.Priority = kHighPriorityCam;
			_mainMenuFromCreditsPlayable.Play();
		}

		StartCoroutine(GoToMainMenuFromCreditsCoroutine());
	}

	public void OnFinishedGoToGameFromMainMenu()
	{
		SceneManager.UnloadSceneAsync("MainMenu");
		GameManager.Instance.StartGame();
	}

	public void GoToGameFromMainMenu()
	{
		_creditsFromMainMenuCam.Priority = kLowPriorityCam;
		_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
		_mainMenuFromGameCam.Priority = kLowPriorityCam;
		_gameFromMainMenuCam.Priority = kHighPriorityCam;
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
			_creditsFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
			_gameFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromGameCam.Priority = kHighPriorityCam;
			_mainMenuFromGamePlayable.Play();
		}

		StartCoroutine(GoToMainMenuFromGameCoroutine());
	}
}

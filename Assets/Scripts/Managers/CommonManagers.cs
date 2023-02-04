using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CommonManagers : MonoBehaviour
{
	private const int kLowPriorityCam = 10;
	private const int kHighPriorityCam = 1000;

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
		_creditsFromMainMenuCam.Priority = kHighPriorityCam;
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
			_gameFromMainMenuPlayable.time = 0.0f;
			_gameFromMainMenuPlayable.Stop();
			_gameFromMainMenuPlayable.Evaluate();
			_mainMenuFromCreditsPlayable.time = 0.0f;
			_mainMenuFromCreditsPlayable.Stop();
			_mainMenuFromCreditsPlayable.Evaluate();
			_mainMenuFromGamePlayable.time = 0.0f;
			_mainMenuFromGamePlayable.Stop();
			_mainMenuFromGamePlayable.Evaluate();

			_gameFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
			_mainMenuFromGameCam.Priority = kLowPriorityCam;
			_creditsFromMainMenuPlayable.Play();
		}

		StartCoroutine(GoToCreditsFromMainMenuCoroutine());
	}

	public void OnFinishedGoToMainMenuFromCredits()
	{
		_mainMenuFromCreditsCam.Priority = kHighPriorityCam;
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
			_creditsFromMainMenuPlayable.time = 0.0f;
			_creditsFromMainMenuPlayable.Stop();
			_creditsFromMainMenuPlayable.Evaluate();
			_gameFromMainMenuPlayable.time = 0.0f;
			_gameFromMainMenuPlayable.Stop();
			_gameFromMainMenuPlayable.Evaluate();
			_mainMenuFromGamePlayable.time = 0.0f;
			_mainMenuFromGamePlayable.Stop();
			_mainMenuFromGamePlayable.Evaluate();

			_creditsFromMainMenuCam.Priority = kLowPriorityCam;
			_gameFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromGameCam.Priority = kLowPriorityCam;
			_mainMenuFromCreditsPlayable.Play();
		}

		StartCoroutine(GoToMainMenuFromCreditsCoroutine());
	}

	public void OnFinishedGoToGameFromMainMenu()
	{
		_gameFromMainMenuCam.Priority = kHighPriorityCam;
		SceneManager.UnloadSceneAsync("MainMenu");
		GameManager.Instance.StartGame();
	}

	public void GoToGameFromMainMenu()
	{
		_creditsFromMainMenuPlayable.time = 0.0f;
		_creditsFromMainMenuPlayable.Stop();
		_creditsFromMainMenuPlayable.Evaluate();
		_mainMenuFromCreditsPlayable.time = 0.0f;
		_mainMenuFromCreditsPlayable.Stop();
		_mainMenuFromCreditsPlayable.Evaluate();
		_mainMenuFromGamePlayable.time = 0.0f;
		_mainMenuFromGamePlayable.Stop();
		_mainMenuFromGamePlayable.Evaluate();

		_creditsFromMainMenuCam.Priority = kLowPriorityCam;
		_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
		_mainMenuFromGameCam.Priority = kLowPriorityCam;
		_gameFromMainMenuPlayable.Play();
	}

	public void OnFinishedGoToMainMenuFromGame()
	{
		_mainMenuFromGameCam.Priority = kHighPriorityCam;
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
			_creditsFromMainMenuPlayable.time = 0.0f;
			_creditsFromMainMenuPlayable.Stop();
			_creditsFromMainMenuPlayable.Evaluate();
			_mainMenuFromCreditsPlayable.time = 0.0f;
			_mainMenuFromCreditsPlayable.Stop();
			_mainMenuFromCreditsPlayable.Evaluate();
			_gameFromMainMenuPlayable.time = 0.0f;
			_gameFromMainMenuPlayable.Stop();
			_gameFromMainMenuPlayable.Evaluate();

			_creditsFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
			_gameFromMainMenuCam.Priority = kLowPriorityCam;
			_mainMenuFromGamePlayable.Play();
		}

		StartCoroutine(GoToMainMenuFromGameCoroutine());
	}
}

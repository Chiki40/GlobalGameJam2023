using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CommonManagers : MonoBehaviour
{
	public const int kLowPriorityCam = 10;
	public const int kHighPriorityCam = 1000;

	[SerializeField]
	Animator _creditsAnimator;
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

	protected void StopPlayableDirector(PlayableDirector director)
	{
		director.time = 0.0f;
		director.Stop();
		director.Evaluate();
		director.gameObject.SetActive(false);
	}

	protected void PlayPlayableDirector(PlayableDirector director)
	{
		director.time = 0;
		director.Evaluate();
		director.gameObject.SetActive(true);
		director.Play(director.playableAsset, DirectorWrapMode.Hold);
	}
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
		_creditsAnimator.SetTrigger("CreditsTrigger");
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
			StopPlayableDirector(_gameFromMainMenuPlayable);
			StopPlayableDirector(_mainMenuFromCreditsPlayable);
			StopPlayableDirector(_mainMenuFromGamePlayable);
			PlayPlayableDirector(_creditsFromMainMenuPlayable);
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
			StopPlayableDirector(_creditsFromMainMenuPlayable);
			StopPlayableDirector(_gameFromMainMenuPlayable);
			StopPlayableDirector(_mainMenuFromGamePlayable);
			PlayPlayableDirector(_mainMenuFromCreditsPlayable);
			_creditsAnimator.SetTrigger("CreditsTrigger");
		}

		StartCoroutine(GoToMainMenuFromCreditsCoroutine());
	}

	public void OnFinishedGoToGameFromMainMenu()
	{
		//_gameFromMainMenuCam.Priority = kHighPriorityCam;
		SceneManager.UnloadSceneAsync("MainMenu");
		GameManager.Instance.StartGame();
	}

	public void GoToGameFromMainMenu()
	{
		StopPlayableDirector(_creditsFromMainMenuPlayable);
		StopPlayableDirector(_mainMenuFromCreditsPlayable);
		StopPlayableDirector(_mainMenuFromGamePlayable);
		PlayPlayableDirector(_gameFromMainMenuPlayable);
	}

	public void OnFinishedGoToMainMenuFromGame()
	{
		//_mainMenuFromGameCam.Priority = kHighPriorityCam;
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

			StopPlayableDirector(_creditsFromMainMenuPlayable);
			StopPlayableDirector(_mainMenuFromCreditsPlayable);
			StopPlayableDirector(_gameFromMainMenuPlayable);
			PlayPlayableDirector(_mainMenuFromGamePlayable);
		}

		StartCoroutine(GoToMainMenuFromGameCoroutine());
	}

	public void SwitchToCharacterCamera()
	{
		StopPlayableDirector(_creditsFromMainMenuPlayable);
		StopPlayableDirector(_mainMenuFromCreditsPlayable);
		StopPlayableDirector(_gameFromMainMenuPlayable);
		StopPlayableDirector(_mainMenuFromGamePlayable);

		_creditsFromMainMenuCam.Priority = kLowPriorityCam;
		_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
		_gameFromMainMenuCam.Priority = kLowPriorityCam;
		_mainMenuFromGameCam.Priority = kLowPriorityCam;
	}

	public void ReturnFromCharacterCamera()
	{
		_creditsFromMainMenuCam.Priority = kLowPriorityCam;
		_mainMenuFromCreditsCam.Priority = kLowPriorityCam;
		_mainMenuFromGameCam.Priority = kLowPriorityCam;
		_gameFromMainMenuCam.Priority = kHighPriorityCam;
		_gameFromMainMenuPlayable.gameObject.SetActive(true);
	}
}

using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CommonManagers : MonoBehaviour
{
	public const int kLowPriorityCam = 10;
	public const int kHighPriorityCam = 99999999;

	[SerializeField]
	Animator _creditsAnimator;
	[SerializeField]
	protected PlayableDirector _gameFromMainMenuPlayable;
	[SerializeField]
	protected PlayableDirector _gameFromMainMenuSpeedPlayable;
	[SerializeField]
	protected PlayableDirector _mainMenuFromGamePlayable;
	[SerializeField]
	protected PlayableDirector _creditsFromMainMenuPlayable;
	[SerializeField]
	protected PlayableDirector _mainMenuFromCreditsPlayable;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _gameCam;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _mainMenuCam;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _creditsCam;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _arbolCam;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _characterCam;

	private static CommonManagers _instance;
	public static CommonManagers Instance => _instance;

	protected void Start()
	{
		UtilSound.Instance.PlaySound("MainTheme", 1, true);
	}
	protected void StopPlayableDirector(PlayableDirector director)
	{
		director.Stop();
		director.time = 0.0f;
		director.Evaluate();
		//director.gameObject.SetActive(false);
	}

	protected void PlayPlayableDirector(PlayableDirector director)
	{
		IEnumerator StopAndPay(PlayableDirector director)
		{
			StopPlayableDirector(director);
			yield return null;
			director.Play(director.playableAsset, DirectorWrapMode.None);
		}
		StartCoroutine(StopAndPay(director));
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
		_creditsCam.Priority = kHighPriorityCam;
		_mainMenuCam.Priority = kLowPriorityCam;
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
			PlayPlayableDirector(_creditsFromMainMenuPlayable);
		}

		StartCoroutine(GoToCreditsFromMainMenuCoroutine());
	}

	public void OnFinishedGoToMainMenuFromCredits()
	{
		_mainMenuCam.Priority = kHighPriorityCam;
		_creditsCam.Priority = kLowPriorityCam;
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
			PlayPlayableDirector(_mainMenuFromCreditsPlayable);
			_creditsAnimator.SetTrigger("CreditsTrigger");
		}

		StartCoroutine(GoToMainMenuFromCreditsCoroutine());
	}

	public void OnFinishedGoToGameFromMainMenu()
	{
		_gameCam.Priority = kHighPriorityCam;
		_mainMenuCam.Priority = kLowPriorityCam;
		SceneManager.UnloadSceneAsync("MainMenu");
		GameManager.Instance.StartGame();
		UtilSound.Instance.PlaySound("Game", 1, true);
	}

	public void GoToGameFromMainMenu()
	{
		PlayPlayableDirector(GameManager.Instance.introSaw ? _gameFromMainMenuSpeedPlayable:_gameFromMainMenuPlayable);
	}

	public void OnFinishedGoToMainMenuFromGame()
	{
		MainMenuManager menuManager = FindObjectOfType<MainMenuManager>();
		if (menuManager != null)
		{
			menuManager.Return();
		}
		_gameCam.Priority = kLowPriorityCam;
		_mainMenuCam.Priority = kHighPriorityCam;
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
			PlayPlayableDirector(_mainMenuFromGamePlayable);
			UtilSound.Instance.PlaySound("MainTheme", 1, true);
			UtilSound.Instance.StopSound("Game", 2);
		}

		StartCoroutine(GoToMainMenuFromGameCoroutine());
	}

	public void GoToCharacterFromGame()
	{
		_characterCam.Priority = kHighPriorityCam;
		_gameCam.Priority = kLowPriorityCam;
	}
	public void GoToGameFromGameCharacter()
	{
		_characterCam.Priority = kLowPriorityCam;
		_gameCam.Priority = kHighPriorityCam;
	}

	public void GoToArbolFromCharacter()
	{
		_arbolCam.Priority = kHighPriorityCam;
		_characterCam.Priority = kLowPriorityCam;
	}
	public void GoToCharacterFromArbol()
	{
		_arbolCam.Priority = kLowPriorityCam;
		_characterCam.Priority = kHighPriorityCam;
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private enum EGameState { ROOM = 0, CHARACTER = 1, TREE = 2};

	[SerializeField]
	protected SpriteRenderer _character;
	[SerializeField]
	protected Cinemachine.CinemachineVirtualCamera _characterCam;

	private static GameManager _instance;
	public static GameManager Instance => _instance;

	private bool _gamePlaying = false;
	public bool GamePlaying => _gamePlaying;

	private EGameState _gameState = EGameState.ROOM;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			Init();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}


	private void Init()
	{
		if (!SceneManager.GetSceneByName("MainMenu").isLoaded)
		{
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
		}
	}

	private void Update()
	{
		if (_gamePlaying && Input.GetKeyDown(KeyCode.Escape))
		{
			if (_gameState == EGameState.ROOM)
			{
				ExitToMainMenu();
			}
			else if (_gameState == EGameState.CHARACTER)
			{
				ReturnFromCharacterCamera();
			}
		}
	}

	public void StartGame()
	{
		if (!_gamePlaying)
		{
			_gamePlaying = true;
			_gameState = EGameState.ROOM;
		}
	}

	private void ExitToMainMenu()
	{
		if (_gamePlaying && _gameState == EGameState.ROOM)
		{
			CommonManagers.Instance.GoToMainMenuFromGame();
			_gamePlaying = false;
		}
	}

	public void SwitchToCharacterCamera(Sprite sprite)
	{
		if (_gamePlaying && _gameState == EGameState.ROOM)
		{
			CommonManagers.Instance.SwitchToCharacterCamera();
			_characterCam.Priority = CommonManagers.kHighPriorityCam;
			_character.sprite = sprite;
			_character.gameObject.SetActive(true);
			_gameState = EGameState.CHARACTER;
		}
	}

	public void ReturnFromCharacterCamera()
	{
		if (_gamePlaying && _gameState == EGameState.CHARACTER)
		{
			_character.gameObject.SetActive(false);
			_characterCam.Priority = CommonManagers.kLowPriorityCam;
			CommonManagers.Instance.ReturnFromCharacterCamera();
			_gameState = EGameState.ROOM;
		}
	}
}

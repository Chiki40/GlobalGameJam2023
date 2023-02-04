using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private enum EGameState { PHOTO = 0, CHARACTER = 1, TREE = 2};

	[SerializeField]
	protected bool _introSaw = false;
	public bool introSaw { get { return _introSaw; } }
	[SerializeField]
	protected SpriteRenderer _character;
	[SerializeField]
	protected TextMeshProUGUI _dialogue;
	[SerializeField]
	protected Canvas _treeCanvas;
	[SerializeField]
	protected Collider _photoCollider;

	private static GameManager _instance;
	public static GameManager Instance => _instance;

	private bool _gamePlaying = false;
	public bool GamePlaying => _gamePlaying;

	private EGameState _gameState = EGameState.PHOTO;

	CharacterInfo _currentCharacterInfo = null;

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
#if DEBUG
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Time.timeScale = 15.0f;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Time.timeScale = 1.0f;
		}
#endif

		if (_gamePlaying && Input.GetKeyDown(KeyCode.Escape))
		{
			if (_gameState == EGameState.PHOTO)
			{
				ExitToMainMenu();
			}
			else if (_gameState == EGameState.CHARACTER)
			{
				SwitchToPhotoCamera();
			}
			else if (_gameState == EGameState.TREE)
			{
				SwitchToCharacterCamera(_currentCharacterInfo);
			}
		}
	}

	public void StartGame()
	{
		if (!_gamePlaying)
		{
			_gamePlaying = true;
			_introSaw = true;
			SwitchToPhotoCamera(true);
		}
	}

	private void ExitToMainMenu()
	{
		if (_gamePlaying && _gameState == EGameState.PHOTO)
		{
			CommonManagers.Instance.GoToMainMenuFromGame();
			_gamePlaying = false;
		}
	}

	public void SwitchToPhotoCamera(bool force = false)
	{
		if (_gamePlaying && (_gameState == EGameState.CHARACTER || force))
		{
			CommonManagers.Instance.GoToGameFromGameCharacter();
			_character.gameObject.SetActive(false);
			_gameState = EGameState.PHOTO;
			_photoCollider.enabled = false;
		}
	}

	public void SwitchToCharacterCamera(CharacterInfo charInfo)
	{
		if (_gamePlaying && (_gameState == EGameState.PHOTO || _gameState == EGameState.TREE))
		{
			CommonManagers.Instance.GoToCharacterFromGame();
			_character.sprite = charInfo.CharacterSprite;
			_dialogue.text = TextsManager.Instance.GetDialogueText(charInfo.CharacterDialogueKey);
			_character.gameObject.SetActive(true);
			_photoCollider.enabled = true;
			_treeCanvas.gameObject.SetActive(false);
			_currentCharacterInfo = charInfo;
			_gameState = EGameState.CHARACTER;
		}
	}

	public void SwitchToTree()
	{
		if (_gamePlaying && _gameState == EGameState.CHARACTER)
		{
			CommonManagers.Instance.GoToArbolFromCharacter();
			_treeCanvas.gameObject.SetActive(true);
			_character.gameObject.SetActive(false);
			_gameState = EGameState.TREE;
		}
	}
}

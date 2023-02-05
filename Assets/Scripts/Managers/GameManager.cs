using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private enum EGameState { PHOTO = 0, CHARACTER = 1, TREE = 2};

	[System.Serializable]
	public struct LevelData
	{
		public List<GameObject> fotoCharactersToShow;
		public List<GameObject> arbolImagesToUnlock;
		public List<GameObject> scrollImagesToUnlock;
		public Image background;
	}

	[SerializeField]
	protected List<LevelData> levels;

	[SerializeField]
	int currentLevel = 0;

	[SerializeField]
	protected bool _introSaw = false;
	public bool introSaw { get { return _introSaw; } }
	[SerializeField]
	protected SpriteRenderer _character;
	[SerializeField]
	protected GameObject _characterMonologueGameObject;
	[SerializeField]
	protected TextMeshProUGUI _dialogue;
	[SerializeField]
	protected Canvas _treeCanvas;
	[SerializeField]
	protected Collider _mainPhotoCollider = null;
	[SerializeField]
	protected Transform _photoCollidersParents = null;

	protected Collider2D[] _cachedPhotoColliders = null;

	private static GameManager _instance;
	public static GameManager Instance => _instance;

	private bool _gamePlaying = false;
	public bool GamePlaying => _gamePlaying;

	private bool _controlsBlocked = false;

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

	private void Start()
	{
		_cachedPhotoColliders = _photoCollidersParents.GetComponentsInChildren<BoxCollider2D>();
		UtilSound.Instance.PlaySound("ambience", loop: true);
	}

	private void OnDestroy()
	{
		UtilSound.Instance.StopAllSounds();
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

		if (!_controlsBlocked && _gamePlaying && Input.GetKeyDown(KeyCode.Escape))
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
			PrepareLevel(levels[0]);
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
		if (_controlsBlocked)
		{
			return;
		}

		IEnumerator HideCharacterCoroutine()
		{
			yield return new WaitForSeconds(1.8f);
			_character.gameObject.SetActive(false);
			EnablePhotoColliders(true);
			_controlsBlocked = false;
		}

		if (_gamePlaying && (_gameState == EGameState.CHARACTER || force))
		{
			if (!force)
			{
				UtilSound.Instance.PlaySound("EnterPhoto");
			}
			_controlsBlocked = true;
			CommonManagers.Instance.GoToGameFromGameCharacter();
			_gameState = EGameState.PHOTO;
			_mainPhotoCollider.enabled = false;
			_characterMonologueGameObject.SetActive(false);
			StartCoroutine(HideCharacterCoroutine());
		}
	}

	public void SwitchToCharacterCamera(CharacterInfo charInfo)
	{
		if (_controlsBlocked)
		{
			return;
		}

		IEnumerator SwitchToCharacterCameraCoroutine()
		{
			yield return new WaitForSeconds(1.5f);
			_mainPhotoCollider.enabled = true;
			_currentCharacterInfo = charInfo;
			_characterMonologueGameObject.SetActive(true);
			_gameState = EGameState.CHARACTER;
			_controlsBlocked = false;
		}

		if (_gamePlaying && (_gameState == EGameState.PHOTO || _gameState == EGameState.TREE))
		{
			UtilSound.Instance.PlaySound("SelectPhoto");
			_controlsBlocked = true;
			CommonManagers.Instance.GoToCharacterFromGame();
			_character.sprite = charInfo.CharacterSprite;
			_dialogue.text = TextsManager.Instance.GetDialogueText(charInfo.CharacterDialogueKey);
			_character.gameObject.SetActive(true);
			_treeCanvas.gameObject.SetActive(false);
			EnablePhotoColliders(false);
			StartCoroutine(SwitchToCharacterCameraCoroutine());
		}
	}

    internal bool CheckCurrentLevel(int numCorrect)
    {
		int correctNeeded = 0;
        for (int i = 0; i < currentLevel; ++i)
		{
			correctNeeded += levels[i].arbolImagesToUnlock.Count;
		}
		return numCorrect >= correctNeeded;
    }

    public void SwitchToTree()
	{
		if (_controlsBlocked)
		{
			return;
		}

		IEnumerator HeadingToArbolCoroutine()
		{
			yield return new WaitForSeconds(2.0f);
			_character.gameObject.SetActive(false);
			_treeCanvas.gameObject.SetActive(true);
			_gameState = EGameState.TREE;
			_controlsBlocked = false;
		}

		if (_gamePlaying && _gameState == EGameState.CHARACTER)
		{
			UtilSound.Instance.PlaySound("EnterTree");
			_controlsBlocked = true;
			CommonManagers.Instance.GoToArbolFromCharacter();
			_mainPhotoCollider.enabled = false;
			EnablePhotoColliders(false);
			_characterMonologueGameObject.SetActive(false);
			StartCoroutine(HeadingToArbolCoroutine());
		}
	}

	public void CloseDialogue()
	{
		_characterMonologueGameObject.SetActive(false);
	}

	public void OnLevelCompleted()
	{
		FinishLevel(levels[currentLevel]);
		++currentLevel;
		if (currentLevel > levels.Count)
		{
			OnGameEnd();
		}
		else
		{
			PrepareLevel(levels[currentLevel]);
		}
	}

	private void FinishLevel(LevelData levelData)
	{
		foreach (GameObject imageToUnlock in levelData.arbolImagesToUnlock)
		{
			imageToUnlock.GetComponent<Button>().enabled = false;
		}

		foreach (GameObject imageToUnlock in levelData.scrollImagesToUnlock)
		{
			imageToUnlock.SetActive(false);
		}

		levelData.background.enabled = false;
		
	}

	private void PrepareLevel(LevelData levelData)
	{
		foreach (GameObject imageToUnlock in levelData.arbolImagesToUnlock)
		{
			imageToUnlock.SetActive(true);
		}

		foreach (GameObject imageToUnlock in levelData.scrollImagesToUnlock)
		{
			imageToUnlock.SetActive(true);
		}

		foreach (GameObject gameObjectToShow in levelData.fotoCharactersToShow)
		{
			gameObjectToShow.SetActive(true);
		}

		IEnumerator ShowBackground(Image background)
		{
			//Color color = background.color;
			//color.a = 0;
			yield return null;
			levelData.background.enabled = true;
			//float totalTime = 0;
			//while (totalTime < 2)
			//{
			//	color.a = 
			//	background.color = color.a
			//}
			//background.color = 
		}
		StartCoroutine(ShowBackground(levelData.background));
		
	}

	private void OnGameEnd()
	{
		SceneManager.LoadScene("Victory");
	}

	private void EnablePhotoColliders(bool enable)
	{
		for (int i = 0; i < _cachedPhotoColliders.Length; ++i)
		{
			_cachedPhotoColliders[i].enabled = enable;
		}
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	private static GameManager _instance;
	public static GameManager Instance => _instance;

	private bool _gamePlaying = false;
	public bool GamePlaying => _gamePlaying;

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
			ExitToMainMenu();
		}
	}

	public void StartGame()
	{
		if (!_gamePlaying)
		{
			_gamePlaying = true;
		}
	}

	private void ExitToMainMenu()
	{
		if (_gamePlaying)
		{
			CommonManagers.Instance.GoToMainMenuFromGame();
			_gamePlaying = false;
		}
	}
}

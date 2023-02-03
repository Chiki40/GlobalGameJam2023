using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton
{
	private bool _gamePlaying = false;
	public bool GamePlaying => _gamePlaying;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
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
			((CommonManagers)CommonManagers.Instance).GoToMainMenuFromGame();
			_gamePlaying = false;
		}
	}
}

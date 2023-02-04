using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup _canvasGroup = null;
	[SerializeField]
	private float _timeToFade = 2.0f;

	private bool _goingToGame = false;
	private bool _returning = false;
	private float _timeFading = 0.0f;

	private IEnumerator Start()
	{
		if (!SceneManager.GetSceneByName("Game").isLoaded)
		{
			SceneManager.LoadScene("Game", LoadSceneMode.Additive);
			yield return null;
			SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
		}
	}

	private void Update()
	{
		if (_goingToGame)
		{
			_timeFading = Mathf.Min(_timeFading + Time.deltaTime, _timeToFade);
			_canvasGroup.alpha = 1.0f - (_timeFading / _timeToFade);
			if (_timeFading >= _timeToFade)
			{
				CommonManagers.Instance.GoToGameFromMainMenu();
			}
		}
		else if (_returning)
		{
			_timeFading = Mathf.Min(_timeFading + Time.deltaTime, _timeToFade);
			_canvasGroup.alpha = _timeFading / _timeToFade;
		}
	}

	public void Return()
	{
		if (!_returning)
		{
			_timeFading = 0.0f;
			_returning = true;
		}
	}

	public void InstantHide()
	{
		_canvasGroup.alpha = 0.0f;
	}

	public void OnStart()
	{
		if (!_goingToGame && !_returning)
		{
			_goingToGame = true;
			_timeFading = 0.0f;
		}
	}

	public void OnCredits()
	{
		if (!_goingToGame && !_returning)
		{
			CommonManagers.Instance.GoToCreditsFromMainMenu();
		}
	}

	public void OnExit()
	{
		if (!_goingToGame && !_returning)
		{
#if !UNITY_EDITOR
			Application.Quit();
#else
			UnityEditor.EditorApplication.ExitPlaymode();
#endif
		}
	}
}

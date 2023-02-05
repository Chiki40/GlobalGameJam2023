using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup _canvasGroup = null;
	[SerializeField]
	private float _timeToFade = 2.0f;
	private Button[] _buttons = null;

	private bool _exitingMenuToGame = false;
	private bool _returningFromGame = false;
	private bool _exitingMenuToCredits = false;
	private float _timeFading = 0.0f;

	private IEnumerator Start()
	{
		if (!SceneManager.GetSceneByName("Game").isLoaded)
		{
			SceneManager.LoadScene("Game", LoadSceneMode.Additive);
			yield return null;
			SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
		}
		_buttons = transform.GetComponentsInChildren<Button>();
	}

	private void Update()
	{
		if (_exitingMenuToGame)
		{
			_timeFading = Mathf.Min(_timeFading + Time.deltaTime, _timeToFade);
			_canvasGroup.alpha = 1.0f - (_timeFading / _timeToFade);
			if (_timeFading >= _timeToFade)
			{
				_exitingMenuToGame = false;
				CommonManagers.Instance.GoToGameFromMainMenu();
			}
		}
		else if (_returningFromGame)
		{
			_timeFading = Mathf.Min(_timeFading + Time.deltaTime, _timeToFade);
			_canvasGroup.alpha = _timeFading / _timeToFade;
			if (_timeFading >= _timeToFade)
			{
				_returningFromGame = false;
				EnableButtons(true);
			}
		}
		else if (_exitingMenuToCredits)
		{
			_timeFading = Mathf.Min(_timeFading + Time.deltaTime, _timeToFade);
			_canvasGroup.alpha = 1.0f - (_timeFading / _timeToFade);
			if (_timeFading >= _timeToFade)
			{
				_exitingMenuToCredits = false;
				CommonManagers.Instance.GoToCreditsFromMainMenu();
			}
		}
	}

	private void EnableButtons(bool enable)
	{
		for (int i = 0; i < _buttons.Length; ++i)
		{
			_buttons[i].interactable = enable;
		}
	}

	public void InstantHide()
	{
		_canvasGroup.alpha = 0.0f;
		EnableButtons(false);
	}

	public void Return()
	{
		EnableButtons(false);
		_returningFromGame = true;
		_timeFading = 0.0f;
	}

	public void OnStart()
	{
		EnableButtons(false);
		_exitingMenuToGame = true;
		_timeFading = 0.0f;
		UtilSound.Instance.PlaySound("Sonido de comienzo");
		UtilSound.Instance.StopSound("MainTheme",2);
	}

	public void OnCredits()
	{
		UtilSound.Instance.PlaySound("Sonido de comienzo");
		EnableButtons(false);
		_exitingMenuToCredits = true;
	}

	public void OnExit()
	{
#if !UNITY_EDITOR
		Application.Quit();
#else
		UnityEditor.EditorApplication.ExitPlaymode();
#endif
		EnableButtons(false);
	}
}

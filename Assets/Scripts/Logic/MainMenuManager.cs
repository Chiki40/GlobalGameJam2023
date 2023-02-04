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
		_buttons = transform.GetComponentsInChildren<Button>();
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
			if (_timeFading >= _timeToFade)
			{
				EnableButtons(true);
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
		_returning = true;
		_timeFading = 0.0f;
	}

	public void OnStart()
	{
		EnableButtons(false);
		_goingToGame = true;
		_timeFading = 0.0f;
	}

	public void OnCredits()
	{
		EnableButtons(false);
		CommonManagers.Instance.GoToCreditsFromMainMenu();
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

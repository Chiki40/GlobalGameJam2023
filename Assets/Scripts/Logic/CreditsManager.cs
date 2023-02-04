using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup _canvasGroup = null;
	[SerializeField]
	private float _timeToFade = 2.0f;
	private Button _exitButton = null;

	private bool _goingToMenu = false;
	private bool _showingCredits = false;
	private float _timeFading = 0.0f;

	private void Start()
	{
		_exitButton = transform.GetComponentInChildren<Button>();
	}

	private void Update()
	{
		if (_goingToMenu)
		{
			_timeFading = Mathf.Min(_timeFading + Time.deltaTime, _timeToFade);
			_canvasGroup.alpha = 1.0f - (_timeFading / _timeToFade);
			if (_timeFading >= _timeToFade)
			{
				_goingToMenu = false;
				CommonManagers.Instance.GoToMainMenuFromCredits();
			}
		}
		else if (_showingCredits)
		{
			_timeFading = Mathf.Min(_timeFading + Time.deltaTime, _timeToFade);
			_canvasGroup.alpha = _timeFading / _timeToFade;
			if (_timeFading >= _timeToFade)
			{
				_showingCredits = false;
				EnableButtons(true);
			}
		}
		else if (_exitButton.interactable && Input.GetKeyDown(KeyCode.Escape))
		{
			OnExit();
		}
	}
	public void OnExit()
	{
		EnableButtons(false);
		_goingToMenu = true;
		_timeFading = 0.0f;
	}

	public void ShowCredits()
	{
		_showingCredits = true;
		_timeFading = 0.0f;
	}

	private void EnableButtons(bool enable)
	{
		_exitButton.interactable = enable;
	}

	public void InstantHide()
	{
		_canvasGroup.alpha = 0.0f;
		EnableButtons(false);
	}
}

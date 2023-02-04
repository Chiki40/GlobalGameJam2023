using UnityEngine;
using UnityEngine.Events;

public class OnMouseDownEvent : MonoBehaviour
{

	[SerializeField]
	private UnityEvent _onMouseDown = null;

	[SerializeField]
	private UnityEvent _onMouseEnter = null;
	[SerializeField]
	private UnityEvent _onMouseExit = null;

	private void OnMouseDown()
	{
		_onMouseDown?.Invoke();
	}

	private void OnMouseEnter()
	{
		_onMouseEnter?.Invoke();
	}

	private void OnMouseExit()
	{
		_onMouseExit?.Invoke();
	}
}

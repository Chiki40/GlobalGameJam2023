using UnityEngine;
using UnityEngine.Events;

public class OnMouseDownEvent : MonoBehaviour
{

	[SerializeField]
	private UnityEvent _onMouseDown = null;

	[SerializeField]
	private UnityEvent _onMouseOver = null;

	private void OnMouseDown()
	{
		_onMouseDown?.Invoke();
	}

	private void OnMouseOver()
	{
		_onMouseOver?.Invoke();
	}
}

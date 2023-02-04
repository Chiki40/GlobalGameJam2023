using UnityEngine;
using UnityEngine.Events;

public class OnMouseDownEvent : MonoBehaviour
{

	[SerializeField]
	private UnityEvent _onMouseDown = null;

	private void OnMouseDown()
	{
		_onMouseDown?.Invoke();
	}
}

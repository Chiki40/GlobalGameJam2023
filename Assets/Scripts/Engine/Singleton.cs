using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : new()
{
    private static T _instance;
    public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new T();
			}
			return _instance;
		}
	}
}

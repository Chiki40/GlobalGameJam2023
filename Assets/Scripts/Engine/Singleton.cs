using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton _instance;
    public static Singleton Instance => _instance;

    protected virtual void Awake()
	{
        if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}

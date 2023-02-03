using UnityEngine;

public class Singleton : MonoBehaviour
{
    private Singleton _instance;
    public Singleton Instance => _instance;

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

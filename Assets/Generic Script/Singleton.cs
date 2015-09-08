using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;


	private static void FindInstance(){
		_instance = (T) FindObjectOfType(typeof(T));
	}

	public static T Instance
	{
		get
		{
			if(_instance == null)
				FindInstance();
			return _instance;
		}
	}
}
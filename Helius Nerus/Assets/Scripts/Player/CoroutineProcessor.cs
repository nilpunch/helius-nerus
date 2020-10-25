using System.Collections;
using UnityEngine;

public class CoroutineProcessor : MonoBehaviour
{
	public static CoroutineProcessor Instance { get; private set; } = null;

	public static void LaunchCoroutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(coroutine);
    }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
}

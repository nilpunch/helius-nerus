using System.Collections;
using UnityEngine;

public class PlayerLevelStartAnimation : MonoBehaviour
{
	public static event System.Action AnimationEnded = delegate { };

    public static PlayerLevelStartAnimation Instance
    {
        get;
        private set;
    } = null;

    private void Awake()
    {
        StartCoroutine(StartAnim());
        Instance = this;
    }

    private IEnumerator StartAnim()
    {
        Transform transform = Player.Instance.transform;
        transform.position = new Vector3(0.0f, -ParallaxCamera.ParallaxSize.y / 2 - 1, 0.0f);

        float timeElapsed = 0.0f;

        while (timeElapsed <= 2)
        {
            transform.position += new Vector3(0.0f, TimeManager.AnimationDeltaTime, 0.0f);
            timeElapsed += TimeManager.AnimationDeltaTime;
            yield return null;
        }

		EnemiesSpawner.InstanceOnScene.enabled = true;
        Player.Instance.IsNotMoving = false;
        Player.Instance.IsNotShooting = false;
    }

    public void EndLevelAnim()
    {
        StartCoroutine(EndAnim());
    }

    private IEnumerator EndAnim()
    {
        Transform transform = Player.Instance.transform;
        Player.Instance.IsNotMoving = true;
        Player.Instance.IsNotShooting = true;
        //transform.position = new Vector3(0.0f, -ParallaxCamera.ParallaxSize.y / 2 - 1, 0.0f);

        float timeElapsed = 0.0f;

        while (timeElapsed <= 2)
        {
            transform.position += new Vector3(0.0f, 2 * TimeManager.AnimationDeltaTime, 0.0f);
            timeElapsed += TimeManager.AnimationDeltaTime;
            yield return null;
        }

        BulletPoolsContainer.Instance.ClearAllBullets();

		AnimationEnded.Invoke();
	}
}

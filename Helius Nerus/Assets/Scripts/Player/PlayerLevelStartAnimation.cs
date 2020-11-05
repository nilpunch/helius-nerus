using System.Collections;
using UnityEngine;

public class PlayerLevelStartAnimation : MonoBehaviour
{
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
        Player.Instance.IsStatic = false;
        Player.Instance.IsNoShooting = false;
    }

    public void EndLevelAnim()
    {
        StartCoroutine(EndAnim());
    }

    private IEnumerator EndAnim()
    {
        Transform transform = Player.Instance.transform;
        Player.Instance.IsStatic = true;
        Player.Instance.IsNoShooting = true;
        //transform.position = new Vector3(0.0f, -ParallaxCamera.ParallaxSize.y / 2 - 1, 0.0f);

        float timeElapsed = 0.0f;

        while (timeElapsed <= 2)
        {
            transform.position += new Vector3(0.0f, 2 * TimeManager.AnimationDeltaTime, 0.0f);
            timeElapsed += TimeManager.AnimationDeltaTime;
            yield return null;
        }

        BulletPoolsContainer.Instance.ClearAllBullets();

        TransitionScene.Instance.LoadUnloadScene((int)Scenes.INGAME);
    }
}

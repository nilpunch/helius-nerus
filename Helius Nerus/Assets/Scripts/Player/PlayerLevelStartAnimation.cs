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
            transform.position += new Vector3(0.0f, Time.deltaTime, 0.0f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

		EnemiesSpawner.InstanceOnScene.enabled = true;
        Player.Instance.IsStaticAndNoShooting = false;
    }

    public void EndLevelAnim()
    {
        StartCoroutine(EndAnim());
    }

    private IEnumerator EndAnim()
    {
        Transform transform = Player.Instance.transform;
        Player.Instance.IsStaticAndNoShooting = true;
        //transform.position = new Vector3(0.0f, -ParallaxCamera.ParallaxSize.y / 2 - 1, 0.0f);

        float timeElapsed = 0.0f;

        while (timeElapsed <= 2)
        {
            transform.position += new Vector3(0.0f, Time.deltaTime * 2, 0.0f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }



        TransitionScene.Instance.LoadUnloadScene((int)Scenes.UPGRADES);
    }
}

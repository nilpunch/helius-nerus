using DG.Tweening;
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
		Player.CollideWithDamageDealers = true;
	}

	public void EndLevelAnim()
	{
		Transform transform = Player.Instance.transform;
		Player.Instance.IsNotMoving = true;
		Player.Instance.IsNotShooting = true;
		Player.CollideWithDamageDealers = false;
		//transform.position = new Vector3(0.0f, -ParallaxCamera.ParallaxSize.y / 2 - 1, 0.0f);


		//yield return transform.DOMove(transform.position.With(y:transform.position.y - 2f), 0.5f)
		//					  .SetEase<Tween>(Ease.OutSine)
		//					  .WaitForCompletion();
		transform.DOMove(transform.position.With(y: ParallaxCamera.CameraBoundary.yMax + 2f), 2f)
				 .SetEase<Tween>(Ease.InBack)
				 .OnComplete(() =>
				 {
					 BulletPoolsContainer.Instance.ClearAllBullets();
					 AnimationEnded.Invoke();
				 });
	}
}

using UnityEngine;

[System.Serializable]
public abstract class InputCanvas<T> : MonoBehaviour where T : IMoveInput
{
	public static InputCanvas<T> Instance { get; private set; }

	[SerializeField] protected Canvas Canvas = null;

	protected abstract string InputType { get; }

	protected virtual void Awake()
	{
		Instance = this;
		Deactivate();
	}

	protected virtual void OnEnable()
	{
		if (PlayerPrefs.GetString("InputType") != InputType)
        {
			Deactivate();
            return;
        }
        Player.PlayerDie += Player_PlayerDie;
        LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
	}

    protected virtual void OnDisable()
    {
        Player.PlayerDie -= Player_PlayerDie;
        LevelBoss.FinalBossDied -= LevelBoss_FinalBossDied;
    }

    private void LevelBoss_FinalBossDied(int obj)
    {
        Deactivate();
    }

    private void Player_PlayerDie()
    {
        Deactivate();
    }

    public static void Activate()
	{
		Instance.Canvas.enabled = true;
	}

	public static void Deactivate()
	{
		Instance.Canvas.enabled = false;
	}
}
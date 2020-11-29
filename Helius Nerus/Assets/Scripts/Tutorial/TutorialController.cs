using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialEnemy _tutorialEnemy = null;

    // STAGES
    private TutorialStage[] _stages;

    public TutorialEnemy TutorialEnemy
    {
        get => _tutorialEnemy;
    }

    private void Awake()
    {
        _stages = new TutorialStage[6];
        _stages[0] = new UIStage(this);
        _stages[1] = new ShootingStage(this);
        _stages[2] = new MovingStage(this);
        _stages[3] = new EnemyStage(this);
        _stages[4] = new UpgradeStage(this);
        _stages[5] = new MOdifiersAndPortalStage(this);
    }


    private void Start()
    {
        //ChangeStage();
        StartNextStage();
    }

    private int _currentStage = -1;

    public void ChangeStage()
    {
        _stages[_currentStage].EndStage();
    }

    public void StartNextStage()
    {
        _currentStage++;
        if (_currentStage <= _stages.Length - 1)
            _stages[_currentStage].StartStage();
        else
        {
            TransitionScene.Instance.LoadUnloadScene((int)Scenes.HUB);
        }
    }
}

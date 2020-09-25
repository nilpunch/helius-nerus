using UnityEngine;

public class TemporaryEnemy : MonoBehaviour
{
    [SerializeField] private MovementCommandsProcessor _processor = new MovementCommandsProcessor();

    private void Awake()
    {
        _processor.Initialize(transform);
    }

    private void Update()
    {
        _processor.Tick();
    }
}

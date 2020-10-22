using UnityEngine;

public class ButtonSceneLoading : MonoBehaviour
{
    [SerializeField] private Scenes _sceneToLoad = Scenes.INGAME;

    public void OnClick()
    {
        TransitionScene.Instance.LoadUnloadScene((int)_sceneToLoad);
    }
}

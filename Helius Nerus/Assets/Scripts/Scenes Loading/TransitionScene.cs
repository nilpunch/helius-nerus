using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScene : MonoBehaviour
{
    public static TransitionScene Instance;
    [SerializeField] private GameObject _loadingScreen = null;
    [SerializeField] private Image _progressBar = null;
    [SerializeField] private GameObject _cameraGO = null;
    private float _totalProgress;
    private int _newScene;
    private AsyncOperation _firstLoading;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        LoadOneScene((int)Scenes.MENU);
    }

    private void LoadOneScene(int newScene)
    {
        _scenesLoading.Add(SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive));
        _newScene = (int)newScene;

        StartCoroutine(GetOneSceneLoadProgress());
    }

    private IEnumerator GetOneSceneLoadProgress()
    {
        _cameraGO.SetActive(true);
        _loadingScreen.SetActive(true);

        for (int i = 0; i < _scenesLoading.Count; ++i)
        {
            while (!_scenesLoading[i].isDone)
            {
                _totalProgress = 0;
                foreach (AsyncOperation operation in _scenesLoading)
                {
                    _totalProgress += operation.progress;
                }
                _totalProgress = (_totalProgress / _scenesLoading.Count);
                _progressBar.fillAmount = _totalProgress;
                yield return null;
            }
        }

        _loadingScreen.SetActive(false);
        _cameraGO.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_newScene));

        _scenesLoading.Clear();
        //destroy loading screen?
        Destroy(_loadingScreen);
    }

    public void LoadUnloadScene(int newScene, int currentScene = -1)
    {
        if (currentScene == -1)
            currentScene = SceneManager.GetActiveScene().buildIndex;
        _scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        _scenesLoading.Add(SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive));
        _newScene = newScene;

        StartCoroutine(WaitForLoadUnloadScene());
    }

    private IEnumerator WaitForLoadUnloadScene()
    {
        _cameraGO.SetActive(true);

        for (int i = 0; i < _scenesLoading.Count; ++i)
        {
            while (!_scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        _cameraGO.SetActive(false);

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_newScene));
        _scenesLoading.Clear();
    }
}
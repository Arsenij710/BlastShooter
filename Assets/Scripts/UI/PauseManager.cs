using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private CanvasGroup _canvasGroup;

    private bool _isPause;
    public bool IsPause => _isPause;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();

        ResumeGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPause)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void ResumeGame()
    {
        _isPause = false;
        Time.timeScale = 1f;
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
    public void PauseGame()
    {
        _isPause = true;
        Time.timeScale = 0f;
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);

    }
}

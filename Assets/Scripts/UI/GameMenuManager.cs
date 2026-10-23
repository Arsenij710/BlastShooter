using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Panels")]
    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private CanvasGroup gameOverPanel;
    [SerializeField] private CanvasGroup victoryPanel;

    private bool _isPause = false;
    public bool IsPause => _isPause;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        HidePanel(pausePanel);
        HidePanel(gameOverPanel);
        HidePanel(victoryPanel);
        
    }
    private void Start()
    {
        playerHealth.OnDeath += ShowGameOver;
        EnemySpawner.Instance.OnVictory += ShowVictory;
    }
    private void OnDestroy()
    {
        playerHealth.OnDeath -= ShowGameOver;
        EnemySpawner.Instance.OnVictory -= ShowVictory;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPanelActive(gameOverPanel) || IsPanelActive(victoryPanel)) return;

            if (_isPause) ResumeGame();
            else PauseGame();
        }
    }
    public void PauseGame()
    {
        _isPause = true;
        Time.timeScale = 0f;
        ShowPanel(pausePanel);
    }
    public void ResumeGame()
    {
        _isPause = false;
        Time.timeScale = 1f;
        HidePanel(pausePanel);
    }
    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        ShowPanel(gameOverPanel);
    }

    public void ShowVictory()
    {
        Time.timeScale = 0f;
        ShowPanel(victoryPanel);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    private void ShowPanel(CanvasGroup group)
    {
        if (group == null) return;
        group.alpha = 1f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }
    private void HidePanel(CanvasGroup group)
    {
        if (group == null) return;
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    private bool IsPanelActive(CanvasGroup group)
    {
        return group != null && group.alpha > 0f;
    }
}

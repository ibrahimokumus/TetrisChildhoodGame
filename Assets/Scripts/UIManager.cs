using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool isGamePaused = false;
    public GameObject pausePanel;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanelTurnOnOff();
        }
    }

    public void pausePanelTurnOnOff()
    {
        if (gameManager.isGameOver)
        {
            return;
        }

        isGamePaused = !isGamePaused;
        if (pausePanel)
        {
            pausePanel.SetActive(isGamePaused);
            if (SoundManager.instance)
            {
                SoundManager.instance.makeSoundEffect(0);
                Time.timeScale = isGamePaused ? 0 : 1;// used for pause  
            }
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goBackMainMenu()
    {
        SceneManager.LoadScene(0);//loading main menu
    }
}

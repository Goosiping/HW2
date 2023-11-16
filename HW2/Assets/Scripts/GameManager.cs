using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private string[] gameScenes = { "Game 1", "Game 2", "Game 3", "Final"};
    private string menuScene = "Menu";
    private int _currentStage = 0;

    private static GameManager instance = null;

    public GameObject pauseCanvas;
    public GameObject BGM;
//<<<<<<< HEAD
    //public AudioSource audioSourceBGM;
    public GameObject gameOverCanvas;
//=======
    public static AudioSource audioSourceBGM;

//>>>>>>> c153a54b427befca63c9fe76519ceb6ed4a74238

    public static GameState state;
    public static float startTime;
    public static float finishTime;
    private float pauseTime;

    // Scene Changes
    private bool _pass = false;
    public static int previousHP = 100;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        state = GameState.Menu;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseCanvas);
        DontDestroyOnLoad(gameOverCanvas);
        DontDestroyOnLoad(BGM);
        audioSourceBGM = BGM.GetComponent<AudioSource>();
        //SceneManager.LoadScene(menuScene);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        previousHP = 100;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    public static void pause()
    {
        Time.timeScale = 0;
        instance.pauseTime = Time.time;
        state = GameState.Pause;
        audioSourceBGM.Pause();
        Cursor.lockState = CursorLockMode.None;
        instance.pauseCanvas.SetActive(true);
    }
    public static void resume()
    {
        Time.timeScale = 1;
        state = GameState.Playing;
        instance.pauseCanvas.SetActive(false);
        float totalPauseTime = Time.time - instance.pauseTime;
        startTime += totalPauseTime;
        Cursor.lockState = CursorLockMode.Locked;
        audioSourceBGM.Play();
    }
    public void startGame()
    {

        _currentStage = 0;
        state = GameState.Playing;
        SceneManager.LoadScene(gameScenes[_currentStage]);
        Cursor.lockState = CursorLockMode.Locked;
        startTime = Time.time;
        _pass = false;
    }
    public static void backToMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        state = GameState.Menu;
        SceneManager.LoadScene(instance.menuScene);
        instance.pauseCanvas.SetActive(false);
    }
    public void exit()
    {
        Application.Quit();
    }
    public static void checkNextStage()
    {
        instance.Invoke("_check", 5);
        
    }
    private void _check()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        if(enemies.Length == 0)
        {
            _pass = true;
        }
    }
    public static bool isPass()
    {
        return instance._pass;
    }
    public static void nextStage(int playerHP)
    {
        instance._currentStage += 1;
        //instance.previousHP = playerHP;
        SceneManager.LoadScene(instance.gameScenes[instance._currentStage]);
        instance._pass = false;
        if(instance._currentStage == instance.gameScenes.Length - 1)
        {
            finishTime = Time.time - startTime;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public static void gameOver(){
        instance.gameOverCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0;
    }
    public void restartGame(){
        //Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
        Destroy(BGM);
        Destroy(gameOverCanvas);
        Destroy(pauseCanvas);
        Destroy(gameObject);
        SceneManager.LoadScene(menuScene);
    }
}

public enum GameState
{
    Menu,
    Pause,
    Playing
}

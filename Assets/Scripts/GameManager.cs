using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private string game1Scene = "HW2 - Scene";
    private string menuScene = "Menu";
    private string game2Scene = "Scene2";
    private string game3Scene = "Scene3";

    private static GameManager instance = null;
    
    public GameObject pauseCanvas;
    public GameObject BGM;
    public AudioSource audioSourceBGM;

    //public GameObject player;
    public static GameState state;
    public static float startTime;
    private float pauseTime;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        state = GameState.Menu;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseCanvas);
        DontDestroyOnLoad(BGM);
        audioSourceBGM = BGM.GetComponent<AudioSource>();
        //SceneManager.LoadScene(menuScene);
        pauseCanvas.SetActive(false);
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
        instance.pauseTime = Time.time;
        state = GameState.Pause;
        instance.audioSourceBGM.Pause();
        Cursor.lockState = CursorLockMode.None;
        instance.pauseCanvas.SetActive(true);
    }
    public static void resume()
    {
        state = GameState.Playing;
        instance.pauseCanvas.SetActive(false);
        float totalPauseTime = Time.time - instance.pauseTime;
        startTime += totalPauseTime;
        Cursor.lockState = CursorLockMode.Locked;
        instance.audioSourceBGM.Play();
    }
    public void startGame()
    {
        state = GameState.Playing;
        Scene nextScene = SceneManager.GetSceneByName(game1Scene);
        SceneManager.LoadScene(game1Scene);
        Cursor.lockState = CursorLockMode.Locked;
        startTime = Time.time;
    }
    public void backToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        state = GameState.Menu;
        SceneManager.LoadScene(menuScene);
        instance.pauseCanvas.SetActive(false);
    }
    public void exit()
    {
        Application.Quit();
    }
}

public enum GameState
{
    Menu,
    Pause,
    Playing
}

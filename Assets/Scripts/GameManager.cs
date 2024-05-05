using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    private bool Playing = true;
    public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
    private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
    private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
    
    public GameObject deleteSnos;
    public GameObject ButtonPlay;
    public GameObject ButtonMainMenu;
    public GameObject Snos_Prefab;
    public GameObject SpiderSingle_Prefab;
    public GameObject SpiderTeam_Prefab;
    public GameObject Snos_Spawnpoint;
    public GameObject sniffTimerVisual;
    public GameObject HealthBar;

    public GameObject ScoreCounter;
    public AudioManager audioManager;

    public TextMeshProUGUI text;
    private bool playerWon = false;
    
    // void Awake()
    // {
    //     DontDestroyOnLoad(transform.gameObject);
    // }

    // Start is called before the first frame update
    void Start()
    {
        // Create the delays so they only have to be made once.
        //m_StartWait = new WaitForSeconds (m_StartDelay);
        m_EndWait = new WaitForSeconds (m_EndDelay);

        // Start Gameloop
        //StartCoroutine (GameLoop ());
    }

    void Update(){
        ScoreCounter.GetComponent<TextMeshProUGUI>().text = "Score: " + Globals.killCount;
    }

    private void OnSosModeEnabled()
    {
        Globals.sosModeEnabled = true;
        text.text = "SNOS";
    }

    private void OnEnable()
    {
        EnlargePupil.eyeFilled += () => playerWon = true;
        CheatCode.onCheatActivated += OnSosModeEnabled;
    }

    public void StartGame(){
        StartCoroutine (GameLoop());
    }

    private IEnumerator GameLoop (){
        text.text = "";
        ButtonPlay.SetActive(false);
        Destroy(deleteSnos);
        Globals.killCount = 0;
        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine (RoundPlaying());
        text.text = playerWon ? "You Won" : "Game Over";
        Time.timeScale = 0;
        ButtonMainMenu.SetActive(true);
        HealthBar.SetActive(false);
        sniffTimerVisual.SetActive(false);
        
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    // private IEnumerator MainMenu ()
    // {
    //     m_HealthBar.SetupHealthBar(15);
        
    //     // Wait for the specified length of time until yielding control back to the game loop.
    //     yield return m_StartWait;
    // }

    private IEnumerator RoundPlaying ()
        {
            HealthBar.SetActive(true);
            sniffTimerVisual.SetActive(true);
            ScoreCounter.SetActive(true);
            HealthBar.GetComponent<HealthBar>().SetupHealthBar(15);
            audioManager.startBackgroundMusic();
            

            GameObject Snos = Instantiate(Snos_Prefab, Snos_Spawnpoint.transform.position, Snos_Spawnpoint.transform.rotation) as GameObject;
            Snos.GetComponent<DamageManager>().healthBar = HealthBar.GetComponent<HealthBar>();
            Snos.GetComponent<DamageManager>().audioManager = audioManager;
            Snos.GetComponent<SnosStats>().audioManager = audioManager;
            Snos.GetComponent<ShootReloadController>().sniffTimerVisual = sniffTimerVisual;
            Snos.GetComponent<ShootReloadController>().audioManager = audioManager;

            StartCoroutine(SpawnInitSpiders());
            StartCoroutine(SpawnTeamSpiders());
            
            while (!HealthBar.GetComponent<HealthBar>().isDead() && !playerWon)
            {

                yield return null;
            }
        }


    private IEnumerator SpawnInitSpiders()
    {
        const int initialSpawnDelay = 2;
        var i = 1;
        while (true){
            GameObject spider = Instantiate(SpiderSingle_Prefab, transform.position + new Vector3(12f,Random.Range(-4.5f,4.5f),0f ), Quaternion.identity) as GameObject;
            i++;
            yield return new WaitForSeconds(Mathf.Pow(0.995f, i) * initialSpawnDelay);
        }
    }
    private IEnumerator SpawnTeamSpiders()
    {
        const int initialSpawnDelay = 20;
        var i = 1;
        while (true)
        {
            if (i == 1)
            {
                i++;
                yield return new WaitForSeconds(10);
            }
            var spawnTop = Random.Range(0, 1) > 0.5;
            var yLocation = spawnTop ? -6f : 6f;
            GameObject spiderTeam = Instantiate(SpiderTeam_Prefab, transform.position + new Vector3(Random.Range(-9f, 9f),yLocation,0f ), Quaternion.identity) as GameObject;
            i++;
            yield return new WaitForSeconds(Mathf.Pow(0.9f, i) * initialSpawnDelay);
        }
    }

    
}

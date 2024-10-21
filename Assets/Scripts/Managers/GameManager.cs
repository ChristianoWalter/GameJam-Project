using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Instances")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] GameObject firstBg;
    [SerializeField] GameObject player;
    int currentLevel;
    [HideInInspector] public bool gameStarted;
    [HideInInspector] public bool canPause;
    [HideInInspector] public bool transitioning;

    //[Header("Game Levels Objects")]
    //[SerializeField] List<GameObject> levelOneObjects;
    
    [Header("Transition Elements")]
    [SerializeField] Animator transitionAnim;
    [SerializeField] Animator startScreenAnim;

    private void Awake()
    {
        instance = this;
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().gameObject;
        }
        gameStarted = false;
        currentLevel = 0;
    }
    private void Start()
    {
        PlayerController.instance.canInput = false;
    }

    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
            {
                gameStarted = true;
                // diving_up
                startScreenAnim.SetTrigger("StartGame");
                PlayerController.instance.anim.SetTrigger("StartGame");
            }
        }
    }

    #region Level Transition Management
    IEnumerator LevelTransition()
    {
        canPause = false;
        PlayerController.instance.canMove = false;
        PlayerController.instance.rb.velocity = Vector2.zero;
        transitionAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.5f);
        backgrounds[currentLevel].SetActive(false);
        PlayerController.instance.ClearSpirits();
        InterfaceController.instance.DesactiveSpiritsCaught();
        currentLevel++;
        player.transform.position = spawnPoints[currentLevel].position;
        backgrounds[currentLevel].SetActive(true);
        yield return new WaitForSeconds(1);
        transitionAnim.SetTrigger("FadeOut");
        // bgmusic2
        PlayerController.instance.canMove = true;
        canPause = true;
    }

    public void CheckSpirits(int spiritsCount)
    {
        if (spiritsCount == 3)
        {
            // last fish
            if (currentLevel < 1)
            {
                StartCoroutine(LevelTransition());
            }
            else StartCoroutine(FinishGame());
        }
    }
    #endregion


    IEnumerator FinishGame()
    {
        transitionAnim.SetTrigger("FadeIn");

        PlayerController.instance.canMove = false;
        PlayerController.instance.rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        PlayerController.instance.ClearSpirits();
        yield return new WaitForSeconds(.5f);
        transitionAnim.SetTrigger("FadeOut");

        InterfaceController.instance.SwitchScreen((int)InterfaceController.Screens.creditsScreen);
    }

    public void StartGame()
    {
        StartCoroutine(StartingGame());
        IEnumerator StartingGame()
        {
            transitionAnim.SetTrigger("FadeIn");
            yield return new WaitForSeconds(1);
            PlayerController.instance.canMove = false;
            PlayerController.instance.rb.velocity = Vector2.zero;
            PlayerController.instance.canInput = true;
            firstBg.SetActive(false);
            player.transform.position = spawnPoints[currentLevel].position;
            backgrounds[currentLevel].SetActive(true);
            InterfaceController.instance.SwitchScreen((int)InterfaceController.Screens.gameScreen);
            yield return new WaitForSeconds(1);
            transitionAnim.SetTrigger("FadeOut");
            // bgmusic1
            gameStarted = true;
            PlayerController.instance.canMove = true;
        }
    }
}

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
    [SerializeField] GameObject player;
    int currentLevel;
    [HideInInspector] public bool gameStarted;
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
        PlayerController.instance.canInput = false;
    }

    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
            {
                gameStarted = true;
                startScreenAnim.SetTrigger("StartGame");
                //StartCoroutine(StartGame());
            }
        }
    }

    #region Level Transition Management
    IEnumerator LevelTransition()
    {
        PlayerController.instance.canMove = false;
        transitionAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        backgrounds[currentLevel].SetActive(false);
        currentLevel++;
        player.transform.position = spawnPoints[currentLevel].position;
        backgrounds[currentLevel].SetActive(true);
        yield return new WaitForSeconds(1);
        transitionAnim.SetTrigger("FadeOut");
        PlayerController.instance.canMove = true;
    }

    public void CheckSpirits(int spiritsCount)
    {
        if (spiritsCount == 3)
        {
            if(currentLevel < 2) StartCoroutine(LevelTransition());
            else StartCoroutine(FinishGame());
        }
    }
    #endregion


    IEnumerator FinishGame()
    {
        transitionAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        InterfaceController.instance.SwitchScreen((int)InterfaceController.Screens.configScreen);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        PlayerController.instance.canInput = true;
    }
}

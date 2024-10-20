using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Instances")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] GameObject player;
    int currentSpawnPoint;

    [Header("Transition Elements")]
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        instance = this;
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().gameObject;
        }
        currentSpawnPoint = 0;
        
    }

    IEnumerator LevelTransition()
    {
        PlayerController.instance.canMove = false;
        transitionAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        backgrounds[currentSpawnPoint].SetActive(false);
        currentSpawnPoint++;
        player.transform.position = spawnPoints[currentSpawnPoint].position;
        backgrounds[currentSpawnPoint].SetActive(true);
        yield return new WaitForSeconds(1);
        transitionAnim.SetTrigger("FadeOut");
        PlayerController.instance.canMove = true;
    }
}

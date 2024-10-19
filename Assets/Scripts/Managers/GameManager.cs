using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Instances")]
    [SerializeField] Transform[] spawnPoints;
    int currentSpawnPoint;

    [Header("Transition Elements")]
    [SerializeField] Animator transitionAnim;
}

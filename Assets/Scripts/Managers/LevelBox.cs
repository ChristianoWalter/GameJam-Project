using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBox : MonoBehaviour
{
    [SerializeField] int spiritsCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (spiritsCount <= PlayerController.instance.spiritsList.Count)
            {
                PlayerController.instance.ClearSpirits();
            }
        }
    }
}

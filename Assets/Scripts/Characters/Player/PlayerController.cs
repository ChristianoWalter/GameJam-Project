using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovementControl
{
    public static PlayerController instance;

    [Header("Player elements")]
    public List<GameObject> spiritsList = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void ClearSpirits()
    {
        for (int i = 0; i < spiritsList.Count; i++)
        {
            Destroy(spiritsList[i]);
        }
        spiritsList.Clear();
    }

    public void AddSpirits(GameObject _spirits)
    {
        spiritsList.Add(_spirits);
    }

    #region Input Methods
    public void MovementInput(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }
    #endregion
}

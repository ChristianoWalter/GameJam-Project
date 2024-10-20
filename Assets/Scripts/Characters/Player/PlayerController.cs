using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovementControl
{
    public static PlayerController instance;

    [Header("Player elements")]
    public List<GameObject> spiritsList = new List<GameObject>();
    [HideInInspector] public bool canInput;

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
        InterfaceController.instance.SetSpiritsCaught(spiritsList.Count);
    }

    #region Input Methods
    public void MovementInput(InputAction.CallbackContext value)
    {
        if (canInput) moveDirection = value.ReadValue<Vector2>();
    }

    public void PauseInput(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            InterfaceController.instance.PauseUnpause();
        }
    }
    #endregion
}

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
    [SerializeField] GameObject appearence;
    [SerializeField] GameObject trail;

    [Header("Sound Effects")]
    [SerializeField] private FMODUnity.EventReference onIsSwimming;
    private FMOD.Studio.EventInstance swimmingSfx;
    private bool isLooping;

    private void Awake()
    {
        instance = this;
        //canInput = true;
    }

    protected override void Start()
    {
        base.Start();
        isLooping = false;
    }

    protected override void Update()
    {
        base.Update();

        if (isMoving && !isLooping) // Movement Control field
        {
            isLooping = true;
            swimmingSfx = FMODUnity.RuntimeManager.CreateInstance(onIsSwimming);
            swimmingSfx.start();
            // FMODUnity.RuntimeManager.PlayOneShot(onIsSwimming);
        }
        else if (!isMoving)
        {
            isLooping = false;
            swimmingSfx.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            swimmingSfx.release();
        }
    }

    /*

    void OnDestroy()
    {
        FMOD.Studio.Bus playerBus = FMODUnity.RuntimeManager.GetBus("bus:/event");
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);

        swimmingSfx.release();
    }

    */

    public void StartGame()
    {
        appearence.SetActive(true);
        StartCoroutine(StartingRoutine());
        IEnumerator StartingRoutine()
        {
            GameManager.instance.StartGame();
            moveDirection.y = 2; 
            yield return new WaitForSeconds(1f);
            trail.SetActive(true);
            moveDirection.y = 0;
            anim.SetTrigger("EntranceFinish");
        }
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
        GameManager.instance.CheckSpirits(spiritsList.Count);
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

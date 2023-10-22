using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    Cinemachine.CinemachineTargetGroup targetGroup;
    [SerializeField]
    GameObject playerOne;
    [SerializeField]
    GameObject playerTwo;
    public bool coop;

    private PlayerControls playerController;
    private InputAction menu;
    public static bool isPaused;
    public static bool state;

    private void Awake()
    {
        playerController = new PlayerControls();
    }
    void Start()
    {
        isPaused = false;
        coop = MainMenu.coop;
        var Targets = targetGroup.m_Targets;      // arreglo que contiene los targets de la cámara
        Targets[0].target = playerOne.transform;
        if(coop)
        {
            playerTwo.SetActive(true);
            Targets[1].target = playerTwo.transform;
            Targets[0].radius = 3.0f;
        }
        Doors.player1 = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
        if(coop)
        {
            Doors.player2 = GameObject.Find("Player 2").GetComponent<CapsuleCollider2D>();
        }
    }

    public void ChangeState(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        if (isPaused)
            ActivateMenu();
        else
            DeactivateMenu();
    }
    private void OnEnable()
    {
        menu = playerController.PauseMenu.Pause;
        menu.Enable();
        menu.performed += ChangeState;
    }

    private void OnDisable()
    {
        menu.Disable();
    }
    void ActivateMenu()
    {
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
        canvas.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        canvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        StartCoroutine(Menu());
    }

    IEnumerator Menu()
    {
        yield return 1.0f;
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

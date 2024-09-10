using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOverpanel;
    [SerializeField] private TextMeshProUGUI GameOverText;
    [SerializeField] private TextMeshProUGUI ControlText;

    [SerializeField] private Playercontrol playercontrol;
    [SerializeField] private Playercontrol robotcontrol;
    [SerializeField] private Animator animator;
    [SerializeField] private Robot robot;
    private InputAction ControlAction;
    private bool isShowControl =false;
    private void Start() {
        ControlAction = new InputAction("control", binding: "<Keyboard>/T");
       ControlAction.Enable();
    }
    private void Update() {
        if(ControlAction.triggered)
        {
            isShowControl =!isShowControl;
            ControlText.enabled = isShowControl;
            GameOverpanel.SetActive(isShowControl);
            if(isShowControl)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;  
            }else{
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;  
            }
        }
    }
    public void GameOver() 
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;   //停止所有与时间有关的动作\
        GameOverText.enabled = true;
        GameOverpanel.SetActive(true);
    }
    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Replay()
    {
        Time.timeScale = 1; 
        GameOverText.enabled = false;
        GameOverpanel.SetActive(false);
        ControlText.enabled = false;
        isShowControl = false;
        robot.ExitRiding();
        robotcontrol.playerHP = 500f;
        playercontrol.playerHP = 100f;
        animator.SetTrigger("Idle");
        // playercontrol.
    }
}

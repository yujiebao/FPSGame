using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Robot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ExitMessage;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject RobotPrefeb;
    
    private InputAction exitAction;
    private void OnEnable() {
        ExitMessage.text = "Press E To Exit The Robot";
        StartCoroutine(ShowMessage());
    }
    // Start is called before the first frame update
    void Start()
    { 
        exitAction = new InputAction("Exit",binding:"<Keyboard>/E");
        exitAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // ExitMessage.enabled = true;
        if(exitAction.triggered)
        {
            ExitRiding();
        }
    }
    public void ExitRiding()
    {
       Player.transform.position = gameObject.transform.position; 
       Player.transform.rotation = gameObject.transform.rotation;
       gameObject.SetActive(false);
       Player.SetActive(true);
       RobotPrefeb.gameObject.SetActive(true);
       RobotPrefeb.transform.position = Player.transform.position;
    }
     private IEnumerator ShowMessage()
    {
        ExitMessage.enabled = true;
        yield return new WaitForSeconds(0.5f);
        ExitMessage.enabled = false;
    } 
}

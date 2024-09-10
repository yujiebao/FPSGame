using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RidingSystem : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI ridingMessage;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Robot;
    
     private InputAction rideAction;
     private void Start() 
     {
         rideAction = new InputAction("Ride",binding:"<Keyboard>/F");  
         rideAction.Enable();
     }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag =="Player")
        {
        ridingMessage.text = "Press F to Ride The Robot";
        ridingMessage.enabled = true;
        // gameObject.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        if(rideAction.triggered)
        {
            Ride();
        }
    }
        private void OnTriggerExit(Collider other) 
    {
        ridingMessage.enabled = false;
        // gameObject.SetActive(false);

    }
    private void Ride() 
    {
        Player.SetActive(false);
        Robot.SetActive(true);
        gameObject.SetActive(false);
    }
}

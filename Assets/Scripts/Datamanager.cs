using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 此脚本用于储存数据，如玩家血量，机器人血量，机器人位置等便于重新开始游戏，场景跳转....
/// </summary>
public class Datamanager : MonoBehaviour
{
    public static Datamanager Instance { get; private set;}
    [SerializeField] private Playercontrol Player;
    [SerializeField] private Playercontrol Robot;
    [SerializeField] private List<Gun> GunList;
    private float PlayerHP;
    private float RobotHP;
    private Transform PlayerPlace;
    private Transform RobotPlace;
    private void Start() {
        PlayerHP = Player.playerHP;
        RobotHP =  Robot.playerHP;
        PlayerPlace = Player.transform;
        RobotPlace = Robot.transform;
    }

}

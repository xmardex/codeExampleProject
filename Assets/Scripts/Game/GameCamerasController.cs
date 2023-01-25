using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameCamerasController : MonoBehaviour
{
    [SerializeField]
    private PlayerCamerasPreset camerasP1;
    [SerializeField]
    private PlayerCamerasPreset camerasP2;
    [SerializeField]
    private Camera generalSceneCamera;
}
[Serializable]
public class PlayerCamerasPreset
{
    public GameObject startGameCamera;
    public GameObject roundPlayCamera;
    public GameObject roundCastCamera;
    public GameObject roundResolutionCamera;
    public GameObject roundWinCamera;
    public GameObject roundLoseCamera;
}

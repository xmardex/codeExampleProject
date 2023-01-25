using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameInPreload : MonoBehaviour
{
    [SerializeField]
    private string firstGameScene;
    private void Start() {
        Preloader.Instance.LoadLevel(firstGameScene,false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScript : MonoBehaviour
{   
    public virtual void Initialize()
    {
        
    }
    public virtual void Activate(bool isOn)
    {
        gameObject.SetActive(isOn);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSingletonScript : Singleton<BaseSingletonScript>
{   
    protected GameObject mainObject;
    private void Awake() 
    {
        base.SetAsCrossScene();
    }
    protected virtual void Initialize()
    {
        
    }
    public virtual void Activate()
    {
        mainObject.SetActive(true);
    }
    public virtual void Deactivate()
    {
        mainObject.SetActive(false);
    }
}

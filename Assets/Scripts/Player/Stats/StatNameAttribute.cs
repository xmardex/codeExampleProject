using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatUIName : Attribute
{
    public StatUIName(string statName)
    {
        StatName = statName;
    }
    public string StatName {get;set;}
}

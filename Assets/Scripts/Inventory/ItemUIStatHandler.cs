using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemUIStatHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text statName;

    [SerializeField]
    private TMP_Text statValue;

    public void Initialize(string name, string value)
    {
        statName.text = name;
        statValue.text = value;
    }
}

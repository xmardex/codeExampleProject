using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Reflection;
public class ItemMoreInfoPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Item currentItem;
    [SerializeField]
    private Image itemIconImage;
    [SerializeField]
    private TMP_Text itemNameText;
    [SerializeField]
    private Transform statsRoot;
    [SerializeField]
    private GameObject buffStartLinePrefab;
    [SerializeField]
    private GameObject buffItemStatPrefab;
    [SerializeField]
    private GameObject debuffStartLinePrefab;
    [SerializeField]
    private GameObject debuffItemStatPrefab;

    private List<ItemStat> buffItemStats = new List<ItemStat>();
    private List<ItemStat> debuffItemStats = new List<ItemStat>();

    private List<GameObject> buffsGOs = new List<GameObject>();
    private List<GameObject> debuffsGOs = new List<GameObject>();
    private GameObject buffStartLine;
    private GameObject debuffStartLine;
    private List<ElementOfStat> elementOfStats = new List<ElementOfStat>();
    public void Initialize(Item item)
    {
        currentItem = item;
        itemIconImage.sprite = item.Sprite;
        itemNameText.text = item.Name;
        GenerateAllStats();
        InstantialeAllStats();
        Show();
    }
    void Show()
    {
        panel.SetActive(true);
    }
    public void Hide()
    {
        panel.SetActive(false);
    }
    private void InstantialeAllStats()
    {
        if(buffItemStats.Count>0)
        {
            buffStartLine = Instantiate(buffStartLinePrefab,statsRoot);
            foreach(ItemStat itemStat in buffItemStats)
            {
                GameObject buffStatGO = Instantiate(buffItemStatPrefab,statsRoot);
                ItemUIStatHandler itemUIStatHandler = buffStatGO.GetComponent<ItemUIStatHandler>();
                itemUIStatHandler.Initialize(itemStat.statName,itemStat.statValue.ToString());
                buffsGOs.Add(buffStatGO);
            }
        }
        if(debuffItemStats.Count>0)
        {
            debuffStartLine = Instantiate(debuffStartLinePrefab,statsRoot);
            foreach(ItemStat itemStat in debuffItemStats)
            {
                GameObject debuffStatGO = Instantiate(debuffItemStatPrefab,statsRoot);
                ItemUIStatHandler itemUIStatHandler = debuffStatGO.GetComponent<ItemUIStatHandler>();
                itemUIStatHandler.Initialize(itemStat.statName,itemStat.statValue.ToString());
                debuffsGOs.Add(debuffStatGO);
            }
        }
    }

    void ClearStatLists()
    {
        foreach(GameObject itemStatGO in buffsGOs)
        {
            Destroy(itemStatGO);
        }
        foreach(GameObject itemStatGO in debuffsGOs)
        {
            Destroy(itemStatGO);
        }

        buffItemStats.Clear();
        debuffItemStats.Clear();

        elementOfStats.Clear();

        if(buffStartLine)
        Destroy(buffStartLine);
        if(debuffStartLine)
        Destroy(debuffStartLine);
    }
    void GenerateAllStats()
    {
        ClearStatLists();
        var statsFields = typeof(PlayerStats).GetFields(BindingFlags.Public | BindingFlags.Instance).ToList();
        var allBuffsStats = currentItem.PlayerStatModifier.GetType()
                     .GetFields(BindingFlags.Public | BindingFlags.Instance)
                     .Select(field => field.GetValue(currentItem.PlayerStatModifier))
                     .ToList();
        var allDebuffsStats = currentItem.EnemyStatModifier.GetType()
                     .GetFields(BindingFlags.Public | BindingFlags.Instance)
                     .Select(field => field.GetValue(currentItem.EnemyStatModifier))
                     .ToList();

        foreach(FieldInfo fieldInfo in statsFields.ToArray())
        {
            int fieldIndex = statsFields.IndexOf(fieldInfo);
            if(fieldInfo.FieldType.IsClass)
            {
                string statName = ((StatUIName)Attribute.GetCustomAttribute(fieldInfo,typeof(StatUIName)))?.StatName;
                statsFields.InsertRange(fieldIndex,fieldInfo.FieldType.GetFields(BindingFlags.Public | BindingFlags.Instance));
                var buffFields = allBuffsStats[fieldIndex].GetType().GetFields().Select(field => field.GetValue(allBuffsStats[fieldIndex])).ToList();
                var debuffFields = allDebuffsStats[fieldIndex].GetType().GetFields().Select(field => field.GetValue(allDebuffsStats[fieldIndex])).ToList();

                foreach(object obj in buffFields)
                    elementOfStats.Add(new ElementOfStat(obj,statName));
                foreach(object obj in debuffFields)
                    elementOfStats.Add(new ElementOfStat(obj,statName));
                
                allBuffsStats.InsertRange(fieldIndex,buffFields);
                allDebuffsStats.InsertRange(fieldIndex,debuffFields);
            }
        }


        for (int i = 0; i < statsFields.Count; i++)
        {
            FieldInfo statFieldInfo = statsFields[i];
            string statName = ((StatUIName)Attribute.GetCustomAttribute(statFieldInfo, typeof(StatUIName)))?.StatName;
            
            string elementName = elementOfStats.Find(e=>e.obj == allBuffsStats[i])?.elementName;
            statName = elementName != null ? statName.Insert(0,$"{elementName} ") : statName;

            if(float.TryParse(allBuffsStats[i].ToString(), out float buffValue) && buffValue != 0)
            {
                string statValue = buffValue > 0 ? $"+{buffValue}" : $"{buffValue}"; 
                ItemStat buffItemStat = new ItemStat(statName,statValue);
                buffItemStats.Add(buffItemStat);
            }
            if(float.TryParse(allDebuffsStats[i].ToString(), out float debuffValue) && debuffValue != 0)
            {
                string statValue = debuffValue > 0 ? $"+{debuffValue}" : $"{debuffValue}"; 
                ItemStat debuffItemStat = new ItemStat(statName,statValue);
                debuffItemStats.Add(debuffItemStat);
            }
        }
    }
}
[Serializable]
public class ItemStat
{
    public string statName;
    public string statValue;

    public ItemStat(string statName, string statValue)
    {
        this.statName = statName;
        this.statValue = statValue;
    }
}
[Serializable]
public class ElementOfStat
{
    public object obj;
    public string elementName;

    public ElementOfStat(object obj, string elementName)
    {
        this.obj = obj;
        this.elementName = elementName;
    }
}


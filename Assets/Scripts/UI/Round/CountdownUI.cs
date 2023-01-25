using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
[RequireComponent(typeof(CanvasGroup))]
public class CountdownUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform numsRoot;
    [SerializeField]
    private TMP_Text secondsText;
    private CanvasGroup canvasGroup;
    private int secondsTo;
    [SerializeField]
    private float scaleMultiplier = 2;

    [SerializeField]
    private string startGameText;
    
    private Action<int> tempAction;

    public void Initialize() 
    {
        secondsTo = GameStateManager.Instance.TimeBeforeGameStart;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Activate(Action<int> subAction = null)
    {
        secondsText.text = secondsTo.ToString();
        tempAction = subAction;
        Timer.StartNewTimer("roundStartCountDown",secondsTo,onTimerTick:UpdateUI,onTimerFinish:LastTick);
    }
    void UpdateUI(string id, int seconds)
    {
        tempAction?.Invoke(seconds);
        StopAllCoroutines();
        secondsText.text = seconds.ToString();
        StartCoroutine(EaseUI());
    }
    void LastTick(string id, int seconds)
    {
        tempAction?.Invoke(seconds);
        StopAllCoroutines();
        secondsText.text = startGameText;
        StartCoroutine(EaseUI());
    }
    IEnumerator EaseUI()
    {
        float s = 0;
        float a = 1;

        while(s <= 1)
        {
            s += Time.deltaTime;
            a -= Time.deltaTime;
            numsRoot.localScale = new Vector3(s,s,s)*scaleMultiplier;
            canvasGroup.alpha = a;
            yield return null;
        }
    }
}

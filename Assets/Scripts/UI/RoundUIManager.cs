using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class RoundUIManager : Singleton<RoundUIManager>
{
    [SerializeField]
    private CountdownUI countdownUI;
    [SerializeField]
    private TMP_Text roundNumText;
    [SerializeField]
    private TMP_Text roundTimeText;
    private Action onRoundTimerEnd;

    [SerializeField]
    private RoundEndPanel roundEndPanel;

    public void Initialize()
    {
        countdownUI.Initialize();
        GlobalEventsManager.Instance.roundEvents.onGameOver += ShowEndRoundPanel;
    }
    public void StartNewRound(int roundNum,int roundDuration, Action onRoundTimerEnd)
    {
        this.onRoundTimerEnd = onRoundTimerEnd;
        roundNumText.text = roundNum.ToString("000");
        roundTimeText.text = roundDuration.ToString("00");

        Timer.StartNewTimer("roundTime",roundDuration,onTimerTick:UpdateRoundTimerUI,onTimerFinish:LastUpdateRoundTimerUI);
    }
    void UpdateRoundTimerUI(string id, int seconds)
    {
        roundTimeText.text = seconds.ToString();
    }
    void LastUpdateRoundTimerUI(string id, int seconds)
    {
        roundTimeText.text = seconds.ToString();
        onRoundTimerEnd?.Invoke();
    }
    public void StartPregameCountdown(Action<int> onCountdownUpdate = null)
    {
        countdownUI.Activate(onCountdownUpdate);
    }
    public void ShowEndRoundPanel(bool isAIMode, int playerWinNum)
    {
        roundEndPanel.ShowPanel(isAIMode,playerWinNum);
    }
    private void OnDestroy() {
        GlobalEventsManager.Instance.roundEvents.onGameOver -= ShowEndRoundPanel;
    }
}

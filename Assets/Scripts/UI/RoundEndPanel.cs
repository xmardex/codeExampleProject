using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoundEndPanel : MonoBehaviour
{
    [SerializeField,TextArea(1,3)]
    private string YouWin, YouLose, PlayerN_Win, Draw;

    [SerializeField]
    private string playerNumTagInText;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private TMP_Text textUI;

    [SerializeField]
    private GameObject defaultSelectionButton;

    void SetText(bool isAIMode, int playerWinNum)
    {
        if(isAIMode)
        {
            if(playerWinNum == 1)
            {
                textUI.text = YouWin;
            }
            if(playerWinNum == 2)
            {
                textUI.text = YouLose;
            }
        }
        else
        {
            string text = PlayerN_Win.Replace(playerNumTagInText,playerWinNum.ToString());
            textUI.text = text;
        }

        if(playerWinNum == 0)
        {
            textUI.text = Draw;
        }
    }
    public void ShowPanel(bool isAIMode, int playerNum)
    {
        SetText(isAIMode, playerNum);
        EventsSystemHelper.SelectUIElement(defaultSelectionButton);
        panel.SetActive(true);
    }
}

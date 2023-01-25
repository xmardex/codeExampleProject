using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListButtonSelector : MonoBehaviour
{
    [SerializeField]
    private bool _multiplySelects;
    [SerializeField]
    private Transform _rootObject;

    [SerializeField]
    private Button defaultBtn;
    public Button DefaultButton {get => defaultBtn; set => defaultBtn = value;}

    private List<Button> _allButtons = new List<Button>();
    private List<Button> _selectedButtons = new List<Button>();

    [SerializeField]
    private Color selected;
    [SerializeField]
    private Color deselected;

    public void Initialize()
    {
        _allButtons.AddRange(_rootObject.GetComponentsInChildren<Button>());
        foreach(Button btn in _allButtons)
        {
            btn.onClick.AddListener(() => OnSelect(btn));
        }
        DeselectAll();
        if(defaultBtn)
            SelectBtn(defaultBtn);
    }
    
    void SelectBtn(Button btn)
    {

        // Color baseBtnColor = btn.image.color;
        // baseBtnColor.a = 1f;
        // btn.image.color = baseBtnColor;
        btn.image.color = selected;

        if(!_selectedButtons.Contains(btn))
            _selectedButtons.Add(btn);
    }
    void DeselectBtn(Button btn)
    {

        // Color baseBtnColor = btn.image.color;
        // baseBtnColor.a = 0.5f;
        // btn.image.color = baseBtnColor;
        btn.image.color = deselected;

        if(_selectedButtons.Contains(btn))
            _selectedButtons.Remove(btn);
    }
    void DeselectAll()
    {
        foreach(Button btn in _allButtons)
        {
            DeselectBtn(btn);
        }
    }
    void OnSelect(Button btn)
    {
        if(_multiplySelects)
        {
            if(_selectedButtons.Contains(btn))
            {
                DeselectBtn(btn);
            }
            else
            {
                SelectBtn(btn);
            }
        }
        else
        {
            DeselectAll();
            SelectBtn(btn);
        }

    }
}

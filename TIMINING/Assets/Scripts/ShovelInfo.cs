using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelInfo : MonoBehaviour
{
    [Header("Info")]
    public Text _shovelID;
    public Text _shovelName;
    [Header("Performance")]
    public Image _fillPerformance;
    public Text _percentPerformance;
    public Image _fillPlanned;
    public Text _percentPlanned;
    [Header("State")]
    public Text _start;
    public Text _end;
    public Text _state;
    [Header("Arrows")]
    public GameObject _leftArrow;
    public GameObject _rightArrow;
    [MMReadOnly] public int _index;
    public void RestartGlobalSettings()
    {
        _leftArrow.SetActive(false);
        _rightArrow.SetActive(false);
        GameManager.Instance._popUpIsOpen = false;
    }
    void Start()
    {
        Next();
    }
    
    public void Next()
    {
        //First find if the current report have more than 1 last state
        for (int i = 0; i < GameManager.Instance._reportManager._jsonReports.Reports.Length; i++)
        {
            if (GameManager.Instance._reportManager._jsonReports.Reports[i].ShovelID == GameManager.Instance._currentSelectedItemID)
            {
                if (GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates.Length > 0)
                {
                    if (GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates.Length > 1)
                    {
                        _leftArrow.SetActive(true);
                        _rightArrow.SetActive(true);
                    }
                    //Go to next last state.
                    _index += 1;
                    if (_index > GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates.Length - 1)
                        _index = 0;
                    GameManager.Instance._popupOpener.FillData(_index);
                }
            }
        }
    }
    public void Back()
    {
        for (int i = 0; i < GameManager.Instance._reportManager._jsonReports.Reports.Length; i++)
        {
            if (GameManager.Instance._reportManager._jsonReports.Reports[i].ShovelID == GameManager.Instance._currentSelectedItemID)
            {
                if (GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates.Length > 0)
                {
                    //Go to back last state.
                    _index -= 1;
                    if (_index < 0)
                        _index = GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates.Length - 1;
                    GameManager.Instance._popupOpener.FillData(_index);
                }
            }
        }
    }
}

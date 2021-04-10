using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Make a singleton class
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [Header("Global Settings")]
    [MMReadOnly] public bool _popUpIsOpen;
    [MMReadOnly] public string _currentSelectedItemID;
    [MMReadOnly] public string _currentSelectedItemName;
    [Header("Scripts")]
    public CanvasShow _canvas;
    public ReportManager _reportManager;
    public ShovelManager _shovelManager;
    public PopupOpener _popupOpener;
    void Start()
    {
        Invoke("InitialFade", 1f);
    }
    public void InitialFade()
    {
        StartCoroutine(_canvas.FadeAlpha(1f, 0f, 2f));
    }
}
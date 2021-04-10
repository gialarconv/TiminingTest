using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShow : MonoBehaviour
{
    CanvasGroup canvas;
    [SerializeField, Range(0, 5.0f)]
    private float fadeSpeed = 0.3f;
    bool isScreenHidden;
    public bool _initialFade;
    void Awake()
    {
        if (_initialFade)
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 1;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
        else
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
    }
    public void CanvasChange(bool noFade = false)
    {
        isScreenHidden = !isScreenHidden;

        if (isScreenHidden)
        {
            StartCoroutine(FadeAlpha(0, 1, fadeSpeed));
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
        else
        {
            StartCoroutine(FadeAlpha(1, 0, fadeSpeed));
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
    }
    public IEnumerator FadeAlpha(float from, float to, float duration)
    {
        float elaspedTime = 0f;

        while (elaspedTime <= duration)
        {
            elaspedTime += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
            yield return null;
        }
        canvas.alpha = to;
    }
}

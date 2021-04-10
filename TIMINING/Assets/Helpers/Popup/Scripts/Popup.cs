using System.Collections;
using UnityEngine;
public class Popup : MonoBehaviour
{
    private GameObject m_background;

    //Close the current popup.
    public void Close()
    {
        var animator = GetComponent<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            animator.Play("Close");
        StartCoroutine(RunPopupDestroy());
        FindObjectOfType<PopupOpener>().Remove();
    }
    private IEnumerator RunPopupDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(m_background);
        Destroy(gameObject);
    }
}
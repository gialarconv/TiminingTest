using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

/*
with this script, you can select each object in scene, containing the popupopener script
*/
public class MouseManager : MonoBehaviour
{
    public LayerMask _layerMask;
    public float _length;
    [MMReadOnly] public GameObject _currentGFX;
    public AudioSource _audioSource;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Show KPI On Click
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !GameManager.Instance._popUpIsOpen)
        {
            if (Physics.Raycast(ray, out hit, _length, _layerMask))
            {
                GameManager.Instance._popUpIsOpen = true;
                //Get ID form object name.
                GameManager.Instance._currentSelectedItemID = hit.collider.name.Split(' ').Last();
                //Get Name from object.
                GameManager.Instance._currentSelectedItemName = hit.collider.GetComponentInChildren<LookAt>()._text.text;
                GetComponent<PopupOpener>().Spawn();
                _audioSource.Play();
            }
        }
        //Using this function to change animation of each shovels.
        if (Physics.Raycast(ray, out hit, _length, _layerMask) && !GameManager.Instance._popUpIsOpen)
        {
            _currentGFX = hit.collider.gameObject;
            hit.collider.GetComponentInChildren<LookAt>()._idle = false;
            hit.collider.GetComponentInChildren<LookAt>()._onHover = true;
        }
        else if (_currentGFX != null)
        {
            _currentGFX.GetComponentInChildren<LookAt>()._idle = true;
            _currentGFX.GetComponentInChildren<LookAt>()._onHover = false;
            _currentGFX = null;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class PopupOpener : MonoBehaviour
{
    [SerializeField] private AssetReference _assetReference;
    public List<GameObject> _completedObj = new List<GameObject>();
    private Canvas m_canvas;
    void Start()
    {
        m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    public void Spawn()
    {
        _assetReference.InstantiateAsync().Completed += OnLoadDone;
    }
    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        // Add the Addressable object into a public list of gameobject.
        _completedObj.Add(obj.Result);
        for (int i = 0; i < _completedObj.Count; i++)
        {
            //Change the prefab name.
            _completedObj[i].gameObject.transform.localScale = Vector3.zero;
            _completedObj[i].gameObject.transform.SetParent(m_canvas.transform, false);
            FillData(0);
        }
    }
    //Fill method.
    public void FillData(int _indexer)
    {
        for (int i = 0; i < GameManager.Instance._reportManager._jsonReports.Reports.Length; i++)
        {
            if (GameManager.Instance._reportManager._jsonReports.Reports[i].ShovelID == GameManager.Instance._currentSelectedItemID)
            {
                for (int x = 0; x < _completedObj.Count; x++)
                {
                    //Set the Shovel ID
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._shovelID.text = GameManager.Instance._reportManager._jsonReports.Reports[i].ShovelID;
                    //Set the Shovel Name
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._shovelName.text = GameManager.Instance._currentSelectedItemName;
                    //Set the Shovel Amount Settings
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._fillPerformance.fillAmount = float.Parse(GameManager.Instance._reportManager._jsonReports.Reports[i].Performance) / Mathf.Round(float.Parse(GameManager.Instance._reportManager._jsonReports.Reports[i].PlannedPerformance));
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._percentPerformance.text = Mathf.Round(float.Parse(GameManager.Instance._reportManager._jsonReports.Reports[i].Performance)).ToString();
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._fillPlanned.fillAmount = float.Parse(GameManager.Instance._reportManager._jsonReports.Reports[i].PlannedPerformance) / Mathf.Round(float.Parse(GameManager.Instance._reportManager._jsonReports.Reports[i].PlannedPerformance));
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._percentPlanned.text = Mathf.Round(float.Parse(GameManager.Instance._reportManager._jsonReports.Reports[i].PlannedPerformance)).ToString();
                    //Parse the json Start string to a Date Time string and show into scene view.
                    string startTimeString = GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates[_indexer].Start;
                    System.DateTime startTime = System.DateTime.Parse(startTimeString);
                    //Parse the json End string to a Date Time string and show into scene view.
                    string endTimeString = GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates[_indexer].End;
                    System.DateTime endTime = System.DateTime.Parse(endTimeString);
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._state.text = $"<color={GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates[_indexer].Color}>" + GameManager.Instance._reportManager._jsonReports.Reports[i].LastStates[_indexer].Name + "</color>";
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._start.text = startTime.ToString("dd/MM/yyyy - h:mm:ss tt");
                    _completedObj[x].gameObject.GetComponent<ShovelInfo>()._end.text = endTime.ToString("dd/MM/yyyy - h:mm:ss tt");
                }
            }
        }
    }
    //Remove item from object list.
    public void Remove()
    {
        _completedObj.Clear();
    }
}
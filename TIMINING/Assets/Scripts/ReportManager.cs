using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class ReportManager : MonoBehaviour
{
    [MMReadOnly] public string _url = "https://unity-exercise.dt.timlabtesting.com/data/report";
    public ReportsList _jsonReports = new ReportsList();
    public bool _testing;
    private string file_path;
    void Start()
    {
        if (_testing)
        {
            JsonTest();
        }
        else
        {
            StartCoroutine(GetJsonData());
        }
    }
    private void JsonTest()
    {
        file_path = Application.streamingAssetsPath + "/Reports.json";
        var jsonData = File.ReadAllText(file_path);
        _jsonReports = JsonUtility.FromJson<ReportsList>(jsonData);
    }
    IEnumerator GetJsonData()
    {
        using (UnityWebRequest webData = UnityWebRequest.Get(_url))
        {
            yield return webData.SendWebRequest();
            if (webData.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webData.error);
            }
            else
            {
                string response = System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data);
                _jsonReports = JsonUtility.FromJson<ReportsList>(response);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class ShovelManager : MonoBehaviour
{
    [MMReadOnly] public string _url = "https://unity-exercise.dt.timlabtesting.com/data/report";
    public ShovelsList _jsonReports = new ShovelsList();
    public GameObject _shovelGFX;
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
        file_path = Application.streamingAssetsPath + "/Shovels.json";
        var jsonData = File.ReadAllText(file_path);
        _jsonReports = JsonUtility.FromJson<ShovelsList>(jsonData);
        Invoke("Instantiate3DModel", .5f);
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
                _jsonReports = JsonUtility.FromJson<ShovelsList>(response);
                //Invoke the Instantiate Method for each position.
                Invoke("Instantiate3DModel", 0f);
            }
        }
    }
    private void Instantiate3DModel()
    {
        for (int i = 0; i < _jsonReports.Shovels.Length; i++)
        {
            //Tnstantiate a prefab for each object in the array
            var _vector = new Vector3(float.Parse(_jsonReports.Shovels[i].Position.x), float.Parse(_jsonReports.Shovels[i].Position.y), float.Parse(_jsonReports.Shovels[i].Position.z));
            FindObjectOfType<Spawner>().Spawn(_vector, Quaternion.identity);
        }

    }
}

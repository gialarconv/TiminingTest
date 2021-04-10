using Dummiesman;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
public class RequestTerrain : MonoBehaviour
{
    [MMReadOnly] public string _url = "https://unity-exercise.dt.timlabtesting.com/data/mesh-obj";
    [Tooltip("The name for Terrain")] public string _name;
    void Start()
    {
        StartCoroutine(GetJsonData());
    }
    IEnumerator GetJsonData()
    {
        //Get Json from API.
        using (UnityWebRequest webData = UnityWebRequest.Get(_url))
        {
            yield return webData.SendWebRequest();
            if (webData.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webData.error);
            }
            else
            {
                JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));
                Debug.Log(jsonData);
                //After download and get info from Json, download the obj and texture.
                StartCoroutine(Get(jsonData["ObjUrl"], jsonData["MtlUrl"], jsonData["TextureUrl"]));
            }
        }
    }
    //Get All files from API Object - Material - Texture
    IEnumerator Get(string obj, string _mat, string _texture)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(obj))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                //Using this class to import the info from API and convert to a new Mesh.
                var textStream = new MemoryStream(Encoding.UTF8.GetBytes(uwr.downloadHandler.text));
                var loadedObj = new OBJLoader().Load(textStream, _name);
            }
        }
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_mat))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                //Using this class to import the info from API and convert to a new Material.
                var textStream = new MemoryStream(Encoding.UTF8.GetBytes(uwr.downloadHandler.text));
                var loadedMtl = new MTLLoader().Load(textStream);
            }
        }
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_texture))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(uwr);
                //Find the current Object download, and set the texture.
                GameObject.Find(_name).GetComponentInChildren<Renderer>().material.mainTexture = texture;
            }
        }
    }
}

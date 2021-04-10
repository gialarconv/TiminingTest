using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class Spawner : MonoBehaviour
{
    public ShovelManager _shovelManager;
    [SerializeField] private AssetReference _assetReference;
    public List<GameObject> _completedObj = new List<GameObject>();
    public void Spawn(Vector3 pos, Quaternion rot)
    {
        _assetReference.InstantiateAsync(pos, rot).Completed += OnLoadDone;

    }
    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        // Add the Addressable object into a public list of gameobject.
        _completedObj.Add(obj.Result);
        for (int i = 0; i < _completedObj.Count; i++)
        {
            //Change the prefab name.
            _completedObj[i].gameObject.name = "Shovel " + _shovelManager._jsonReports.Shovels[i].ID;
            //Change the Title name of each prefab.
            _completedObj[i].gameObject.GetComponentInChildren<LookAt>()._text.text = _shovelManager._jsonReports.Shovels[i].Name;
        }
    }
}

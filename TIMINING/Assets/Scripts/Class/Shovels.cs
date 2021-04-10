using UnityEngine;

[System.Serializable]
public class ShovelsList
{
    public Shovels[] Shovels;
}

[System.Serializable]
public class Shovels
{
    public string ID, Name;
    public Position Position;
}
[System.Serializable]
public class Position
{
    public string x, y, z;
}
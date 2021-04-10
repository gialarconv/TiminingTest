using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReportsList
{
    public Reports[] Reports;
}
[System.Serializable]
public class Reports
{
    public string ShovelID, Performance, PlannedPerformance;
    public LastStates[] LastStates;
}
[System.Serializable]
public class LastStates
{
    public string Start, End, Name, Color;
}

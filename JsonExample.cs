using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;
using System;

[System.Serializable]
public class EANs
{
    public String value;
}

[System.Serializable]
public class EANinfo
{
    public List<EANs> EAN;
}

private void processResponse(string responsetext)
{
    string jsonResponse = reader.ReadToEnd();
    // JsonUtility will make response serializable: Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a ...Info object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.
    EANinfo info = JsonUtility.FromJson<EANinfo>(jsonResponse);

    Debug.Log(message: info);
    return info;
}
# Unity-REST-KG
This is a short tutorial on how to retrieve SPARQL query results of a knowledge graph via RESTful API in Unity.

Since this project is applied to the HoloLens 1, I use Unity version 2018.4.23 and UnityWebRequest(https://docs.unity3d.com/540/Documentation/Manual/UnityWebRequest.html).
Older versions could use the WWW package, versions 2019 and higher can use HTTPWebRequest.

The communication file RESTget is in c#.

First we need to set up libraries to be used. Then in the main class we define a List<string> EANList, which is a list of strings that we receive from the REST call. This might be different if you receive json objects or xml files.
The URL string is the actual REST call to a given URL ( http://localhost:7200/repositories/K4R_RtoP ). Here I query my knowledge graph for all EANs that have a certain ingredient ( action=QUERY? ... )

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RESTGet : MonoBehaviour
{
    List<string> EANlist;
    string URL = "http://localhost:7200/repositories/K4R_RtoP?action=QUERY?name=ProductwithFragranceAllergen&infer=true&sameAs=true&query=PREFIX%20rdfs%3A%20%3Chttp%3A%2F%2Fwww.w3.org%2F2000%2F01%2Frdf-schema%23%3E%0APREFIX%20rdf%3A%20%3Chttp%3A%2F%2Fwww.w3.org%2F1999%2F02%2F22-rdf-syntax-ns%23%3E%0APREFIX%20owl%3A%20%3Chttp%3A%2F%2Fwww.w3.org%2F2002%2F07%2Fowl%23%3E%0Aprefix%20pp%3A%20%3Chttp%3A%2F%2Fknowrob.org%2Fkb%2FProductPoses.owl%23%3E%0Aprefix%20dm%3A%20%3Chttp%3A%2F%2Fknowrob.org%2Fkb%2Fdmproducts.owl%23%3E%0Aprefix%20gr%3A%20%3Chttp%3A%2F%2Fpurl.org%2Fgoodrelations%2Fv1%23%3E%0A%0Aselect%20distinct%20%3FEAN%20%7B%20%0A%20%20%20%20%3Finstance%20gr%3AhasEAN_UCC-13%20%3FEAN.%0A%20%20%20%20%3Finstance%20dm%3AhasIngredient%20%3Fallergen.%0A%20%20%20%20%3Fallergen%20rdf%3Atype%20dm%3AFragrance.%0A%7D%20%0A";

```

For this demo I call my coroutine to make the REST call in start() with the defined URL. If you want an action to invoke this, you need to call it Object.OnSelected(), for example. 
```
// Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData(URL));
    }
```

This is the getData coroutine, given the REST call as argument. A new UnityWebRequest is initialized, then sent. If the call was successful, processResponse is called. It is given the result in text format (webRequest.downloadHandler.text). See the Unity manual for more on the donloadHandler (https://docs.unity3d.com/ScriptReference/Networking.DownloadHandler.html).
```
IEnumerator getData(string uri)
    {
        Debug.Log("Processing Website");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Call/Request website and wait to finish download
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log("No response from website");
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                processResponse(webRequest.downloadHandler.text);
            }
        }
    }
```

Here is the last part of this tutorial - to process the response. I put in an example of how to process a json response (please note that for a json response you also need to define the json structure in serializable classes beforehand).
To simplify this tutorial, I simply ask if the response contains a EAN.
```
private void processResponse(string responsetext)
    {
        //if you get a json response you neet to implement the json handler: 
        //jsonData respdata = JsonUtility.FromJson<jsonData>(responsetext);
        

        //Handler to go through result list
        if (responsetext.Contains("4058172179648"))
        {
            Debug.Log("EAN found");
        }
        else
        {
            Debug.Log("EAN not found");
        }
       
    }
```

With the Debug.Log statements I get "Processing Website" when entering the coroutine. You get "No response from website" if there are problems with the REST statement itself (check if you make the correct call). If you don't get anything, you might have problems with your communication.
If you are using a localhost like I do in this example, make sure that it is running.
If everything works correctly, you should see something like this:
[Logresponse](Debug.Logresponse.png)

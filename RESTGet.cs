using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class RESTGet : MonoBehaviour
{
    List<string> EANlist;
    string URL = "http://localhost:7200/repositories/K4R_RtoP?action=QUERY?name=ProductwithFragranceAllergen&infer=true&sameAs=true&query=PREFIX%20rdfs%3A%20%3Chttp%3A%2F%2Fwww.w3.org%2F2000%2F01%2Frdf-schema%23%3E%0APREFIX%20rdf%3A%20%3Chttp%3A%2F%2Fwww.w3.org%2F1999%2F02%2F22-rdf-syntax-ns%23%3E%0APREFIX%20owl%3A%20%3Chttp%3A%2F%2Fwww.w3.org%2F2002%2F07%2Fowl%23%3E%0Aprefix%20pp%3A%20%3Chttp%3A%2F%2Fknowrob.org%2Fkb%2FProductPoses.owl%23%3E%0Aprefix%20dm%3A%20%3Chttp%3A%2F%2Fknowrob.org%2Fkb%2Fdmproducts.owl%23%3E%0Aprefix%20gr%3A%20%3Chttp%3A%2F%2Fpurl.org%2Fgoodrelations%2Fv1%23%3E%0A%0Aselect%20distinct%20%3FEAN%20%7B%20%0A%20%20%20%20%3Finstance%20gr%3AhasEAN_UCC-13%20%3FEAN.%0A%20%20%20%20%3Finstance%20dm%3AhasIngredient%20%3Fallergen.%0A%20%20%20%20%3Fallergen%20rdf%3Atype%20dm%3AFragrance.%0A%7D%20%0A";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData(URL));
    }

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

    
}

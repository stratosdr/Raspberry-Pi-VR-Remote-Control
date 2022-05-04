using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class InfoBehavior : MonoBehaviour
{
    const float SPEED = 6f;
    [SerializeField]
    Transform SectionInfo;

    Vector3 desiredScale = Vector3.zero;

    public bool Yonoff = true;
    public bool Bonoff = true;

    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
    }
    public void OpenInfo()
    {
        desiredScale = Vector3.one;
        if(gameObject.name == "YellowButton") {
            if (Yonoff == true) {
                StartCoroutine(GetRequest("http://192.168.1.4:8000/ledYlw/on"));
                Debug.Log("Yellow on");
                Yonoff = false;
            }
            else {
                StartCoroutine(GetRequest("http://192.168.1.4:8000/ledYlw/off"));
                Debug.Log("Yellow off");
                Yonoff = true;
            }
        }
        if(gameObject.name == "BlueButton") {
            if (Bonoff == true) {
                StartCoroutine(GetRequest("http://192.168.1.4:8000/ledBlu/on"));
                Debug.Log("Blue on");
                Bonoff = false;
            }
            else {
                StartCoroutine(GetRequest("http://192.168.1.4:8000/ledBlu/off"));
                Debug.Log("Blue off");
                Bonoff = true;
            }
        }
        
    }

    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
        StopAllCoroutines();
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
}

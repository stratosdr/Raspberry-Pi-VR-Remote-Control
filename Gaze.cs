using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    List<InfoBehavior> infos = new List<InfoBehavior>();
    public bool button = true;
    float time;
    float timeDelay;

    void Start()
    {
        time = 0f;
        timeDelay = 3f;
        infos = FindObjectsOfType<InfoBehavior>().ToList();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                GameObject go = hit.collider.gameObject;
                if (go.CompareTag("hasInfo") && button == true)
                {
                    OpenInfo(go.GetComponent<InfoBehavior>());

                }
                else
                {
                    CloseAll();
                }
            }
        }
    }
    void OpenInfo(InfoBehavior desiredInfo)
    {   
            foreach (InfoBehavior info in infos)
            {
                if (info == desiredInfo && button == true)
                {
                    StartCoroutine(Delay());
                    StartCoroutine(Switch(button));
                    Debug.Log("button = " + button);
                    info.OpenInfo();
                }
                else
                {
                    info.CloseInfo();
                }
            }
    }
    void CloseAll()
    {
        foreach (InfoBehavior info in infos)
        {
            info.CloseInfo();
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(10.0f);
    }
    IEnumerator Switch(bool button)
    {
        button = !button;
        yield return button;
    }
}

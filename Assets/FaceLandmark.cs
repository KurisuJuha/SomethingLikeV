using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;


public class FaceLandmark : MonoBehaviour
{
    public List<Landmark> Landmarks;

    void Start()
    {
        StartCoroutine(getFaceLandmark());
    }

    void Update()
    {
        
    }

    private IEnumerator getFaceLandmark()
    {
        string url = @"http://192.168.11.30:62711/landmark";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        string data = request.downloadHandler.text;
        if (request.error == null)
        {
            Parse(data);
        }

        StartCoroutine(getFaceLandmark());
    }

    private void Parse(string data)
    {
        Landmarks = new List<Landmark>();
        string[] datas_str = data.Split(',');

        int i = 0;
        float x = 0;
        float y = 0;
        foreach (var d in datas_str)
        {
            float f = float.Parse(d);

            if (i == 0)
            {
                x = f;
            }
            else
            {
                y = f;
                Landmarks.Add(new Landmark(x, y));
                i = 0;
            }
            
            i++;
        }
    }
}

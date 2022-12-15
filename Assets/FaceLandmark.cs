using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;


public class FaceLandmark : MonoBehaviour
{
    public static List<Landmark> Landmarks;
    public GameObject obj;
    public List<GameObject> objects;

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
        
        // 描画
        Normalize();
        if (Landmarks.Count != 0)
        {
            for (int i = 0; i < Landmarks.Count; i++)
            {
                if (objects.Count <= i)
                {
                    objects.Add(Instantiate(obj));
                }

                objects[i].transform.position = new Vector3(Landmarks[i].X, Landmarks[i].Y, 0) * 10;
            }
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
                            
                i++;
            }
            else
            {
                y = f;
                Landmarks.Add(new Landmark(x, y * -1));
                i = 0;
            }
        }
    }

    private void Normalize()
    {
        float x_max = Landmarks[0].X;
        float x_min = Landmarks[0].X;
        float y_max = Landmarks[0].Y;
        float y_min = Landmarks[0].Y;

        float width;
        float height;

        // 各最低値と各最高値を取得
        foreach (var landmark in Landmarks)
        {
            if (x_max < landmark.X)
            {
                x_max = landmark.X;
            }
            if (x_min > landmark.X)
            {
                x_min = landmark.X;
            }
            if (y_max < landmark.Y)
            {
                y_max = landmark.Y;
            }
            if (y_min > landmark.Y)
            {
                y_min = landmark.Y;
            }
        }

        width = x_max - x_min;
        height = y_max - y_min;
        
        // 顔の位置を中央にする
        foreach (var landmark in Landmarks)
        {
            landmark.X -= x_min + width / 2;
            landmark.X /= height;
            landmark.Y -= y_min + height / 2;
            landmark.Y /= height;
        }
    }
}

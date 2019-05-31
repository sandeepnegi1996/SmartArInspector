using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class Telemetry3
{
    public string DeviceId;

    public string DeviceName;

    public int Temperature;

    public int Humidity;

    public int Load;

    public int Pressure;

    public string Date;

}

public class plane : MonoBehaviour
{
    private string myString = "Object = Cube\n Color = Red";
    public Text myText, myText1;
    public float fadeTime;
    public bool displayInfo;
    public GameObject panel;
    // public GameObject panel_health;
    //public GameObject panel_image;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;


    public void Start()
    {
        StartCoroutine(GetRequest("https://telemetry2service.azurewebsites.net/api/Telemetry/recentdata"));
        //GetRequest("https://telemetry2service.azurewebsites.net/api/Telemetry/recentdata");
        myText = GameObject.Find("Text4").GetComponent<Text>();
        Debug.Log("hello world");

        //myText.color = Color.clear;

    }
    public void Update()
    {
        StartCoroutine(GetRequest("https://telemetry2service.azurewebsites.net/api/Telemetry/recentdata"));
        //GetRequest("https://telemetry2service.azurewebsites.net/api/Telemetry/recentdata");

    }


    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else

            {
                // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }

            string x = webRequest.downloadHandler.text;
            //Debug.Log(x);

            //  Telemetry obj = JsonUtility.FromJson<Telemetry>(x);
            //Debug.Log(obj.temperature);

            var jsonObject = JsonConvert.DeserializeObject<List<Telemetry3>>(x);
            // myText.text = obj[0].temperature.ToString();
            // Debug.Log(obj[0].temperature + "  " + "  Temperature  " + obj[0].Id);
            //  Debug.Log(obj[1].temperature + "  " + "Temperature " + obj[1].Id);

            if (displayInfo)
            {
                //string str = "1234";
                string s1 = "DeviceId : ";
                string s2 = "\n\nName : ";
                string s3 = "\n\nTemperature : ";
                string s4 = "\n\nHumidity : ";
                string s5 = "\n\nLoad : ";
                string s6 = "\n\nPressure : ";
                string s7 = "\n\n DateAndTime:";


                string str = s1 + jsonObject[0].DeviceId + s2 + jsonObject[0].DeviceName + s3 + jsonObject[0].Temperature.ToString() + s4 + jsonObject[0].Humidity.ToString() + s5 + jsonObject[0].Load.ToString() + s6 + jsonObject[0].Pressure.ToString() + s7 + jsonObject[0].Date;
                myText.text = str;

                myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);

                if (panel != null)
                {
                    panel.SetActive(true);
                    panel1.SetActive(true);
                    panel2.SetActive(true);
                    panel3.SetActive(true);
                    //   panel_health.SetActive(true);
                    //  panel_image.SetActive(true);

                }





            }

        }
        //   yield return new WaitForSeconds(5);
    }


}

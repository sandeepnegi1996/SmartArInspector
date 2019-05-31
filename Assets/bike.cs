using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class Telemetry12
{
    public string IdMachine1;

    // public string DeviceName;

    public int Speed;

    public int Distance;

    // public int Load;

    // public int Pressure;

    public int Fuel;

}


public class bike : MonoBehaviour
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
        StartCoroutine(GetRequest("https://sarirestapi.azurewebsites.net/api/Telemetry/recentBikeData"));

        myText = GameObject.Find("Text3").GetComponent<Text>();
        Debug.Log("hello world");

        //myText.color = Color.clear;

    }
    public void Update()
    {
        StartCoroutine(GetRequest("https://sarirestapi.azurewebsites.net/api/Telemetry/recentBikeData"));


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
            Debug.Log(x);

            //Telemetry obj = JsonUtility.FromJson<Telemetry>(x);
            //Debug.Log(obj.temperature);

            var jsonObject = JsonConvert.DeserializeObject<List<Telemetry12>>(x);
            // myText.text = jsonObject[0].Temperature.ToString();
            // Debug.Log(jsonObject[0].DeviceId);
            //  Debug.Log(obj[1].temperature + "  " + "Temperature " + obj[1].Id);

            if (displayInfo)
            {
                //string str = "1234";
                string s1 = "MachineName : ";
                string s2 = "\n\nSpeed : ";
                string s3 = "\n\nDistance : ";
                string s4 = "\n\nFuel: ";



                string str = s1 + jsonObject[0].IdMachine1+ s2 + jsonObject[0].Speed.ToString() + s3 + jsonObject[0].Distance.ToString() + s4 + jsonObject[0].Fuel.ToString();
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

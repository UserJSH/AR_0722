using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;

public class APIdata : MonoBehaviour
{
    string key = "lLB%2Fvbg70mX6CdplRHM%2FXqOtl9nUIL0eJMNpBp3ks1Oli%2FpzuLEJ4XOS61xZSO3DYH9Z2JUCmIzJM0qlkxd3fA%3D%3D";

    private void OnGUI()
    {
        if (GUILayout.Button("코로나 감염 현황"))
        {
            StartCoroutine(SendCorona());
        }
    }

    IEnumerator SendCorona()
    {
        string url = $"http://openapi.data.go.kr/openapi/service/rest/Covid19/getCovid19InfStateJson?serviceKey={key}&pageNo=1&numOfRows=10&startCreateDt=20200310&endCreateDt=20200315";

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {

        }
        else
        {
            print(www.downloadHandler.text);

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(www.downloadHandler.text);

            if(xmldoc != null)
            {
                XmlNodeList nodes = xmldoc.SelectNodes("response/body/items/item");

                foreach (XmlNode node in nodes)
                {
                    print(node.SelectSingleNode("./decideCnt").InnerText);
                    print(node.SelectSingleNode("./examCnt").InnerText);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToURL : MonoBehaviour
{
    [SerializeField] private Text word; // InputFiled Text
    
    
    public void OpenURL()
    {
        Application.OpenURL($"https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=0&ie=utf8&query={word.text}");
    }

}

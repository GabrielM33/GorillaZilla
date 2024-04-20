using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveDisplay : MonoBehaviour
{
    public GameObject holder;
    public TextMeshProUGUI txt_WaveNum;
    public void ShowMessage(string message)
    {
        holder.SetActive(true); 
        txt_WaveNum.text = message;
    }
    public void Hide()
    {
        holder.SetActive(false);
    }

}

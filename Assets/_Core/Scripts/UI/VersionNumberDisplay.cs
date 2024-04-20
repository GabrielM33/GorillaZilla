using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionNumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI txt_Display;
    // Start is called before the first frame update
    void Start()
    {
        txt_Display.text = "Version " + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

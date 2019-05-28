using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNumber : MonoBehaviour
{
    int number = 2;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 20), " "+  number);
    }

}

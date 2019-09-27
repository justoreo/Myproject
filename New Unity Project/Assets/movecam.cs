using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecam : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 movdir = Vector3.zero;
    public float camspeed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Translate(-camspeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Translate(0, 0, camspeed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(0, 0, -camspeed * Time.deltaTime);
        }

    }
}

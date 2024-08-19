using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    void Start()
    {
        
    }

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyEvent.Post(gameObject);
        }
    }
}
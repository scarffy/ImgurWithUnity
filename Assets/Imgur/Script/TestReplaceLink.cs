using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReplaceLink : MonoBehaviour
{
    public string url = "somestring/{0}";

    // Start is called before the first frame update
    void Start()
    {
        // This work as well
        url = string.Format("somestring/{0}", "picture.png");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

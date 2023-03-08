using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : MonoBehaviour
{
    [SerializeField]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
    }
}

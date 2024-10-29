using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    //public float _camLerp = 10f;


    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - Player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + Player.position.z);
        transform.position = newPosition; // Vector3.Lerp(transform.position, newPosition, _camLerp*Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    private Transform player;
    public float smooth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        smooth = 5;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (player.position.x > 0 && player.position.x < 18)
        {
            Vector3 newCameraPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newCameraPosition, smooth * Time.deltaTime);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Target;
    public float delay;
    public Vector3 Offset;

    public float panSpeed = 0.1f;

    bool drag;

    private void FixedUpdate()
    {

        ///if(drag == false)
        ///{
        ///transform.position = Vector3.Lerp(
        ///    transform.position,
        ///    Target.position + Offset,
        ///    1 / delay
        ///    );
        ///}
    }

    public void Start()
    {
        StartCoroutine(IdeeDeZinzin()); 
    }

    IEnumerator IdeeDeZinzin()
    {
        yield return new WaitForSeconds(0.001f);

        if (drag == false)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                Target.position + Offset,
                1 / delay
                );
        }

        StartCoroutine(IdeeDeZinzin());

    }

    private void Update()
    {
        if (PlayerMovement.instance.Zoomable) {
            if (Input.GetMouseButton(1)) // right mouse button
            {
                drag = true;
                var newPosition = new Vector3();
                if (newPosition.x > -25 && newPosition.x < 75)
                {
                    newPosition.x = Input.GetAxis("Mouse X") * panSpeed * Camera.main.orthographicSize / 2;
                }
                if (newPosition.y > -150f && newPosition.y < 300f)
                {
                    newPosition.y = Input.GetAxis("Mouse Y") * panSpeed * Camera.main.orthographicSize / 2;
                }

                // translates to the opposite direction of mouse position.
                transform.Translate(-newPosition);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (Camera.main.orthographicSize <= 20)
                    Camera.main.orthographicSize += 0.5f;

            }
            // ---------------Code for Zooming In------------------------
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (Camera.main.orthographicSize >= 1)
                    Camera.main.orthographicSize -= 0.5f;
            }
        }
        else
        {
            drag = false;
            Camera.main.orthographicSize = 5;
        }
       
    }
}

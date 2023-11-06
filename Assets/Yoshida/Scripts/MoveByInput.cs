using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByInput : MonoBehaviour
{
    private void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.z = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move.z = -1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x = 1f;
        }

        transform.position += 4f * Time.deltaTime * move;
    }
}

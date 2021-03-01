using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwivel : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;
    private float currentSwivelAxis;

    private void GetSwivelInput()
    {
        currentSwivelAxis = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentSwivelAxis--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentSwivelAxis++;
        }
    }

    void Update()
    {
        GetSwivelInput();

        if (!Mathf.Approximately(currentSwivelAxis, 0))
        {
            transform.Rotate(0, rotateSpeed * currentSwivelAxis * Time.deltaTime, 0);
        }
    }
}

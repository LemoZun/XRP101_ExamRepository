using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotater : MonoBehaviour
{
    private void Update()
    {
        if(!GameManager.Instance.isPaused)
            transform.Rotate(Vector3.up * GameManager.Instance.Score);
    }
}

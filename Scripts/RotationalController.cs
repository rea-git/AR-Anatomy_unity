
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class RotationController : MonoBehaviour
{
    public GameObject OrganObj;
    public Vector3 RotationVector;

    private void Update()
    {
        OrganObj.transform.Rotate(RotationVector * Time.deltaTime);
    }
}


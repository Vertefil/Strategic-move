using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuildButn : MonoBehaviour
{
    public GameObject build;

    public void PlaceBuild()
    {
        Instantiate(build, Vector3.zero, Quaternion.identity);
    }
}

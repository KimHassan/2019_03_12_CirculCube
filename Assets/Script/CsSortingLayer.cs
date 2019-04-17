using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSortingLayer : MonoBehaviour
{
    TextMesh t;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingOrder = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

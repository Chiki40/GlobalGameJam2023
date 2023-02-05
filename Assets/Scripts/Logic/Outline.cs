using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    // Start is called before the first frame update
    public int matIndex = 0;
    Material mat;
    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[matIndex];
    }

    // Update is called once per frame
    public void outline(bool enable)
    {
        mat.SetFloat("_alpha", enable ? 1 : 0);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiredCaraID : MonoBehaviour
{
    [SerializeField]
    private string _id;

    public string ID
    {
        get { return _id; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaraID : MonoBehaviour
{
    [SerializeField]
    private string _id;

    public string ID
    {
        get { return _id; }
        set { _id = value; }
    }
}

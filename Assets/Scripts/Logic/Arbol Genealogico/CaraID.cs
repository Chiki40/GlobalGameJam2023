using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaraID : MonoBehaviour
{
    [SerializeField]
    private int _id;

    public int ID
    {
        get { return _id; }
    }
}

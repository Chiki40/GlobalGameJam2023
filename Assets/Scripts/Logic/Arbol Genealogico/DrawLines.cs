using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class DrawLines : MonoBehaviour
{
    [SerializeField]private Transform[] lines;

    [SerializeField] private GameObject linesParent;

    [SerializeField] private float margin = 4;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < lines.Length; i = i +2)
        {
            Transform t1 = lines[i];
            Transform t2 = lines[i + 1];

            if(Mathf.Abs(t1.position.x - t2.position.x) < margin)
            {
                DrawVertical(t1, t2);
            }
            else
            {
                DrawHorizontal(t1, t2);
            }
        }
    }
    

    private void DrawVertical(Transform t1, Transform t2)
    {
        GameObject line = new GameObject("line Vertical");
        line.transform.parent = linesParent.transform;
        line.transform.position = t1.transform.position;
        RectTransform rt = line.GetComponent<RectTransform>();
    }

    private void DrawHorizontal(Transform t1, Transform t2)
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            GameObject go = new GameObject();
            go.transform.position = Input.mousePosition;
        }
    }
}

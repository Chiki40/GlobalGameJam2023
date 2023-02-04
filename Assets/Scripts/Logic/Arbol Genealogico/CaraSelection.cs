using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaraSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private TreeManager _treeManager;

    public void SelectButton(GameObject selection)
    {
        _treeManager.SelectCara(selection);
    }

    public void RemoveCara(string id)
    {
        id = id.ToLower();
        for(int i = 0; i < _content.transform.childCount; ++i)
        {
            GameObject child = _content.transform.GetChild(i).gameObject;
            string childID = child.GetComponent<CaraID>().ID.ToLower();
            if(id == childID)
            {
                child.SetActive(false);
                return;
            }
        }
    }

    public void AddCara(string id)
    {
        id = id.ToLower();
        for (int i = 0; i < _content.transform.childCount; ++i)
        {
            GameObject child = _content.transform.GetChild(i).gameObject;
            string childID = child.GetComponent<CaraID>().ID.ToLower();
            if (id == childID)
            {
                child.SetActive(true);
                return;
            }
        }
    }
}

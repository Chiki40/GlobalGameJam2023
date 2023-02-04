using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    private GameObject _selectedGO;
    [SerializeField]
    private CaraSelection _caraSelection;

    public void SelectCara(GameObject go)
    {
        _selectedGO = go;
    }

    public void SelectTreePosition(GameObject go)
    {
        if(go.GetComponent<CaraID>() != null)
        {
            _selectedGO = null;
            RemoveCara(go);
        }
        else
        {
            InsertCara(go);
        }
    }

    private void InsertCara(GameObject go)
    {
        if(_selectedGO != null)
        {
            _caraSelection.RemoveCara(_selectedGO.GetComponent<CaraID>().ID);//remove from selection
            CaraID cara = go.AddComponent<CaraID>();
            cara.ID = _selectedGO.GetComponent<CaraID>().ID;
            ChangeCara(go, _selectedGO);
            _selectedGO = null;
        }
    }

    private void RemoveCara(GameObject go)
    {
        _caraSelection.AddCara(go.GetComponent<CaraID>().ID);//add to selection
        Destroy(go.GetComponent<CaraID>());
        ResetCara(go);
    }

    private void ChangeCara(GameObject target, GameObject source)
    {
        Image img = target.GetComponent<Image>();
        img.color = Color.white;
        img.sprite = source.GetComponent<Image>().sprite;
    }

    private void ResetCara(GameObject go)
    {
        Image img = go.GetComponent<Image>();
        img.sprite = null;
    }

    public void CheckTree()
    {
        DesiredCaraID[] allElements = GetComponentsInChildren<DesiredCaraID>();

        bool allCorrect = true;

        for(int i = 0; i < allElements.Length; ++i)
        {
            if (allElements[i].gameObject.GetComponent<CaraID>() == null)
            {
                allCorrect = false;
                //Debug.Log("no hay cara en el boton => " + allElements[i].ID);
                continue;
            }

            CaraID cara = allElements[i].gameObject.GetComponent<CaraID>();
            if(cara.ID.ToLower() == allElements[i].ID.ToLower())
            {
                Debug.Log("la cara esta bien => " + cara.ID);
            }
            else
            {
                allCorrect = false;
                Debug.Log("la cara esta mal => " + cara.ID);
            }
        }

        if(allCorrect)
        {
            Debug.Log("Game over");
        }
    }
}

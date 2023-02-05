using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    private GameObject _selectedGO;
    [SerializeField]
    private CaraSelection _caraSelection;
    [SerializeField]
    private Color _freeSpriteColor = Color.white;

    [SerializeField]
    private int _timesSetCara = 3;

    private void Start()
    {
        //reset everything
        _selectedGO = null;
        DesiredCaraID[] allElements = GetComponentsInChildren<DesiredCaraID>();

        for (int i = 0; i < allElements.Length; ++i)
        {
            RemoveCaraFromTree(allElements[i].ID, allElements[i].gameObject);
            allElements[i].GetComponent<Button>().enabled = true;
        }
    }

    public void SelectCara(GameObject go)
    {
        if(_selectedGO != null)
        {
            _caraSelection.AddCara(_selectedGO.GetComponent<CaraID>().ID);//add to selection
        }
        UtilSound.Instance.PlaySound("SelectTreeAvatar");
        _selectedGO = go;
    }

    public void SelectTreePosition(GameObject go)
    {
        CaraID caraId = go.GetComponent<CaraID>();
        if (caraId != null)
        {
            RemoveCaraFromTree(go, playAudio:_selectedGO == null);
            if (_selectedGO != null)
			{
                InsertCaraOnTree(go);
            }
        }
        else
        {
            InsertCaraOnTree(go);
        }
    }

    private void InsertCaraOnTree(GameObject go)
    {
        if(_selectedGO != null)
        {
            UtilSound.Instance.PlaySound("PlaceTreeAvatar");
            CaraID selectedID = _selectedGO.GetComponent<CaraID>();
            _caraSelection.RemoveCara(selectedID.ID);//remove from selection
            CaraID cara = go.AddComponent<CaraID>();
            cara.ID = selectedID.ID;
            ChangeCaraOnTree(go, _selectedGO);
            _selectedGO = null;
            CheckTree();
        }
    }

    private void RemoveCaraFromTree(GameObject go, bool playAudio=false)
    {
        RemoveCaraFromTree(go.GetComponent<CaraID>().ID, go, playAudio);
    }

    private void RemoveCaraFromTree(string id, GameObject go, bool playAudio=false)
    {
        if (playAudio)
		{
            UtilSound.Instance.PlaySound("DeleteTreeAvatar");
		}
        _caraSelection.AddCara(id);//add to selection
        ResetCaraOnTree(go);
    }

    private void ChangeCaraOnTree(GameObject target, GameObject source)
    {
        Image img = target.GetComponent<Image>();
        img.color = Color.white;
        img.sprite = source.GetComponent<Image>().sprite;
    }

    private void ResetCaraOnTree(GameObject go)
    {
        Image img = go.GetComponent<Image>();
        img.sprite = null;
        img.color = _freeSpriteColor;
        CaraID caraID = go.GetComponent<CaraID>();
        if (caraID != null)
        {
            Destroy(caraID);
        }
    }

    public void CheckTree()
    {
        DesiredCaraID[] allElements = GetComponentsInChildren<DesiredCaraID>();

        int numCorrect = 0;

        List<GameObject> _carasCorrect = new List<GameObject>();

        for(int i = 0; i < allElements.Length; ++i)
        {
            if (allElements[i].gameObject.GetComponent<CaraID>() == null)
            {
                continue;
            }

            CaraID cara = allElements[i].gameObject.GetComponent<CaraID>();
            if(cara.ID.ToLower() == allElements[i].ID.ToLower())
            {
                ++numCorrect;
                _carasCorrect.Add(cara.gameObject);
            }
        }
      
        if(numCorrect == allElements.Length)
        {
            SceneManager.LoadScene("Victory");
        }

    }
}

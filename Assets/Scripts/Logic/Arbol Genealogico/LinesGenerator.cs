using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LinesGenerator : MonoBehaviour
{
    public GameObject[] Casados;
    public Hermanos[] _hermanos;
    public Hijos[] _hijos;

    public int down = 50;
    public int up = 10;

    public Sprite sprite;
    public float LineThickness = 8;
    public Color _color;
    public Material _material;

    [System.Serializable]
    public struct Hermanos
    {
        public GameObject[] _hermanos;
    }

    [System.Serializable]
    public struct Hijos
    {
        public int _idPadres;
        public int _idHijos;
    }

    // Start is called before the first frame update
    void Start()
    {
        paintParejas();
        paintHermanos();
        paintHijos();
    }

    private void paintParejas()
    {
        for (int i = 0; i < Casados.Length; i = i + 2)
        {
            GameObject p1 = Casados[i];
            GameObject p2 = Casados[i + 1];

            Vector2 startP1 = new Vector2();
            startP1.x = p1.transform.position.x + p1.GetComponent<RectTransform>().rect.width / 2;
            startP1.y = p1.transform.position.y + p1.GetComponent<RectTransform>().rect.height / 2;

            Vector2 endP1 = new Vector2();
            endP1.x = startP1.x;
            endP1.y = startP1.y - down;

            Vector2 startP2 = new Vector2();
            startP2.x = p2.transform.position.x + p2.GetComponent<RectTransform>().rect.height / 2;
            startP2.y = startP1.y;//asi no hay saltos raros

            Vector2 endP2 = new Vector2();
            endP2.x = startP2.x;
            endP2.y = startP2.y - down;

            List<Vector2> points = new List<Vector2>();
            points.Add(startP1);
            points.Add(endP1);
            points.Add(endP2);
            points.Add(startP2);

            GameObject go = new GameObject("lines => " + i);
            go.transform.SetParent(this.transform);
            UILineTextureRenderer lineRenderer = go.AddComponent<UILineTextureRenderer>();
            lineRenderer.Points = points.ToArray();
            lineRenderer.sprite = sprite;
            lineRenderer.LineThickness = LineThickness;
            lineRenderer.color = _color;
            lineRenderer.material = _material;
        }
    }

    private void paintHermanos()
    {
        for (int i = 0; i < _hermanos.Length; ++i)
        {
            float posicionCental = 0;
            for (int j = 0; j < _hermanos[i]._hermanos.Length; ++j)
            {
                posicionCental += _hermanos[i]._hermanos[j].transform.position.x + _hermanos[i]._hermanos[j].GetComponent<RectTransform>().rect.width / 2;
            }
            posicionCental /= _hermanos[i]._hermanos.Length;
            float startY = _hermanos[i]._hermanos[0].transform.position.y + _hermanos[i]._hermanos[0].GetComponent<RectTransform>().rect.height / 2;

            for (int j = 0; j < _hermanos[i]._hermanos.Length; ++j)
            {
                List<Vector2> points = new List<Vector2>();

                Vector2 start = new Vector2();
                start.x = _hermanos[i]._hermanos[j].transform.position.x + _hermanos[i]._hermanos[j].GetComponent<RectTransform>().rect.width / 2; ;
                start.y = startY;
                points.Add(start);

                Vector2 startUp = new Vector2();
                startUp.x = start.x;
                startUp.y = start.y + up;
                points.Add(startUp);

                Vector2 center = new Vector2();
                center.x = posicionCental;
                center.y = start.y + up;
                points.Add(center);

                GameObject go = new GameObject("lines => " + i);
                go.transform.SetParent(this.transform);
                UILineTextureRenderer lineRenderer = go.AddComponent<UILineTextureRenderer>();
                lineRenderer.Points = points.ToArray();
                lineRenderer.sprite = sprite;
                lineRenderer.LineThickness = LineThickness;
                lineRenderer.color = _color;
                lineRenderer.material = _material;
            }

        }
    }

    private void paintHijos()
    {
        for (int i = 0; i < _hijos.Length; ++i)
        {
            GameObject p1 = Casados[_hijos[i]._idPadres].gameObject;

            float mitadHijos = 0;

            for (int j = 0; j < _hermanos[_hijos[i]._idHijos]._hermanos.Length; ++j)
            {
                mitadHijos += _hermanos[_hijos[i]._idHijos]._hermanos[j].transform.position.x + _hermanos[_hijos[i]._idHijos]._hermanos[j].GetComponent<RectTransform>().rect.width / 2;
            }

            mitadHijos /= _hermanos[_hijos[i]._idHijos]._hermanos.Length;
            List<Vector2> points = new List<Vector2>();

            GameObject hijo = _hermanos[_hijos[i]._idHijos]._hermanos[0];

            Vector2 point1 = new Vector2(mitadHijos, p1.transform.position.y + p1.GetComponent<RectTransform>().rect.height / 2 - down);
            Vector2 point2 = new Vector2(mitadHijos, hijo.transform.position.y + hijo.GetComponent<RectTransform>().rect.height / 2 + up);

            points.Add(point1);
            points.Add(point2);

            GameObject go = new GameObject("lines => " + i);
            go.transform.SetParent(this.transform);
            UILineTextureRenderer lineRenderer = go.AddComponent<UILineTextureRenderer>();
            lineRenderer.Points = points.ToArray();
            lineRenderer.sprite = sprite;
            lineRenderer.LineThickness = LineThickness;
            lineRenderer.color = _color;
            lineRenderer.material = _material;

        }
    }

}

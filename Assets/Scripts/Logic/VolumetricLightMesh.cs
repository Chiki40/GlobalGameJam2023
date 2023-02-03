using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Light))]
#if UNITY_EDITOR
[ExecuteAlways]
#endif
public class VolumetricLightMesh : MonoBehaviour
{
    [SerializeField]
    private MeshFilter _meshFilter = null;
    [SerializeField]
    private Light _light = null;
    [SerializeField]
    private float _maximumOpacity = 0.25f;

    private void Start()
    {
        if (_meshFilter == null)
        {
            _meshFilter = this.GetComponent<MeshFilter>();
        }
        if (_light == null)
        {
            _light = this.GetComponent<Light>();
        }

        _meshFilter.mesh = BuildMesh();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
        {
            _meshFilter.mesh = BuildMesh();
        }
    }
#endif

    private Mesh BuildMesh()
    {
        Mesh mesh = new Mesh();

        float farPosition = Mathf.Tan(_light.spotAngle / 2.0f * Mathf.Deg2Rad) * _light.range;
        mesh.vertices = new Vector3[]
        {
            Vector3.zero,
            new Vector3(farPosition, farPosition, _light.range),
            new Vector3(-farPosition, farPosition, _light.range),
            new Vector3(-farPosition, -farPosition, _light.range),
            new Vector3(farPosition, -farPosition, _light.range)
        };
        mesh.colors = new Color[]
        {
            new Color(_light.color.r, _light.color.g, _light.color.b, _light.color.a * _maximumOpacity),
            Color.clear,
            Color.clear,
            Color.clear,
            Color.clear
        };
        mesh.triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 1
        };

        return mesh;
    }
}
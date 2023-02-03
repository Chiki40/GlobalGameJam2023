using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField]
    private Transform _transform = null;
    [SerializeField]
    private float _rotationSpeed = 2.0f;

    private void Update()
    {
        transform.localEulerAngles += new Vector3(0.0f, _rotationSpeed * Time.deltaTime, 0.0f);
    }
}

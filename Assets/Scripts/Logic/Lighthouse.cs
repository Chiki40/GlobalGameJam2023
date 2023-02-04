using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField]
    private Transform _transformToRot = null;
    [SerializeField]
    private Transform _transformToSin = null;
    [SerializeField]
    private float _rotationSpeed = 2.0f;
    [SerializeField]
    private float _sinAmplitude = 1.0f;

    private float _sinTime = 0.0f;

    private void Update()
    {
        _sinTime = (_sinTime + Time.deltaTime) % (2.0f * Mathf.PI);

        _transformToRot.localEulerAngles += new Vector3(0.0f, _rotationSpeed * Time.deltaTime, 0.0f);
        _transformToSin.Rotate(new Vector3(Mathf.Sin(_sinTime) * _sinAmplitude, 0.0f, 0.0f));
    }
}

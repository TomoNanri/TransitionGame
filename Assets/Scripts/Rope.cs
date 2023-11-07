using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField]
    GameObject _pivot;
    [SerializeField]
    GameObject _coin;

    LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        //_lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
    }

    // Update is called once per frame
    void Update()
    {
        //_lineRenderer.SetPosition(0, _pivot.transform.position);
        _lineRenderer.SetPosition(1, _coin.transform.position);
    }
}

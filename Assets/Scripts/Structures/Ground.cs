using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private GameObject _turfPrefab;
    private float _groundWidth;
    private float _groundLength;
    private float _offsetX;
    private float _offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        _groundWidth = transform.localScale.x;
        _offsetX = -_groundWidth / 2;
        Debug.Log($"[{name}] _groundWidth={_groundWidth} _offsetX={_offsetX}");
        _groundLength = transform.localScale.z;
        _offsetZ = - _groundLength / 2;
        Debug.Log($"[{name}] _groundLength={_groundLength} _offsetZ={_offsetZ}");

        for (int w = 0; w < (int)_groundWidth + 1; w++)
        {
            for(int l = 0; l < (int)_groundLength + 1; l++)
            {
                Vector3 position = new Vector3(transform.position.x + (float)w + _offsetX, 0f, transform.position.z + (float)l + _offsetZ);
                var obj = Instantiate(_turfPrefab, transform, false);
                obj.transform.position = position;
                Debug.Log($"[{name}] New Turf Pos={obj.transform.position}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

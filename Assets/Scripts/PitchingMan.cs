using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchingMan : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Vector3 _initialVelocity = new Vector3(0, 0, 10);

    private GameObject _ball;

    // Start is called before the first frame update
    void Start()
    {
        _ball = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(_ball != null)
            {
                Destroy(_ball);
            }
            _ball = Instantiate(_prefab, transform.position, Quaternion.identity);
            //_ball.transform.position = Vector3.zero;
            var rb = _ball.GetComponent<Rigidbody>();
            rb.AddForce(_initialVelocity, ForceMode.VelocityChange);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _factor = 1f;
    [SerializeField]
    private float _gravity = 4.9f;
    [SerializeField]
    private Vector3 _initialVelocity = new Vector3(0,0,10f);

    private Rigidbody _rigidbody;
    private Vector3 _initialPosition;
    private bool _isNewPitching;
    private float _timer;
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        _isNewPitching = false;
        _target = GameObject.Find("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity += new Vector3(0, _gravity, 0) * Time.deltaTime;
        _timer -= Time.deltaTime;
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = _initialPosition;
            transform.LookAt(_target);
            _rigidbody.AddForce(_initialVelocity, ForceMode.VelocityChange);
            _isNewPitching = true;
            _timer = 0.03f;
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var f = Vector3.right * _factor;
            _rigidbody.AddForce(f, ForceMode.Impulse);
            //Debug.Log($"[{name}] Add Force! f={f}");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var f = Vector3.left * _factor;
            _rigidbody.AddForce(f, ForceMode.Impulse);
            //Debug.Log($"[{name}] Add Force! f={f}");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var f = Vector3.up *_factor;
            _rigidbody.AddForce(f, ForceMode.Impulse);
            //Debug.Log($"[{name}] Add Force! f={f}");
        }
        if (_isNewPitching && _timer < 0f)
        {
            _isNewPitching = false;
            Debug.Log($"[{name}] Position={transform.position}, Rotation={transform.rotation}, Velocity={_rigidbody.velocity}");
        }
    }
    private void FixedUpdate()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision == null)
        {
            if(collision.transform.tag == "BackBoard")
            {
                transform.LookAt(_target);
            }
        }
    }
}

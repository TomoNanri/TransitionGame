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
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _ball = null;
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if( _gameManager.GameState == GameManager.GameStateType.InGame && Input.GetKeyDown(KeyCode.Space))
        {
            if(_ball != null)
            {
                Destroy(_ball);
            }
            _ball = Instantiate(_prefab, transform.position, Quaternion.identity);
            var rb = _ball.GetComponent<Rigidbody>();
            rb.AddForce(_initialVelocity, ForceMode.VelocityChange);
        }
    }
}

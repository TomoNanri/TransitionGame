using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Action HitAction;
    public Action BrokenAction;

    [SerializeField]
    private GameObject _explosionFire;
    [SerializeField]
    private AudioClip _hitSE;
    [SerializeField]
    private AudioClip _explosionSE;
    [SerializeField] private float _coinBrokedDelay = 2f;

    private Material _coinMaterial;
    private AudioSource _audio;
    private CoinStateController _coinStateController;

    private void Awake()
    {
        _coinMaterial = GetComponent<MeshRenderer>().materials[0];
        Debug.Log($"[{name}] CoinMaterial={_coinMaterial}");
        _audio = GetComponent<AudioSource>();
        _coinStateController = FindAnyObjectByType<CoinStateController>();
        _coinStateController.Initialize((int)CoinStateController.StateType.Stable);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _coinStateController.UpdateSequence();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.transform.tag == "Ball")
        {
            _audio.PlayOneShot(_hitSE);
            if(HitAction != null)
            {
                HitAction();
            }
        }
    }
    public Color SetColor(Color color)
    {
        var _originColor = _coinMaterial.color;
        _coinMaterial.color = color;
        return _originColor;
    }
    public Color GetColor()
    { 
        return _coinMaterial.color;
    }
    public void Explosion()
    {
        _audio.PlayOneShot(_explosionSE);
        var pos = transform.position + new Vector3(0, 2f, -2f);
        SetColor(Color.clear);
        var eo = Instantiate(_explosionFire, pos, Quaternion.identity);
        Destroy(eo, 2f);
        if(BrokenAction != null)
        {
            StartCoroutine(InvokeAction(_coinBrokedDelay));
        }
    }
    private IEnumerator InvokeAction(float sec)
    {
        yield return new WaitForSeconds(sec);
        BrokenAction();
    }
}

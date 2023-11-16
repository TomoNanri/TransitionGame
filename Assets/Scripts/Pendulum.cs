using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Pendulum : MonoBehaviour
{
    // 支点の座標
    [SerializeField]
    private GameObject _pivot;
    // 糸の長さ
    private float _stringLength;
    // 時刻 t の振り子の速度
    private Vector3 _velocity;
    // 重力
    private Vector3 _gravity;
    // 糸にかかる張力
    private Vector3 _constraint;

    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _stringLength = (Vector3.Distance(transform.position, _pivot.transform.position));
        _gravity = Physics.gravity;
        _velocity = Vector3.zero;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {

    }
    void FixedUpdate()
    {
        // 時刻 t のおもりの位置を x、速度を x'、加速度を x''（それぞれベクトル）とする
        // また、fc を糸による張力、g を重力加速度とすると振り子の運動方程式は以下となる
        // mx'' = g + fc
        // fc = - m((|x'|^2 + Dot(x, g))/L) * xv（運動方向の単位ベクトル）なので
        // pivot からみた相対位置 x を _position とすると

        Vector3 _position = transform.position - _pivot.transform.position;

        //糸に掛かる張力 fc を _constraint とすると

        _constraint = (_velocity.sqrMagnitude + Vector3.Dot(_position, _gravity)) / _stringLength * _position.normalized;

        // Delta t 時のおもりの位置と速度は
        _velocity += (_gravity - _constraint) * Time.deltaTime;
        transform.position += _velocity * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.transform.tag == "Ball") 
        {
            _audioSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Pendulum : Product
{
    // �x�_�̍��W
    [SerializeField]
    private GameObject _pivot;
    // ���̒���
    private float _stringLength;
    // ���� t �̐U��q�̑��x
    private Vector3 _velocity;
    // �d��
    private Vector3 _gravity;
    // ���ɂ����钣��
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
        // ���� t �̂�����̈ʒu�� x�A���x�� x'�A�����x�� x''�i���ꂼ��x�N�g���j�Ƃ���
        // �܂��Afc �����ɂ�钣�́Ag ���d�͉����x�Ƃ���ƐU��q�̉^���������͈ȉ��ƂȂ�
        // mx'' = g + fc
        // fc = - m((|x'|^2 + Dot(x, g))/L) * xv�i�^�������̒P�ʃx�N�g���j�Ȃ̂�
        // pivot ����݂����Έʒu x �� _position �Ƃ����

        Vector3 _position = transform.position - _pivot.transform.position;

        //���Ɋ|���钣�� fc �� _constraint �Ƃ����

        _constraint = (_velocity.sqrMagnitude + Vector3.Dot(_position, _gravity)) / _stringLength * _position.normalized;

        // Delta t ���̂�����̑��x
        _velocity += (_gravity - _constraint) * Time.deltaTime;

        // Delta t ���̂�����̈ʒu
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

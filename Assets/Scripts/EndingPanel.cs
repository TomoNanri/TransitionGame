using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndingPanel : MonoBehaviour
{
    [SerializeField] float _LineAppearInterval = 4.0f;
    [SerializeField] int _moveCount = 1730;

    private GameManager _gM;
    private bool _isEndRollActive = false;
    private List<GameObject> _endingTexts = new List<GameObject>();
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _gM = GameManager.Instance;
        _audioSource = GetComponent<AudioSource>();
        foreach(Transform t in this.transform)
        {
            if (Regex.IsMatch(t.name, "EndingText"))
            {
                _endingTexts.Add(t.gameObject);
                t.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_gM.IsEndingStarted && !_isEndRollActive)
        {
            _isEndRollActive=true;
            _audioSource.Play();
            StartCoroutine(PlayEnding(_LineAppearInterval, _moveCount));
        }
        if(_isEndRollActive && !_audioSource.isPlaying)
        {
            Application.Quit();
        }
    }
    private IEnumerator PlayEnding(float sec, int count)
    {
        foreach(GameObject go in _endingTexts)
        {
            yield return new WaitForSeconds(sec);
            go.SetActive(true);
        }
        Vector3 deltaPos = new Vector3(0, 1, 0); 
        for(int i=0;i<count; i++)
        {
            yield return new WaitForEndOfFrame();
            transform.position += deltaPos;
        }
    }
}

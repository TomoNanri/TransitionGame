using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class IntroPanel : MonoBehaviour
{
    public Action EndOfOpening;

    [SerializeField]
    private float _msgAppearInterval = 3f;
    [SerializeField]
    GameObject _fire;
    [SerializeField]
    AudioSource _soundSource;
    Transform _titlePanel;
    private List<TextMeshProUGUI> _textLines = new List<TextMeshProUGUI>();
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _soundSource = GetComponent<AudioSource>();
        _soundSource.Play();

        foreach(Transform t in transform)
        {
            if(Regex.IsMatch(t.name, "TextLine"))
            {
                t.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 0);
                _textLines.Add(t.GetComponent<TextMeshProUGUI>());
            }
        }

        StartCoroutine(DisplayText(_textLines, _msgAppearInterval));
        _titlePanel = transform.Find("TitlePanel");
        foreach(Transform t in _titlePanel)
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 0);
        }
        _titlePanel.rotation = Quaternion.Euler(90,0,0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator DisplayText(List<TextMeshProUGUI> texts, float sec)
    {
        foreach(TextMeshProUGUI text in texts)
        {
            yield return new WaitForSeconds(sec);
            StartCoroutine(FadeIn(text, sec/100));
        }
        yield return new WaitForSeconds(sec);
        StartCoroutine(RotateTitle(sec/100));
    }
    IEnumerator RotateTitle(float sec)
    {
        foreach (Transform t in _titlePanel)
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 100, 255);
        }
        for (int i=0; i<90; i++)
        {
            if (_titlePanel != null)
            {
                _titlePanel.rotation = Quaternion.Euler(90-i, 0, 0);
            }
            yield return new WaitForSeconds(sec);

        }
        if (EndOfOpening != null)
        {
            EndOfOpening.Invoke();
        }
    }
    IEnumerator FadeIn(TextMeshProUGUI text, float sec)
    {
        for(int i=0; i<255; i++)
        {
            text.color += new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(sec);
        }
    }
}

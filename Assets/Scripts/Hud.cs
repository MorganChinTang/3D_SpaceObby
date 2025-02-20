using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timer = null;

    private float time = 0.0f;
    private float lastSecondBlink = -1.0f;
    Coroutine blinkCoroutine = null;
    Color textColor = Color.black;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rectTransform = transform as RectTransform;
        rectTransform.anchoredPosition = new Vector2(0.0f, 300.0f);
        rectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.InOutQuad);
        textColor = timer.color;
    }

    public void OnButtonClick()
    {
        time = 0.0f;
        lastSecondBlink = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time = Math.Max(time, 0.0f);
        TimeSpan span = TimeSpan.FromSeconds(time);
        timer.text = span.ToString(@"mm\:ss\.ff");
        if (time >= 10.0f)
        {
            float flooredTime = Mathf.Floor(time);
            if (flooredTime > lastSecondBlink)
            {
                lastSecondBlink = flooredTime;
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                }
                blinkCoroutine = StartCoroutine(DoBlink(0.75f));
            }
        }
    }

    IEnumerator DoBlink(float duration)
    {
        yield return null;

        RectTransform rectTransform = timer.transform as RectTransform;
        rectTransform.localScale = Vector3.one;
        timer.color = textColor;

        Tween colorRedTween = timer.DOColor(Color.red, duration * 0.1f);
        Tween colorYellowTween = timer.DOColor(Color.yellow, duration * 0.1f);

        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.Append(colorRedTween);
        seq.Append(colorYellowTween);
        seq.SetLoops(-1);

        seq.Play();

        Tween scaleTween = null;
        for (int i = 0; i < 3; ++i)
        {
            scaleTween = rectTransform.DOScale(1.5f, duration * 0.125f);
            yield return scaleTween.WaitForCompletion();

            scaleTween.Kill();
            scaleTween = rectTransform.DOScale(0.8f, duration * 0.125f);
            yield return scaleTween.WaitForCompletion();
        }

        scaleTween.Kill();
        seq.Kill();
        colorRedTween.Kill();
        colorYellowTween.Kill();

        rectTransform.DOScale(1.0f, duration * 0.2f);
        timer.DOColor(textColor, duration * 0.25f);
    }
 }

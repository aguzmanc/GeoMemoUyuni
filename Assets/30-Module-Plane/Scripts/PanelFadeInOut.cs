using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelFadeInOut : MonoBehaviour
{
    [SerializeField] Image _panel;
    [SerializeField] float _timeAnimation;

    private void OnEnable ()
    {
        PlaneMove.onBackScene += BackScene;
    }


    void Start ()
    {
        ModifyAlpha(0);
    }

    private void OnDisable ()
    {
        PlaneMove.onBackScene -= BackScene;
    }

    public void ModifyAlpha (float value)
    {
        var currentColor = _panel.color;
        currentColor.a = value;
        _panel.color = currentColor;
    }

    public void FadeIn ()
    {
        StartCoroutine(FadeCoroutine(1));
    }

    public void FadeOut ()
    {
        StartCoroutine(FadeCoroutine(0));
    }

    public void FadeInOut (float timeAnimation)
    {
        StartCoroutine(FadeInOutCoroutine(timeAnimation));
    }

    void BackScene ()
    {
        StartCoroutine(BackSceneCoroutine());
    }

    IEnumerator FadeCoroutine (float value)
    {
        var time = 0f;
        var currentColor = _panel.color;
        var nextColor = _panel.color;
        nextColor.a = value;
        while (time < 1)
        {
            time += Time.deltaTime / _timeAnimation;
            _panel.color = Color.Lerp(currentColor, nextColor, time);
            yield return null;
        }
    }

    IEnumerator FadeInOutCoroutine (float timeAnimation)
    {
        var time = 0f;
        var currentColor = _panel.color;
        var nextColor = _panel.color;
        nextColor.a = 1;
        while (time < 1)
        {
            time += Time.deltaTime / _timeAnimation;
            _panel.color = Color.Lerp(currentColor, nextColor, time);
            yield return null;
        }

        yield return new WaitForSeconds(timeAnimation);

        time = 0f;
        currentColor = _panel.color;
        nextColor = _panel.color;
        nextColor.a = 0;
        while (time < 1)
        {
            time += Time.deltaTime / _timeAnimation;
            _panel.color = Color.Lerp(currentColor, nextColor, time);
            yield return null;
        }
    }

    IEnumerator BackSceneCoroutine ()
    {
        yield return new WaitForSeconds(1.5f);
        FadeInOut(1.5f);
    }
}
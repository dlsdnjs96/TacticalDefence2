using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notice : Singleton<Notice>
{
    [SerializeField] BaseWindow noticeWindow;
    [SerializeField] BaseWindow noticeText;
    [SerializeField] TextMeshProUGUI windowContent;
    [SerializeField] TextMeshProUGUI textContent;

    public void ShowNotice(string _content)
    {
        windowContent.text = _content;
        noticeWindow.OpenWindow();
    }

    public void ShowNoticeText(string _content, float _duration = 3f, float _disappear = 1f)
    {
        textContent.text = _content;
        noticeText.gameObject.SetActive(true);
        StartCoroutine(DisappearWindow(_duration, _disappear));
    }

    IEnumerator DisappearWindow(float _duration, float _disappear)
    {
        yield return new WaitForSeconds(_duration);

        Color textColor = textContent.color;
        float passedTime = 0f;
        while (passedTime < _disappear)
        {
            passedTime += Time.deltaTime;
            textColor.a = ((_disappear - passedTime) / _disappear);
            textContent.color = textColor;
            noticeText.gameObject.GetComponent<Image>().color = Color.black * ((_disappear - passedTime) / _disappear);
            yield return null;
        }
        noticeText.CloseWindow();
    }
}

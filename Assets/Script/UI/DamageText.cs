using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;


public delegate void ConvertText(float _damage);

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI text;

    private Vector3 from;
    private float passedTime;
    private const float expireTime = 2f;

    private static ConvertText convertText;

    static string[] koreanDigitUnits = { "만", "억", "조", "경", "해" };
    static string[] englishDigitUnits = { "K", "M", "B", "T", "Q" };

    static string colorHex;



    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        convertText = ConvertTextEnglish;

        Color32 color32 = Color.gray;
        colorHex = "#808080";//
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        text.rectTransform.localScale = (3f - passedTime) * Vector3.one;
        text.rectTransform.position = from + (Vector3.up * Mathf.Log(1f + (20f * passedTime)) * 18f);
    }

    public void Initialize(DamageData _damage, Vector3 _pos)
    {
        passedTime = 0f;
        from = Camera.main.WorldToScreenPoint(_pos);
        text.rectTransform.position = from;
        convertText(_damage.damage);

        if ((_damage.option & DamageOption.CRITICAL) != 0) text.color = Color.red;
        else text.color = Color.yellow;

        StartCoroutine(ExpireObj());
    }

    IEnumerator ExpireObj()
    {
        yield return new WaitForSeconds(expireTime);
        DamageTextPool.instance.ReturnObject(this);
    }

    void ConvertTextKorean(float _damage)
    {
        text.text = string.Format(CultureInfo.InstalledUICulture, "{0:#,#}", _damage);
        //text.text = _damage.ToString("0");

        int unitIndex = 1;
        int len = text.text.Length;

        text.text = text.text.Substring(0, len % 4)+","+ text.text.Substring(len % 4);
        for (int i = 0; i+unitIndex + 1 < text.text.Length; i++)
        {
            if (i%4 == len % 4)
            {
                text.text = text.text.Substring(0, i + unitIndex) + "," + text.text.Substring(i + unitIndex + 1);
                unitIndex++;
            }
        }

        //unitIndex = 0;
        //while (text.text.Contains(","))
        //{
        //    int commaIndex = text.text.LastIndexOf(",");
        //    string before = text.text.Substring(0, commaIndex);
        //    string after = text.text.Substring(commaIndex + 1);
        //
        //
        //    text.text = before + "<color=" + colorHex + ">" + koreanDigitUnits[unitIndex] + "</color>" + after;
        //    unitIndex++;
        //}


        return;
    }
    void ConvertTextEnglish(float _damage)
    {
        text.text = _damage.ToString("#,0");
        int unitIndex = 0;

        while (text.text.Contains(","))
        {
            int commaIndex = text.text.LastIndexOf(",");
            string before = text.text.Substring(0, commaIndex);
            string after = text.text.Substring(commaIndex + 1);


            text.text = before + "<color="+ colorHex + ">" + englishDigitUnits[unitIndex] + "</color>" + after;
            unitIndex++;
        }


        return;
    }
    void ConvertText(float _damage)
    {
        text.text = _damage.ToString();

        return;
    }
}

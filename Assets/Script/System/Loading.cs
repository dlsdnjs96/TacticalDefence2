using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Loading : Singleton<Loading>
{
    [SerializeField] Image imageBackground;
    [SerializeField] Image imageLoading;

    private void Awake()
    {
        //StartLoading();
    }
    public void StartLoading()
    {
        StopCoroutine(CoLoading());
        gameObject.transform.SetAsLastSibling();
        imageBackground.gameObject.SetActive(true);
        imageLoading.gameObject.SetActive(true);
        StartCoroutine(CoLoading());
    }

    public void StopLoading()
    {
        StopCoroutine(CoLoading());
        imageBackground.gameObject.SetActive(false);
        imageLoading.gameObject.SetActive(false);
    }

    IEnumerator CoLoading()
    {
        float rotationZ = 0f;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            rotationZ += 360f / 12f;
            imageLoading.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }
}

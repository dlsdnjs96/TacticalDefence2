using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    // ��ȣ�ۿ��� ������
    [SerializeField] public BaseWindow connectedWindow;
    // ������ �޹��
    [SerializeField] public BaseWindow backgroundWindow;
    public virtual void OpenWindow() { 
        // �̹� Ȱ��ȭ ������ ��� ����
        if (gameObject.activeSelf) return; 
        // �޹�� �����찡 ������ Ȱ��ȭ
        if (backgroundWindow != null) backgroundWindow.OpenWindow();
        // ������ Ȱ��ȭ
        gameObject.SetActive(true); 
        // ������ �� ������â�� Ŀ���� �۾����� ȿ��
        BounceWindow(); 
    }
    public virtual void CloseWindow()
    {
        // �޹�� �����찡 ������ ��Ȱ��ȭ
        if (backgroundWindow != null) backgroundWindow.CloseWindow();
        // ������ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    public virtual void ReceiveHeroInfo(HeroInfo _heroInfo, out bool _selected) { _selected = true; }
    public virtual void ReceiveEquipment(Equipment _equipment, out bool _selected) { _selected = true; }
    public virtual void HorizontalScrollWindow() { StartCoroutine(CoHorizontalScrollWindow()); }
    public virtual void VerticalScrollWindow() { StartCoroutine(CoVerticalScrollWindow()); }
    public virtual void BounceWindow() { StartCoroutine(CoBounceWindow()); }

    IEnumerator CoHorizontalScrollWindow()
    {
        float passedTime = 0f;

        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.right + (Vector3.up * Mathf.Lerp(0f, 1f, passedTime / 0.2f)); 
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator CoVerticalScrollWindow()
    {
        float passedTime = 0f;

        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.right + (Vector3.up * Mathf.Lerp(0f, 1f, passedTime / 0.2f)); 
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator CoBounceWindow()
    {
        float passedTime = 0f;

        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.one * Mathf.Lerp(1f, 1.05f, passedTime / 0.2f); 
            yield return new WaitForEndOfFrame();
        }

        passedTime = 0f;
        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.one * Mathf.Lerp(1.05f, 1f, passedTime / 0.2f); 
            yield return new WaitForEndOfFrame();
        }
    }
}

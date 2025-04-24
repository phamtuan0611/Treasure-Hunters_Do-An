using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestCotroller : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject rewardUI; 
    [SerializeField] private Sprite rewardSprite; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isPlayer");
            ShowRewardFloatingUI();
        }
    }

    private void ShowRewardFloatingUI()
    {
        //GameObject ui = Instantiate(rewardUI, GameObject.FindObjectOfType<UIController>().transform); // phải có Canvas trong scene
        //RectTransform rt = ui.GetComponent<RectTransform>();
        //rt.anchoredPosition = Vector2.zero; // hiện ở giữa màn hình

        //// Gán ảnh và text
        ////ui.transform.Find("Image").GetComponent<Image>().sprite = rewardSprite;
        ////ui.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = rewardText;

        //StartCoroutine(MoveUIToScoreCorner(ui));
    }

    private IEnumerator MoveUIToScoreCorner(GameObject ui)
    {
        yield return new WaitForSeconds(1f);

        Vector2 start = ui.GetComponent<RectTransform>().anchoredPosition;
        Vector2 end = new Vector2(500, 300); // góc phải trên canvas, tùy chỉnh cho khớp điểm số

        float t = 0f, duration = 0.5f;
        while (t < duration)
        {
            t += Time.deltaTime;
            ui.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(start, end, t / duration);
            yield return null;
        }

        // Sau khi bay xong thì cộng điểm và hủy UI
        CollectiblesManager.instance.GetCollectibleDiamond(10); // hoặc gọi function tùy bạn
        Destroy(ui);
    }
}

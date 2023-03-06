using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOver : MonoBehaviour
{
    public GameObject mask;
    public GameObject button_over;

    public GameObject image_over_Score;//最终分数UI
    public GameObject score_source;//获取最终分数
    string over_score;//分数文本

    public void GameOver()
    {
        //print("GameOver");
        mask.SetActive(true);
        button_over.SetActive(true);

        over_score = "得分:" + score_source.GetComponent<Text>().text;
        image_over_Score.SetActive(true);
        image_over_Score.transform.GetChild(0).GetComponent<Text>().text = over_score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    public Image black;

    public GameObject clockcopter_Boss;
    public GameObject boss;
    public Image bossUI;
    public Image bossHealthUI;
    public Image bossHealthBackUI;
    float bossCurHealth;
    float bossMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        black.canvasRenderer.SetAlpha(0.0f);

        bossUI.canvasRenderer.SetAlpha(0.0f);
        bossHealthUI.canvasRenderer.SetAlpha(0.0f);
        bossHealthBackUI.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        bossCurHealth = boss.GetComponent<EnemyStats>().curHealth;
        bossMaxHealth = boss.GetComponent<EnemyStats>().maxHealth;
        //For the boss' health bar
        bossHealthUI.rectTransform.sizeDelta = new Vector2((float)bossCurHealth / bossMaxHealth * 100, 123.7f);

        if (boss.activeInHierarchy)
        {
            bossUI.CrossFadeAlpha(1, 1, false);
            bossHealthUI.CrossFadeAlpha(1, 1, false);
            bossHealthBackUI.CrossFadeAlpha(1, 1, false);
        }
        else
        {
            bossUI.CrossFadeAlpha(0, 1, false);
            bossHealthUI.CrossFadeAlpha(0, 1, false);
            bossHealthBackUI.CrossFadeAlpha(0, 1, false);
        }
    }
}

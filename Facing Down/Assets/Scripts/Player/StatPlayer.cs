using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatPlayer : MonoBehaviour
{
    [HideInInspector]
    public StatEntity statEntity;
    private PlayerIframes playerIframes;

    public Text hpText;

    [Min(0)] public int numberOfDashes = 0;
    [Min(0)] public int maxDashes = 10;

    public void Start()
    {
        statEntity = GetComponent<Player>().self.GetComponent<StatEntity>();
        playerIframes = statEntity.GetComponent<PlayerIframes>();
        hpText.text = statEntity.currentHitPoints.ToString();
    }

    public void takeDamage(int damage, float iframeDuration = 2.0f)
    {
        if (!playerIframes.isIframe)
        {
            statEntity.takeDamage(damage);
            hpText.text = statEntity.currentHitPoints.ToString();
            playerIframes.getIframe(iframeDuration);
        }
    }
}

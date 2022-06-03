using System.Collections;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected Vector3 startPos = new Vector3();
    protected float timePassed = 0.0f;
    protected bool isAttacking = false;

    public Color color = Color.white;
    public float angle = 0.0f;
    public float timeSpan = 1.0f;
    public float startDelay = 0.0f;
    public float endDelay = 0.0f;
    public Entity src;

    //public AudioSource audioSource;
    public AudioClip audioClip;
    private bool hasStartedAttacking = false;

    public float acceleration = 2.0f;

    public delegate void endAttackEvent(Entity self, float angle);
    public event endAttackEvent onEndAttack;


    public bool isUsingEndAnimation = false;

    void Awake()
    {
        //gameObject.SetActive(false);
        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);

        //audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void startAttack()
    {
        //gameObject.SetActive(true);
        startPos = transform.position;
        timePassed = 0.0f;
        isAttacking = true;
        onStart();
        ComputeAttack(0.0f);



        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().color = color;
    }

    protected abstract void onStart();

    void FixedUpdate()
    {
        if (!isAttacking)
            return;

        timePassed += Time.fixedDeltaTime;
        if (timePassed - startDelay >= timeSpan + endDelay)
        {
            /*if (audioSource != null)
            {
                audioSource.loop = false;
                audioSource.clip = null;
                audioSource.Stop();
            }*/
            attackEnd();
            if (onEndAttack != null) onEndAttack(src, angle);
            if (isUsingEndAnimation) gameObject.GetComponent<Animator>().SetBool("isDead", true);
            else Destroy(gameObject);
        }

        float percentageTime = (timePassed - startDelay) / timeSpan;
        percentageTime = percentageTime > 1 ? 1 : percentageTime;
        percentageTime = percentageTime < 0 ? 0 : percentageTime;
        if(!hasStartedAttacking && percentageTime > 0)
        {
            hasStartedAttacking = true;
            /*if(audioSource != null && audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.PlayOneShot(audioClip);
            }*/
            if (audioClip != null)
            {
                GameObject soundEffect = new GameObject("Sound Effect");
                soundEffect.transform.parent = src.transform;
                soundEffect.AddComponent<AudioSource>();
                soundEffect.GetComponent<AudioSource>().volume = 0.5f;
                soundEffect.GetComponent<AudioSource>().PlayOneShot(audioClip);
                Destroy(soundEffect, audioClip.length);
            }
        }

        ComputeAttack(Mathf.Pow(percentageTime, acceleration));
    }

    protected virtual void attackEnd() { }

    protected abstract void ComputeAttack(float percentageTime);
}

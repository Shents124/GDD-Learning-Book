using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sound.Service;
using Spine.Unity;
using UnityEngine;

public class AnimHomeManager : MonoBehaviour
{
    public float timeIdle = 1.5f;

    public SkeletonGraphic[] playerAnims;
    public GameObject[] colorPens;

    private SkeletonGraphic playerAnim;

    [SpineAnimation]
    public string[] animRandom;

    [SpineAnimation]
    public string animIdle;

    private int idActive;

    private void Start()
    {
        foreach (var playerAnim in playerAnims)
        {
            playerAnim.gameObject.SetActive(false);
        }
        int ran = Random.Range(0, playerAnims.Length);
        playerAnim = playerAnims[ran];
        idActive = ran;
        playerAnim.gameObject.SetActive(true);
        PlayRandomAnim();

        foreach (var colorPen in colorPens)
        {
            colorPen.gameObject.SetActive(false);
        }

        colorPens[ran].gameObject.SetActive(true);

    }

    public void PlayRandomAnim()
    {
        int idAnim = Random.Range(0, animRandom.Length);
        var track = playerAnim.AnimationState.SetAnimation(0, animRandom[idAnim], false);
        if(idActive != 1)
        {
            AudioUtility.PlaySFX(AudioClipName.Menu_boy_hello);
        }
        else
        {
            AudioUtility.PlaySFX(AudioClipName.Menu_girl_hello);
        }
        track.Complete += Entry => {
            playerAnim.AnimationState.SetAnimation(0, animIdle, true);
            StartCoroutine(DelayChangeAnim());
        };
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public IEnumerator DelayChangeAnim()
    {
        yield return new WaitForSeconds(timeIdle);
        PlayRandomAnim();
    }
}

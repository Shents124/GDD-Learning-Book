using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

    private void Start()
    {
        foreach (var playerAnim in playerAnims)
        {
            playerAnim.gameObject.SetActive(false);
        }
        int ran = Random.Range(0, playerAnims.Length);
        playerAnim = playerAnims[ran];
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
        track.Complete += async Entry => {
            playerAnim.AnimationState.SetAnimation(0, animIdle, true);
            await UniTask.Delay(System.TimeSpan.FromSeconds(timeIdle));
            PlayRandomAnim();
        };
    }
}

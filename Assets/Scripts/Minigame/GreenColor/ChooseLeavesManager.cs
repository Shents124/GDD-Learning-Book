using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLeavesManager : MonoBehaviour
{
    public Leave currentLeave;

    public SkeletonAnimation frogAnim;

    public int currentStep;

    public int stepNeed;

    public List<Transform> posDoneLeaves;

    public GameObject choseLeaves;

    public Button[] btnChoose;

    public void NextStep()
    {
        choseLeaves.SetActive(false);
        //TODO anim jump
        currentStep++;
        if(currentStep == stepNeed) 
        {

        }
        else
        {

        }
    }
}

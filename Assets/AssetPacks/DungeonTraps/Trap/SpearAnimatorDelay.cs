using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAnimatorDelay : MonoBehaviour
{
    int spearsCount;
    const float fixedDelay = 0.5f;
    float []delays;
    Animator []animators;
    string launchSpearAnimationParameter= "launch";
    private void Start()
    {
        spearsCount = transform.childCount;
        animators = new Animator[spearsCount];
        delays = new float[spearsCount];
        for (int i = 0; i < spearsCount; i++)
        {
            animators[i]= transform.GetChild(i).GetComponent<Animator>();
            delays[i] = fixedDelay*i;
        }
        for (int i = 0; i < spearsCount; i++)
        {
            StartCoroutine(CO_StartSpearAnimatorAfterDelay(i));
        }
    }
    IEnumerator CO_StartSpearAnimatorAfterDelay(int index)
    {
        yield return delays[index];
        print("spear " + index + " launched");
        animators[index].SetTrigger(launchSpearAnimationParameter);
    }

}

using System;
using System.Collections;
using Mikabrytu.LD46;
using Mikabrytu.LD46.Systems;
using UnityEngine;

public class FixSystem : IFix
{
    private IEnumerator fixRoutine;
    private Action OnFinish;
    private float timeToFix;

    public void Setup(float timeToFix)
    {
        this.timeToFix = timeToFix;
    }

    public void StartFix(Action OnFinish)
    {
        this.OnFinish = OnFinish;

        if (fixRoutine == null)
        {
            fixRoutine = FixTimer();
            GameManager.Instance.StartCoroutine(fixRoutine);
        }
    }

    public void CancelFix()
    {
        if (fixRoutine != null)
            GameManager.Instance.StopCoroutine(fixRoutine);
        fixRoutine = null;
    }

    public void FinishFix()
    {
        OnFinish.Invoke();
    }

    public IEnumerator FixTimer()
    {
        yield return new WaitForSeconds(timeToFix);
        FinishFix();
    }
}

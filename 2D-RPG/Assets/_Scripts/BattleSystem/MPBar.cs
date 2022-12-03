using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBar : MonoBehaviour
{
    [SerializeField] GameObject mp;

    public void SetMP(float mpNormalized)
    {
        mp.transform.localScale = new Vector3(mpNormalized, 1f);
    }

    public IEnumerator SetMPSmooth(float newMP)
    {
        float curMP = mp.transform.localScale.x;
        float changeAmt = curMP - newMP;

        while (curMP - newMP > Mathf.Epsilon)
        {
            curMP -= changeAmt * Time.deltaTime;
            mp.transform.localScale = new Vector3(curMP, 1f);
            yield return null;
        }

        mp.transform.localScale = new Vector3(newMP, 1f);
    }
}

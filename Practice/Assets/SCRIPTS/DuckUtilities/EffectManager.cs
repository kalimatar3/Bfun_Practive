using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckGame.Ultilities;

public enum EFFECTTYPE {MoneyBlast, NitroBlast}
public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] GameObject moneyBlastEffect;
    [SerializeField] GameObject nitroBlastEffect;

    public void SpawnEffect(EFFECTTYPE effectType ,Vector3 pos)
    {
        GameObject tempGO = null;
        switch(effectType)
        {
            case EFFECTTYPE.MoneyBlast:
                tempGO = moneyBlastEffect;
                break;
            
            case EFFECTTYPE.NitroBlast:
                tempGO = nitroBlastEffect;
                break;
        }
        tempGO = Instantiate(tempGO, pos, Quaternion.identity);
        StartCoroutine(IDestroyAfter(tempGO));
    }

    IEnumerator IDestroyAfter(GameObject go)
    {
        yield return new WaitForSeconds(3);
        Destroy(go);
    }
    

}

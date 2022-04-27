using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

//Charge bar works by controlling image mask fill amount

public class ChargeBar : MonoBehaviour
{
    public Image mask;
    
    public void SetFillAmt(float x) {
        mask.fillAmount = x;
    }
}

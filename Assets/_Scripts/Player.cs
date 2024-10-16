using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isAttacking=false;
    private Animator _anim;
    private bool _insideComboWindow=false;
    private bool _comboContinues=false;
    private ComboController _comboController;
    // Start is called before the first frame update
    void Start()
    {
        _anim=GetComponent<Animator>();
        _comboController=GetComponent<ComboController>();
    }

    public void OnAttack()
    {
        
        if (!isAttacking)
        {
            isAttacking = true;
            _anim.SetTrigger("TriggerAttack");
        }
        else 
            if (_insideComboWindow)
        {
            _comboContinues = true;
        }
    }
    public void OnAttackAnimationFinished()
    {
        _comboController.CurrentCombo=_comboController.CurrentCombo+1;
        if (_comboContinues)
        {
            _comboContinues = false;
            _anim.SetTrigger("TriggerAttack");
        }
        else
        {
            isAttacking = false;
            _comboController.CurrentCombo = 1;
            Debug.Log("entered");
        }
            
    }
    public void SetInsideComboWindow(int flag)
    {
        if (flag == 0) { _insideComboWindow= true; }
            
        else if (flag == 1) { _insideComboWindow= false; }
            
    }
}

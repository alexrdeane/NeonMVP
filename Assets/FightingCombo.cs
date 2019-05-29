using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightingCombo : MonoBehaviour
{
    public KeyCode lightKey;
    public KeyCode mediumKey;
    public KeyCode heavyKey;

    public Attack lightAttack;
    public Attack mediumAttack;
    public Attack heavyAttack;
    public List<Combo> combos;
    public float comboLeeway = 0.2f;
    private float leeway = 0;

    public Animator anim;
    Attack curAttack = null;
    private float timer = 0;
    ComboInput lastInput = null;
    List<int> currentCombos = new List<int>();
    public bool skip = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        PrimeCombos();
    }

    void PrimeCombos()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo combo = combos[i];
            combo.onInputted.AddListener(() =>
            {
                skip = true;
                Attack(combo.comboAttack);
                ResetCombos();
            });
        }
    }

    void Update()
    {
        if (curAttack != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                curAttack = null;
            }
            return;
        }

        if (currentCombos.Count > 0)
        {
            leeway += Time.deltaTime;
            if (leeway >= comboLeeway)
            {
                if (lastInput != null)
                {
                    skip = true;
                    Attack(getAttackFromType(lastInput.type));
                    lastInput = null;
                }
                ResetCombos();
            }
        }
        else
            leeway = 0;

        ComboInput input = null;
        if (Input.GetKeyDown(lightKey))
            input = new ComboInput(AttackType.light);
        if (Input.GetKeyDown(mediumKey))
            input = new ComboInput(AttackType.medium);
        if (Input.GetKeyDown(heavyKey))
            input = new ComboInput(AttackType.heavy);


        if (input == null) return;
            lastInput = input;

        List<int> remove = new List<int>();
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo combo = combos[currentCombos[i]];
            if (combo.continueCombo(input))
            {
                leeway = 0;
            }
            else
            {
                remove.Add(i);
            }

        }

        if (skip)
        {
            skip = false;
            return;
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].continueCombo(input))
            {
                currentCombos.Add(i);
                leeway = 0;
            }
        }

        foreach (int i in remove)
            currentCombos.RemoveAt(i);

        if (currentCombos.Count <= 0)
            Attack(getAttackFromType(input.type));
 
    }

    void ResetCombos()
    {
        leeway = 0;
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }

        currentCombos.Clear();
    }

    void Attack(Attack attack)
    {
        curAttack = attack;
        timer = attack.length;
        anim.Play(attack.name, -1, 0);
    }

    Attack getAttackFromType(AttackType t)
    {
        if (t == AttackType.light)
            return lightAttack;
        if (t == AttackType.medium)
            return mediumAttack;
        if (t == AttackType.heavy)
            return heavyAttack;
        return null;
    }
}

[System.Serializable]
public class Attack
{
    public float length;
    public string name;
}

[System.Serializable]
public class ComboInput
{
    public AttackType type;
    public ComboInput(AttackType t)
    {
        type = t;
    }
    public bool isSameAs(ComboInput test)
    {
        return (type == test.type);
    }
}

[System.Serializable]
public class Combo
{
    public List<ComboInput> Inputs;
    public Attack comboAttack;
    public UnityEvent onInputted;
    int curInput = 0;

    public bool continueCombo(ComboInput i)
    {
        if (Inputs[curInput].isSameAs(i))
        {
            curInput++;
            if (curInput >= Inputs.Count)
            {
                onInputted.Invoke();
                curInput = 0;
            }
            return true;
        }
        else
        {
            curInput = 0;
            return false;
        }
    }
    public ComboInput currentComboInput()
    {
        if (curInput >= Inputs.Count) return null;
        return Inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}
public enum AttackType { light = 0, medium = 1, heavy = 2 };

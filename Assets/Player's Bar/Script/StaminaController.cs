using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{

    public Slider staminaBar;

    private int maxStamina = 100;
    private int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(1);
    private Coroutine regen;

    public static StaminaController Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            regen = StartCoroutine(RegenStamina());

            if(regen != null)
            {
                StopCoroutine(regen);

            }
        }
        else{
            Debug.Log("Not enough stamina");
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

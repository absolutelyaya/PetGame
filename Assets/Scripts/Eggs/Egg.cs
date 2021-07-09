using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public EggScriptableObject EggData;

    public EggPhase phaseData;
    public float phaseTime = 0f;
    int phase = 0;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        phaseData = EggData.Phases[phase];
    }

    private void Update()
    {
        phaseTime += Time.deltaTime;

        if(phaseData.WiggleAmount > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(Time.time * phaseData.WiggleSpeed) * phaseData.WiggleAmount));
        }

        if (phaseTime / 60 > phaseData.Duration)
            Upgrade();
    }

    void Upgrade()
    {
        phase++;
        phaseTime = 0f;
        if (phase > EggData.Phases.Count)
        {
            Debug.Log("Hatch!");
            return;
        }    
        phaseData = EggData.Phases[phase];
        sprite.sprite = phaseData.Sprite;
    }

    public void OnUpdateSettings()
    {

    }
}

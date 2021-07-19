using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PetEntity : MonoBehaviour
{
    public PetSO EvolutionTree;
    public string StageID;
    public PetStats Stats;

    Vector2 destination;
    Animator anim;
    GrowthStage stage;
    SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stage = EvolutionTree.GetStageByID(StageID);
        if(stage != null)
        {
            anim.runtimeAnimatorController = stage.Animator;
            Stats = stage.DefaultStats;
        }
        StartCoroutine(MovementRoutine());
    }

    IEnumerator MovementRoutine()
    {
        while(true)
        {
            while (Vector2.Distance(transform.position, destination) > 1 * Time.fixedDeltaTime * Stats.Speed)
            {
                Vector3 direction = ((Vector3)destination - transform.position).normalized;
                render.flipX = direction.x > 0;
                anim.SetBool("moving", true);
                transform.position += direction * Time.fixedDeltaTime * Stats.Speed;
                yield return new WaitForFixedUpdate();
            }
            anim.SetBool("moving", false);
            yield return new WaitForSeconds(Random.Range(Stats.MoveInterval.x, Stats.MoveInterval.y));
            destination = new Vector2(Random.Range(-3f, 3f), Random.Range(-2f, 2f));
            yield return new WaitForEndOfFrame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEntity : MonoBehaviour
{
    public PetSO EvolutionTree;
    public string PhaseID;
    public PetStats Stats;

    Vector2 destination;

    void Start()
    {
        StartCoroutine(MovementRoutine());
    }

    IEnumerator MovementRoutine()
    {
        while(true)
        {
            while (Vector2.Distance(transform.position, destination) > 1 * Time.fixedDeltaTime * Stats.Speed)
            {
                transform.position += ((Vector3)destination - transform.position).normalized * Time.fixedDeltaTime * Stats.Speed;
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(Random.Range(Stats.MoveInterval.x, Stats.MoveInterval.y));
            destination = new Vector2(Random.Range(-3f, 3f), Random.Range(-2f, 2f));
            yield return new WaitForEndOfFrame();
        }
    }
}

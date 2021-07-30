using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[DefaultExecutionOrder(1)]
public class PetEntity : MonoBehaviour
{
    public PetSO EvolutionTree;
    public string StageID;
    public PetStats Stats;

    ItemDrop target;
    Vector2 destination;
    Animator anim;
    GrowthStage stage;
    SpriteRenderer render;
    SpeechBubble speechBubble;
    List<Message> messages;
    public Message.MessageType ChatMode;
    public List<ItemStack> FoodHistory = new List<ItemStack>();

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        speechBubble = GetComponentInChildren<SpeechBubble>();
        UpdateEvolutionState(StageID);
        StartCoroutine(MovementRoutine());
        StartCoroutine(ChatRoutine());
        ItemDrop.OnConsumeEvent += OnConsumeFood;
    }

    void UpdateEvolutionState(string newStageID)
    {
        stage = EvolutionTree.GetStageByID(StageID = newStageID);
        if (stage != null)
        {
            anim.runtimeAnimatorController = stage.Animator;
            Stats = stage.DefaultStats;
            messages = stage.Messages;
            if (stage.ChatFont != null)
                speechBubble.SetFont(stage.ChatFont);
            speechBubble.CreateTextTyper(Stats.SpeakSpeed);
        }
    }

    IEnumerator MovementRoutine()
    {
        while(true)
        {
            Collider2D c = Physics2D.OverlapCircle(transform.position, 10, LayerMask.GetMask("Items"));
            if (c != null)
            {
                target = c.GetComponent<ItemDrop>();
                destination = target.transform.position;
            }
            while (Vector2.Distance(transform.position, destination) > 1 * Time.fixedDeltaTime * Stats.Speed)
            {
                Vector3 direction = ((Vector3)destination - transform.position).normalized;
                render.flipX = direction.x > 0;
                anim.SetBool("moving", true);
                transform.position += direction * Time.fixedDeltaTime * Stats.Speed;
                yield return new WaitForFixedUpdate();
            }
            anim.SetBool("moving", false);
            while (target != null)
            {
                target.Damage();
                yield return new WaitForSeconds(1); //maybe replace 1 by consumptionspeed?
            }
            yield return new WaitForSeconds(Random.Range(Stats.MoveInterval.x, Stats.MoveInterval.y));
            destination = new Vector2(Random.Range(-3f, 3f), Random.Range(-2f, 2f));
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnConsumeFood(ItemSO template)
    {
        foreach (var item in FoodHistory)
        {
            if (item.Template == template)
            {
                item.Count++;
                return;
            }
        }
        FoodHistory.Add(new ItemStack(template, 1));
    }

    IEnumerator ChatRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(Stats.SpeakInterval.x, Stats.SpeakInterval.y));
            Message m = GetAppropriateMessage();
            if(m != null)
            {
                speechBubble.SetText(m);
                yield return new WaitForSeconds(m.Duration);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    Message GetAppropriateMessage()
    {
        List<Message> candidates = new List<Message>();
        foreach (Message m in messages)
            if (ChatMode == m.Type)
                for (int i = 0; i < m.Weight; i++)
                    candidates.Add(m);
        if (candidates.Count > 0)
            return candidates[Random.Range(0, candidates.Count)];
        else
            return null;
    }
}

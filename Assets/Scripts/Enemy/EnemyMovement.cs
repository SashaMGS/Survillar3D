using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Для дефолт мобов")]
    public Transform Target;
    public float movementSpeed = 3f;
    public bool isAroundTarget;
    public NavMeshAgent agent;
    public bool isMob2;
    public bool isMob3;

    [Header("Для боссов")]
    public bool isBoss;
    public bool isRandomGo;
    public bool isUFO;
    public bool isWheel;
    public float defaultSpeed = 3f;
    public float speedIfAttack1 = 0f;
    public float speedIfAttack2 = 4f;

    [Header("Для призывателя")]
    public Transform[] TargetsArray = new Transform[8];
    public short isCurrentPos = 0;
    public float changePlaceTime = 3f;
    private bool circle = false;
    public bool goToPlayer = false;

    private void Start()
    {
        if (!isMob3)
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        else if (isMob3)
        {
            for (int i = 0; i < TargetsArray.Length; i++)
                TargetsArray[i] = GameObject.FindGameObjectWithTag("PlaceSummoner").transform.GetChild(i).GetComponent<Transform>();
        }
        if (isRandomGo)
        {
            if (isUFO)
                for (int i = 0; i < TargetsArray.Length; i++)
                    TargetsArray[i] = GameObject.FindGameObjectWithTag("PlacesUFO").transform.GetChild(i).GetComponent<Transform>();
            if (isWheel)
                for (int i = 0; i < TargetsArray.Length; i++)
                    TargetsArray[i] = GameObject.FindGameObjectWithTag("PlacesWheel").transform.GetChild(i).GetComponent<Transform>();
            StartCor_posSummuner();
        }

    }

    private void Update()
    {
        try
        {
            if (isMob3 || (isRandomGo && !goToPlayer))
            {
                agent.destination = TargetsArray[isCurrentPos].position;
                agent.speed = movementSpeed;
            }
            if (isRandomGo && goToPlayer)
            {
                agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
                agent.speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>().walkSpeed * 1.25f;
            }
        }
        catch (System.Exception)
        {
            Debug.Log("У тебя тут какая-то ошибка с боссом или мобом");
            throw;
        }

        if (!isMob3)
        {
            if (!isBoss)
            {
                if (!GetComponent<EnemyScript>().isOnTriggerPlayer)
                {
                    agent.speed = movementSpeed;
                    agent.destination = Target.position; // Точка преследования
                }
                else
                    agent.speed = 0f;
            }
            else
            {
                if (GetComponent<BossScript>() != null)
                    if (!GetComponent<BossScript>().isOnTriggerPlayer)
                    {
                        agent.speed = movementSpeed;
                        agent.destination = Target.position; // Точка преследования
                    }
                    else
                        agent.speed = 0f;
                if (GetComponent<BossScript3>() != null)
                    if (!GetComponent<BossScript3>().isOnTriggerPlayer)
                    {
                        agent.speed = movementSpeed;
                        agent.destination = Target.position; // Точка преследования
                    }
                    else
                        agent.speed = 0f;
                if (GetComponent<BossScript4>() != null)
                    if (!GetComponent<BossScript4>().isOnTriggerPlayer)
                    {
                        agent.speed = movementSpeed;
                        agent.destination = Target.position; // Точка преследования
                    }
                    else
                        agent.speed = 0f;
            }

        }

    }

    public void StartCor_posSummuner()
    {
        StartCoroutine(posSummuner());
    }

    public IEnumerator posSummuner()
    {
        while (isMob3 || isRandomGo)
        {
            if (isMob3)
            {
                yield return new WaitForSeconds(changePlaceTime);
                isCurrentPos = (short)Random.Range(0, TargetsArray.Length - 1);
            }
            if (isRandomGo)
            {
                if (isUFO)
                {
                    yield return new WaitForSeconds(changePlaceTime);
                    int i = Random.Range(0, 2);
                    if (i == 0) circle = false;
                    if (i == 1) circle = true;
                    if (i == 2) circle = false;
                    if (isCurrentPos < 7 && !circle)
                        isCurrentPos++;
                    if (isCurrentPos == 7 && !circle)
                        isCurrentPos = 0;
                    if (isCurrentPos > 0 && circle)
                        isCurrentPos--;
                    if (isCurrentPos == 0 && circle)
                        isCurrentPos = 7;
                }
                else if (isWheel)
                {
                    yield return new WaitForSeconds(changePlaceTime);
                    isCurrentPos = (short)Random.Range(0, TargetsArray.Length - 1);
                }
                else
                    yield return null;
            }
        }
        yield break;
    }
}
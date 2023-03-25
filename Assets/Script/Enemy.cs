using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class Enemy : NetworkBehaviour
{
    private Rigidbody rig;
    private Transform player;

    public float speed;


    NavMeshAgent agent;


    Vector3 targetOldPosition = Vector3.zero;

    public float checkEvery = 0.2f; // check every one second
    float time;

    // Start is called before the first frame update
    void Start()
    {


        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //player = FindGameObjectWithTag("Player").transform; // GameObject.Find("Player Variant").transform;       //GameObject.FindGameObjectWithTag("Player").transform;
        //agent.destination = player.position;
        //agent.speed = speed;
        //agent.angularSpeed = speed * 1.4f;

    }

    // Update is called once per frame
    void Update()
    {
        //player = FindGameObjectWithTag("Player").transform; // GameObject.Find("Player Variant").transform;
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //transform.LookAt(2 * transform.position - player.position);
        //agent.destination = player.position;

        //agent.SetDestination(player.position);

        //time += Time.deltaTime;
        //if (time >= checkEvery)
        //{
        //    if (player.position != targetOldPosition)
        //    {
        //        //agent.SetDestination(player.position);
        //        agent.destination = player.position;
        //        targetOldPosition = player.position;
        //    }
        //    time = 0;
        //}



    }
}

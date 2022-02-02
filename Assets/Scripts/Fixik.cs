using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fixik : MonoBehaviour
{
    private PillowManager pillowManager;
    private Vector3 startPosition, destination;
    [SerializeField] private Colors color1, color2;
    [SerializeField] private Renderer quad1, quad2;
    private NavMeshAgent agent;

   void Awake()
    {
        pillowManager = FindObjectOfType<PillowManager>();
        quad1.material.color= pillowManager.SetColor(color1);
        quad2.material.color = pillowManager.SetColor(color2);
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }
    public bool HaveColor(Colors color)
    {
        if (color == color1|| color == color2)
        {
            return true;
        }
        else return false;
    }

    public async void MoveFixik(Vector3 waypoint)
    {
        agent.destination = waypoint;
        destination = waypoint;
        StartCoroutine(FixTimer());
        Debug.Log(waypoint);
    }

    private IEnumerator FixTimer()
    {
        while (Vector3.Distance(transform.position, destination) >0.1)
        {            
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(agent.pathEndPosition);
        yield return new WaitForSeconds(3);
        pillowManager.FixComplete();
        agent.destination = startPosition;
        yield break;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowManager : MonoBehaviour
{
    private Colors colors;
   // public Renderer renderer;
     private GameObject[] pillows;
    private Fixik[] fixix;
    public GameObject currentPillow;

    private void Awake()
    {
        pillows = new GameObject[transform.childCount];
        for (int i =0; i<transform.childCount;i++)
        {
            pillows[i] = transform.GetChild(i).gameObject;
        }
    }

    void Start()
    {
        StartCoroutine( ColorRandom());
    }


    private IEnumerator ColorRandom()
    {
        currentPillow = pillows[Random.Range(0,pillows.Length)];
        int time = Random.Range(3, 10);
        colors = (Colors)Random.Range(0,3);
        yield return new WaitForSeconds(time);
        currentPillow.GetComponentInChildren<Renderer>().material.color = SetColor(colors);
        CallFixik(currentPillow.transform.GetChild(1).position);
        yield break;
    }

    public Color SetColor(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                return Color.red;
            case Colors.Green:
                return Color.green;
            case Colors.Blue:
                return Color.blue;
        }
            
        return Color.white;
    }

    public void CallFixik(Vector3 pillowPoint)
    {
        fixix = FindObjectsOfType<Fixik>();
        foreach (Fixik fixik in fixix)
        {
            if (fixik.HaveColor(colors))
            {
                fixik.MoveFixik(pillowPoint);
                break;
            }
        }
    }

    public void FixComplete()
    {
        currentPillow.GetComponentInChildren<Renderer>().material.color = Color.white;
        StartCoroutine(ColorRandom());
    }
}

public enum Colors
{
    Red,
    Green,
    Blue
}
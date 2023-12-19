using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningText : MonoBehaviour
{
    public GameObject warningText;
    public int rows = 3;
    public float speed = 1f;
    public float range = 10f;
    public float space = 1f;


    private float spaceTimer;
    private float height;
    private float length;

    private Coroutine spawningText;

    private void Start()
    {
        height = warningText.GetComponent<Renderer>().bounds.size.y + .1f;
        length = warningText.GetComponent<Renderer>().bounds.size.x;
        spaceTimer = space / speed;
    }

    IEnumerator spawnText()
    {
        spawnExtra();

        while (true)
        {
            for (int i = 0; i < rows; i++)
            {
                GameObject wt = Instantiate(warningText);
                MoveWarningText moveWarningText = wt.GetComponent<MoveWarningText>();
                moveWarningText.transform.position = transform.position;
                moveWarningText.speed = speed;
                moveWarningText.maxDistance = range;

                moveWarningText.transform.position += new Vector3(0, i * height);

                if (i % 2 == 1)
                {
                    moveWarningText.transform.position += new Vector3(range, 0);
                    moveWarningText.speed = -speed;
                }
            }

            yield return new WaitForSeconds(spaceTimer);
        }
    }

    void spawnExtra()
    {
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < (int)((int)range / length); i++)
            {
                GameObject wt = Instantiate(warningText);
                MoveWarningText moveWarningText = wt.GetComponent<MoveWarningText>();
                moveWarningText.transform.position = transform.position;
                moveWarningText.speed = speed;
                moveWarningText.maxDistance = range;
                moveWarningText.transform.position += new Vector3((i + 1) * (space), 0);

                moveWarningText.transform.position += new Vector3(0, j * height);

                if (j % 2 == 1)
                {
                    moveWarningText.transform.position += new Vector3(range, 0);
                    moveWarningText.transform.position -= new Vector3((i + 1) * (space), 0) * 2;
                    moveWarningText.speed = -speed;
                }
            }

        }
    }

    public void spawnWarningScreen()
    {
        spawningText = StartCoroutine(spawnText());
    }

    public void stopWarningScreen()
    {
        StopCoroutine(spawningText);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStars : MonoBehaviour
{
    [SerializeField]
    List<GameObject> starPrefabs;
    [SerializeField]
    int count = 200;

    [SerializeField]
    float n_x = -1.6f;
    [SerializeField]
    float x = 1.6f;

    [SerializeField]
    float n_y = -2.75f;
    [SerializeField]
    float y = 2.75f;

    [SerializeField]
    float border_y;

    float pixelSize = .01f;
    float minDistance;

    List<Vector3> positions = new List<Vector3>();

    GameObject highestStar;

    private void Start()
    {
        border_y = y;
        minDistance = pixelSize * 4;
        placeStars();
        n_y += y*2;
        y += y*2;
    }

    private void Update()
    {
        StartCoroutine(checkToPlaceStars());
    }

    IEnumerator checkToPlaceStars()
    {
        yield return new WaitForSeconds(0.1f);
        if (highestStar.transform.position.y < border_y)
        {
            placeStars();
        }
    }


    void placeStars()
    {
        Vector3 newPos;
        bool validPosition;

        int maxTries = 10;
        int timesTried;

        float highestStarY = -10f;

        for (int i = 0; i < count; i++)
        {
            timesTried = 0;

            do
            {
                newPos = new Vector3(Random.Range(n_x, x), Random.Range(n_y, y), 0);

                validPosition = isValidPosition(newPos);
                timesTried += 1;
            } while (!validPosition && timesTried < maxTries);

            if (maxTries < timesTried)
            {
                continue;
            }

            positions.Add(newPos);
            GameObject star = starPrefabs[(Random.Range(0, starPrefabs.Count-1))];
            star = Instantiate(star, newPos, Quaternion.identity);

            if (newPos.y > highestStarY){
                highestStarY = newPos.y;
                highestStar = star;
            }

        }
    }

    bool isValidPosition(Vector3 newPos)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            if (Vector3.Distance(positions[i], newPos) < minDistance)
            {
                return false;
            }
        }

        return true;
    }
}

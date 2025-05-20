using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRandom : MonoBehaviour
{

    public int newCirclePositionMinDistance=0, newCirclePositionMaxDistance = 0;
    public GameObject Eggs;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   public void GetARandomPositionInTorus()
    {

        Vector3 dest = transform.position;

        while (Vector3.Distance(dest, transform.position) < newCirclePositionMinDistance)
        {
            Vector3 randomPosition = Random.insideUnitSphere * newCirclePositionMaxDistance;
            randomPosition = new Vector3(randomPosition.x, transform.position.y, randomPosition.z);
            dest = randomPosition + gameObject.transform.position;
        }
       // return dest;
        GameObject temp=    Instantiate(Eggs);
        temp.transform.position = dest;
    
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientHandler : MonoBehaviour
{

    public Transform objectsSpawnPlaceHolder;

    public List<GameObject> objectParts;

    public float xOffset = 2f;
    public float yOffset = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLevelParts()
    {
        if (GameManager.instance.levelObjects != null)
        {
            for (int i = 0; i < GameManager.instance.levelObjects.Count; i++)
            {
                GameObject part = Instantiate(GameManager.instance.levelObjects[i]);
                float x = Random.Range(objectsSpawnPlaceHolder.position.x - xOffset, objectsSpawnPlaceHolder.position.x + xOffset);
                float y = Random.Range(objectsSpawnPlaceHolder.position.y - yOffset, objectsSpawnPlaceHolder.position.y + yOffset);
                part.transform.position = new Vector3(x, y, part.transform.position.z);
                objectParts.Add(part);
            }
        }
        if(objectParts.Count > 0)
        {

        }
    }
}

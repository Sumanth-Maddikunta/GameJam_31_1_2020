using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PatientHandler : MonoBehaviour
{

    public Transform objectsSpawnPlaceHolder;

    public List<GameObject> objectParts;

    public float xOffset = 2f;
    public float yOffset = 4f;

    public void CreateLevelParts()
    {
        StartCoroutine(CreateLevelPartsCoroutine());
    }
    public IEnumerator CreateLevelPartsCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (GameManager.instance.levelObjects != null)
        {
            ObjectControl temp = GameManager.instance.levelObjects[GameManager.instance.levelNo - 1].GetComponent<ObjectControl>();
            if (temp != null)
            {
                for (int i = 0; i < temp.brokenObjs.Count; i++)
                {
                    GameObject part = Instantiate(temp.brokenObjs[i]);
                    part.transform.localScale = Vector3.one * 0.5f;
                    float x = Random.Range(objectsSpawnPlaceHolder.position.x - xOffset, objectsSpawnPlaceHolder.position.x + xOffset);
                    float y = Random.Range(objectsSpawnPlaceHolder.position.y, objectsSpawnPlaceHolder.position.y + yOffset);
                    part.transform.position = transform.position;
                    Vector3 tempPos = part.transform.position;
                    tempPos.z += 0.5f;
                    part.transform.position = tempPos;
                    part.transform.DOMove(new Vector3(x, y, part.transform.position.z - 1.5f), 0.3f);
                    objectParts.Add(part);
                }
            }
            else
            {
                Debug.LogWarning("PARTS NOT FOUND");
            }
        }
        yield return new WaitForSeconds(1.5f);

        for(int i = 0;i<objectParts.Count;i++)
        {
            objectParts[i].transform.DOMove(GameManager.instance.platformObject.transform.position, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.instance.OnDialoguesCompleted();

        yield return new WaitForSeconds(0.5f);
        DestroyObjects();
    }

    public void DestroyObjects()
    {
        if(objectParts.Count > 0)
        {
            for( int i=0;i<objectParts.Count;i++)
            {
                GameObject temp = objectParts[i];
                objectParts.RemoveAt(i);
                Destroy(temp);
            }
        }
    }
}

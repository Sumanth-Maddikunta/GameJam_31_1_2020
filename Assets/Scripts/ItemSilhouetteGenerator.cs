using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSilhouetteGenerator : MonoBehaviour
{
    public List<ItemComponent> components;

    float silChance = 0.3f;
    int childCount = 0;
    int silCount = 0;
    private void Start()
    {
        components = new List<ItemComponent>();
        childCount = transform.childCount;

        silCount = (int)(silChance * childCount);

        GenerateSilhouetteObjects();
    }

    void GenerateSilhouetteObjects()
    {
        for (int i = 0; i < childCount; i++)
        {
            GameObject itemObject = transform.GetChild(i).gameObject;
            components.Add(itemObject.AddComponent<ItemComponent>());
            //components[i].rb = itemObject.GetComponent<Rigidbody>();
            components[i].meshRenderer = itemObject.GetComponent<MeshRenderer>();
            
            components[i].componentId = i + 1;

            if (Random.value < silChance || (childCount - 1 - i) <= silCount)
            {
                silCount--;
                components[i].SetState(EItemState.SILHOUETTE);

                ItemComponent newComp = Instantiate(components[i]);
                newComp.transform.position = Vector3.up * 4;
                newComp.SetState(EItemState.BROKEN);
            }
            else
            {
                components[i].SetState(EItemState.FIXED);
            }
        }
    }
}

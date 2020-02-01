﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

[RequireComponent(typeof(ObjectControl))]
public class ItemSilhouetteGenerator : MonoBehaviour
{
    public List<ItemComponent> components;

    float silChance = 0.5f;
    int childCount = 0;
    int silCount = 0;

    public bool onRotated;

    ObjectControl control;
    private void Start()
    {
        control = GetComponent<ObjectControl>();

        components = new List<ItemComponent>();
        childCount = control.brokenObjs.Count;

        silCount = Mathf.CeilToInt((silChance * childCount));

        GenerateSilhouetteObjects();

        GameManager.instance.OnRotationCompleted = OnObjectRotated;
        //MoveToPlacements();
    }

    void MoveToPlacements()
    {
        for (int i = 0; i < control.placmeents.Count; ++i)
        {
            control.brokenObjs[i].transform.DOMove(control.placmeents[i].transform.position, 1);
        }
    }

    void GenerateSilhouetteObjects()
    {
        for (int i = 0; i < childCount; i++)
        {
            GameObject itemObject = control.brokenObjs[i];
            components.Add(itemObject.AddComponent<ItemComponent>());
            components[i].meshRenderer = itemObject.GetComponent<MeshRenderer>();
            
            components[i].componentId = i + 1;

            if (silCount > 0 &&( Random.value < silChance || (childCount - 1 - i) <= silCount))
            {
                silCount--;
                components[i].SetState(EItemState.BROKEN);

                ItemComponent newComp = Instantiate(components[i],transform);
                newComp.SetState(EItemState.SILHOUETTE);
                newComp.transform.position = components[i].transform.position;
                components[i].transform.position = control.placmeents[i].transform.position;
                components[i].transform.parent = null;
                components[i].SnapComponent = newComp;
            }
            else
            {
                components[i].SetState(EItemState.FIXED);
            }
        }
    }

    private void Update()
    {
        if(onRotated)
        {
            onRotated = false;
            OnObjectRotated();
        }
    }

    public void OnObjectRotated()
    {
        for(int i = 0;i < components.Count;i++)
        {
            ItemComponent comp = components[i];
            if(comp.state.Equals(EItemState.BROKEN))
            {
                comp.transform.rotation = comp.SnapComponent.transform.rotation;
            }
        }
    }
}

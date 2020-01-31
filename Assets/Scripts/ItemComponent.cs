using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public EItem item = EItem.NONE;
    public EItemState state = EItemState.NONE;
    public int componentId = 0;

    public Material silhouetteMaterial;
    public Material normalMaterial;

    Rigidbody rb;
    MeshRenderer meshRenderer;

    private float mZPos;
    private Vector3 mOffset;
    private Vector3 pos;
    private bool isSelected = false;

    public float zUnitMovement = 50f;

    private void Start()
    {
        zUnitMovement = 30f;
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();

        switch (state)
        {
            case EItemState.NONE:
                break;
            case EItemState.FIXED:
                rb.isKinematic = true;
                rb.useGravity = false;
                meshRenderer.material = normalMaterial;

                break;
            case EItemState.SILHOUETTE:
                rb.isKinematic = true;
                rb.useGravity = false;
                meshRenderer.material = silhouetteMaterial;

                break;
            case EItemState.BROKEN:
                rb.isKinematic = false;
                rb.useGravity = true;
                meshRenderer.material = normalMaterial;

                break;
        }
    }


    void Update()
    {
        if (isSelected)
        {
            pos.z = 0;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                pos.z += zUnitMovement * Time.deltaTime;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backward
            {
                pos.z -= zUnitMovement * Time.deltaTime;
            }
            mOffset += pos; 
        }
    }


    public void OnMouseOver()
    {
        Debug.Log("OnMouseObver");
        if(Input.GetMouseButtonDown(1))//This acts as a replacement for OnMouseDown since it dosen't work with right mouse click
        {
            mZPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPosition();
            isSelected = true;
        }
        if(Input.GetMouseButton(1))//Acys as drag
        {
            transform.position = GetMouseWorldPosition() + mOffset;
        }
        if(Input.GetMouseButtonUp(1))//Acts as mouse up
        {
            isSelected = false;
        }
    }
    
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZPos;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

   
}

public enum EItem
{
    NONE = -1,
    CUBE,
}

public enum EItemState
{
    NONE = -1,
    FIXED,
    SILHOUETTE,
    BROKEN,

}

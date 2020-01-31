using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public EItem item = EItem.NONE;
    [HideInInspector] public EItemState state = EItemState.NONE;
    public int componentId = 0;

    public Material silhouetteMaterial;
    public Material normalMaterial;

    Rigidbody rb;
    MeshRenderer meshRenderer;

    private float mZPos;
    private Vector3 mOffset;
    private Vector3 pos;
    private bool isSelected = false;
    private bool onMouseOver = false;

    public float zUnitMovement = 50f;

    private void Start()
    {
        zUnitMovement = 30f;
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();

        SetObjectProperties();
    }

    public void SetObjectProperties()
    {
        switch (state)
        {
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

            case EItemState.PICKEDUP:
                rb.isKinematic = true;
                rb.useGravity = false;
                meshRenderer.material = normalMaterial;
                break;
        }
    }


    void Update()
    {
        if (isSelected && PlayerController.instance.currentHandling == this)
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

        if (onMouseOver )
        {
            if (Input.GetMouseButtonDown(1))//This acts as a replacement for OnMouseDown since it dosen't work with right mouse click
            {
                if (PlayerController.instance.currentHandling == null)
                {
                    PlayerController.instance.currentHandling = this;
                    mZPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                    mOffset = gameObject.transform.position - GetMouseWorldPosition();
                    isSelected = true;
                }
            }
            if (Input.GetMouseButton(1) && PlayerController.instance.currentHandling == this)//Acts as drag
            {
                transform.position = GetMouseWorldPosition() + mOffset;
            }
            if (Input.GetMouseButtonUp(1) && PlayerController.instance.currentHandling == this)//Acts as mouse up
            {
                PlayerController.instance.currentHandling = null;
                isSelected = false;
            }
        }

    }

    void SetState(EItemState itemState)
    {
        if (state == itemState)
        {
            return;
        }

        state = itemState;
        SetObjectProperties();
    }
    public void OnMouseOver()
    {
        onMouseOver = true;
    }

    public void OnMouseUp()
    {
        onMouseOver = false;
        PlayerController.instance.currentHandling = null;
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
    PICKEDUP,
    BROKEN,

}

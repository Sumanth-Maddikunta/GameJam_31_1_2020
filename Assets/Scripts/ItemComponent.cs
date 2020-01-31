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

    private void Start()
    {
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

    void SetState(EItemState itemState)
    {
        if (state == itemState)
        {
            return;
        }

        state = itemState;
        SetObjectProperties();
    }
    public void OnMouseDown()
    {
        mZPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPosition();

    }

    public void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mOffset;
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

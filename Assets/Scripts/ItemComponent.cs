using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public EItem item = EItem.NONE;
    [HideInInspector]public EItemState state = EItemState.NONE;
    public int componentId = 0;

    public Material silhouetteMaterial;
    public Material normalMaterial;

    Rigidbody rb;
    MeshRenderer meshRenderer;

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
        if(state == itemState)
        {
            return;
        }

        state = itemState;
        SetObjectProperties();
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

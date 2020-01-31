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

    private void Start()
    {
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

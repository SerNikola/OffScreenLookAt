using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenTarget : MonoBehaviour
{
    const string PARENT_TAG = "Pointers";

    public GameObject pointerPrefab;

    GameObject pointer;
    Camera mainCamera;
    Transform parent;
    Renderer targetRenderer;
    // Start is called before the first frame update
    void Start()
    {
        targetRenderer = transform.GetChild(0).GetComponent<Renderer>();
        mainCamera = Camera.main;
        parent = FindObjectOfType<Canvas>().transform;//GameObject.FindGameObjectWithTag(PARENT_TAG).transform;
    }
    private void OnBecameInvisible()
    {
        if (pointer == null)
        {
            pointer = Instantiate(pointerPrefab, parent);
            pointer.GetComponent<OffScreenPointer>().target = transform;
        }
    }
    private void OnBecameVisible()
    {
        if (pointer != null)
            Destroy(pointer);
    }
    void Update()
    {
        if (targetRenderer.isVisible)
        {

            if (pointer != null)
                Destroy(pointer);
        }

        else
        {
            if (pointer == null)
            {
                pointer = Instantiate(pointerPrefab, parent);
                pointer.GetComponent<OffScreenPointer>().target = transform;
            }
        }
    }
}

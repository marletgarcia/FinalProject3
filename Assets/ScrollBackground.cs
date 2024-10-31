using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollVertical = 0.5f;

    private Renderer re;

    // Start is called before the first frame update
    void Start()
    {
        re = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
       Vector2 offset = new Vector2(0, Time.time * scrollVertical);
        re.material.mainTextureOffset = offset;
    }
}

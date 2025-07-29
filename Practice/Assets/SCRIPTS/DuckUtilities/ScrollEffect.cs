using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollEffect : MonoBehaviour
{
    RawImage img;
    [SerializeField] float x,y;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
    }
}

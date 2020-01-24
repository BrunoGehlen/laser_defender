using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScroolSpeed = 0;
    Material myMaterial;
    Vector2 offSet;
    void Start() {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScroolSpeed);
    }
    void Update() {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PettingDebug : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Start(){
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColor(Material material){
        meshRenderer.material = material;
    }
}

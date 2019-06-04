using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour
{
    public List<Material> materials = new List<Material>();

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void SwitchMaterialTo(int _index)
    {
        rend.material = materials[_index];
    }
}

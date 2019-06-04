using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAnimation : MonoBehaviour
{
    public bool isEnabled = true;

    public Material materialDissolve;

    private Renderer[] childeren;

    [SerializeField]
    bool debugTest = false;

    [Range(0, 1)]
    public float progress = 1f;

    static float time = 0f;

    public float transMinVal = 0f, transMaxVal = 1f;

    void Start()
    {
        if (!isEnabled)
            return;

        DissolveSetup();
    }

    void Update()
    {
        // If debug is false the dont run the next code
        if (!debugTest || !isEnabled)
            return;


        if (Input.GetKeyUp(KeyCode.A))
        {
            Dissolve(2f, 1f);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            Dissolve(2f, 0f);
        }

    }



    void DissolveSetup()
    {
        //Get all childeren and set their Material to the dissolve one, when the child already has an Texture place that in the dissolve Material

        ChangeMaterialOfChilderen(materialDissolve);
        UpdateMaterialOfChilderen();
    }
    

    /// <summary>
    /// Dissolves the object instantly
    /// </summary>
    /// <param name="_in">true: Dissolve In - false: Dissolve Out</param>
    /// <returns></returns>
    public void DissolveInstantly(bool _in)
    {
        if (!isEnabled)
            return;

        if (_in)
            Dissolve(transMaxVal, transMinVal);
        else
            Dissolve(transMinVal, transMaxVal);
    }



    public void DissolveIn()
    {
        if (!isEnabled)
            return;

        Dissolve(2f, transMaxVal);
    }



    public void DissolveOut()
    {
        if (!isEnabled)
            return;

        Dissolve(2f, transMinVal);
    }



    void Dissolve(float _time, float _target)
    {
        if (!isEnabled)
            return;

        iTween.ValueTo(gameObject, iTween.Hash("from", progress, "to", _target, "time", _time, "onupdate", "TweenProgress"));
    }

   


    void ChangeMaterialOfChilderen(Material _newMat)
    {
        if (!isEnabled)
            return;

        childeren = GetComponentsInChildren<Renderer>();

        //Change all the childeren their materials
        foreach (Renderer _rend in childeren)
        {
            var _mats = new Material[_rend.materials.Length];

            //Loop through all materials of the child
            for (var i = 0; i < _rend.materials.Length; i++)
            {
                Texture _oldTex = _mats[i].mainTexture;

                _mats[i] = _newMat;
                _mats[i].mainTexture = _oldTex;
            }
            _rend.materials = _mats;
        }
    }

    void UpdateMaterialOfChilderen()
    {
        if (!isEnabled)
            return;

        //Change all the childeren their materials
        foreach (Renderer _rend in childeren)
        {
            //print(_rend.material.GetFloat("_Progress"));
            _rend.material.SetFloat("_Progress", progress);
        }
    }


    void TweenProgress(float _value)
    {
        if (!isEnabled)
            return;

        progress = _value;
        UpdateMaterialOfChilderen();
    }
}

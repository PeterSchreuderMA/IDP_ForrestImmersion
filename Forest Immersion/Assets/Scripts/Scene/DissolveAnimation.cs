using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAnimation : MonoBehaviour
{
    public Material materialDissolve;

    private Renderer[] childeren;

    [Range(0, 1)]
    public float progress = 0f;

    static float time = 0f;

    void Start()
    {
        DissolveSetup();
    }

    void Update()
    {
        //DissolveTest();
        if (Input.GetKeyUp(KeyCode.A))
        {
            //iTween.ValueTo(gameObject, iTween.Hash());
            Dissolve(2f, 1f);
            //DissolveIn();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            Dissolve(2f, 0f);
            //DissolveOut();
        }

    }

    void DissolveSetup()
    {
        //Get all childeren and set their Material to the dissolve one, when the child already has an Texture place that in the dissolve Material

        ChangeMaterialOfChilderen(materialDissolve);
        UpdateMaterialOfChilderen();
    }


    public bool DissolveIn()
    {
        bool _return = false;

        _return = Dissolve(2f, 1f);


        return _return;
    }

    public bool DissolveOut()
    {


        return Dissolve(2f, 0f);
    }

    bool Dissolve(float _time, float _target)
    {
        bool _return = false;
        
        iTween.ValueTo(gameObject, iTween.Hash("from", progress, "to", _target, "time", _time, "onupdate", "TweenProgress"));

        if (progress == _target)
            _return = true;

        return _return;
    }

   


    void ChangeMaterialOfChilderen(Material _newMat)
    {
        childeren = GetComponentsInChildren<Renderer>();

        //Change all the childeren their materials
        foreach (Renderer _rend in childeren)
        {
            var _mats = new Material[_rend.materials.Length];

            //Loop through all materials of the child
            for (var j = 0; j < _rend.materials.Length; j++)
            {
                _mats[j] = _newMat;
            }
            _rend.materials = _mats;
        }
    }

    void UpdateMaterialOfChilderen()
    {

        //Change all the childeren their materials
        foreach (Renderer _rend in childeren)
        {
            //print(_rend.material.GetFloat("_Progress"));
            _rend.material.SetFloat("_Progress", progress);
        }
    }


    void TweenProgress(float _value)
    {
        progress = _value;
        UpdateMaterialOfChilderen();
    }
}

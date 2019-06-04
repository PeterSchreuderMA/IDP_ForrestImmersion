using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAnimation : MonoBehaviour
{
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
        DissolveSetup();
    }

    void Update()
    {
        // If debug is false the dont run the next code
        if (!debugTest)
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
    public bool DissolveInstantly(bool _in)
    {
        if (_in)
            return Dissolve(transMaxVal, transMinVal);
        else
            return Dissolve(transMinVal, transMaxVal);
    }



    public bool DissolveIn()
    {
        bool _return = false;

        _return = Dissolve(2f, transMaxVal);


        return _return;
    }



    public bool DissolveOut()
    {
        return Dissolve(2f, transMinVal);
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
            for (var i = 0; i < _rend.materials.Length; i++)
            {
                _mats[i] = _newMat;
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

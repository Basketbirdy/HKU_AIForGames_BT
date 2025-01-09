using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTInstantiateNode<T> : BTBaseNode where T : struct
{
    private Vector3 origin;
    private string originBBVariable;
    private Vector3 rotation;
    private GameObject obj;
    private T setupData;
    private Transform parent;

    public BTInstantiateNode(string _originBBVariable, Vector3 _rotation, GameObject _obj, T _setupData, Transform _parent = null)
    {
        originBBVariable = _originBBVariable;
        rotation = _rotation;
        obj = _obj;
        setupData = _setupData;

        parent = _parent;
    }

    public BTInstantiateNode(Vector3 _origin, Vector3 _rotation, GameObject _obj, T _setupData, Transform _parent = null)
    {
        origin = _origin;
        originBBVariable = "";
        rotation = _rotation;
        obj = _obj;
        setupData = _setupData;

        parent = _parent;
    }

    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {

    }

    protected override TaskStatus OnUpdate()
    {
        if(obj == null) { return TaskStatus.FAILURE; }

        if(originBBVariable != "") { origin = blackboard.GetVariable<Transform>(originBBVariable).position; }
        Quaternion rot = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

        GameObject newObj = GameObject.Instantiate(obj, origin, rot, parent);
        ISetup<T> setup = newObj.GetComponent<ISetup<T>>();

        if(setup != null) { setup.Setup(setupData); }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {

    }
}

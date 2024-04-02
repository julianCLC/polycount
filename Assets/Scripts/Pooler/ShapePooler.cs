using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePooler : PoolerBase<ShapeScript>
{
    [SerializeField] private ShapeScript _shapeScript;

    private void Start() {
        InitPool(_shapeScript, 50, 100);
    }

    protected override void GetSetup(ShapeScript obj){
        base.GetSetup(obj);
        obj.OnUseSetup(killAction);
    }

    private void killAction(ShapeScript _script){
        Release(_script);
    }
}

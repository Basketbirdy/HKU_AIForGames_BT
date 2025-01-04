using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChangeDynamicTextNode : BTBaseNode
{
    private Transform parent;
    private string text;
    private IDynamicText dynamicText;

    public BTChangeDynamicTextNode(string _text, Transform _parent)
    {
        parent = _parent;
        text = _text;
    }

    protected override TaskStatus OnUpdate()
    {
        dynamicText = parent.GetComponent<IDynamicText>();

        if(dynamicText == null) { return TaskStatus.FAILURE; }

        dynamicText.ChangeText(text);
        return TaskStatus.SUCCESS;
    }

}

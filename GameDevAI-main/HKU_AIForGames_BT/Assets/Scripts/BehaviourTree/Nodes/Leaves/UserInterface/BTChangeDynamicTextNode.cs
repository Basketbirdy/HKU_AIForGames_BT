using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChangeDynamicTextNode : BTBaseNode
{
    private string text;
    private IDynamicText dynamicText;

    public BTChangeDynamicTextNode(string _text)
    {
        text = _text;
    }

    protected override TaskStatus OnUpdate()
    {
        dynamicText = self.GetComponent<IDynamicText>();

        if(dynamicText == null) { return TaskStatus.FAILURE; }

        dynamicText.ChangeText(text);
        return TaskStatus.SUCCESS;
    }

}

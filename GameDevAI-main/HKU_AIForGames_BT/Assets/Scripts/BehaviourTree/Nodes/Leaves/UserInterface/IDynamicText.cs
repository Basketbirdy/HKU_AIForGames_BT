using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IDynamicText
{
    TextMeshProUGUI TextObj { get; }
    void ChangeText(string _text);
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityUI : MonoBehaviour, IDynamicText
{
    [SerializeField] private GameObject uiPrefab;

    private GameObject obj;

    private TextMeshProUGUI textObj;
    public TextMeshProUGUI TextObj => textObj;

    private void Awake()
    {
        obj = Instantiate(uiPrefab);
        
        FollowObject follow = obj.GetComponent<FollowObject>();

        if(follow != null)
        {
            follow.SetTarget(transform);
        }
        else
        {
            Debug.LogError($"Could not find a follow object on instantiated prefab");
        }

        textObj = obj.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeText(string _text)
    {
        if(TextObj == null)
        {
            Debug.LogWarning($"Could not find TextMeshProUGUI on object with name: {gameObject.name}");
            return;
        }

        TextObj.text = _text;
    }

}

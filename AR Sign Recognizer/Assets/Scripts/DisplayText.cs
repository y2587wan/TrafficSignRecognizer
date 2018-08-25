using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {
    [SerializeField]
    private Text text;
    private void Start()
    {
        text.text = gameObject.name;
    }
}

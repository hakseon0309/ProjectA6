using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UITK_Manager : MonoBehaviour
{
    private VisualElement _body;
    private Button _btn_Start;
    


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _body = root.Q<VisualElement>("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

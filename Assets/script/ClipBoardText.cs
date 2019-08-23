using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipBoardText : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void InputFieldClip(){
        this.GetComponent<InputField>().text = UniClipboard.Clipboard.Text;
    }
}

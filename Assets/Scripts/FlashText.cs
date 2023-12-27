using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    Text text;
    string saveText;
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<Text>();
        saveText = text.text;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        while (true)
        {

            text.text = "";



            yield return new WaitForSeconds(0.3f);



            text.text = saveText;



            yield return new WaitForSeconds(0.3f);
        }
    }
}

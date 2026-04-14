using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GetCharacters : MonoBehaviour
{

    public TMP_Text listing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("GetResults");
    }

    IEnumerator GetResults()
    {
        using(UnityWebRequest request = UnityWebRequest.Get("http://localhost/get_characters.php"))
        {
            yield return request.SendWebRequest(); // waits for result

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log("Success: " + request.downloadHandler.text);
                    listing.text = request.downloadHandler.text; // update UI element
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Connection Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("Protocol Error: " + request.error);
                    break;
            }
        }
    }
}

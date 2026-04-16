using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CreateCharacter : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Dropdown avatarClass;
    public Slider experience;
    public Button submit;

    private string characterName;
    private string classText;
    private float experienceNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        submit.onClick.AddListener(AddData);
    }

    void AddData()
    {
        Debug.Log("Button clicked");
        characterName = nameInput.text;
        classText = avatarClass.options[avatarClass.value].text;
        experienceNum = experience.value;

        StartCoroutine("PostData");
    }

    IEnumerator PostData()
    {
        WWWForm form = new WWWForm(); // should be new WWW object
        form.AddField("name", characterName);
        form.AddField("avatarClass", classText);
        form.AddField("experience", (experienceNum * 100).ToString());

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/insert_character.php", form))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log("Success: " + webRequest.downloadHandler.text);
                    //listing.text = request.downloadHandler.text; // update UI element
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Connection Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("Protocol Error: " + webRequest.error);
                    break;
            }
        }
    }
}


using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{
    public int characterIndex;
    public GameObject[] allCharacters;
    public Button selectButton;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter");
        if (characterIndex == 0)
        {
            allCharacters[0].SetActive(true);
        }
        else
        {
            allCharacters[characterIndex].SetActive(true);
        }
        UpdateUI();
    }
    private void Update()
    {
        if (Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x > Screen.width / 3 && Input.mousePosition.x < Screen.width - (Screen.width / 3))
        {
            if (SwipeManager.swipeRight)
            {
                NextCharacter();
            }
            else if (SwipeManager.swipeLeft)
            {
                PreviousCharacter();
            }
        }
    }

    public void NextCharacter()
    {
        allCharacters[characterIndex].SetActive(false);
        characterIndex++;
        if (characterIndex == allCharacters.Length)
            characterIndex = 0;
        allCharacters[characterIndex].SetActive(true);
        UpdateUI();
    }
    public void PreviousCharacter()
    {
        allCharacters[characterIndex].SetActive(false);
        characterIndex--;
        if (characterIndex == -1)
            characterIndex = allCharacters.Length - 1;
        allCharacters[characterIndex].SetActive(true);

        UpdateUI();
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        UpdateUI();

    }
    private void UpdateUI()
    {
        if (characterIndex == PlayerPrefs.GetInt("SelectedCharacter"))
        {
            selectButton.gameObject.SetActive(false);
        }
        else
        {
            selectButton.gameObject.SetActive(true);
        }

    }
}

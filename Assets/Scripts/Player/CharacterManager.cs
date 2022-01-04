
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("SelectedCharacter");
        characterPrefabs[index].SetActive(true);
    }
}

using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ch in characters)
        {
            ch.SetActive(false);
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void ChangeCharacter(int newCh)
    {
        characters[selectedCharacter].SetActive(false);
        characters[newCh].SetActive(true);
        selectedCharacter = newCh;
    }
}

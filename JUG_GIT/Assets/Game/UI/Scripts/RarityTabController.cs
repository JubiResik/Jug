using UnityEngine;
using UnityEngine.UI;

public class RarityTabController : MonoBehaviour
{
    public Image[] rarityTabImages;
    public GameObject[] rarityPanels;

    void Start()
    {
        ActivateRarityTab(0);
    }

    public void ActivateRarityTab(int tabNo)
    {
        for (int i = 0; i < rarityPanels.Length; i++)
        {
            rarityPanels[i].SetActive(false);
            rarityTabImages[i].color = Color.grey;
        }

        if (tabNo >= 0 && tabNo < rarityPanels.Length)
        {
            rarityPanels[tabNo].SetActive(true);
            rarityTabImages[tabNo].color = Color.white;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MainTabController : MonoBehaviour
{
    public Image[] tabImages; // Main tabs
    public GameObject[] pages; // Pages for each main tab

    void Start()
    {
        ActivateMainTab(0); // Activate the first tab (Player) by default
    }

    // Activate the specific main tab and its corresponding page
    public void ActivateMainTab(int tabNo)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey; // Grey out inactive tabs
        }

        pages[tabNo].SetActive(true); // Activate the selected page
        tabImages[tabNo].color = Color.white; // Highlight the selected tab
    }
}

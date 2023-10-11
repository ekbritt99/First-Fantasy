using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHP : MonoBehaviour
{
    private PlayerPersistency playerStats;

    [SerializeField] private GameObject hpBar;
    private Slider hpBarSlider;

    [SerializeField] private GameObject Weapon;
    private Image weaponSprite;

    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private InventoryObject playerEquipment;
    [SerializeField] private Sprite katanaSprite;
    [SerializeField] private Sprite maceSprite;
    [SerializeField] private Sprite romanSprite;
    [SerializeField] private Sprite scimitarSprite;


    // Start is called before the first frame update
    void Start()
    {

        // Find camera in scene
        GameObject camera = GameObject.Find("Main Camera");
        // Set parent canvas render camera
        transform.parent.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();

        playerStats = PlayerPersistency.Instance;
        hpBarSlider = hpBar.GetComponent<Slider>();
        currHealth = playerStats.currentHP;
        maxHealth = playerStats.maxHP;

        float cHealth = (float)currHealth;
        float mHealth = (float)maxHealth;
        // hpBarSlider. = new Vector2((cHealth / mHealth) * 14.4f, 8f);
        hpBarSlider.value = cHealth / mHealth;

        weaponSprite = Weapon.GetComponent<Image>();
        int currentWeapon = playerEquipment.container.Items[4].item.ID;
        if(currentWeapon < 0)
        {
            weaponSprite.sprite = null;
            weaponSprite.color = Color.clear;
        }
        if(currentWeapon == 1)
        {
            Debug.Log("Katana equipped");
            weaponSprite.sprite = katanaSprite;
            weaponSprite.color = Color.white;
            // Weapon.transform.localScale = new Vector3(0.09564409f, 0.09564409f, 0.09564409f);
        }
        if (currentWeapon == 3)
        {
            Debug.Log("Mace equipped");
            weaponSprite.sprite = maceSprite;
            weaponSprite.color = Color.white;
        }
        if (currentWeapon == 4)
        {
            Debug.Log("Roman sword equipped");
            weaponSprite.sprite = romanSprite;
            weaponSprite.color = Color.white;
        }
        if (currentWeapon == 5)
        {
            Debug.Log("Scimitar equipped");
            weaponSprite.sprite = scimitarSprite;
            weaponSprite.color = Color.white;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        currHealth = playerStats.currentHP;
        maxHealth = playerStats.maxHP;

        float cHealth = (float)currHealth;
        float mHealth = (float)maxHealth;
        // hpBarSlider. = new Vector2((cHealth / mHealth) * 14.4f, 8f);
        hpBarSlider.value = cHealth / mHealth;

        weaponSprite = Weapon.GetComponent<Image>();
        int currentWeapon = playerEquipment.container.Items[4].item.ID;
        if(currentWeapon < 0)
        {
            weaponSprite.sprite = null;
            weaponSprite.color = Color.clear;
        }
        if (currentWeapon == 1)
        {
            weaponSprite.sprite = katanaSprite;
            weaponSprite.color = Color.white;
            // Weapon.transform.localScale = new Vector3(0.09564409f, 0.09564409f, 0.09564409f);
        }
        if (currentWeapon == 3)
        {
            weaponSprite.sprite = maceSprite;
            weaponSprite.color = Color.white;
        }
        if (currentWeapon == 4)
        {
            weaponSprite.sprite = romanSprite;
            weaponSprite.color = Color.white;
        }
        if (currentWeapon == 5)
        {
            weaponSprite.sprite = scimitarSprite;
            weaponSprite.color = Color.white;
        }
    }
}

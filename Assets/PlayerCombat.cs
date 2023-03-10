using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] private int maxSwordHold;
    [SerializeField] private int curSwordHold;
    [SerializeField] private float damagePerSword;
    [SerializeField] private GameObject swordPrefab;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI swordDisplay;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if(collision.collider.tag == "Supply")
            {
                AddSwordToPack();
            }
        }
    }

    public void UseSword()
    {
        curSwordHold--;
        GameObject justThrownSword = Instantiate(swordPrefab, transform.forward, transform.rotation);
        justThrownSword.GetComponent<Rigidbody>().AddForce(justThrownSword.transform.forward * 20, ForceMode.Force);
        UpdateUI();
    }

    private void UpdateUI()
    {
        swordDisplay.text = curSwordHold + " / " + maxSwordHold + " Swords";
    }

    void AddSwordToPack()
    {
        if(curSwordHold < maxSwordHold)
        {
            curSwordHold++;
            UpdateUI();
        }
        else
        {
            print("Cant carry any more swords");
        }
    }
}

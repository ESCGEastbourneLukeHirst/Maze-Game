using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] private int maxSwordHold;
    [SerializeField] private int curSwordHold;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Transform swordSpawn;
    [SerializeField] private AudioSource swordSource;

    [Header("Other referances")]
    public Animator animator;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI swordDisplay;

    private void Start()
    {
        UpdateUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if(collision.collider.tag == "Supply")
            {
                AddSwordToPack();
                Destroy(collision.gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && curSwordHold > 0)
        {
            animator.Play("PunchRight");
        }
    }

    public void UseSword()
    {
        curSwordHold--;
        swordSource.Play();
        Instantiate(swordPrefab, swordSpawn.position, swordSpawn.rotation);
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

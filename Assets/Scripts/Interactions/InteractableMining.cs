using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMining : Interactable
{
    [Header("Informations")]
    [SerializeField] bool canInteract = true;
    [SerializeField] float objectLifeMax = 100;
    [SerializeField] float objectLife = 100;
    [SerializeField] float rockSize = 1;
    [SerializeField] LootDrop[] lootDrop;

    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem[] particles;
    [SerializeField] LifeBilboard lifeContainer;

    Coroutine hideCoroutine;

    private void OnValidate() {
        GetReferences();
    }
    private void Awake() {
        GetReferences();
    }
    void GetReferences()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (particles.Length == 0)
            particles = GetComponentsInChildren<ParticleSystem>();
        if (lifeContainer == null)
            lifeContainer = GetComponentInChildren<LifeBilboard>();
    }

    public override void Interact()
    {
        if (!canInteract)
            return;

        foreach (ParticleSystem particle in particles)
            particle.Play();

        objectLife -= 10;
        lifeContainer.LifeFloat = objectLife / objectLifeMax;
        lifeContainer.Show();

        if (hideCoroutine != null) {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }
        hideCoroutine = StartCoroutine(HideMiningLife());

        if (objectLife > 0)
            animator.SetTrigger("Mining");
        else 
            StartCoroutine(EndMining());
    }

    IEnumerator HideMiningLife()
    {
        yield return new WaitForSeconds(3);
        lifeContainer.Hide();
    }

    #region Loot

    IEnumerator EndMining()
    {
        canInteract = false;

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);
        lifeContainer.Hide();

        animator.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        GenerateLoot();
        Destroy(gameObject);
    }

    void GenerateLoot()
    {
        foreach(LootDrop loot in lootDrop) {
            float lootChanche = Random.Range(0, 100);
            if (lootChanche < loot.LootChance)
                continue;

            Vector2 lootCountRange = loot.LootCount;
            int lootCount = Random.Range((int)lootCountRange.x, (int)lootCountRange.y);
            for(int i = 0; i < lootCount; i++) {
                GameObject drop = Instantiate(loot.LootPrefab, transform.position + (Vector3.up * 1), Quaternion.identity);
                drop.transform.position = RandomPositionSpawn(drop.transform.position);
                drop.GetComponent<Rigidbody>().AddForce(Random.Range(-2, 2), 2, Random.Range(-2, 2), ForceMode.Impulse);
                drop.GetComponent<Rigidbody>().AddTorque(Random.Range(-2, 2), 2, Random.Range(-2, 2), ForceMode.Impulse);
            }
        }
    }

    Vector3 RandomPositionSpawn(Vector3 center)
    {
        Vector3 pos = new Vector3(center.x + Random.Range(-1, 1), center.y, center.z + Random.Range(-1, 1));
        return pos;
    }

    #endregion

    [System.Serializable]
    public struct LootDrop {
        [SerializeField] private GameObject lootPrefab;
        [SerializeField][Range(0, 100)] private float lootChance;
        [SerializeField] private Vector2 lootCount;

        public GameObject LootPrefab { get { return lootPrefab; } }
        public float LootChance { get { return lootChance; } }
        public Vector2 LootCount { get { return lootCount; } }
    }
}

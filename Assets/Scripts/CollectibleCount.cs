using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;

    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    void Start() => UpdateCount();

    void OnEnable() => Collectibles.OnCollected += onCollectibleCollected;
    void OnDisable() => Collectibles.OnCollected -= onCollectibleCollected;

    void onCollectibleCollected()
    {
        count++;
        UpdateCount();
    }

    void UpdateCount()
    {
        text.text = $"{count} / {Collectibles.total}";
    }

    private void Update()
    {
        if (count == Collectibles.total) {
            text.text = "WIN WIN WIN WIN WIN WIN WIN";
        } else if (count == 0) { 
            text.text = "Collect all presents ";
        }
    }
}

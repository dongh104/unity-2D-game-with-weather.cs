using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asdf : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Sprite aaaa;
    // Start is called before the first frame update
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sprite = aaaa;
    }
}

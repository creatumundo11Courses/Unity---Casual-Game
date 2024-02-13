using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSwitchable : MonoBehaviour
{
    [Serializable]
    public sealed class SwitchablePart
    {
        public string Id;
        public GameObject Part;
    }
    [SerializeField]
    private List<SwitchablePart> _characters;

    private CharacterAnimations _characterAnimations;

    private void OnEnable()
    {
        GameShop.Instance.OnEquipProduct += OnEquipProduct;
    }
    private void OnDisable()
    {
        GameShop.Instance.OnEquipProduct -= OnEquipProduct;
    }
    private void Awake()
    {
        _characterAnimations = GetComponent<CharacterAnimations>();
    }
    private void Start()
    {
        GameShop.MyProduct characterProduct = GameShop.Instance.GetProductByCategory(GameShop.MyProductCategory.Character);
        SwitchablePart character = _characters.Find(x => x.Id == characterProduct.Id);
        SetAnimatorTarget(characterProduct);
    }
    public GameObject ActivateCharacter(string id)
    {
        DesactivateAllCharacters();

        SwitchablePart character = _characters.Find(x => x.Id == id);

        if (character == null) return null;
       
        character.Part.SetActive(true);

        return character.Part;
    }

    private void DesactivateAllCharacters()
    {
        foreach (var character in _characters)
        {
            character.Part.SetActive(false);    
        }
    }

    private void OnEquipProduct(GameShop.MyProduct product)
    {
        if (product.Category == GameShop.MyProductCategory.Character)
        {
            SetAnimatorTarget(product);
        }
    }

    private void SetAnimatorTarget(GameShop.MyProduct product)
    {
        GameObject _character = ActivateCharacter(product.Id);
        if (_character != null)
        {
            Animator animator = _character.GetComponent<Animator>();
            _characterAnimations.SetAnimatorTarget(animator);
        }
    }
}

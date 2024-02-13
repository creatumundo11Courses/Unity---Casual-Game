using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    [Serializable]
    public sealed class ConsumableProduct
    {
        public string Id;
        public string Name;
        public string Description;
        public float Price;
    }
    [Serializable]
    public sealed class NonConsumableProduct
    {
        public string Id;
        public string Name;
        public string Description;
        public float Price;
    }
    [SerializeField]
    private ConsumableProduct _200CoinsProduct;
    [SerializeField]
    private ConsumableProduct _1000CoinsProduct;
    [SerializeField]
    private NonConsumableProduct _noAdsProduct;

    private IStoreController _controller;
    private IExtensionProvider _extensions;

    public static IAPManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(_200CoinsProduct.Id, ProductType.Consumable);
        builder.AddProduct(_1000CoinsProduct.Id, ProductType.Consumable);
        builder.AddProduct(_noAdsProduct.Id, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void Purchase200Coins()
    {
        _controller.InitiatePurchase(_200CoinsProduct.Id);
    }
    public void Purchase1000Coins()
    {
        _controller.InitiatePurchase(_1000CoinsProduct.Id);
    }
    public void PurchaseNoAds()
    {
        _controller.InitiatePurchase(_noAdsProduct.Id);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _controller = controller;
        _extensions = extensions;

        Product product = _controller.products.WithID(_noAdsProduct.Id);
        if (product != null && product.transactionID != null  && product.hasReceipt)
        {
            AdsManager.Instance.DisableAds();
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Product product = purchaseEvent.purchasedProduct;

        if (product.definition.id == _200CoinsProduct.Id)
        {
            GameMode.Instance.AddCoins(200);
        }
        else if (product.definition.id == _1000CoinsProduct.Id)
        {
            GameMode.Instance.AddCoins(1000);
        }
        else if (product.definition.id == _noAdsProduct.Id)
        {
            AdsManager.Instance.DisableAds();
        }

        Debug.Log($"Purchase Complete {product.definition.id}");
        return PurchaseProcessingResult.Complete;

    }

    public string GetAdsPrice()
    {
        return GetProduct(_noAdsProduct.Id).metadata.localizedPriceString;
    }
    public string Get200CoinsPrice()
    {
        return GetProduct(_200CoinsProduct.Id).metadata.localizedPriceString;
    }
    public string Get1000CoinsPrice()
    {
        return GetProduct(_1000CoinsProduct.Id).metadata.localizedPriceString;
    }

    private Product GetProduct(string id)
    {
        return _controller.products.WithID(id);
    }



    

   
}



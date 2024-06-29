using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerView : CreatureView
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private float bulletRangeSeconds;
    [SerializeField] private float coinMultiplier;
    [SerializeField] private BulletView bulletViewPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject interactionExclamation;
    [SerializeField] private Light2D lanternLight;
    [SerializeField] private float lanternCostBySec;

    [SerializeField] private Transform skinLocator;
    

    private InteractableObject _closeInteractableObject;
    private bool _isLanternActive;
    private float _initialLanternintensity;
    private PlayerSkin _actualSkinUsed;
    protected override void Start()
    {
        base.Start();
        GameManager.inst.ResourcesManager.OnSkinSelected += OnSkinSelected;
        SetupInitialValues();
    }
    private void SetupInitialValues()
    {
        _initialLanternintensity = lanternLight.intensity;
        HideLantern();
        OnSkinSelected(GameManager.inst.ResourcesManager.ActualSkin);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _closeInteractableObject = collision.gameObject.GetComponent<InteractableObject>();
        if (_closeInteractableObject != null && _closeInteractableObject.CanInteract)
        {
            EnableInteractionExclamation();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            _closeInteractableObject = null;
            DisableInteractionExclamation();
        }
    }
    void Update()
    {
        Vector2 movementVector = new Vector2();
        // Get input from the player
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        
        Move(movementVector);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!_isLanternActive)
            {
                UseLantern();
            }
            else
            {
                HideLantern();
            }
        }
    }
    protected override void Move(Vector2 movementVector)
    {
        base.Move(movementVector);
        rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);


        if (movementVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }
    protected override void Attack()
    {
        base.Attack();
        if(SpendBullet())
        {
            ShootBullet();
        }
    }
    private void ShootBullet()
    {
        BulletView bulletView = Instantiate(bulletViewPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bulletView.init(bulletRangeSeconds);
    }
    private bool SpendBullet()
    {
        return GameManager.inst.ResourcesManager.TryToSpendResource(ResourcesManager.ResourceType.Bullet, 1);
    }

    #region Interaction
    private void Interact()
    {
        if(_closeInteractableObject != null)
        {
            _closeInteractableObject.OnInteraction();
        }
    }
    private void EnableInteractionExclamation()
    {
        interactionExclamation.SetActive(true);
    }
    private void DisableInteractionExclamation()
    {
        interactionExclamation.SetActive(false);
    }
    #endregion
    #region lantern
    private void UseLantern()
    {
        if(SpendLanternGas())
        {
            _isLanternActive = true;
            StartCoroutine(DoUseLantern());
            lanternLight.intensity = _initialLanternintensity;
        }
    }
    private IEnumerator DoUseLantern()
    {
        while(_isLanternActive)
        {
            yield return new WaitForSeconds(1);
            if (SpendLanternGas())
            {
                yield return null;
            }
            else
            {
                HideLantern();
            }
        }
    }
    private bool SpendLanternGas()
    {
        return GameManager.inst.ResourcesManager.TryToSpendResource(ResourcesManager.ResourceType.LanternGas, lanternCostBySec);
    }
    private void HideLantern()
    {
        StopCoroutine(DoUseLantern());  
        _isLanternActive = false;
        lanternLight.intensity = 0;
    }
    #endregion

# region Skin

    private void OnSkinSelected(string SkinConfig)
    {
        CleanActualSkin();
        _actualSkinUsed = GameManager.inst.ResourcesManager.resourcesConfig.GetSkinConfigByID(SkinConfig).playerSkinPrefab;
        SetSkin(_actualSkinUsed);
    }
    private void SetSkin(PlayerSkin playerSkin)
    {
        Instantiate(playerSkin, skinLocator);
    }
    private void CleanActualSkin()
    {
        foreach (Transform child in skinLocator)
        {
            Destroy(child.gameObject);
        }
        _actualSkinUsed = null;
    }
    #endregion
    private void OnDestroy()
    {

        GameManager.inst.ResourcesManager.OnSkinSelected -= OnSkinSelected;
    }
}

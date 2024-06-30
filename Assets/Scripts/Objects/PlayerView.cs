using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerView : CreatureView
{
    
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
    private PlayerState _actualstate = PlayerState.InGame;
    private float _initialHealth;
    private Vector2 _initialPosition;

    public enum PlayerState
    {
        InGame,
        Paused
    }

    protected override void Start()
    {
        GameManager.inst.ResourcesManager.OnSkinSelected += OnSkinSelected;
        GameManager.inst.OnPausedGameplay += OnPaused;
        GameManager.inst.OnResumedGameplay += OnResume;
        GameManager.inst.OnRestartGameplay += OnRestart;
        _initialPosition = transform.localPosition;

        _initialHealth = health;
        base.Start();
    }
    protected override void SetupInitialValues()
    {
        transform.localPosition = _initialPosition;
        health = _initialHealth;
        _initialLanternintensity = lanternLight.intensity;
        HideLantern();
        OnSkinSelected(GameManager.inst.ResourcesManager.ActualSkin);
        GameManager.inst.UIManager.SetHealthSliderValue(health);
        OnResume();
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
        if (_actualstate == PlayerState.Paused) return;

        Vector2 movementVector = new Vector2();

        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        
        Move(movementVector);
        RotateShootPointToMouse();


        if (Input.GetKeyDown(KeyCode.Mouse0))
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
    #region Shooting
    void RotateShootPointToMouse()
    {
        // Get the mouse position in screen coordinates
        Vector3 mouseScreenPos = Input.mousePosition;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; 

        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletSpawn.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
        Vector3 mouseScreenPos = Input.mousePosition;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = (mouseWorldPos - bulletSpawn.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        BulletView bulletView = Instantiate(bulletViewPrefab, bulletSpawn.position, Quaternion.identity);
        bulletView.init(direction);
    }
    private bool SpendBullet()
    {
        return GameManager.inst.ResourcesManager.TryToSpendResource(ResourcesManager.ResourceType.Bullet, 1);
    }
    #endregion

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
        StopAllCoroutines();  
        _isLanternActive = false;
        lanternLight.intensity = 0;
    }
    #endregion

    #region Skin

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
    #region Damage
    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        GameManager.inst.UIManager.SetHealthSliderValue(health/ _initialHealth);
    }

    public override void Dies()
    {
        base.Dies();
        GameManager.inst.EndGame(false);
    }
    #endregion
    private void OnPaused()
    {
        _actualstate = PlayerState.Paused;
    }
    private void OnResume()
    {
        _actualstate = PlayerState.InGame;
    }
    private void OnRestart()
    {
        _actualstate = PlayerState.InGame;
        SetupInitialValues();
    }
    private void OnDestroy()
    {
        GameManager.inst.OnPausedGameplay -= OnPaused;
        GameManager.inst.ResourcesManager.OnSkinSelected -= OnSkinSelected;
        GameManager.inst.OnResumedGameplay -= OnResume;
        GameManager.inst.OnRestartGameplay -= OnRestart;
    }
}

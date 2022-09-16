using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : Boss
{
    public GameObject portalPrefab;    //传送门预制体
    public GameObject thunderPrefab;    //雷电预制体
    public GameObject bird_ThunderPrefab;   //雷鸟预制体
    bool spriteRendererActive;  //用于更改SpriteRender开关状态
    SpriteRenderer spriteRenderer;
    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        TransitionState(bringerPatrolState);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    #region Animation Event

    public void GetPortal()
    {
        Portal _Object =  ObjectPool.Instance.GetObjectButNoSetActive(portalPrefab).GetComponent<Portal>();
        if(_Object != null)
        {
            _Object.Init(null, new Vector3(target.position.x, transform.position.y - 0.9f, 0), target, characterStats, new Vector3(0,0,0));
            _Object.gameObject.SetActive(true);
        }
    }

    public void GetThunder()
    {
        PortalEnvironment thunder = ObjectPool.Instance.GetObjectButNoSetActive(thunderPrefab).GetComponent<PortalEnvironment>();
        if(thunder != null)
        {
            thunder.InitEnvironment(new Vector3(target.position.x, transform.position.y, 1), transform, 0, characterStats);
        }
    }

    public void CreateBirdThunder()
    {
        Portal _object = null;
        _object = ObjectPool.Instance.GetObjectButNoSetActive(portalPrefab).GetComponent<Portal>();
        if(_object != null)
        {
            if(transform.rotation.y != 0)
            {
                _object.Init(bird_ThunderPrefab, new Vector3(transform.position.x + 3, transform.position.y - 1f, 0), target, characterStats, new Vector3(0f,0f, 90f));
            }
            else
            {
                _object.Init(bird_ThunderPrefab, new Vector3(transform.position.x - 3, transform.position.y - 1f, 0), target, characterStats, new Vector3(0f, 0f, -90f));
            }
            _object.gameObject.SetActive(true);
        }

        _object = ObjectPool.Instance.GetObjectButNoSetActive(portalPrefab).GetComponent<Portal>();
        if(_object != null)
        {
            if(transform.rotation.y != 0)
            {
                _object.Init(bird_ThunderPrefab, new Vector3(transform.position.x + 3, transform.position.y + 2f, 0), target, characterStats, new Vector3(0f,0f, 90f));
            }
            else
            {
                _object.Init(bird_ThunderPrefab, new Vector3(transform.position.x - 3, transform.position.y + 2f, 0), target, characterStats, new Vector3(0f, 0f, -90f));
            }
            _object.gameObject.SetActive(true);
        }

        _object = ObjectPool.Instance.GetObjectButNoSetActive(portalPrefab).GetComponent<Portal>();
        if(_object != null)
        {
            if(transform.rotation.y != 0)
            {
                _object.Init(bird_ThunderPrefab, new Vector3(transform.position.x + 3, transform.position.y + 5f, 0), target, characterStats, new Vector3(0f,0f, 90f));
            }
            else
            {
                _object.Init(bird_ThunderPrefab, new Vector3(transform.position.x - 3, transform.position.y + 5f, 0), target, characterStats, new Vector3(0f, 0f, -90f));
            }
            _object.gameObject.SetActive(true);
        }
    }

    public void SetSpriteRender() //更改图片可见状态
    {
        spriteRenderer.enabled  = spriteRendererActive;

        if(spriteRendererActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }

        spriteRendererActive = !spriteRendererActive;
    }

    public void SetHideBool()
    {
        isHide = !isHide;
    }

    public void DestoryGameObject()
    {
        Destroy(transform.parent.gameObject);
    }

    #endregion
}

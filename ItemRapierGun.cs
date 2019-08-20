using BS;
using UnityEngine;

namespace RapierGun
{

    public class ItemRapierGun : MonoBehaviour
    {
        protected Item item;
        public bool isChanging = false;
        public bool gunForm = false;
        public float count = 0f;
        
        public float speed = 36f;
        public Transform blade;
        public Transform movingPart;
        public float radius;
        public ItemShooter.ItemShooter shootController;

        public AudioSource changeSFX;
        public ParticleSystem sparks;



        protected void Awake()
        {
            item = this.GetComponent<Item>();
            item.OnHeldActionEvent += OnHeldAction;
            blade = item.transform.Find("Blade");
            movingPart = item.transform.Find("MovingPart");
            radius = Vector3.Distance(blade.transform.position, movingPart.transform.position);
            if (item.GetComponent<ItemShooter.ItemShooter>())
            {
                shootController = item.GetComponent<ItemShooter.ItemShooter>();
                shootController.isShootingAllowed = false;
            }
            else
            {
                Debug.Log("ItemShooter not found");
            }

            changeSFX = item.transform.Find("ChangeSFX").GetComponent<AudioSource>();
            sparks = item.transform.Find("Sparks").GetComponent<ParticleSystem>();
        }

        
        public void OnHeldAction(Interactor interactor, Handle handle, Interactable.Action action)
        {
            if (action == Interactable.Action.AlternateUseStop)
            {
                isChanging = true;
            }

            if(action == Interactable.Action.UseStart && gunForm)
            {
                Shoot();
            }
        }

        void Shoot()
        {

        }

        void FixedUpdate()
        {
            if (isChanging)
            {
                ChangeWeapon();
            }

        }

        public void ChangeWeapon()
        {
            count++;

            if (gunForm)
            {
                Quaternion bladeRotation = blade.transform.rotation;

                blade.transform.RotateAround(movingPart.transform.position, blade.forward, -78f/speed);
                movingPart.Rotate(78f / speed, 0, 0);

                blade.transform.rotation = bladeRotation;

                if (count >= speed)
                {
                   
                    count = 0f;
                    gunForm = false;
                    isChanging = false;
                    shootController.isShootingAllowed = false;
                    sparks.Play();
                    changeSFX.Play();


                }
            }
            else
            {
                
                Quaternion bladeRotation = blade.transform.rotation;

                blade.transform.RotateAround(movingPart.transform.position, blade.forward, 78f / speed);
                movingPart.Rotate(-78f / speed, 0, 0);

                blade.transform.rotation = bladeRotation;

                if (count >= speed)
                {

                    
                    count = 0f;
                    gunForm = true;
                    isChanging = false;
                    shootController.isShootingAllowed = true;
                    sparks.Play();
                    changeSFX.Play();


                }
            }


            
        }

    }
}
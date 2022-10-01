using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEditor.UIElements;

namespace EpicToonFX
{
    public class ETFXFireProjectile : MonoBehaviour
    {
        public GameObject[] projectiles;
        public Transform spawnPosition;
        public int currentProjectile = 0;
        public float speed = 1000;

        // private RaycastHit hit;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                previousEffect();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                previousEffect();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()) //On left mouse down-click
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.parent.right);
                if (hit) //Finds the point where you click with the mouse
                {
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
                    projectile.transform.LookAt(hit.point); //Sets the projectiles rotation to look at the point clicked
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                }
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
                projectile.GetComponent<Rigidbody>().AddForce(spawnPosition.right * speed); //Set the speed of the projectile by applying force to the rigidbody
            }
            Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);
        }

        public void nextEffect() //Changes the selected projectile to the next. Used by UI
        {
            if (currentProjectile < projectiles.Length - 1)
                currentProjectile++;
            else
                currentProjectile = 0;
        }

        public void previousEffect() //Changes selected projectile to the previous. Used by UI
        {
            if (currentProjectile > 0)
                currentProjectile--;
            else
                currentProjectile = projectiles.Length - 1;
        }

        public void AdjustSpeed(float newSpeed) //Used by UI to set projectile speed
        {
            speed = newSpeed;
        }
    }
}
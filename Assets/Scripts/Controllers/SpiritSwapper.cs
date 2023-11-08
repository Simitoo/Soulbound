using Assets.Scripts.Models;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Controllers
{
    public class SpiritSwapper : MonoBehaviour
    {
        public static SpiritSwapper instance;

        private SpiritManager spiritManager;
        private PlayerManager playerManager;

        private Spirit mainSpirit;

        private AsyncOperationHandle<GameObject> asyncOperationHandle;

        private GameObject spawnedSpirit;
        private Transform spawnedPoint;

        private SpiritController spiritController;
        private Pickup pickup;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            spiritManager = SpiritManager.instance;
            playerManager = PlayerManager.instance;

            spiritManager.onSpiritChanged += UpdateCurrentSpirit;

            spawnedPoint = playerManager.player.transform.Find("SpiritPosition");           
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && spiritManager.CurrentSpirits.All(s => s.Value != null))
            {
                ReleaseSpirit();

                Swapper();
                AssetName();

                LoadSpirit();
            }
            else
            {
                //user message
            }
        }

        private void Swapper()
        {
            Spirit main = spiritManager.CurrentSpirits[Enum.SpiritSlot.Main];
            Spirit second = spiritManager.CurrentSpirits[Enum.SpiritSlot.Second];

            if (mainSpirit == main)
            {
                mainSpirit = second;
            }
            else if (mainSpirit == second)
            {
                mainSpirit = main;
            };
        }

        private void LoadSpirit()
        {
            if (mainSpirit != null)
            {
                string assetPath = AssetName();

                asyncOperationHandle = Addressables.InstantiateAsync(assetPath);

                asyncOperationHandle.Completed += (asyncOperation) =>
                {
                    if (asyncOperation.Status == AsyncOperationStatus.Succeeded)
                    {
                        spawnedSpirit = asyncOperation.Result;
                        if (spawnedSpirit != null)
                        {
                            SetSpiritPosition();
                            SettingUpSpiritScriptComponents(true);
                        }
                    }
                };
            }
        }

        private void ReleaseSpirit()
        {
            if (spawnedSpirit != null)
            {
                Addressables.ReleaseInstance(spawnedSpirit);
                SettingUpSpiritScriptComponents(false);
            }
        }

        private string AssetName()
        {
            StringBuilder sb = new StringBuilder();

            sb.Clear();

            if(mainSpirit != null)
            sb.Append($"Assets/Prefabs/Spirits/{mainSpirit.name}.prefab");

            return sb.ToString().TrimEnd();
        }

        private void UpdateCurrentSpirit(Spirit newSpirit, Spirit oldSpirit)
        {          
            ReleaseSpirit();

            if (spiritManager.CurrentSpirits[Enum.SpiritSlot.Main] == null && mainSpirit != spiritManager.CurrentSpirits[Enum.SpiritSlot.Second])
            {
                mainSpirit = spiritManager.CurrentSpirits[Enum.SpiritSlot.Second];

                LoadSpirit();
            }
            else if(spiritManager.CurrentSpirits[Enum.SpiritSlot.Main] != null && mainSpirit != spiritManager.CurrentSpirits[Enum.SpiritSlot.Main])
            {
                mainSpirit = spiritManager.CurrentSpirits[Enum.SpiritSlot.Main];

                LoadSpirit();
            }
        }

        private void SetSpiritPosition()
        {
            SpriteRenderer spriteRenderer = spawnedSpirit.GetComponent<SpriteRenderer>();

            if(spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }

            spawnedSpirit.transform.SetParent(spawnedPoint);
            spawnedSpirit.transform.localPosition = Vector2.zero;
            spawnedSpirit.transform.localScale = spawnedPoint.localScale;
        }

        private void SettingUpSpiritScriptComponents(bool enable)
        {
            if (spawnedSpirit != null)
            {
                spiritController = spawnedSpirit.GetComponent<SpiritController>();
                pickup = spawnedSpirit.GetComponent<Pickup>();
            }

            if (spiritController != null && pickup != null)
            {
                spiritController.enabled = enable;
                pickup.enabled = false;
            }         
        }     
    }
}

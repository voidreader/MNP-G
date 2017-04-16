namespace G2USample
{
    using System;
    using UnityEngine;
    using System.Collections;

    public class SampleSpawnPoint : MonoBehaviour
    {

        public GameObject MyAIObject = null;
        public AIManager MyAIManager = null;

        public void DoSpawn(Google2u.SampleCharactersRow in_aiRow, Google2u.SampleDialogMapRow in_dialogMapRow,
            Google2u.SampleWeaponsRow in_weaponsRow, AIManager in_aiManager)
        {
            if (MyAIObject == null)
            {
                MyAIManager = in_aiManager;
                GameObject aiGameObject;
                if (in_aiRow._Prefab.Equals("mine_bot", StringComparison.InvariantCultureIgnoreCase))
                    aiGameObject = (GameObject) Instantiate(in_aiManager.pMineBot);
                else if (in_aiRow._Prefab.Equals("buzzer_bot", StringComparison.InvariantCultureIgnoreCase))
                    aiGameObject = (GameObject) Instantiate(in_aiManager.pBuzzBot);
                else
                    return;

                var aiGameObjectScript = aiGameObject.GetComponent<BaseAIScript>();
                if (aiGameObjectScript == null)
                    return;

                var pos = transform.position + in_aiRow._Offset;
                aiGameObject.transform.position = pos;
                aiGameObject.transform.localScale = new Vector3(in_aiRow._Scale, in_aiRow._Scale, in_aiRow._Scale);

                aiGameObjectScript.BotLevel = in_aiRow._Level;
                aiGameObjectScript.BotName = in_aiRow._Name;
                aiGameObjectScript.CanFly = in_aiRow._CanFly;
                aiGameObjectScript.Health = in_aiRow._Health;
                aiGameObjectScript.Speed = in_aiRow._Speed;
                aiGameObjectScript.MySpawnPoint = this;

                aiGameObjectScript.WeaponID = in_weaponsRow._Name;


                aiGameObjectScript.OnDamaged = in_dialogMapRow._OnDamaged.ToArray();
                aiGameObjectScript.OnDeath = in_dialogMapRow._OnDestroyed.ToArray();

                aiGameObjectScript.EnableAiScript(in_aiRow._Rotation);

                MyAIObject = aiGameObject;

            }
        }
    }
}
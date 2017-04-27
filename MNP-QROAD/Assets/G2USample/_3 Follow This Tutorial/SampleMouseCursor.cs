using System;
using UnityEngine;
using System.Collections;

namespace Google2u
{
    public class SampleMouseCursor : MonoBehaviour
    {
        public GameObject CursorDatabaseObj;
        public IGoogle2uDB CursorDatabase;
        public string CursorName;
        public int CursorDamage;
        public Texture2D cursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        public void SetCursor(string in_cursorId)
        {
            if (CursorDatabase == null && CursorDatabaseObj != null)
            {
                var allComponents = CursorDatabaseObj.GetComponents<Component>();
                foreach (var component in allComponents)
                {
                    var componentName = component.ToString();
                    if (componentName.ToLower().Contains("samplecursor"))
                    {
                        CursorDatabase = component as IGoogle2uDB;
                        break;
                    }
                }
            }

            if (CursorDatabase == null) 
                return;

            var cursorRow = CursorDatabase.GetGenRow(in_cursorId);
            if (cursorRow == null) 
                return;

            CursorName = cursorRow.GetStringData("_ReticleName");
            var tmpDamage = 0;
            int.TryParse(cursorRow.GetStringData("_ReticleDamage"), out tmpDamage);
            CursorDamage = tmpDamage;
            var tmpCursorTextureName = cursorRow.GetStringData("_ReticleTextureName");
            if (!string.IsNullOrEmpty(tmpCursorTextureName))
                cursorTexture = Resources.Load(tmpCursorTextureName) as Texture2D;

            if (cursorTexture != null)
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetCursor("RETICLE_001");
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SetCursor("RETICLE_002");
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SetCursor("RETICLE_003");
        }

        void OnMouseEnter()
        {
            if(cursorTexture != null)
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
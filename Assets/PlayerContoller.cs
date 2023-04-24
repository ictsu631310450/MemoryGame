using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerContoller : MonoBehaviour
    {

        void Update()
        {
            float dirx,diry;
            dirx = Input.GetAxis("Horizontal");
            diry = Input.GetAxis("Vertical");
            transform.Translate(Time.deltaTime * 5 * dirx, diry/50, 0);

            #region สำหรับการหันซ้ายขวา
            if (dirx > 0) { transform.localScale = new Vector3(1, 1, 1); }
            if (dirx < 0) { transform.localScale = new Vector3(-1, 1, 1); }
            #endregion

            

        }

    }
}

using UnityEngine;

public class ControllerMobile : MonoBehaviour
{
    // Start is called before the first frame update
    character Player;
    void Start()
    {
        Player = FindObjectOfType<character>();
    }

    public void PointerLeftDown(){
        Player.horizontal = -1;
    }
    public void PointerLeftUp(){
        Player.horizontal = 0;
    }
    public void PointerRightDown(){
        Player.horizontal = 1;
    }
     public void PointerRightUp(){
        Player.horizontal = 0;
    }
    public void PointerDownDash(){
        Player.PointerDash = true;
    }
    public void PointerUpDash(){
        Player.PointerDash = false;
    }

    public void PointerDownJump(){
        Player.PointerJump = true;
    }
    public void PointerUpJump(){
        Player.PointerJump = false;
    }
    public void PointerDownUlti(){
        Player.PointerUlti = true;
    }
    public void PointerUpUlti(){
        Player.PointerUlti = false;
    }
     public void PointerDownAttack(){
        Player.PointerAttack = true;
    }
     public void PointerUpAttack(){
        Player.PointerAttack = false;
    }
    
}

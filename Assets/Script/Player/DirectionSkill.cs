
using UnityEngine;
using UnityEngine.EventSystems;


public class DirectionSkill : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    [SerializeField] GameObject parentPosCurrent;
    public Vector2 currentPos;
    public Vector2 endPos;
    public Vector2 dirSkill;
    private RectTransform rect;
    character Player;
    Animator ani;
    void Start()
    {
        ani = GameObject.Find("Character").GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
        Player = FindObjectOfType<character>();
        dirSkill = Vector2.right * Player.direction;
        currentPos = new Vector2(parentPosCurrent.transform.position.x, parentPosCurrent.transform.position.y);
    }


     public void OnPointerDown(PointerEventData eventData)
       {
           // Lưu vị trí ban đầu khi người chơi bắt đầu kéo button
           if(Player.skillTime > 0 || Player.manaCharCurrent < Player.manaSkill || Player.HealCharCurrent<=0) return;
                currentPos = new Vector2(parentPosCurrent.transform.position.x, parentPosCurrent.transform.position.y);
                dirSkill = Vector2.right * Player.direction;
                Player.isUseSkill = true;
           
       }

       public void OnDrag(PointerEventData eventData)
       {
            if(Player.skillTime > 0 || Player.manaCharCurrent < Player.manaSkill  || Player.HealCharCurrent<=0) return;

            ani.SetBool("isSkillWait",true);

            if (Vector2.Distance(eventData.position,currentPos) > 50f)
            {
                Vector2 direction = (eventData.position - currentPos).normalized;
                rect.position = currentPos + direction * 50f;
            }
            else
            {
                rect.position = eventData.position;
            }
            // Di chuyển button theo vị trí của con trỏ hoặc điểm chạm
            //    rect.position = eventData.position;
            //    endPos = eventData.position;
            endPos = eventData.position;
            dirSkill = (endPos - currentPos).normalized;
            Player.isUseSkill = true;

        

       }
       public void OnPointerUp(PointerEventData eventData)
       {
            if(Player.skillTime > 0 || Player.manaCharCurrent < Player.manaSkill  || Player.HealCharCurrent<=0) return;
            ani.SetBool("isSkillWait",false);
            Player.ani.SetTrigger("isUseSkill");   
            rect.position = parentPosCurrent.transform.position;
            // Player.isUseSkill = false;
       }
   
}

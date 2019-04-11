using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girescipt : MonoBehaviour
{
    Animator anim;

	private Vector3 velocity;
	private AnimatorStateInfo currentBaseState;			// 指基层使用的动画师的当前状态
	private GameObject cameraObject;	// 参考主摄像头

	public float animSpeed = 1.5f;				// 动画播放速度设置

    public float RunSpeed = 7.0f;
    public float walkSpeed=1.0f;
	// 後退速度
	public float backwardSpeed = 2.0f;
	// 旋回速度
	public float rotateSpeed = 2.0f;
	// 跳跃威力
	public float jumpPower = 3.0f; 

    // state
	static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int restState = Animator.StringToHash("Base Layer.Rest");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cameraObject = GameObject.FindWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {

        //
        float h = Input.GetAxis("Horizontal");				
		float v = Input.GetAxis("Vertical");	
        int state=anim.GetInteger("state");			
        float speed=walkSpeed;
        if(state==0){
            //walk
            speed=walkSpeed;
        }else if(state==1){
            speed=RunSpeed;
        }
        if(v>0.1){
            v*=speed;
        }else if (v<-0.1){
            v*=speed;
        }

		anim.SetFloat("Speed", v);							
		anim.SetFloat("Direction", h); 	
        // 设置动画播放速度					
		anim.speed = animSpeed;								
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	

        velocity = new Vector3(0, 0, v);		
		velocity = transform.TransformDirection(velocity);

        transform.localPosition += velocity * Time.fixedDeltaTime;
		transform.Rotate(0, h * rotateSpeed, 0);	


        // state walk/wait=0 run=1 jump=2 rest=3 slide=4
        if(Input.GetKeyDown(KeyCode.U)){
            anim.SetInteger("state",1);
        }else if(Input.GetKeyUp(KeyCode.U)){
            anim.SetInteger("state",0);
        }
        
        if(Input.GetKeyDown(KeyCode.J)){
            anim.SetInteger("state",2);
        }else if(Input.GetKeyUp(KeyCode.J)){
            anim.SetInteger("state",0);
        }

        if(Input.GetKeyDown(KeyCode.H)){
            anim.SetInteger("state",3);
        }else if(Input.GetKeyUp(KeyCode.H)){
            anim.SetInteger("state",0);
        }
        
        if(state==1&&Input.GetKeyDown(KeyCode.Space)){
            anim.SetInteger("state",4);
        }else if(Input.GetKeyUp(KeyCode.Space)){
            anim.SetInteger("state",1);
        }
    }
}

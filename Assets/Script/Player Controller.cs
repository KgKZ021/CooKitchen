using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IKitchenObjectParent
{


    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform kitchenObjHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player Instance!");
        }
        Instance = this;//only one player
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;//should listen in Start not Awake
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //lastInteractDir is needed to point the obj even is the player is stopped.Won't assign the value if moveDir is zero.
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f; //not using 2f directly in function with purpose of clean code
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, layerMask)) //normal raycast will hit only one obj.layermasking let you hit all the obj you want
        {
            //identifying things this way is much better than using Tags
            //transform is the pointed obj's
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) //TryGetComponent will return bool. 
            {
                //Has ClearCounter
                if (baseCounter != selectedCounter)
                {
                    selectedCounter = baseCounter;

                    SetSelectedCounnter(selectedCounter);
                }

            }
            else//raycast hit other things other than counter
            {
                SetSelectedCounnter(null);
            }

            //Same as above function
            //ClearCounter clearCounter=raycastHit.transform.GetComponent<ClearCounter>();
            //if (clearCounter != null) { 
            //    //Has clear counter
            //}
        }//another ver of Raycast. out parameter raycastHit returns the hit obj while the function itseld is returning bool
        else //if raycast doesn't hit
        {
            SetSelectedCounnter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        //old legacy Input system
        //refactored to new input system with clean coded new class
        //if(Input.GetKey(KeyCode.W))
        //{
        //    inputVector.y = +1;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVector.x = -1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVector.y = -1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVector.x = +1;
        //}


        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);// the reason for not using vector3 directly is logic and clean code

        //collision detection
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //if cannot move towards moveDir,attempt to move diagonally
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;//normalized because it move slower compare to A, D keys
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX; //frame independent speed
            }
            else
            {
                //if Cannot move only on the X, attempt Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ; //frame independent speed
                }
                else
                {
                    //Cannot movein any direction

                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance; //frame independent speed
        }

        //one of many rotation methods. forward have both get and set // also up and and right(right for 2D games)
        //Lerp for smooth movements. Learn More.
        //Lerp for interpolate.Slerp for spherical interpolation. Look splines to Learn More.

        isWalking = moveDir != Vector3.zero; //(0,0,0)

        float rotateSpeed = 7f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // interpolate between two vectors and with a flaot value
    }

    private void SetSelectedCounnter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        //Invokes when the counter is selected and do the listener function
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjHoldPoint;
    }

    //set the old parent counter
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    //get the old parent counter
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

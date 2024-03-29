using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform playerPushingToolHoldPoint;
    [SerializeField] private Transform playerHandToolHoldPoint;
    [SerializeField] private Transform collectablesHoldPoint;

    private bool isWalking;
    private bool isPushing;

    public enum State
    {
        Idle,
        Walking,
        Pushing,
        Driving,
    }

    public int moneyAmount;

    public State state;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Walking:
                HandleMovement();
                break;
            case State.Pushing:
                HandlePushing();
                break;
        }
    }

    private void HandleIdle()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        if (inputVector != Vector2.zero && !PlayerHasPushingTool())
        {
            state = State.Walking;
            isWalking = true;
            playerAnimator.SetWalkingState(true);
        }
        else
        {
            playerAnimator.SetWalkingState(false);
            playerAnimator.SetPushingState(false);
            playerAnimator.SetPushSpeed(1);
        }


    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        if (inputVector == Vector2.zero)
        {
            if (isWalking)
            {
                playerAnimator.SetWalkingState(false);
                isWalking = false;
                state = State.Idle;
            }
            return;
        }

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

        if (!isWalking)
        {
            playerAnimator.SetWalkingState(true);
            isWalking = true;
            state = State.Walking;
        }
    }

    private void HandlePushing()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        if (inputVector == Vector2.zero)
        {

            if (isPushing)
            {

                playerAnimator.SetPushingState(true);
                playerAnimator.SetPushSpeed(0);
                isPushing = false;
            }
            return;
        }
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        if (!isPushing)
        {
            playerAnimator.SetWalkingState(true);
            playerAnimator.SetPushSpeed(1);
            rotateSpeed = 20f;
            isPushing = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FarmingTool farmingTool))
        {
            if(farmingTool.toolName == FarmingTool.ToolName.CleaningAndLoosingFarmTool || farmingTool.toolName == FarmingTool.ToolName.PlantingTool)
            {
                farmingTool.PlayerUseTool(playerPushingToolHoldPoint);
                
                playerAnimator.SetPushingState(true);
                state = State.Pushing;
            }
            if(farmingTool.toolName == FarmingTool.ToolName.WateringTool)
            {
                farmingTool.PlayerHoldToolInHand(playerHandToolHoldPoint);
            }
            
        }
        if(other.TryGetComponent(out Money money))
        {
            Destroy(money.gameObject);
            MoneyManager.Instance.AddMoney();
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool PlayerHasCollectables()
    {
        return collectablesHoldPoint.childCount != 0;
    }

    public bool PlayerHasPushingTool()
    {
        return playerPushingToolHoldPoint.childCount != 0;
    }

    public bool PlayerHasHandTool()
    {
        return playerHandToolHoldPoint.childCount != 0;
    }

    public Transform GetCollectablesHoldPoint()
    {
        return collectablesHoldPoint;
    }

    public bool PlayerHasNoTool()
    {
        return PlayerHasPushingTool() && PlayerHasHandTool();
    }


}

using UnityEngine;
using JunkCity.Tools;

namespace JunkCity.World
{
    [RequireComponent(typeof(CharacterDriver))]
    public class PatrolBrain : MonoBehaviour
    {
        private CharacterDriver driver;
        private FSM<PatrolBrain> fsm;

        [SerializeField] private float patrolTime = 0.5f;
        [SerializeField] private float waitingTime = 0.2f;
        [SerializeField] private float idlingTime = 0.5f;
        private float currentWorkTime = 0;


        private void Awake()
        {
            driver = GetComponent<CharacterDriver>();

            var states = new FSM<PatrolBrain>.State[]
            {
                new("Idle_Right")
                {
                    OnOpen = () =>
                    {
                        currentWorkTime = 0;
                        driver.Jump(1);
                    },
                    OnInvoke = () =>
                    {
                        currentWorkTime += Time.deltaTime;

                        if (currentWorkTime > idlingTime)
                            fsm.SetState("Patrol_Right");
                    }
                },
                new("Patrol_Right")
                {
                    OnOpen = () =>
                    {
                        currentWorkTime = 0;
                        driver.MoveHorizontal(1);
                    },
                    OnInvoke = () =>
                    {
                        currentWorkTime += Time.deltaTime;

                        if (currentWorkTime > patrolTime)
                            fsm.SetState("Wait_Right");
                    },
                    OnClose = () => driver.MoveHorizontal(0)
                },
                new("Wait_Right")
                {
                    OnOpen = () => currentWorkTime = 0,
                    OnInvoke = () =>
                    {
                        currentWorkTime += Time.deltaTime;

                        if (currentWorkTime > waitingTime)
                            fsm.SetState("Idle_Left");
                    }
                },
                new("Idle_Left")
                {
                    OnOpen = () =>
                    {
                        currentWorkTime = 0;
                        driver.Jump(1);
                    },
                    OnInvoke = () =>
                    {
                        currentWorkTime += Time.deltaTime;

                        if (currentWorkTime > idlingTime)
                            fsm.SetState("Patrol_Left");
                    }
                },
                new("Patrol_Left")
                {
                    OnOpen = () =>
                    {
                        currentWorkTime = 0;
                        driver.MoveHorizontal(-1);
                    },
                    OnInvoke = () =>
                    {
                        currentWorkTime += Time.deltaTime;

                        if (currentWorkTime > patrolTime)
                            fsm.SetState("Wait_Left");
                    },
                    OnClose = () => driver.MoveHorizontal(0)
                },
                new("Wait_Left")
                {
                    OnOpen = () => currentWorkTime = 0,
                    OnInvoke = () =>
                    {
                        currentWorkTime += Time.deltaTime;

                        if (currentWorkTime > waitingTime)
                            fsm.SetState("Idle_Right");
                    }
                },
            };

            fsm = new(this, states);
        }

        private void Start()
        {
            fsm.Open();
        }

        private void FixedUpdate()
        {
            fsm.Invoke();
        }

        private void OnDestroy()
        {
            fsm.Close();
        }
    }
}

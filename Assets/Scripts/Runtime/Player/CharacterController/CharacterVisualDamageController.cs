using UnityEngine;

namespace Z3.GMTK2024.Runtime.Player.CharacterController
{
    public class CharacterVisualDamageController : MonoBehaviour
    {
        [SerializeField] private GameObject characterObject; 
        [SerializeField] private Mesh[] meshes; 


        public enum CharacterState
        {
            State0,
            State1,
            State2,
            State3,
            State4,
            State5
        }

        private CharacterState currentState;
        private SkinnedMeshRenderer meshFilter;

        void Start()
        {
            meshFilter = characterObject.GetComponent<SkinnedMeshRenderer>();
            SetState(CharacterState.State0);
        }

        void Update()
        {
            // Loop through states using the G key
            if (Input.GetKeyDown(KeyCode.G))
            {
                int nextState = ((int)currentState + 1) % 6; 
                SetState((CharacterState)nextState);
            }
        }

        public void SetState(CharacterState newState)
        {
            currentState = newState;
            UpdateMesh();
        }

        public CharacterState GetCurrentState()
        {
            return currentState;
        }

        private void UpdateMesh()
        {
            if (meshFilter != null && (int)currentState < meshes.Length)
            {
                meshFilter.sharedMesh = meshes[(int)currentState];
            }
        }
    }
}
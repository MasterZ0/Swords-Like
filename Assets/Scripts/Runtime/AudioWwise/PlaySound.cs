using UnityEngine;
using Z3.Audio.FMODIntegration;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.GMTK2024
{
    [NodeCategory(Categories.NodeGraph + "/NewActionTask")]
    [NodeDescription("Please describe what this ActionTask does.")]
    public class PlaySound : ActionTask 
    {
        [SerializeField] private Parameter<Transform> attachedObject;
        [SerializeField] private Parameter<SoundData> soundData;
               
        protected override void StartAction()
        {
            soundData.Value.PlaySound(attachedObject.Value);
            EndAction();
        }
    }
}